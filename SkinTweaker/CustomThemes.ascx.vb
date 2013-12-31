Imports SkinTweaker.Common.Variables

Public Class CustomThemes
    Inherits SkinTweakerModuleBase

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            If DemoModeON(PortalId) Then
                MultiView1.ActiveViewIndex = 0
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

End Class