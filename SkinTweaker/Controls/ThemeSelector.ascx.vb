Imports System.IO
Imports DotNetNuke.Entities.Modules
Imports SkinTweaker.Common.InjectorResources
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions


Public Class ThemeSelector
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    Inherits SkinTweakerModuleBase
    'Inherits UserControlBase


#Region "PUBLIC PROPERTIES"

    Public ReadOnly Property ApplyCssScope As Boolean
        Get
            Return CBool(ddlApplyCssScopeToBody.SelectedValue)
        End Get
    End Property

#End Region


#Region "Event Handlers"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Call PopulateThemesDropDownList()
                ddlApplyCssScopeToBody.SelectedValue = PortalApplyCssScopeToPage(PortalId)
                ddlScriptSelector.SelectedValue = PortaljQueryScriptName(PortalId)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)

        End Try
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            Call ApplyThemeToTestAreaONLY()
            Call SetupHelpTextAndToolTips()
            If CBool(ddlApplyCssScopeToBody.SelectedValue) Then Call ApplyScopeToEntireTestPage()
            lbApplyThemeNow.OnClientClick = _
                "return confirm('This will apply the selected theme to the entire website. \nBacking up the website files first is highly recommended. \n\nClick OK to continue.');"
            If DemoModeON(PortalId) Then
                lbApplyThemeNow.Enabled = False
                lbApplyThemeNow.ToolTip = "Not Available in Demo Mode"
                lbApplyThemeNow.CssClass = "lbApplyThemeNow Disabled"
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbApplyThemeNow_Click(sender As Object, e As System.EventArgs) Handles lbApplyThemeNow.Click
        Try
            Call ApplyThemeToEntireWebSite()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "ENTIRE WEBSITE"

    Private Sub ApplyThemeToEntireWebSite()
        Try
            Dim strErrorMessage As String = String.Empty
            'CSS
            Dim strThemeNames As String = ddlThemes.SelectedValue
            PortalCssThemeName(PortalId) = strThemeNames
            If strThemeNames = NoThemeName Then
                PortalThemeCssFilePath(PortalId) = String.Empty
            Else
                PortalThemeCssFilePath(PortalId) = GetThemeCssFileRelativePath(strThemeNames, PortalId, strErrorMessage)
            End If
            'jQuery Script
            Dim strjQueryScriptName As String = ddlScriptSelector.SelectedValue
            PortaljQueryScriptName(PortalId) = strjQueryScriptName
            If strjQueryScriptName = NoScriptName Then
                PortalThemeSciptFilePath(PortalId) = String.Empty
            Else
                PortalThemeSciptFilePath(PortalId) = ThemesjQueryDirectoryRelativePath & "/" & strjQueryScriptName & ".js"
            End If
            'Page Scope
            PortalApplyCssScopeToPage(PortalId) = CBool(ddlApplyCssScopeToBody.SelectedValue)
            If strErrorMessage = String.Empty Then
                SetMessage(Me, "Settings updated successfully.", MessageType.Success)
            Else
                SetMessage(Me, strErrorMessage, MessageType.Warning)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "TEST AREA ONLY"

    Private Sub ApplyThemeToTestAreaONLY()
        Try
            If ddlThemes.SelectedValue <> NoThemeName Then Call RegisterStyleSheetForThisPageONLY(ddlThemes.SelectedValue)
            If ddlScriptSelector.SelectedValue <> NoScriptName Then Call RegisterjQueryScriptForThisPageONLY(ddlScriptSelector.SelectedValue)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub ApplyScopeToEntireTestPage()
        RegisterScript(Page, ThemesApplyToPageFilePath)
    End Sub

    Private Sub RegisterStyleSheetForThisPageONLY(strThemeName As String)
        Try
            Dim ErrorMessage As String = String.Empty
            RegisterStyleSheet(Page, GetThemeCssFileRelativePath(strThemeName, PortalId, ErrorMessage))

            If ErrorMessage <> String.Empty Then
                Throw New Exception(ErrorMessage)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub RegisterjQueryScriptForThisPageONLY(strScriptName As String)
        Try
            Dim ErrorMessage As String = String.Empty
            RegisterScript(Page, ThemesjQueryDirectoryRelativePath & "/" & strScriptName & ".js")

            If ErrorMessage <> String.Empty Then
                Throw New Exception(ErrorMessage)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub PopulateThemesDropDownList()
        Try
            Dim objDir As New DirectoryInfo(ThemesCssDirectoryPhysicalPath)
            Dim objItemNone As New ListItem

            objItemNone.Value = NoThemeName
            objItemNone.Text = NoThemeName
            ddlThemes.Items.Add(objItemNone)

            For Each objSubFolder As DirectoryInfo In objDir.GetDirectories
                Dim objItem As New ListItem
                objItem.Value = objSubFolder.Name
                objItem.Text = objSubFolder.Name
                ddlThemes.Items.Add(objItem)
            Next

            ddlThemes.SelectedValue = PortalCssThemeName(PortalId)

        Catch exc As Exception
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub SetupHelpTextAndToolTips()
        If Split(DotNetNukeVersion, ".")(0) <> "6" Then
            'Use the text from HelpText since the help text feature in DNN 5 sucks and the tool tip works better
            ddlThemes.ToolTip = lblThemes.HelpText
            ddlScriptSelector.ToolTip = lbljQueryScripts.HelpText
            ddlApplyCssScopeToBody.ToolTip = lblApplyCssScopeToPage.HelpText
        End If
    End Sub

#End Region


End Class