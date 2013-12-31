Imports System.IO
Imports System.Xml
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.InjectorResources
Imports SkinTweaker.Common.Functions
Imports DotNetNuke.Services.Installer.Packages


Namespace Common

    '===================================================================
    '                           VARIABLES
    '===================================================================

    Public Class Variables

#Region "PUBLIC ENUMS"

        Public Enum jQuerySelectorType
            NONE = 0
            ALL = 1
            Attribute = 2
            CssClass = 3
            Element = 4
            ID = 5
            Input = 6
        End Enum

        Public Enum jQueryPluginType
            NONE = 0
            ALL = 1
            Effect = 2
            Interaction = 3
            Utility = 4
            Widget = 5
        End Enum

        Public Enum BugStatusType
            Unknown = 0
            Alive = 1
            Dead = 2
            NotFound = 3
            NotVersion = 4
        End Enum

        Public Enum BugKillType
            NONE = 0
            StringReplacement = 1
            AppendText = 2
            BothReplaceAndAppend = 3
        End Enum

        Public Enum MessageType
            Warning = 1
            Info = 2
            Success = 3
            ErrorMessage = 4
        End Enum

        Public Enum PageView
            Introduction = 0
            SelectTheme = 1
            CustomThemes = 2
            Download = 3
            Install = 4
            AdvancedSettings = 5
            jQueryScriptEditor = 6
            BugHunter = 7
            NONE = 99
        End Enum

#End Region


#Region "PORTAL BASIC VARIABLES"

        Public Shared ReadOnly Property DNN_ModuleName As String
            Get
                Return "SkinTweaker"
            End Get
        End Property

        Public Shared ReadOnly Property CSS_ScopeName As String
            Get
                Return DNN_ModuleName
            End Get
        End Property

        Public Shared ReadOnly Property PortalSettingsPreFix As String
            Get
                Return DNN_ModuleName & "_"
            End Get
        End Property

        Public Shared ReadOnly Property CookiePreFix As String
            Get
                Return DNN_ModuleName & "_"
            End Get
        End Property

        Public Shared ReadOnly Property NoThemeName As String
            Get
                Return "_NONE"
            End Get
        End Property

        Public Shared ReadOnly Property NoScriptName As String
            Get
                Return "_NONE"
            End Get
        End Property

        Public Shared ReadOnly Property DotNetNukeVersion As String
            Get
                Return PackageController.GetPackageByName("DNN_HTML").Version.ToString
            End Get
        End Property

        Public Shared ReadOnly Property SkinTweakerVersion As String
            Get
                Return PackageController.GetPackageByName("SkinTweaker").Version.ToString
            End Get
        End Property

        Public Shared ReadOnly Property OriginalFileNameExtender As String
            Get
                Return "_ORIGINAL-Keep_For_Reference_Only-DO_NOT_DELETE.bak"
            End Get
        End Property

#End Region


#Region "PORTAL THEME SETTINGS"

        Public Shared Property PortalCssThemeName(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "CssThemeName", PortalId, NoThemeName)
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "CssThemeName", value)
            End Set
        End Property

        Public Shared Property PortaljQueryScriptName(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "jQueryScriptName", PortalId, NoScriptName)
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "jQueryScriptName", value)
            End Set
        End Property

#End Region


#Region "PATHS"

        'Themes CSS Directory
        Public Shared ReadOnly Property ThemesCssDirectoryPhysicalPath As String
            Get
                Return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath & ThemesCssDirectoryRelativePath)
            End Get
        End Property

        Public Shared ReadOnly Property ThemesCssDirectoryRelativePath As String
            Get
                Return "/DesktopModules/" & DNN_ModuleName & "/Themes/css/"
            End Get
        End Property

        'Themes jQuery Directory
        Public Shared ReadOnly Property ThemesjQueryDirectoryPhysicalPath As String
            Get
                Return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath & ThemesjQueryDirectoryRelativePath)
            End Get
        End Property

        Public Shared ReadOnly Property ThemesjQueryDirectoryRelativePath As String
            Get
                Return "/DesktopModules/" & DNN_ModuleName & "/Themes/jQueryScripts"
            End Get
        End Property

        'Themes Module Directory
        Public Shared ReadOnly Property ThemesModuleDirectoryPhysicalPath As String
            Get
                Return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath & ThemesjQueryDirectoryRelativePath)
            End Get
        End Property

        Public Shared ReadOnly Property ThemesModuleDirectoryRelativePath As String
            Get
                Return "/DesktopModules/" & DNN_ModuleName & "/Themes/Module"
            End Get
        End Property

        'Temp Directory
        Public Shared ReadOnly Property TempDirectoryPhysicalPath As String
            Get
                Return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath & TempDirectoryRelativePath)
            End Get
        End Property

        Public Shared ReadOnly Property TempDirectoryRelativePath As String
            Get
                Return "/DesktopModules/" & DNN_ModuleName & "/Temp/"
            End Get
        End Property

        'Logs Directory
        Public Shared ReadOnly Property LogsDirectoryPhysicalPath As String
            Get
                Return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath & LogsDirectoryRelativePath)
            End Get
        End Property

        Public Shared ReadOnly Property LogsDirectoryRelativePath As String
            Get
                Return "/DesktopModules/" & DNN_ModuleName & "/Logs/"
            End Get
        End Property

