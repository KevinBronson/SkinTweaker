Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports System.Xml

Public Class jQueryStatement
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase


#Region "PUBLIC PROPERTIES"

    Public Property Parameters As String
        Get
            Return txtParameters.Text
        End Get
        Set(value As String)
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Parameters.Set", "Fired, value='" & value & "'")
            txtParameters.Text = value
        End Set
    End Property

    Public Property XML As String
        Get
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "Fired")
            'Use this to CREATE XML
            Dim strReturn As String = String.Empty
            Try
                Dim strList As New List(Of String)
                strList.Add("<Statement>")
                strList.Add("<ID>" & Me.ID & "</ID>")
                strList.Add("<SelectorType>" & ddlSelectorType.SelectedValue & "</SelectorType>")
                strList.Add("<SelectorName>" & acSelectorName.Text & "</SelectorName>")
                strList.Add("<PluginName>" & acPluginName.Text & "</PluginName>")
                strList.Add("<PluginParameters>" & txtParameters.Text & "</PluginParameters>")
                strList.Add("</Statement>")
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
                    Dim root As XmlNode = XMLDoc.SelectSingleNode("root")
                    For Each node As XmlNode In root.ChildNodes
                        Select Case node.Name
                            Case "ID"
                                'ID already assigned by persistance string
                            Case "SelectorType"
                                ddlSelectorType.SelectedValue = node.InnerText
                            Case "SelectorName"
                                acSelectorName.Text = node.InnerText

                            Case "PluginName"
                                acPluginName.Text = node.InnerText
                            Case "PluginParameters"
                                txtParameters.Text = node.InnerText
                        End Select
                    Next
                Else
                    'It was empty so like whatever
                End If

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Set
    End Property

    Public ReadOnly Property GetjQueryStatment As String
        Get
            Dim strReturn As String = String.Empty
            Try
                strReturn = vbTab & vbTab & "$(""" & GetSelectorString(ddlSelectorType.SelectedValue, acSelectorName.Text) & """)." & _
                                GetPluginString(acPluginName.Text, txtParameters.Text)

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
            Return strReturn
        End Get
    End Property

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
            Call Populate_acPluginName()
            Call Populate_acSelectorName()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub ddlSelectorType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectorType.SelectedIndexChanged
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "ddlSelectorType_SelectedIndexChanged", "Fired")
        Try
            Call Populate_acSelectorName()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbRemove_Click(sender As Object, e As System.EventArgs) Handles lbRemove.Click
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "lbRemove_Click", "Fired")
        Try
            Dim objParent As jQueryStatements = CType(Parent.Parent, jQueryStatements) 'jQS.ph.me
            objParent.RemoveStatement(Me.ID)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "Plugin"

    Public Sub Populate_acPluginName() 'Needs to be PUBLIC so parent can call when dynamically loaded
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Populate_acPluginName", "Fired")
        Try
            Dim respListIsExclussive As Boolean
            'Generate a list of possible values for the combo box
            acPluginName.DataSource = GetPossibleValuesForjQueryPluginType(jQueryPluginType.ALL, respListIsExclussive)
            acPluginName.DataSource.Sort()
            acPluginName.DataBind()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Function GetPossibleValuesForjQueryPluginType(Type As jQueryPluginType, ByRef respListIsExclussive As Boolean) As List(Of String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetPossibleValuesForjQueryPluginType", "Fired")
        Dim PossibleValues = New List(Of String)
        Try
            respListIsExclussive = False
            Dim CaseType As jQueryPluginType
            Dim TypeValues() As Integer = CType([Enum].GetValues(GetType(jQueryPluginType)), Integer())
            For Each TypeValue In TypeValues
                If Type = jQueryPluginType.ALL Then
                    CaseType = TypeValue
                Else
                    CaseType = Type
                End If
                Select Case CaseType
                    Case jQueryPluginType.NONE
                        If Type <> jQueryPluginType.ALL Then PossibleValues.Add("Type is set to 'NONE'")
                    Case jQueryPluginType.Effect
                        respListIsExclussive = True
                        PossibleValues.Add("animate")
                        PossibleValues.Add("toggleClass")
                        PossibleValues.Add("addClass")
                        PossibleValues.Add("removeClass")
                        PossibleValues.Add("switchClass")
                        PossibleValues.Add("effect")
                        PossibleValues.Add("toggle")
                        PossibleValues.Add("hide")
                        PossibleValues.Add("show")
                    Case jQueryPluginType.Interaction
                        respListIsExclussive = True
                        PossibleValues.Add("draggable")
                        PossibleValues.Add("droppable")
                        PossibleValues.Add("resizable")
                        PossibleValues.Add("selectable")
                        PossibleValues.Add("sortable")
                    Case jQueryPluginType.Utility
                        PossibleValues.Add("position")
                    Case jQueryPluginType.Widget
                        respListIsExclussive = True
                        PossibleValues.Add("accordion")
                        PossibleValues.Add("autocomplete")
                        PossibleValues.Add("button")
                        PossibleValues.Add("datepicker")
                        PossibleValues.Add("dialog")
                        PossibleValues.Add("progressbar")
                        PossibleValues.Add("slider")
                        PossibleValues.Add("tabs")
                End Select
                If Type <> jQueryPluginType.ALL Then Exit For
            Next
            If Type = jQueryPluginType.ALL Then respListIsExclussive = False

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return PossibleValues
    End Function

    Private Function GetPluginString(Name As String, Parameters As String) As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetPluginString", "Fired")
        Dim str As String = String.Empty
        Try
            str = Name & "(" & Parameters & ");"

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return str
    End Function

#End Region


#Region "Selector"

    Public Sub Populate_acSelectorName() 'Needs to be PUBLIC so parent can call when dynamically loaded
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "Populate_acSelectorName", "Fired")
        Try
            Dim respListIsExclussive As Boolean
            'Generate a list of possible values for the combo box
            acSelectorName.DataSource = GetPossibleValuesForjQuerySelectorType(CType(ddlSelectorType.SelectedValue, jQuerySelectorType), respListIsExclussive)
            acSelectorName.DataBind()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Function GetPossibleValuesForjQuerySelectorType(Type As jQuerySelectorType, _
                                                           ByRef respListIsExclussive As Boolean, _
                                                           Optional ByVal blnAddSelectorPrefix As Boolean = False) As List(Of String)
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetPossibleValuesForjQuerySelectorType", "Fired")
        Dim PossibleValues = New List(Of String)
        Try
            respListIsExclussive = False
            Dim strPrefix As String = ""
            Dim CaseType As jQuerySelectorType
            Dim TypeValues() As Integer = CType([Enum].GetValues(GetType(jQuerySelectorType)), Integer())
            For Each TypeValue In TypeValues
                If Type = jQuerySelectorType.ALL Then
                    CaseType = TypeValue
                Else
                    CaseType = Type
                End If
                Select Case CaseType
                    Case jQuerySelectorType.NONE
                        If Type <> jQuerySelectorType.ALL Then PossibleValues.Add("Type is set to 'NONE'")
                    Case jQuerySelectorType.Attribute
                    Case jQuerySelectorType.CssClass
                    Case jQuerySelectorType.Element
                        PossibleValues.Add("a")
                        PossibleValues.Add("button")
                    Case jQuerySelectorType.ID
                    Case jQuerySelectorType.Input
                        If blnAddSelectorPrefix Then strPrefix = "input:"
                        respListIsExclussive = True
                        PossibleValues.Add(strPrefix & "button")
                        PossibleValues.Add(strPrefix & "checkBox")
                        PossibleValues.Add(strPrefix & "file")
                        PossibleValues.Add(strPrefix & "hidden")
                        PossibleValues.Add(strPrefix & "password")
                        PossibleValues.Add(strPrefix & "radio")
                        PossibleValues.Add(strPrefix & "reset")
                        PossibleValues.Add(strPrefix & "submit")
                        PossibleValues.Add(strPrefix & "text")
                End Select
                If Type <> jQuerySelectorType.ALL Then Exit For
            Next
            If Type = jQuerySelectorType.ALL Then respListIsExclussive = False

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return PossibleValues
    End Function

    Private Function GetSelectorString(Type As jQuerySelectorType, Name As String) As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetSelectorString", "Fired")
        Dim str As String = String.Empty
        Try
            Select Case Type
                Case jQuerySelectorType.NONE
                    str = "Type is set to 'NONE'"
                Case jQuerySelectorType.Attribute
                    str = "[" & Name & "]"
                Case jQuerySelectorType.CssClass
                    str = "." & Name
                Case jQuerySelectorType.Element
                    str = Name
                Case jQuerySelectorType.ID
                    str = "#" & Name
                Case jQuerySelectorType.Input
                    str = "input:" & Name
            End Select
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return str
    End Function

#End Region

End Class