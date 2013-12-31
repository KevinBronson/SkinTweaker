Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports System.Xml


Public Class jQueryScriptEditor
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase


#Region "PUBLIC PROPERTIES"

    Public Property XML As String
        Get
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "Fired")
            'Use this to CREATE XML
            Dim strReturn As String = String.Empty
            Try
                Dim strList As New List(Of String)
                strList.Add("<?xml version=""1.0"" encoding=""utf-8"" ?>")
                strList.Add("<jQueryScriptConfig>")
                strList.Add(Options1.XML)
                strList.Add(jQueryStatements1.XML)
                strList.Add("<StatementControlsToPersist>" & jQueryStatements1.StatementControlsToPersist & "</StatementControlsToPersist>")
                strList.Add("</jQueryScriptConfig>")
                strReturn = String.Join(vbCrLf, strList.ToArray)

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "strReturn='" & strReturn & "'")
            Return strReturn
        End Get
        Set(ByVal value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Set", "Fired, value='" & value & "'")
            Try
                'Use this to READ XML
                Options1.XML = value
                jQueryStatements1.XML = value

                'The data for txtCustomScript is a special case; must be loaded from text file, not xml
                Dim respErrorMessage As String = String.Empty
                Dim str_datFilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & CurrentScriptFile & ".dat"
                txtCustomScript.Text = GetTextFileContents(str_datFilePath, respErrorMessage)
                If respErrorMessage <> String.Empty Then
                    SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
                End If

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Set
    End Property

    Public Property CustomScript As String
        Get
            Return txtCustomScript.Text
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "CustomScript.Set", "Fired, value='" & value & "'")
            txtCustomScript.Text = value
        End Set
    End Property

    Public Property FileIsLoaded As Boolean
        Get
            Return (ScriptFilesController1.FileIsLoaded)
        End Get
        Set(value As Boolean)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "FileIsLoaded.Set", "Fired, value='" & value & "'")
            ScriptFilesController1.FileIsLoaded = value
        End Set
    End Property

    Public ReadOnly Property ScriptFilesController As ScriptFilesController
        Get
            Return ScriptFilesController1
        End Get
    End Property

#End Region


#Region "PUBLIC METHODS"

    Public Sub SaveCurrentScript()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "SaveCurrentScript", "Fired")
        ScriptFilesController1.SaveCurrentScript()
    End Sub

    Public Function GenerateScriptFromUserInputs() As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GenerateScriptFromUserInputs", "Fired")
        Dim strReturn As String = String.Empty
        Try
            Dim ListOfStatements As New List(Of String)
            Dim strOptions As String = Options1.GetjQueryStatments
            Dim strjQueryStatements As String = jQueryStatements1.GetjQueryStatments
            If strOptions <> "" Then ListOfStatements.Add(strOptions)
            If strjQueryStatements <> "" Then ListOfStatements.Add(strjQueryStatements)
            If txtCustomScript.Text <> "" Then ListOfStatements.Add(txtCustomScript.Text)
            'See if there are any statements in the list
            If ListOfStatements.Count > 0 Then
                Dim ScriptList As New List(Of String)
                ScriptList.Add(vbTab & "$(function () {")
                ScriptList.Add(String.Join(vbCrLf, ListOfStatements.ToArray))
                ScriptList.Add(vbTab & "});")
                strReturn = String.Join(vbCrLf, ScriptList.ToArray)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return strReturn
    End Function

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
                'MultiView1.ActiveViewIndex = 0
                lblTitle.Text = lblTitle.Text & " - DEMO MODE"
                lbPreviewScript.Enabled = False
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbPreviewScript_Click(sender As Object, e As System.EventArgs) Handles lbPreviewScript.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbPreviewScript_Click", "Fired")
        Try
            txtScriptPreview.Text = GenerateScriptFromUserInputs()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region



End Class