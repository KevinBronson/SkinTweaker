Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports System.Xml
Imports System.IO


Public Class ScriptFilesController
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase


#Region "PUBLIC PROPERTIES"

    Public Property FileIsLoaded As Boolean
        Get
            Return chkFileIsLoaded.Checked
        End Get
        Set(value As Boolean)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "FileIsLoaded.Set", "Fired, value='" & value & "'")
            chkFileIsLoaded.Checked = value
        End Set
    End Property

#End Region


#Region "PUBLIC METHODS"

    Public Sub SaveCurrentScript()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "SaveCurrentScript", "Fired")
        Try
            If MultiView1.ActiveViewIndex = 0 Then
                Call SaveScript(ddlScriptSelector.SelectedValue)
            Else
                If txtNewName.Text <> "" Then
                    If chkCopyCurrentScript.Checked Then
                        Call SaveScript(txtNewName.Text)
                    Else
                        'Simply copy the template files
                        Dim strSource As String = ThemesjQueryDirectoryPhysicalPath & "\Templates\NEW.xml"
                        Dim strDest As String = ThemesjQueryDirectoryPhysicalPath & "\" & txtNewName.Text & ".xml"
                        File.Copy(strSource, strDest)
                        strSource = ThemesjQueryDirectoryPhysicalPath & "\Templates\NEW.js"
                        strDest = ThemesjQueryDirectoryPhysicalPath & "\" & txtNewName.Text & ".js"
                        File.Copy(strSource, strDest)
                        strSource = ThemesjQueryDirectoryPhysicalPath & "\Templates\NEW.dat"
                        strDest = ThemesjQueryDirectoryPhysicalPath & "\" & txtNewName.Text & ".dat"
                        File.Copy(strSource, strDest)
                    End If
                    OpenScript(txtNewName.Text)
                Else
                    SetMessage(Me, "Cannot save a script with a blank name.", MessageType.Warning)
                End If
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Public Sub DeleteCurrentScript()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "DeleteCurrentScript", "Fired")
        Call DeleteScript(ddlScriptSelector.SelectedValue)
    End Sub

    Public Sub OpenScript(strScriptName As String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "OpenScript", "Fired")
        Try
            Dim respErrorMessage As String = String.Empty
            Dim objParent As SkinTweaker.jQueryScriptEditor = CType(Parent.Parent.Parent, SkinTweaker.jQueryScriptEditor) 'jQSE.mv.vw.me
            Dim strXML_FilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".xml"
            Dim strXML As String = GetTextFileContents(strXML_FilePath, respErrorMessage)
            If respErrorMessage = "" Then
                '1.) Pass XML to jQueryScriptEditor so it can set up the persistance string
                objParent.XML = strXML
                '2.)  Set Script File Flags
                ScriptFileLoadRequested = strScriptName
                CurrentScriptFile = strScriptName
                '3.) Reload
                PageViewRequest = PageView.jQueryScriptEditor
                If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "OpenScript", "-----------------  RELOADING NOW  -----------------")
                Response.Redirect(Request.RawUrl) 'This cleans out viewstate for a new script to be loaded
            Else
                SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Public Sub SaveScript(strScriptName As String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "SaveScript", "Fired")
        Try
            If strScriptName <> "" Then
                Dim objParent As SkinTweaker.jQueryScriptEditor = CType(Parent.Parent.Parent, SkinTweaker.jQueryScriptEditor) 'jQSE.mv.vw.me
                Dim respErrorMessage As String = String.Empty
                Dim strXML_FilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".xml"
                Dim str_jsFilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".js"
                Dim str_datFilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".dat"
                'Save to XML file
                If SaveTextToFile(objParent.XML, strXML_FilePath, respErrorMessage) Then
                    'Save to js file
                    If SaveTextToFile(objParent.GenerateScriptFromUserInputs, str_jsFilePath, respErrorMessage) Then
                        'Save to dat file
                        If SaveTextToFile(objParent.CustomScript, str_datFilePath, respErrorMessage) Then
                            'Save Successful
                            SetMessage(Me, "Save Successful", MessageType.Success)
                        Else
                            SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
                        End If
                    Else
                        SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
                    End If
                Else
                    SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
                End If
            Else
                SetMessage(Me, "Cannot save a script with a blank name.", MessageType.Warning)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Public Sub DeleteScript(strScriptName As String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "DeleteScript", "Fired")
        Try
            'Delete all 3 files and then open Default script
            If strScriptName = "Default" Then
                SetMessage(Me, "Cannot delete the Default Script", MessageType.Warning)
            Else
                Dim strXML_FilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".xml"
                Dim str_jsFilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".js"
                Dim str_datFilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strScriptName & ".dat"
                File.Delete(strXML_FilePath)
                File.Delete(str_jsFilePath)
                File.Delete(str_datFilePath)
                OpenScript("Default")
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Init", "Fired")
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Load", "Fired")
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Unload", "Fired")
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_PreRender", "Fired")
        Try
            If DemoModeON(PortalId) Then
                lbCancel.Enabled = False
                lbCopy.Enabled = False
                lbDelete.Enabled = False
                lbNew.Enabled = False
                lbSave.Enabled = False
                ddlScriptSelector.Enabled = False
            Else
                If DebuggingModeON(PortalId) Then
                    lbReload.Visible = True
                    chkFileIsLoaded.Visible = True
                End If
            End If

            Dim strCurrentScriptFile As String = CurrentScriptFile
            If strCurrentScriptFile = String.Empty Then
                OpenScript("Default") 'No file opened yet, so open one
            ElseIf Not chkFileIsLoaded.Checked Then
                OpenScript(strCurrentScriptFile) 'Viewstate has been wiped out so reload script file
            Else
                ddlScriptSelector.SelectedValue = strCurrentScriptFile
            End If
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub ScriptSelector1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlScriptSelector.SelectedIndexChanged
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ScriptSelector1_SelectedIndexChanged", "Fired")
        Call OpenScript(ddlScriptSelector.SelectedValue)
    End Sub

    Private Sub lbSave_Click(sender As Object, e As System.EventArgs) Handles lbSave.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbSave_Click", "Fired")
        Call SaveCurrentScript()
    End Sub

    Private Sub lbNew_Click(sender As Object, e As System.EventArgs) Handles lbNew.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbNew_Click", "Fired")
        Call ShowNewFileCommandBar()
        chkCopyCurrentScript.Checked = False
    End Sub

    Private Sub lbCopy_Click(sender As Object, e As System.EventArgs) Handles lbCopy.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbCopy_Click", "Fired")
        Call ShowNewFileCommandBar()
        chkCopyCurrentScript.Checked = True
    End Sub

    Private Sub lbCancel_Click(sender As Object, e As System.EventArgs) Handles lbCancel.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbCancel_Click", "Fired")
        Call ShowDefaultCommandBar()
    End Sub

    Private Sub lbDelete_Click(sender As Object, e As System.EventArgs) Handles lbDelete.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbDelete_Click", "Fired")
        Call DeleteCurrentScript()
    End Sub

    Private Sub lbReload_Click(sender As Object, e As System.EventArgs) Handles lbReload.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbReload_Click", "Fired")
        OpenScript(ddlScriptSelector.SelectedValue)
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub ShowDefaultCommandBar()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ShowDefaultCommandBar", "Fired")
        MultiView1.ActiveViewIndex = 0
        'Buttons to Show
        lbSave.Visible = True
        lbNew.Visible = True
        lbCopy.Visible = True
        lbDelete.Visible = True
        'Buttons to Hide
        lbCancel.Visible = False
    End Sub

    Private Sub ShowNewFileCommandBar()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ShowNewFileCommandBar", "Fired")
        MultiView1.ActiveViewIndex = 1
        'Buttons to Show
        lbSave.Visible = True
        lbCancel.Visible = True
        'Hide unneeded buttons
        lbNew.Visible = False
        lbCopy.Visible = False
        lbDelete.Visible = False
    End Sub

#End Region


End Class