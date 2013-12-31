
Public Class TestAreajQueryUIControls
    'Inherits Framework.UserControlBase
    Inherits System.Web.UI.UserControl
    'Inherits SkinTweakerModuleBase
    'Inherits UserControlBase

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)

        End Try
    End Sub

    Protected Friend Function GetScopeClass() As String
        Try
            If chkApplyScope.Checked Then
                Return SkinTweaker.Common.Variables.CSS_ScopeName
            Else
                Return String.Empty
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
            Return String.Empty
        End Try
    End Function

    Protected Friend Function GetButtons() As String
        Try
            Dim intTotalButtons As Integer = 173
            Dim i As Integer = 0
            Dim arrButtons As String()
            ReDim arrButtons(intTotalButtons)
            For i = 0 To intTotalButtons - 1
                arrButtons(i) = "<li id=""button" & CStr(i) & """ >&nbsp;</li>"
            Next
            Return Join(arrButtons, vbCrLf)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)

        End Try
        Return String.Empty
    End Function

End Class