#End Region


#Region "PORTAL FLAGS"

        Public Shared Property DemoModeON(PortalId As Integer) As Boolean
            Get
                Return (CBool(Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "DemoModeON", PortalId, "False")))
            End Get
            Set(value As Boolean)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "DemoModeON", CStr(value))
            End Set
        End Property

        Public Shared Property DebuggingModeON(PortalId As Integer) As Boolean
            Get
                Return CBool(Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "DebuggingModeON", PortalId, "False"))
            End Get
            Set(value As Boolean)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "DebuggingModeON", CStr(value))
            End Set
        End Property

        Public Shared Property LoggingON(PortalId As Integer) As Boolean
            Get
                Return CBool(Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "LoggingON", PortalId, "False"))
            End Get
            Set(value As Boolean)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "LoggingON", CStr(value))
            End Set
        End Property

#End Region


#Region "SESSION VARIABLES AND FLAGS"

        'SessionVariables
        Public Shared Property SessionVariables(VarName As String) As String
            Get
                Dim strReturn As String = String.Empty
                Dim strKeyName As String = CookiePreFix & "SessionVariables"
                If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    strReturn = HttpContext.Current.Request.Cookies(strKeyName).Values(VarName)
                End If
                Return strReturn
            End Get
            Set(value As String)
                Dim strKeyName As String = CookiePreFix & "SessionVariables"
                If HttpContext.Current.Response.Cookies(strKeyName) Is Nothing Then
                    Dim aCookie As New HttpCookie(strKeyName)
                    aCookie.Values(VarName) = value
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.Cookies(strKeyName).Values(VarName) = value
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Request.Cookies(strKeyName).Values(VarName) = value
                End If
            End Set
        End Property



        'Page View
        Public Shared Property PageViewRequest As PageView
            Get
                Dim strKeyName As String = CookiePreFix & "PageViewRequest"
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    Return PageView.Introduction
                Else
                    Return CType(HttpContext.Current.Request.Cookies(strKeyName).Value, PageView)
                End If
            End Get
            Set(value As PageView)
                Dim strKeyName As String = CookiePreFix & "PageViewRequest"
                Dim aCookie As New HttpCookie(strKeyName)
                aCookie.Value = CStr(value)
                aCookie.HttpOnly = False 'Setting HttpOnly to False allows the cookie to be changed via javascript
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.SetCookie(aCookie)
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                'If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                '    HttpContext.Current.Request.Cookies(strKeyName).Value = CStr(value)
                'End If
            End Set
        End Property

        Public Shared Property CurrentPageView As PageView
            Get
                Dim strKeyName As String = CookiePreFix & "CurrentPageView"
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    Return PageView.Introduction
                Else
                    Return CType(HttpContext.Current.Request.Cookies(strKeyName).Value, PageView)
                End If
            End Get
            Set(value As PageView)
                Dim strKeyName As String = CookiePreFix & "CurrentPageView"
                Dim aCookie As New HttpCookie(strKeyName)
                aCookie.Value = CStr(value)
                aCookie.HttpOnly = False 'Setting HttpOnly to False allows the cookie to be changed via javascript
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.SetCookie(aCookie)
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                'If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                '    HttpContext.Current.Request.Cookies(strKeyName).Value = CStr(value)
                'End If
            End Set
        End Property

        'Script File Editor Flags
        Public Shared Property ScriptFileLoadRequested As String
            Get
                Dim strKeyName As String = CookiePreFix & "ScriptFileRequested"
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    Return String.Empty
                Else
                    Return HttpContext.Current.Request.Cookies(strKeyName).Value
                End If
            End Get
            Set(value As String)
                Dim strKeyName As String = CookiePreFix & "ScriptFileRequested"
                Dim aCookie As New HttpCookie(strKeyName)
                aCookie.Value = value
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.SetCookie(aCookie)
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                'If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                '    HttpContext.Current.Request.Cookies(strKeyName).Value = value
                'End If
            End Set
        End Property

        Public Shared Property CurrentScriptFile As String
            Get
                Dim strKeyName As String = CookiePreFix & "CurrentScriptFile"
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    Return String.Empty
                Else
                    Return HttpContext.Current.Request.Cookies(strKeyName).Value
                End If
            End Get
            Set(value As String)
                Dim strKeyName As String = CookiePreFix & "CurrentScriptFile"
                Dim aCookie As New HttpCookie(strKeyName)
                aCookie.Value = value
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.SetCookie(aCookie)
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                'If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                '    HttpContext.Current.Request.Cookies(strKeyName).Value = value
                'End If
            End Set
        End Property

        Public Shared Property AddStatementClicked As Boolean
            Get
                Dim strKeyName As String = CookiePreFix & "AddStatementClicked"
                If HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                    Return False
                Else
                    Return CBool(HttpContext.Current.Request.Cookies(strKeyName).Value)
                End If
            End Get
            Set(value As Boolean)
                Dim strKeyName As String = CookiePreFix & "AddStatementClicked"
                Dim aCookie As New HttpCookie(strKeyName)
                aCookie.Value = CStr(value)
                aCookie.HttpOnly = False 'Setting HttpOnly to False allows the cookie to be changed via javascript
                If HttpContext.Current.Response.Cookies(strKeyName) Is Nothing Then
                    HttpContext.Current.Response.Cookies.Add(aCookie)
                Else
                    HttpContext.Current.Response.SetCookie(aCookie)
                End If
                'Set Request Object's cookie value also. This way the value can be read from this property immediately.
                'If Not HttpContext.Current.Request.Cookies(strKeyName) Is Nothing Then
                '    HttpContext.Current.Request.Cookies(strKeyName).Value = CStr(value)
                'End If
            End Set
        End Property

