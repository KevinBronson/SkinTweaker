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

    ''' -----------------------------------------------------------------------------
    ''' <summary>
''' The ViewSkinTweaker class displays the content
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' </history>
''' -----------------------------------------------------------------------------

Partial Class Introduction
    Inherits SkinTweakerModuleBase
    'Implements IActionable

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

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
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

