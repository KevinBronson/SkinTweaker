Public Class AutoComplete
    Inherits SkinTweakerModuleBase


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

    Public Property CssClass As String
        Get
            Return TextBox1.CssClass
        End Get
        Set(value As String)
            TextBox1.CssClass = value
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
        'Not sure if I'll need this or not
        'Might be better for all binding to be done on load; thus leave that to the calling control
        'Call DataBind()
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Function GetScript() As String
        Dim strReturn As String = String.Empty
        Dim strList As New List(Of String)
        Try
            'Get the name straight first
            Dim strTextBoxName As String = TextBox1.ClientID ': TextBox1.CssClass = strTextBoxName
            'Begin function
            strList.Add(vbTab & "$(function () {")
            'Setup DataArray
            strList.Add(vbTab & vbTab & "var DataArray = [" & GetData() & "];")
            'Setup Text Box
            strList.Add(vbTab & vbTab & "$(""#" & strTextBoxName & """).autocomplete({ source: DataArray });")
            strList.Add(vbTab & vbTab & "$(""#" & strTextBoxName & """).autocomplete(""option"", ""minLength"", 0);") 'Can show list with nothing in textbox
            strList.Add(vbTab & vbTab & "$(""#" & strTextBoxName & """).autocomplete(""option"", ""delay"", 0);")
            'Show whole list on click
            strList.Add(vbTab & vbTab & "$(""#" & strTextBoxName & """).click(function () {")
            strList.Add(vbTab & vbTab & vbTab & "$(""#" & strTextBoxName & """).autocomplete(""search"", """");") 'Shows the whole list
            strList.Add(vbTab & vbTab & vbTab & "$(""#" & strTextBoxName & """).focus();")
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