#End Region

    End Class


    '===================================================================
    '                           FUNCTIONS
    '===================================================================

    Public Class Functions

#Region "PUBLIC SHARED SUBS"

        Public Shared Sub ImpelementModuleThemeForThisPage(Page As System.Web.UI.Page)
            Dim strScriptFilePath As String = ThemesModuleDirectoryRelativePath & "/ModuleTheme.js"
            Dim strStyleSheetFilePath As String = ThemesModuleDirectoryRelativePath & "/jquery-ui.css"

            RegisterScript(Page, strScriptFilePath)
            RegisterStyleSheet(Page, strStyleSheetFilePath)

        End Sub

        Public Shared Sub SetMessage(sender As Object, strMessage As String, MessageType As MessageType)
            Try
                'Displays a message via the main view control
                Dim objView As SkinTweaker.View = Nothing
                'Search through parents until find the main View control (View.ascx)
                If TypeOf sender Is SkinTweaker.View Then
                    objView = sender
                ElseIf TypeOf sender.Parent Is SkinTweaker.View Then
                    objView = sender.Parent
                ElseIf TypeOf sender.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent
                ElseIf TypeOf sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent Is SkinTweaker.View Then
                    objView = sender.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent
                End If
                'Pass message info to View control
                If Not IsNothing(objView) Then
                    objView.SetMessage(strMessage, MessageType)
                End If

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(sender, exc)
            End Try
        End Sub

        Public Shared Function GetFilePaths(strDirectoryPath As String, strFileExtensionFilter As String) As List(Of String)
            Dim lstFilePaths As New List(Of String)
            Dim objDir As New DirectoryInfo(ThemesjQueryDirectoryPhysicalPath)
            For Each objFileInfo As FileInfo In objDir.GetFiles
                If objFileInfo.Extension = strFileExtensionFilter Then
                    lstFilePaths.Add(objFileInfo.FullName)
                End If
            Next
            Return lstFilePaths
        End Function

        Public Shared Sub AppendjQueryScriptEditorLog(sender As System.Web.UI.Control, FunctionName As String, Message As String)
            If HttpContext.Current.User.Identity.Name <> "" Then
                AppendTextToFile("User Name: " & HttpContext.Current.User.Identity.Name & ": " & Now.ToString("dd MMM yyyy hh:mm:ss.ffff") & " : " & sender.ID & " : " & FunctionName & " : " & Message, LogsDirectoryPhysicalPath & "jQueryScriptEditor.log")
            End If
        End Sub

