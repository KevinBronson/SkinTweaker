Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports SkinTweaker.Common.InjectorResources

Public Class ThemeSelection
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase


#Region "Event Handlers"

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            Call RefreshPortalSettingsDisplay()
            Call SetupHelpTextAndToolTips()
            Call pnlTestControlsChangeVisibility(CInt(ddlTestControls.SelectedValue) = 0)
            Select Case CInt(ddlTestControls.SelectedValue)
                Case 1
                    pnlTestControls1.Visible = True
                Case 2
                    pnlTestControls2.Visible = True
                Case 3
                    pnlTestControls3.Visible = True
                Case 99
                    Select Case Split(DotNetNukeVersion, ".")(0) 'Get DNN Major Version Number
                        Case "4"
                            pnlTestControls4.Visible = True
                        Case "5"
                            pnlTestControls5.Visible = True
                        Case "6"
                            pnlTestControls6.Visible = True
                    End Select
            End Select

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "PUBLIC FUNCTIONS"

    Private Sub pnlTestControlsChangeVisibility(AllVisible As Boolean)
        pnlTestControls1.Visible = AllVisible
        pnlTestControls2.Visible = AllVisible
        pnlTestControls3.Visible = AllVisible
        If AllVisible Then
            Select Case Split(DotNetNukeVersion, ".")(0)
                Case "4"
                    pnlTestControls4.Visible = AllVisible
                Case "5"
                    pnlTestControls5.Visible = AllVisible
                Case "6"
                    pnlTestControls6.Visible = AllVisible
            End Select
        Else
            pnlTestControls4.Visible = False
            pnlTestControls5.Visible = False
            pnlTestControls6.Visible = False
        End If
    End Sub

    Private Sub RefreshPortalSettingsDisplay()
        lblPortalCssThemeName.Text = PortalCssThemeName(PortalId)
        lblPortaljQueryScriptName.Text = PortaljQueryScriptName(PortalId)
        lblPortalApplyCssScopeToPage.Text = PortalApplyCssScopeToPage(PortalId)
    End Sub

    Private Sub SetupHelpTextAndToolTips()
        If Split(DotNetNukeVersion, ".")(0) <> "6" Then
            'Use the text from HelpText since the help text feature in DNN 5 sucks and the tool tip works better
            ddlTestControls.ToolTip = lblTestControls.HelpText
            ddlSkinSelector.ToolTip = lblSkinSelector.HelpText
        End If
    End Sub

#End Region


End Class