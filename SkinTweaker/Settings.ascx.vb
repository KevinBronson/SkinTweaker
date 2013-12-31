' Copyright (c) 2012 ITM
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports SkinTweaker.Common.Variables
Imports SkinTweaker.Common.InjectorResources

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Settings class manages Module Settings
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
Partial Class Settings
    Inherits SkinTweakerSettingsBase


#Region "Event Handlers"

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Dim strMajor As String = Split(DotNetNukeVersion, ".")(0)
        Select Case strMajor
            Case "6"
                'Don't need to do anything
            Case "5"
                pnlDNN5.Visible = True
            Case "4"
                pnlDNN4.Visible = True
        End Select
    End Sub

#End Region


#Region "Base Method Implementations"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' LoadSettings loads the settings from the Database and displays them
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Overrides Sub LoadSettings()
        Try
            If (Page.IsPostBack = False) Then
                chkDebuggingModeON.Checked = DebuggingModeON(PortalId)
                chkDemoModeON.Checked = DemoModeON(PortalId)
                chkLoggingON.Checked = LoggingON(PortalId)
                chkAddjQuery.Checked = AddjQuery(PortalId)
                txtjQueryVersionNumber.Text = jQueryVersionNumber(PortalId)
                txtjQueryUI_VersionNumber.Text = jQueryUI_VersionNumber(PortalId)

            End If
        Catch exc As Exception           'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' UpdateSettings saves the modified settings to the Database
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Overrides Sub UpdateSettings()
        Try
            DebuggingModeON(PortalId) = chkDebuggingModeON.Checked
            DemoModeON(PortalId) = chkDemoModeON.Checked
            LoggingON(PortalId) = chkLoggingON.Checked
            AddjQuery(PortalId) = chkAddjQuery.Checked
            jQueryVersionNumber(PortalId) = txtjQueryVersionNumber.Text
            jQueryUI_VersionNumber(PortalId) = txtjQueryUI_VersionNumber.Text

        Catch exc As Exception           'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


End Class

