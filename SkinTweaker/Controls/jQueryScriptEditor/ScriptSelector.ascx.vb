Imports System.IO
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions

Public Class ScriptSelector
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase


#Region "PUBLIC PROPERTIES, METHODS AND EVENTS"

    Public Event SelectedIndexChanged(sender As Object, e As System.EventArgs)

    Dim _IncludeNONE As Boolean

    Public Property IncludeNONE As Boolean
        Get
            Return _IncludeNONE
        End Get
        Set(value As Boolean)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "IncludeNONE.Set", "Fired, value='" & CStr(value) & "'")
            _IncludeNONE = value
        End Set
    End Property

    Public Property SelectedValue As String
        Get
            Return ddlScript.SelectedValue
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "SelectedValue.Set", "Fired, value='" & value & "'")
            ddlScript.SelectedValue = value
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return ddlScript.Enabled
        End Get
        Set(value As Boolean)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Enabled.Set", "Fired, value='" & CStr(value) & "'")
            ddlScript.Enabled = value
        End Set
    End Property

    Public Property ToolTip As String
        Get
            Return ddlScript.ToolTip
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ToolTip.Set", "Fired, value='" & value & "'")
            ddlScript.ToolTip = value
        End Set
    End Property

#End Region


#Region "Event Handlers"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Load", "Fired")
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Unload", "Fired")
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_PreRender", "Fired")
    End Sub

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Init", "Fired")
        'This needs to be loaded at Init so that the default selection can be set OnLoad by the page using it.
        If ddlScript.Items.Count < 1 Then Call PopulateDropDownList()
    End Sub

    Private Sub ddlScript_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlScript.SelectedIndexChanged
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ddlScript_SelectedIndexChanged", "Fired")
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub PopulateDropDownList()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "PopulateDropDownList", "Fired")
        Try
            Dim objDir As New DirectoryInfo(ThemesjQueryDirectoryPhysicalPath)
            ddlScript.Items.Clear()
            If _IncludeNONE Then
                Dim objItem As New ListItem
                Dim strName As String = NoScriptName
                objItem.Value = strName
                objItem.Text = strName
                ddlScript.Items.Add(objItem)
            End If
            For Each objFileInfo As FileInfo In objDir.GetFiles
                If objFileInfo.Extension = ".xml" Then
                    Dim objItem As New ListItem
                    Dim strName As String = Split(objFileInfo.Name, ".")(0) 'Remove file exstention
                    objItem.Value = strName
                    objItem.Text = strName
                    ddlScript.Items.Add(objItem)
                End If
            Next

        Catch exc As Exception
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region

End Class