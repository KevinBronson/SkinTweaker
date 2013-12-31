Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports System.Xml


Public Class Options
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase


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
        If DemoModeON(PortalId) Then
            For Each cntrl As System.Web.UI.Control In Me.Controls()
                If TypeOf cntrl Is CheckBox Then CType(cntrl, CheckBox).Enabled = False
            Next
        End If
        Call SetupHelpTextAndToolTips()
    End Sub

#End Region


#Region "PUBLIC PROPERTIES"

    Public Property XML As String
        Get
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "XML.Get", "Fired")
            'Use this to CREATE XML
            Dim strReturn As String = String.Empty
            Try
                Dim strList As New List(Of String)
                strList.Add("<Options>")
                strList.Add("<ChangeAllButtons>" & CStr(chkChangeAllButtons.Checked) & "</ChangeAllButtons>")
                strList.Add("<FillInCalendars>" & CStr(chkFillInCalendars.Checked) & "</FillInCalendars>")
                strList.Add("<FillInBoxes>" & CStr(chkFillInBoxes.Checked) & "</FillInBoxes>")
                strList.Add("<AutoCompleteFix>" & CStr(chkAutoCompleteFix.Checked) & "</AutoCompleteFix>")
                '.... more to come
                strList.Add("</Options>")
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
                    'If a node isn't there, no big deal, just drive on. Sometimes not there because option is new; no problem.
                    'ChangeAllButtons
                    Dim node As XmlNode = XMLDoc.SelectSingleNode("jQueryScriptConfig/Options/ChangeAllButtons")
                    If Not node Is Nothing Then
                        chkChangeAllButtons.Checked = CBool(node.InnerText)
                    End If
                    'FillInCalendars
                    node = XMLDoc.SelectSingleNode("jQueryScriptConfig/Options/FillInCalendars")
                    If Not node Is Nothing Then
                        chkFillInCalendars.Checked = CBool(node.InnerText)
                    End If
                    'FillInBoxes
                    node = XMLDoc.SelectSingleNode("jQueryScriptConfig/Options/FillInBoxes")
                    If Not node Is Nothing Then
                        chkFillInBoxes.Checked = CBool(node.InnerText)
                    End If
                    'AutoCompleteFix
                    node = XMLDoc.SelectSingleNode("jQueryScriptConfig/Options/AutoCompleteFix")
                    If Not node Is Nothing Then
                        chkAutoCompleteFix.Checked = CBool(node.InnerText)
                    End If
                Else
                    SetMessage(Me, "Could not read Options Node", MessageType.ErrorMessage)
                End If

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Set
    End Property

    Public ReadOnly Property GetjQueryStatments As String
        Get
            Dim strReturn As String = String.Empty
            Dim strList As New List(Of String)
            Try
                If chkChangeAllButtons.Checked Then strList.Add(GetStatementsForChangeAllButtons)
                If chkFillInCalendars.Checked Then strList.Add(GetStatementForFillInCalendars)
                If chkFillInBoxes.Checked Then strList.Add(GetStatementForFillInBoxes)
                If chkAutoCompleteFix.Checked Then strList.Add(GetStatementForAutoCompleteFix)

                '.... add more options later ...

                If strList.Count > 0 Then strReturn = String.Join(vbCrLf, strList.ToArray)

            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
            If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetjQueryStatments.Get", "Fired, strReturn='" & strReturn & "'")
            Return strReturn
        End Get
    End Property

#End Region


#Region "PRIVATE SUBS"

    Private Function GetStatementsForChangeAllButtons() As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetStatementsForChangeAllButtons", "Fired")
        Dim strList As New List(Of String)
        Try
            strList.Add(vbTab & vbTab & "$(""button"").button();")
            strList.Add(vbTab & vbTab & "$(""input:button"").button();")
            strList.Add(vbTab & vbTab & "$(""input:reset"").button();")
            strList.Add(vbTab & vbTab & "$(""input:submit"").button();")
            strList.Add(vbTab & vbTab & "$('[id*=""LinkButton""]').button();")

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return String.Join(vbCrLf, strList.ToArray)
    End Function

    Private Function GetStatementForFillInCalendars() As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetStatementForFillInCalendars", "Fired")
        '$(window).bind("load", function () {$("#ui-datepicker-div").wrap('<div class="SkinTweaker"></div>');});
        Try
            Return "$(window).bind(""load"", function () {$(""#ui-datepicker-div"").wrap('<div class=""" & CSS_ScopeName & """></div>');});"

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
            Return String.Empty
        End Try
    End Function

    Private Function GetStatementForFillInBoxes() As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetStatementForFillInBoxes", "Fired")
        '$(window).bind("load", function () {$(".ui-dialog").not(".dnnFormPopup").wrap('<div class="SkinTweaker"></div>');});
        Try
            Return "$(window).bind(""load"", function () {$("".ui-dialog"").not("".dnnFormPopup"").wrap('<div class=""" & CSS_ScopeName & """></div>');});"

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
            Return String.Empty
        End Try
    End Function

    Private Function GetStatementForAutoCompleteFix() As String
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "GetStatementForAutoCompleteFix", "Fired")
        '$(window).bind("load", function (){$(".ui-autocomplete").not('[class~="dnnForm"]').wrap('<div class="SkinTweaker"></div>');});
        Try
            Return "$(window).bind(""load"", function () {$("".ui-autocomplete"").not('[class~=""dnnForm""]').wrap('<div class=""" & CSS_ScopeName & """></div>');});"

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
            Return String.Empty
        End Try
    End Function

    Private Sub SetupHelpTextAndToolTips()
        If LoggingON(PortalId) Then AppendjQueryScriptEditorLog(Me, "SetupHelpTextAndToolTips", "Fired")
        If Split(DotNetNukeVersion, ".")(0) <> "6" Then
            'Use the text from HelpText since the help text feature in DNN 5 sucks and the tool tip works better
            chkAutoCompleteFix.ToolTip = lblAutoCompleteFix.HelpText
            chkChangeAllButtons.ToolTip = lblChangeAllButtons.HelpText
            chkFillInBoxes.ToolTip = lblFillInBoxes.HelpText
            chkFillInCalendars.ToolTip = lblFillInCalendars.HelpText
        End If
    End Sub

#End Region


End Class