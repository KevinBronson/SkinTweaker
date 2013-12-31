' Copyright (c) 2012 ITM
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 

Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Globals
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.Functions
Imports SkinTweaker.Common.InjectorResources

''' -----------------------------------------------------------------------------
''' <summary>
''' The ViewSkinTweaker class displays the content
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' </history>
''' -----------------------------------------------------------------------------

Partial Class View
    Inherits SkinTweakerModuleBase
    'Implements IActionable


#Region "PUBLIC PROPERTIES"

    Public ReadOnly Property InternalMultiView As MultiView
        Get
            Return MultiView1
        End Get
    End Property

#End Region


#Region "PUBLIC METHODS"

    Public Sub SetMessage(strMessage As String, MessageType As MessageType)
        Select Case MessageType
            Case MessageType.ErrorMessage
                lblError.Text = strMessage
            Case MessageType.Info
                lblInfo.Text = strMessage
            Case MessageType.Success
                lblSuccess.Text = strMessage
            Case MessageType.Warning
                lblWarning.Text = strMessage
        End Select
    End Sub

#End Region


#Region "Event Handlers"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Page_Load runs when the control is loaded
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    '''  -----------------------------------------------------------------------------

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Add jQuery & jQuery UI
            If AddjQuery(PortalId) Then
                InjectjQueryLibary(Page, True, True, jQueryVersionNumber(PortalId), jQueryUI_VersionNumber(PortalId))
            End If
            'Register Module Script
            RegisterScript(Page, "/DesktopModules/" & DNN_ModuleName & "/module.js")

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        'Add theme via body tag to every page except for the theme seletion page; too much interference
        If Not (CurrentPageView = PageView.SelectTheme And PageViewRequest = PageView.NONE) Then
            If PageViewRequest <> PageView.SelectTheme Then
                Call ImpelementModuleThemeForThisPage(Page)
            End If
        End If
        'If there is a message, then display it
        lblError.Visible = CBool(lblError.Text <> "")
        lblInfo.Visible = CBool(lblInfo.Text <> "")
        lblSuccess.Visible = CBool(lblSuccess.Text <> "")
        lblWarning.Visible = CBool(lblWarning.Text <> "")
    End Sub

#End Region


#Region "Optional Interfaces"

    ' ''' -----------------------------------------------------------------------------
    ' ''' <summary>
    ' ''' Registers the module actions required for interfacing with the portal framework
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' </history>
    ' ''' -----------------------------------------------------------------------------
    'Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
    '    Get
    '        Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
    '        Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
    '        Return Actions
    '    End Get
    'End Property

#End Region


End Class

