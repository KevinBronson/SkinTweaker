Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions


Public Class NavPanel
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase

    Dim objParent As View


#Region "Event Handlers"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'NavPanel's Page Load fires first
        Try
            If DemoModeON(PortalId) Then
                lblDemoModeMessage.Visible = True
            End If

            If PageViewRequest <> PageView.NONE Then
                objParent = CType(Parent, View)
                objParent.InternalMultiView.ActiveViewIndex = PageViewRequest
                CurrentPageView = PageViewRequest
                PageViewRequest = PageView.NONE 'Reset Flag
            End If

            lblDotNetNukeVersion.Text = "DotNetNuke Version: " & DotNetNukeVersion
            lblSkinTweakerVersion.Text = "SkinTweaker Version: " & SkinTweakerVersion

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)

        End Try
    End Sub

#End Region

End Class