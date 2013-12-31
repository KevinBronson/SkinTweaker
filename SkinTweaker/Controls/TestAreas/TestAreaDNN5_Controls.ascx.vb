Public Class TestAreaDNN5_Controls
    Inherits System.Web.UI.UserControl


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


End Class