#End Region


#Region "PATHS"

        Public Shared Function GetThemeCssFilePhysicalPath(strThemeName As String, PortalId As Integer, ByRef ErrorMessage As String) As String
            Try
                'Returns the physical path if give just the theme's name
                Dim objDir As New DirectoryInfo(ThemesCssDirectoryPhysicalPath & "\" & strThemeName)
                Dim strThemeCssFilePath As String = String.Empty
                Dim intCount As Integer = 0
                ErrorMessage = String.Empty

                'Scan the folder for css files
                For Each objFileInof As FileInfo In objDir.GetFiles
                    If objFileInof.Extension = ".css" Then
                        intCount = intCount + 1
                        strThemeCssFilePath = objFileInof.FullName
                    End If
                Next
                If intCount > 1 Then
                    ErrorMessage = "The theme directory [" & objDir.FullName & "] contains more than one css file. There can be only one."
                End If
                Return strThemeCssFilePath

            Catch exc As Exception        'Module failed to load
                ErrorMessage = exc.Message
                Return String.Empty
            End Try
        End Function

        Public Shared Function GetThemeCssFileRelativePath(strThemeName As String, PortalId As Integer, ByRef ErrorMessage As String) As String
            Try
                'Returns the relative path if give just the theme's name
                Return (ThemesCssDirectoryRelativePath & strThemeName & "/" & Path.GetFileName(GetThemeCssFilePhysicalPath(strThemeName, PortalId, ErrorMessage)))

            Catch exc As Exception        'Module failed to load
                ErrorMessage = exc.Message
                Return String.Empty
            End Try
        End Function

#End Region


#Region "FILES"

        Public Shared Function CreateBackUpCopyOf(FullPathAndFileNameToBackUp As String, Optional ByRef respErrorMessage As String = "") As Boolean
            CreateBackUpCopyOf = False 'Default value, this way ONLY one path can return True
            Try
                Dim strNewPath As String = FullPathAndFileNameToBackUp & ".bak"
                If File.Exists(strNewPath) Then
                    'need to loop until get a free number for new file name
                    Dim intCount As Integer = 0
                    Do While File.Exists(strNewPath)
                        intCount = intCount + 1
                        strNewPath = FullPathAndFileNameToBackUp & ".bak" & CStr(intCount)
                        'safety switch:
                        If intCount > 1000 Then
                            'Something wierd is going on so just abort
                            respErrorMessage = "There are more than a thousand backup copies of '" & FullPathAndFileNameToBackUp & "'"
                            Exit Function
                        End If
                    Loop
                Else
                    'There is no file by that name so this is the First Backup, so make original copy
                    Dim strOriginalRefBackupFileName As String = FullPathAndFileNameToBackUp & OriginalFileNameExtender
                    'Make sure that one does not already exist
                    If Not File.Exists(strOriginalRefBackupFileName) Then
                        'The following should not allow an overwrite, but put inside if statement just in case
                        File.Copy(FullPathAndFileNameToBackUp, strOriginalRefBackupFileName)
                    End If
                End If
                'OK, have a new file name and path now so....
                File.Copy(FullPathAndFileNameToBackUp, strNewPath)
                'Make sure that the backup file is there now
                If File.Exists(strNewPath) Then
                    Return True
                Else
                    'For some reason the file did not save.
                    respErrorMessage = "Attempt to save backup copy of '" & FullPathAndFileNameToBackUp & "' failed."
                    Exit Function
                End If

            Catch ex As Exception
                respErrorMessage = ex.Message
                Return False
            End Try
        End Function

        Public Shared Function GetTextFileContents(ByVal FullPath As String, Optional ByRef respErrorMessage As String = "") As String
            GetTextFileContents = String.Empty 'Default value, this way ONLY one path can return data
            Dim strContents As String
            Try
                GC.Collect() 'Force clean up
                If Not File.Exists(FullPath) Then
                    respErrorMessage = "This File Does Not Exist: '" & FullPath & "'."
                End If
                Using objReader As StreamReader = New StreamReader(FullPath)
                    strContents = String.Copy(objReader.ReadToEnd())
                    objReader.Close()
                    objReader.Dispose()
                End Using
                GC.Collect() 'Force clean up
                Return strContents

            Catch ex As Exception
                respErrorMessage = ex.Message
                Return String.Empty
            End Try
        End Function

        Public Shared Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByRef respErrorMessage As String = "") As Boolean
            'This will overwrite everything in the file.
            SaveTextToFile = False
            Try
                GC.Collect() 'Force clean up
                Using objStreamWriter As StreamWriter = File.CreateText(FullPath)
                    objStreamWriter.Write(strData)
                    objStreamWriter.Close()
                    objStreamWriter.Dispose()
                End Using
                GC.Collect() 'Force clean up
                Return True

            Catch ex As Exception
                respErrorMessage = ex.Message
                Return False
            End Try
        End Function

        Public Shared Function AppendTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByRef respErrorMessage As String = "") As Boolean
            AppendTextToFile = False
            Try
                GC.Collect() 'Force clean up
                Using objStreamWriter As StreamWriter = File.AppendText(FullPath)
                    objStreamWriter.WriteLine(strData)
                    objStreamWriter.Close()
                    objStreamWriter.Dispose()
                End Using
                GC.Collect() 'Force clean up
                Return True

            Catch ex As Exception
                respErrorMessage = ex.Message
                Return False
            End Try
        End Function

