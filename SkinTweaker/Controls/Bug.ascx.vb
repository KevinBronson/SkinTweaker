Imports SkinTweaker.Common.Variables


Public Class Bug
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase

    'ID: Use the built in ID for the control
    Public Property Name As String
    Public Property Description As String = String.Empty
    'Status Info
    Public Property Status As BugStatusType = BugStatusType.Unknown
    Public Property Notes As String = String.Empty
    'Kill Info
    Public Property KillWorksForDnnVersions As List(Of String)
    Public Property AffectedFilePath As New List(Of String)
    Public Property KillType As BugKillType = BugKillType.NONE
    Public Property IdentifyingText As String = String.Empty
    Public Property ErrorText As String = String.Empty
    Public Property ReplacementText As String = String.Empty
    Public Property AppendText As String = String.Empty
    Public Property NormalizeCarriageReturns As Boolean = False
    'Stamping Info
    Public Property UseStamps As Boolean = False
    Public Property CustomKillStamp As String = String.Empty
    Public Property CustomRestoreStamp As String = String.Empty


#Region "Event Handlers"

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If DemoModeON(PortalId) Then
            lbKillOrRestore.Enabled = False
        End If
    End Sub

    Private Sub lbKillOrRestore_Click(sender As Object, e As System.EventArgs) Handles lbKillOrRestore.Click
        Try
            Dim objParent As SkinTweaker.BugHunter = CType(Parent.Parent.Parent.Parent, SkinTweaker.BugHunter) 'bh.mv.vw.ph.me
            Select Case lblStatus.Text
                Case "Alive"
                    objParent.Kill(Me, objParent.CreateBackups)
                    PageViewRequest = PageView.BugHunter 'Used as a flag to refresh status
                    Response.Redirect(Request.RawUrl)
                Case "Dead"
                    objParent.Restore(Me, objParent.CreateBackups)
                    PageViewRequest = PageView.BugHunter 'Used as a flag to refresh status
                    Response.Redirect(Request.RawUrl)
            End Select

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Public Sub UpdateDisplayValues()
        Try
            lblID.Text = CStr(Me.ID)
            lblID.HelpText = "Description: " & Me.Description
            lblName.Text = Me.Name
            If Split(DotNetNukeVersion, ".")(0) <> "6" Then
                lblName.ToolTip = lblName.Text & " " & lblID.HelpText 'Need to use ToolTip for DNN 5, because DNN 5's HelpText sucks
            End If
            lblStatus.Text = [Enum].GetName(GetType(BugStatusType), Me.Status)
            lblNotes.Text = Me.Notes
            Select Case Me.Status
                Case BugStatusType.Alive
                    Call ShowKillCommand()
                Case BugStatusType.Dead
                    Call ShowRestoreCommand()
                Case BugStatusType.NotVersion
                    Call HideKillRestoreCommand()
                Case BugStatusType.NotFound
                    Call HideKillRestoreCommand()
                Case BugStatusType.Unknown
                    Call HideKillRestoreCommand()
            End Select

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub ShowKillCommand()
        lbKillOrRestore.Text = "Kill"
        lbKillOrRestore.ToolTip = "Apply a fix to " & Me.Name & "."
        lbKillOrRestore.Visible = True
    End Sub

    Private Sub ShowRestoreCommand()
        lbKillOrRestore.Text = "Restore"
        lbKillOrRestore.ToolTip = "Undo the fix that has been applied to " & Me.Name & "."
        lbKillOrRestore.Visible = True
    End Sub

    Private Sub HideKillRestoreCommand()
        lbKillOrRestore.Text = ""
        lbKillOrRestore.Visible = False
    End Sub

#End Region


End Class