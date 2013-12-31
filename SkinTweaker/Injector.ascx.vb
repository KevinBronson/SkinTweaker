Imports SkinTweaker.Common.InjectorResources

'This needs to be VERY clean and efficient since it will run EVERY time a page is loaded
' So do NOT implement any DNN interfaces in here; just implement the settings and split!

'   DO NOT USE COOKIES IN HERE!!!

Public Class Injector
    Inherits SkinTweakerModuleBase

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Call ImplementSettings()
    End Sub

    Private Sub ImplementSettings()
        Try
            'Add jQuery
            If AddjQuery(PortalId) Then
                InjectjQueryLibary(Page, True, True, jQueryVersionNumber(PortalId), jQueryUI_VersionNumber(PortalId))
            End If
            'CSS File
            Dim strCssFilePath As String = PortalThemeCssFilePath(PortalId)
            If strCssFilePath <> String.Empty Then
                RegisterStyleSheet(Page, strCssFilePath)
            End If
            'Script File
            Dim strjQueryScriptName As String = PortalThemeSciptFilePath(PortalId)
            If strjQueryScriptName <> String.Empty Then
                RegisterScript(Page, strjQueryScriptName)
            End If
            'Page Scope
            If PortalApplyCssScopeToPage(PortalId) Then
                RegisterScript(Page, ThemesApplyToPageFilePath)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

End Class