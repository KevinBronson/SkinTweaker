Public Class ComboBox
    Inherits SkinTweakerModuleBase

    'HAS NOT BEEN COMPLETED... USING AUTO COMPLETE INSTEAD FOR NOW

#Region "PUBLIC PROPERTIES AND METHODS"

    Public Property DataSource As List(Of String)

    Public Property Text As String
        Get
            Return TextBox1.Text
        End Get
        Set(value As String)
            TextBox1.Text = value
        End Set
    End Property

    Public Overrides Sub DataBind()
        Try
            litScript.Text = "<script type=""text/javascript"">" & GetScript() & "</script>"

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "Event Handlers"

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        'Call DataBind()
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Function GetScript() As String
        Dim strReturn As String = String.Empty
        Dim strList As New List(Of String)
        Try
            'Get the names straight first
            Dim strTextBoxName As String = Me.ID & "-ComboBoxTextBox" : TextBox1.CssClass = strTextBoxName
            Dim strButtonName As String = Me.ID & "-ComboBoxButton" : LinkButton1.CssClass = strButtonName
            strList.Add(vbTab & "$(function () {")
            'Setup DataArray
            strList.Add(vbTab & vbTab & "var DataArray = [" & GetData() & "];")
            'Setup Text Box
            strList.Add(vbTab & vbTab & "$(""." & strTextBoxName & """).autocomplete({ source: DataArray });")
            strList.Add(vbTab & vbTab & "$(""." & strTextBoxName & """).autocomplete(""option"", ""minLength"", 0);") 'Can show list with nothing in textbox
            strList.Add(vbTab & vbTab & "$(""." & strTextBoxName & """).autocomplete(""option"", ""delay"", 0);")
            'strList.Add(vbTab & vbTab & "$(""." & strTextBoxName & """).addClass(""ui-state-default ui-autocomplete-input ui-widget ui-widget-content ui-corner-left"");")
            'Setup Button
            strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).button({icons: {primary: ""ui-icon-triangle-1-s""},text: false});")



            strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).removeClass(""ui-corner-all"");")
            strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).addClass(""ui-button-icon"");")





            'strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).removeClass(""ui-corner-all"");")
            'strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).addClass(""ui-corner-right ui-button-icon"");")

            strList.Add(vbTab & vbTab & "$(""." & strButtonName & """).click(function () {")
            strList.Add(vbTab & vbTab & vbTab & "$(""." & strTextBoxName & """).autocomplete(""search"", """");") 'Shows the whole list
            strList.Add(vbTab & vbTab & vbTab & "$(""." & strTextBoxName & """).focus();")
            strList.Add(vbTab & vbTab & vbTab & "return false;")
            strList.Add(vbTab & vbTab & "});")
            strList.Add(vbTab & "});")
            strReturn = String.Join(vbCrLf, strList.ToArray)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return strReturn
    End Function

    Private Function GetData() As String
        If DataSource Is Nothing Then
            Return String.Empty
        Else
            Dim strDelimiter As String = Chr(34) & ", " & Chr(34)
            Return Chr(34) & String.Join(strDelimiter, DataSource.ToArray) & Chr(34)
        End If
    End Function

#End Region


End Class