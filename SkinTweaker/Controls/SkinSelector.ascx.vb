Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.UI.Skins
Imports SkinTweaker.Common.Variables
Imports System.IO


Public Class SkinSelector
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase


#Region "PUBLIC PROPERTIES, METHODS AND EVENTS"

    Public Property ToolTip As String
        Get
            Return ddlSkin.ToolTip
        End Get
        Set(value As String)
            ddlSkin.ToolTip = value
        End Set
    End Property

#End Region


#Region "Event Handlers"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Call PopulateSkinsDropDownList()
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            If DemoModeON(PortalId) Then
                ddlSkin.Enabled = False
                ddlSkin.ToolTip = "Not Available in Demo Mode"
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub ddlSkin_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSkin.SelectedIndexChanged
        Call UpdateSkin()
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Sub UpdateSkin()
        Try
            Dim tabController = New TabController()
            Dim tab As TabInfo = tabController.GetTab(PortalSettings.ActiveTab.TabID, PortalId, True)
            If Not IsNothing(tab) Then
                tab.SkinSrc = ddlSkin.SelectedValue
                tabController.UpdateTab(tab)
                PageViewRequest = PageView.SelectTheme
                Response.Redirect(Request.RawUrl, True)
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub PopulateSkinsDropDownList()
        Try
            'Get Host Skins
            Dim strRoot As String = Request.MapPath(DotNetNuke.Common.Globals.HostPath & SkinController.RootSkin)
            Call AddSkinsToDropDownList(strRoot, True)
            'Get Portal Skins
            Dim strPath As String = PortalSettings.HomeDirectoryMapPath & SkinController.RootSkin
            If Directory.Exists(strPath) Then Call AddSkinsToDropDownList(strPath, False)
            'Select Current Skin
            Dim tabController = New TabController()
            Dim tab As TabInfo = tabController.GetTab(PortalSettings.ActiveTab.TabID, PortalId, True)
            If Not IsNothing(tab) Then
                ddlSkin.SelectedValue = tab.SkinSrc
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub AddSkinsToDropDownList(strSkinsRootDirectory As String, IsHostDirectory As Boolean)
        Try
            Dim objRootDir As New DirectoryInfo(strSkinsRootDirectory)
            Dim strSkinType As String
            If IsHostDirectory Then
                strSkinType = "[G]Skins"
            Else
                strSkinType = "[L]Skins"
            End If
            For Each objSkinDir As DirectoryInfo In objRootDir.GetDirectories
                If objSkinDir.Name <> "_default" Then
                    For Each objFile As FileInfo In objSkinDir.GetFiles()
                        If objFile.Extension = ".ascx" Then
                            Dim strSkinName As String = Replace(objFile.FullName, strSkinsRootDirectory & "\", "")
                            strSkinName = Replace(strSkinName, ".ascx", "")
                            strSkinName = Replace(strSkinName, "\", " > ")
                            Dim strSkin As String = Replace(objFile.FullName, strSkinsRootDirectory, strSkinType)
                            strSkin = Replace(strSkin, "\", "/")
                            ddlSkin.Items.Add(New ListItem(strSkinName, strSkin))
                        End If
                    Next
                End If
            Next

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


End Class