#End Region

    End Class


    '===================================================================
    '                           INJECTOR RESOURCES
    '===================================================================

    Public Class InjectorResources

        Public Shared ReadOnly Property ThemesApplyToPageFilePath As String
            Get
                Return ThemesjQueryDirectoryRelativePath & "/Templates/ApplyToPage.js"
            End Get
        End Property

        Public Shared Property PortalApplyCssScopeToPage(PortalId As Integer) As Boolean
            Get
                Return CBool(Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "ApplyCssScopeToPage", PortalId, "False"))
            End Get
            Set(value As Boolean)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "ApplyCssScopeToPage", value)
            End Set
        End Property

        Public Shared Property PortalThemeCssFilePath(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "CssFilePath", PortalId, String.Empty)
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "CssFilePath", value)
            End Set
        End Property

        Public Shared Property PortalThemeSciptFilePath(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "SciptFilePath", PortalId, String.Empty)
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "SciptFilePath", value)
            End Set
        End Property

        Public Shared Sub RegisterStyleSheet(Page As System.Web.UI.Page, RelativeFilePath As String)
            Dim objHtmlGenericControl As New HtmlGenericControl("link")
            objHtmlGenericControl.Attributes.Add("href", RelativeFilePath)
            objHtmlGenericControl.Attributes.Add("type", "text/css")
            objHtmlGenericControl.Attributes.Add("rel", "stylesheet")
            Page.Header.Controls.Add(objHtmlGenericControl)
        End Sub

        Public Shared Sub RegisterScript(Page As System.Web.UI.Page, RelativeFilePath As String)
            Dim objHtmlGenericControl As New HtmlGenericControl("script")
            objHtmlGenericControl.Attributes.Add("src", "" & RelativeFilePath & "")
            objHtmlGenericControl.Attributes.Add("type", "text/javascript")
            Page.Header.Controls.Add(objHtmlGenericControl)
        End Sub

        Public Shared Property AddjQuery(PortalId As Integer) As Boolean
            Get
                Return (CBool(Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "AddjQuery", PortalId, "False")))
            End Get
            Set(value As Boolean)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "AddjQuery", CStr(value))
            End Set
        End Property

        Public Shared Property jQueryVersionNumber(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "jQueryVersionNumber", PortalId, "1.6.4")
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "jQueryVersionNumber", value)
            End Set
        End Property

        Public Shared Property jQueryUI_VersionNumber(PortalId As Integer) As String
            Get
                Return Portals.PortalController.GetPortalSetting(PortalSettingsPreFix & "jQueryUI_VersionNumber", PortalId, "1.8.16")
            End Get
            Set(value As String)
                Portals.PortalController.UpdatePortalSetting(PortalId, PortalSettingsPreFix & "jQueryUI_VersionNumber", value)
            End Set
        End Property

        Friend Shared Function SafeDNNVersion(ByRef major As Integer, ByRef minor As Integer, ByRef revision As Integer, ByRef build As Integer) As Boolean
            Dim ver As System.Version = System.Reflection.Assembly.GetAssembly(GetType(DotNetNuke.Common.Globals)).GetName().Version
            If ver IsNot Nothing Then
                major = ver.Major
                minor = ver.Minor
                build = ver.Build
                revision = ver.Revision
                Return True
            Else
                major = 0
                minor = 0
                build = 0
                revision = 0
                Return False
            End If
        End Function

        Friend Shared Sub InjectjQueryLibary(page As System.Web.UI.Page, includejQueryUI As Boolean, uncompressed As Boolean, _
                                              jQueryVersionNumber As String, jQueryUI_VersionNumber As String)

            ' Includes the jQuery libraries onto the page
            ' Page object from calling page/control
            ' if true, includes the jQuery UI libraries
            ' if true, includes the uncompressed libraries

            Dim major As Integer, minor As Integer, build As Integer, revision As Integer
            Dim injectjQueryLib As Boolean = False
            Dim injectjQueryUiLib As Boolean = False
            If SafeDNNVersion(major, minor, revision, build) Then
                Select Case major
                    Case 4
                        injectjQueryLib = True
                        injectjQueryUiLib = True
                        Exit Select
                    Case 5
                        injectjQueryLib = False
                        injectjQueryUiLib = True
                        Exit Select
                    Case Else
                        '6.0 and above
                        injectjQueryLib = False
                        injectjQueryUiLib = False
                        Exit Select
                End Select
            Else
                injectjQueryLib = True
            End If

            If injectjQueryLib Then
                'no in-built jQuery libraries into the framework, so include the google version
                Dim [lib] As String = Nothing
                If uncompressed Then
                    [lib] = "http://ajax.googleapis.com/ajax/libs/jquery/" & jQueryVersionNumber & "/jquery.js"
                Else
                    [lib] = "http://ajax.googleapis.com/ajax/libs/jquery/" & jQueryVersionNumber & "/jquery.min.js"
                End If
                If page.Header.FindControl("jquery") Is Nothing Then
                    Dim jQueryLib As New System.Web.UI.HtmlControls.HtmlGenericControl("script")
                    jQueryLib.Attributes.Add("src", [lib])
                    jQueryLib.Attributes.Add("type", "text/javascript")
                    jQueryLib.ID = "jquery"
                    page.Header.Controls.Add(jQueryLib)

                    ' use the noConflict (stops use of $) due to the use of prototype with a standard DNN distro
                    Dim noConflictScript As New System.Web.UI.HtmlControls.HtmlGenericControl("script")
                    noConflictScript.InnerText = " jQuery.noConflict(); "
                    noConflictScript.Attributes.Add("type", "text/javascript")

                    page.Header.Controls.Add(noConflictScript)
                End If
            Else
                'call DotNetNuke.Framework.jQuery.RequestRegistration();
                Dim jQueryType As Type = Type.[GetType]("DotNetNuke.Framework.jQuery, DotNetNuke")
                If jQueryType IsNot Nothing Then
                    'run the DNN 5.0 specific jQuery registration code
                    jQueryType.InvokeMember("RequestRegistration", System.Reflection.BindingFlags.InvokeMethod Or System.Reflection.BindingFlags.[Public] Or System.Reflection.BindingFlags.[Static], Nothing, jQueryType, Nothing)
                End If
            End If

            'include the UI libraries??
            If includejQueryUI Then
                If injectjQueryUiLib Then
                    Dim [lib] As String = Nothing
                    If uncompressed Then
                        [lib] = "http://ajax.googleapis.com/ajax/libs/jqueryui/" & jQueryUI_VersionNumber & "/jquery-ui.js"
                    Else

                        [lib] = "http://ajax.googleapis.com/ajax/libs/jqueryui/" & jQueryUI_VersionNumber & "/jquery-ui.min.js"
                    End If
                    page.ClientScript.RegisterClientScriptInclude("jqueryUI", [lib])
                Else
                    'use late bound call to request registration of jquery
                    Dim jQueryType As Type = Type.[GetType]("DotNetNuke.Framework.jQuery, DotNetNuke")
                    If jQueryType IsNot Nothing Then
                        'dnn 6.0 and later, allow jquery ui to be loaded from the settings.
                        jQueryType.InvokeMember("RequestUIRegistration", System.Reflection.BindingFlags.InvokeMethod Or System.Reflection.BindingFlags.[Public] Or System.Reflection.BindingFlags.[Static], Nothing, jQueryType, Nothing)
                    End If
                End If
            End If
        End Sub

    End Class

End Namespace