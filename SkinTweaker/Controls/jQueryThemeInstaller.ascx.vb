Imports System.IO
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports Ionic.Zip


Public Class jQueryThemeInstaller
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase


#Region "PRIVATE PROPERTIES"

    Private Shared Property NewThemeJustInstalled As Boolean
        Get
            If HttpContext.Current.Request.Cookies(CookiePreFix & "NewThemeJustInstalled") Is Nothing Then
                Return False
            Else
                Return CBool(HttpContext.Current.Request.Cookies(CookiePreFix & "NewThemeJustInstalled").Value)
            End If
        End Get
        Set(value As Boolean)
            Dim aCookie As New HttpCookie(CookiePreFix & "NewThemeJustInstalled")
            aCookie.Value = CStr(value)
            If HttpContext.Current.Request.Cookies(CookiePreFix & "NewThemeJustInstalled") Is Nothing Then
                HttpContext.Current.Response.Cookies.Add(aCookie)
            Else
                HttpContext.Current.Response.SetCookie(aCookie)
            End If
        End Set
    End Property

#End Region


#Region "Event Handlers"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If NewThemeJustInstalled Then
                NewThemeJustInstalled = False
                'This must be called during Page_Load not Page_PreRender
                Call SetMessage(Me, "Install completed successfully!", MessageType.Success)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbInstallNow_Click(sender As Object, e As System.EventArgs) Handles lbInstallNow.Click
        Call InstallNow()
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub InstallNow()
        Try
            Call CleanOutTempFolder()
            If FileUpload1.HasFile Then
                Dim strZipFileName As String = Path.GetFileName(FileUpload1.FileName)
                'first make sure that it is the right file type
                If Path.GetExtension(strZipFileName) <> ".zip" Then
                    'Wrong file type so just kick back error
                    Call SetMessage(Me, "Invalid File Extension. Expecting a zip file.", MessageType.ErrorMessage)
                Else
                    Dim strZipFilePath As String = TempDirectoryPhysicalPath & strZipFileName
                    Dim UnzipTargetDir As String = TempDirectoryPhysicalPath
                    Dim entry As ZipEntry

                    'Save uploaded file in temp directory
                    FileUpload1.SaveAs(strZipFilePath)

                    'Unzip contents to temp directory
                    Using zip1 As ZipFile = ZipFile.Read(strZipFilePath)
                        For Each entry In zip1
                            entry.Extract(UnzipTargetDir, ExtractExistingFileAction.OverwriteSilently)
                        Next
                    End Using

                    'check for the correct the css directory
                    Dim objDirectoryInfo As DirectoryInfo = New DirectoryInfo(TempDirectoryPhysicalPath & "css")
                    If objDirectoryInfo.Exists Then
                        'Make sure that there is only one direcorty under 'css' directory.
                        If objDirectoryInfo.GetDirectories.Length = 1 Then
                            Dim di As DirectoryInfo
                            Dim strSourcePath As String = String.Empty
                            Dim strDestinationPath As String = String.Empty
                            'There should only be one di in this For Each loop 
                            For Each di In objDirectoryInfo.GetDirectories
                                strSourcePath = di.FullName
                                strDestinationPath = ThemesCssDirectoryPhysicalPath & di.Name
                            Next
                            'Make sure that there is a source path now
                            If strSourcePath <> String.Empty Then
                                If Directory.Exists(strDestinationPath) And chkOverWriteExistingTheme.Checked = False Then
                                    'The new theme directory already exists and cannot overwrite so send message back
                                    Call SetMessage(Me, "The theme already exists. Check 'Overwrite Existing Theme' if you want to overwrite the existing theme.", MessageType.Warning)
                                Else
                                    If Directory.Exists(strDestinationPath) Then Directory.Delete(strDestinationPath, True)
                                    Directory.Move(strSourcePath, strDestinationPath)
                                    Call CleanOutTempFolder() 'Need to clean temp folder here since will redirect and terminate this script now
                                    '1. Clear the cache, set the flag and redirect to self.
                                    '2. The NavPanel will see the flag and make this install page/view visible.
                                    '3. This page/view will see the flag, reset the flag and then display the success message. 
                                    DataCache.ClearModuleCache(TabId) 'Clear the cache, this may also clear the session variables...
                                    NewThemeJustInstalled = True
                                    PageViewRequest = PageView.Install
                                    Response.Redirect(Request.RawUrl, True) 'Will go to main page/view (Intoduction)
                                End If
                            Else
                                'Source path is empty
                                Call SetMessage(Me, "Invalid Directory Structure Inside Zip File. Something went wrong trying to read new theme directory name.", MessageType.ErrorMessage)
                            End If
                        Else
                            'There should only be one dir inside css
                            Call SetMessage(Me, "Invalid Directory Structure Inside Zip File. Was expecting to find only one subdirectory under 'css' directory.", MessageType.ErrorMessage)
                        End If
                    Else
                        'The css directory does not exist
                        Call SetMessage(Me, "Invalid Directory Structure Inside Zip File. Was expecting to find a directory named 'css'.", MessageType.ErrorMessage)
                    End If
                End If
            Else
                'There was no file uploaded
            End If
            'Clean up and go!
            Call CleanOutTempFolder()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub CleanOutTempFolder()
        Try
            If Directory.Exists(TempDirectoryPhysicalPath) Then
                Directory.Delete(TempDirectoryPhysicalPath, True)
            End If
            Directory.CreateDirectory(TempDirectoryPhysicalPath)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


End Class