Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions

Partial Class AdvancedSettings
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase

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