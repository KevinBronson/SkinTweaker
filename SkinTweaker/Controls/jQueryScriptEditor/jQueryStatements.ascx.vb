
'======================================================================================================================
'NOTES FROM: http://www.codeproject.com/Articles/29870/Loading-Web-User-Controls-Dynamically-in-ASP-NET-P
'======================================================================================================================
'Now we have the steps for loading and using web user controls dynamically. We can follow these steps which are briefly 
'described below (for detailed solutions, please refer to the attached source files).
'    1. Implement a private property for saving and retrieving the saved virtual path of the web user controls.
'    2. Implement a private method for creating and loading the web user control according to the saved virtual path. 
'       It is important to remember to set the ID for the control; otherwise, the page won't work properly.
'    3. In Page_Load event, simply call the method we created in step 2 to recreate the control.
'    4. In the Button_Click event, set the private property we created in step 1 to the virtual path of the control that 
'       needs to be created and loaded; then call the method we created in step 2 to recreate and load the control.
'    5. Override the Dispose method and dispose the controls.
'======================================================================================================================

Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports System.Xml


Public Class jQueryStatements
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase

    Dim PathToStatementControl As String = "DesktopModules/SkinTweaker/Controls/jQueryScriptEditor/jQueryStatement.ascx"


#Region "PUBLIC PROPERTIES AND METHODS"

    Public Property XML As String
        Get
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "Fired")
            'Use this to Create XML
            Dim strReturn As String = String.Empty
            Try
                Dim strList As New List(Of String)
                strList.Add("<Statements>")
                For Each obj As jQueryStatement In phStatements.Controls
                    strList.Add(obj.XML)
                Next
                strList.Add("</Statements>")
                strReturn = String.Join(vbCrLf, strList.ToArray)

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "strReturn='" & strReturn & "'")
            Return strReturn
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Set", "Fired, value='" & value & "'")
            Try
                'Use this to READ XML
                Dim XMLDoc As New XmlDocument
                XMLDoc.LoadXml(value)
                If Not XMLDoc Is Nothing Then
                    'Objects should have been loaded by persistance so just assign values now
                    Dim nodes As XmlNodeList = XMLDoc.SelectNodes("jQueryScriptConfig/Statements/Statement")
                    For Each node As XmlNode In nodes
                        'Try to pull objects from placeholder (sometimes not there since this gets called twice during file load process)
                        Dim objStatement As jQueryStatement = CType(phStatements.FindControl(node.FirstChild.InnerText), jQueryStatement)
                        If Not objStatement Is Nothing Then
                            objStatement.XML = "<root>" & node.InnerXml & "</root>"
                        End If
                    Next
                    Dim node2 As XmlNode = XMLDoc.SelectSingleNode("jQueryScriptConfig/StatementControlsToPersist")
                    StatementControlsToPersist = node2.InnerText
                Else
                    SetMessage(Me, "Could not read Statements Node", MessageType.ErrorMessage)
                End If

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Set
    End Property

    Public ReadOnly Property GetjQueryStatments As String
        Get
            Dim strList As New List(Of String)
            Try
                For Each obj As jQueryStatement In phStatements.Controls
                    strList.Add(obj.GetjQueryStatment)
                Next

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
            Return String.Join(vbCrLf, strList.ToArray)
        End Get
    End Property

    Public Property StatementControlsToPersist() As String
        Get
            Dim strReturn As String = SessionVariables("StatementControlsToPersist")
            If strReturn Is Nothing Then strReturn = String.Empty
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "StatementControlsToPersist.Get", "Fired, strReturn='" & strReturn & "'")
            Return strReturn
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "StatementControlsToPersist.Set", "Fired, value='" & value & "'")
            SessionVariables("StatementControlsToPersist") = value
        End Set
    End Property

    Public Sub RemoveStatement(ByVal ID As String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "RemoveStatement", "Fired")
        Try
            Dim arrControlIDs() As String = Split(StatementControlsToPersist, ",")
            Dim intMaxCount As Integer = UBound(arrControlIDs)

            If intMaxCount < 2 Then
                'There is only one ID left so ...
                Call RemoveAllStatements()
            Else
                '1.) Remove from placeholder
                phStatements.Controls.Remove(phStatements.FindControl(ID))
                '2.) Remove from the Persistant Controls string
                Dim strList As New List(Of String)
                ID = Replace(ID, "Statement", "") 'Abstract just the number
                For intCount As Integer = 0 To intMaxCount
                    If intCount = intMaxCount + 1 Then Exit For
                    Dim strID As String = arrControlIDs(intCount)
                    If strID <> ID Then strList.Add(strID)
                Next
                StatementControlsToPersist = String.Join(",", strList.ToArray)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "Event Handlers"

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Unload", "Fired")
    End Sub

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Init", "Fired")
        Try
            If AddStatementClicked Then
                AddStatementClicked = False 'Reset Flag
                Call AddStatement()
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_Load", "Fired")
        Try
            Call LoadPersistedControls() 'Must be called on Page_Load every time to maintain viewstate

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Page_PreRender", "Fired")
        If DemoModeON(PortalId) Then
            lbAddStatement.Enabled = False
            lbRemoveAllStatements.Enabled = False
        End If
    End Sub

    Private Sub lbRemoveAllStatements_Click(sender As Object, e As System.EventArgs) Handles lbRemoveAllStatements.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbRemoveAllStatements_Click", "Fired")
        Call RemoveAllStatements()
    End Sub

    Private Sub lbAddStatement_Click(sender As Object, e As System.EventArgs) Handles lbAddStatement.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbAddStatement_Click", "Fired")
        Try
            'Called via cookie flag so can respond earlier; first response is On_Init
            Dim objParent As jQueryScriptEditor = CType(Parent.Parent.Parent, jQueryScriptEditor) 'jQSE.mv.vw.me
            objParent.SaveCurrentScript()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub LoadPersistedControls()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "LoadPersistedControls", "Fired")
        Try
            'StatementControlsToPersist string array has the following format: LastID#,ID,ID,ID,....
            Dim arrControlIDs() As String = Split(StatementControlsToPersist, ",")
            Dim intMaxCount As Integer = UBound(arrControlIDs)
            Dim intCount As Integer = 0
            'See if there are some controls in the persist string or not
            If intMaxCount > 0 Then
                For intCount = 1 To intMaxCount
                    If intCount = intMaxCount + 1 Then Exit For
                    Dim obj As jQueryStatement = CType(Page.LoadControl(PathToStatementControl), jQueryStatement)
                    obj.ID = "Statement" & arrControlIDs(intCount)
                    phStatements.Controls.Add(obj)
                Next
            Else
                'Sometimes controls get stuck in here between page calls
                phStatements.Controls.Clear()
            End If
            Dim strFileName As String = ScriptFileLoadRequested
            If strFileName <> String.Empty Then
                ScriptFileLoadRequested = String.Empty 'Reset the flag
                Dim strXML_FilePath As String = ThemesjQueryDirectoryPhysicalPath & "\" & strFileName & ".xml"
                Dim respErrorMessage As String = String.Empty
                Dim strXML As String = GetTextFileContents(strXML_FilePath, respErrorMessage)
                If respErrorMessage = String.Empty Then
                    Dim objParent As jQueryScriptEditor = CType(Parent.Parent.Parent, jQueryScriptEditor) 'jQSE.mv.vw.me
                    'Pass XML to jQueryScriptEditor so it can load values
                    objParent.XML = strXML
                    'Set flag to show file is loaded
                    objParent.FileIsLoaded = True
                Else
                    SetMessage(Me, respErrorMessage, MessageType.ErrorMessage)
                End If
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub AddStatement()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "AddStatement", "Fired")
        Try
            'Add new control to the persistance string array and that's it; LoadPersistedControls will do the rest
            '1.) On_Init : AddStatement
            '2.) On_Load : LoadPersistedControls
            '3.) lbAddStatement_Click: SaveCurrentScript()

            'StatementControlsToPersist string array has the following format: LastID#,ID,ID,ID,....
            Dim arrControls() As String = Split(StatementControlsToPersist, ",")
            Dim intUbound As String = UBound(arrControls)
            'See if there are already some controls or not
            If intUbound = 0 Then
                ReDim arrControls(0 To 1)
                arrControls(0) = "0" 'Set initial ID #
            Else
                ReDim Preserve arrControls(intUbound + 1)
            End If
            'Create New Entry in Persisted Controls string
            arrControls(0) = CStr(CInt(arrControls(0)) + 1) 'Create New ID NUMBER
            arrControls(intUbound + 1) = arrControls(0) 'Add New ID NUMBER to StatementControlsToPersist string array
            'Save Persistance String Array
            StatementControlsToPersist = String.Join(",", arrControls)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub RemoveAllStatements()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "RemoveAllStatements", "Fired")
        Try
            StatementControlsToPersist = ""
            phStatements.Controls.Clear()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


End Class

