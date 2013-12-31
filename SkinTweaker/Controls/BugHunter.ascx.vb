Imports System.IO
Imports SkinTweaker.Common.Functions
Imports SkinTweaker.Common.Variables
Imports System.Globalization


Public Class BugHunter
    'Inherits Framework.UserControlBase
    'Inherits System.Web.UI.UserControl
    'Inherits UserControlBase
    Inherits SkinTweakerModuleBase

    Private lstBugs As New List(Of SkinTweaker.Bug)


#Region "PUBLIC PROPERTIES"

    Public ReadOnly Property CreateBackups As Boolean
        Get
            Return chkCreateBackups.Checked
        End Get
    End Property

#End Region


#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try
            If CurrentPageView = PageView.BugHunter Or PageViewRequest = PageView.BugHunter Then
                'Populate bugs in case they are needed; this is the only time to do it so it works, but do not get status or display
                lstBugs = GetBugs()
                If PageViewRequest = PageView.BugHunter Then
                    'Use PageViewRequest as a flag for refreshing the status
                    Call CheckStatusForAll(lstBugs)
                    For Each Bug In lstBugs
                        Bug.UpdateDisplayValues()
                    Next
                End If
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            If DemoModeON(PortalId) Then
                'MultiView1.ActiveViewIndex = 0
                lblTitle.Text = lblTitle.Text & " - DEMO MODE"
                lbKillBugs.Enabled = False
                lbRefresh.Enabled = False
                lbRestoreBugs.Enabled = False
                lbRestoreSystem.Enabled = False
                hlMailTo.Enabled = False
            End If
            Call SetupHelpTextAndToolTips()

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbKillBugs_Click(sender As Object, e As System.EventArgs) Handles lbKillBugs.Click
        Try
            If chkCreateBackups.Checked Then BackupFiles(lstBugs)
            KillAll(lstBugs)
            Call lbRefresh_Click(sender, e)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbRefresh_Click(sender As Object, e As System.EventArgs) Handles lbRefresh.Click
        Try
            PageViewRequest = PageView.BugHunter
            Response.Redirect(Request.RawUrl) 'Used as a flag to refresh the status display

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbRestoreBugs_Click(sender As Object, e As System.EventArgs) Handles lbRestoreBugs.Click
        Try
            If chkCreateBackups.Checked Then BackupFiles(lstBugs)
            RestoreAll(lstBugs)
            Call lbRefresh_Click(sender, e)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub lbRestoreSystem_Click(sender As Object, e As System.EventArgs) Handles lbRestoreSystem.Click
        Try
            Call RestoreSystem(lstBugs)
            Call lbRefresh_Click(sender, e)

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "AGGREGATE BUGS SUBS"

    Private Sub BackupFiles(Bugs As List(Of SkinTweaker.Bug))
        Try
            'Loop through the bugs and make a list of affected files
            Dim lstFilePaths As New List(Of String)
            Dim strFilePath As String = String.Empty
            For Each Bug As SkinTweaker.Bug In Bugs
                For Each strFilePath In Bug.AffectedFilePath
                    'Make sure there are no duplicates
                    If Not lstFilePaths.Contains(strFilePath) Then lstFilePaths.Add(strFilePath)
                Next
            Next
            'Do the backups
            Dim respErrorMessage As String = String.Empty
            For Each strFilePath In lstFilePaths
                If Not CreateBackUpCopyOf(strFilePath, respErrorMessage) Then
                    SetMessage(Me, respErrorMessage, MessageType.Warning)
                End If
            Next

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub KillAll(bugs As List(Of Bug))
        Dim respErrorMessage As String = String.Empty
        For Each Bug As SkinTweaker.Bug In bugs
            If Not Kill(Bug, False, respErrorMessage) Then
                SetMessage(Me, respErrorMessage, MessageType.Warning)
            End If
        Next
    End Sub

    Private Sub RestoreAll(bugs As List(Of Bug))
        Dim respErrorMessage As String = String.Empty
        For Each Bug As SkinTweaker.Bug In bugs
            If Not Restore(Bug, False, respErrorMessage) Then
                SetMessage(Me, respErrorMessage, MessageType.Warning)
            End If
        Next
    End Sub

    Private Sub CheckStatusForAll(bugs As List(Of Bug))
        Dim respErrorMessage As String = String.Empty
        For Each Bug As SkinTweaker.Bug In bugs
            If Not CheckStatus(Bug, respErrorMessage) Then
                SetMessage(Me, respErrorMessage, MessageType.Warning)
            End If
        Next
    End Sub

    Private Sub RestoreSystem(bugs As List(Of Bug))
        Try
            '1.) List and Stamp Files
            '2.) Backup Files
            '3.) Restore File Contents
            '-------------------------
            'Loop through the bugs and make a list of affected files and stamp them
            Dim lstFilePaths As New List(Of String)
            Dim strFilePath As String = String.Empty
            '1.) List and Stamp Files
            For Each Bug As SkinTweaker.Bug In bugs
                For Each strFilePath In Bug.AffectedFilePath
                    'Make sure there are no duplicates
                    If Not lstFilePaths.Contains(strFilePath) Then
                        lstFilePaths.Add(strFilePath)
                        StampFile(Bug, strFilePath, "-- RESTORE SYSTEM --")
                    End If
                Next
            Next
            '2.) Backup Files
            Call BackupFiles(lstBugs)
            '3.) Restore File Contents to each file
            Dim respErrorMessage As String = String.Empty
            Dim strOriginalContents As String
            For Each strFilePath In lstFilePaths
                strOriginalContents = GetTextFileContents(strFilePath & OriginalFileNameExtender, respErrorMessage)
                If Not respErrorMessage = String.Empty Then
                    SetMessage(Me, respErrorMessage, MessageType.Warning)
                End If
                If SaveTextToFile(strOriginalContents, strFilePath, respErrorMessage) Then
                    SetMessage(Me, respErrorMessage, MessageType.Warning)
                End If
            Next

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "SINGULAR BUG SUBS"

    'KILL
    Public Function Kill(bug As SkinTweaker.Bug, blnBackupFiles As Boolean, Optional ByRef respErrorMessage As String = "") As Boolean
        'For each file in list:
        '1. Check if bug kill will work with this version of DNN
        '2. Access File
        '3. Identify Bug: Make sure the bug is there
        '4. Kill Bug & Append Stamp to File
        Dim blnReturn As Boolean = False 'Do NOT do a return inside the for loop...
        Dim strContents As String = String.Empty
        Try
            '1. Check if bug kill will work with this version of DNN
            If IsForThisDnnVersion(bug) Then
                If blnBackupFiles Then Call BackupFiles(New List(Of Bug) From {bug})
                For Each strFilePath As String In bug.AffectedFilePath
                    '2. Access File
                    strContents = GetTextFileContents(strFilePath, respErrorMessage)
                    If bug.NormalizeCarriageReturns Then strContents = NormalizeCarriageReturns(strContents)
                    If respErrorMessage = String.Empty Then
                        '3. Identify Bug: Make sure the bug is there (doesn't work for appends)
                        If CBool(InStr(strContents, bug.IdentifyingText)) Then
                            '4. Kill Bug & Stamp File
                            Select Case bug.KillType
                                Case BugKillType.AppendText
                                    blnReturn = (KillVia_AppendText(bug, strContents, strFilePath, respErrorMessage))

                                Case BugKillType.StringReplacement
                                    blnReturn = KillVia_StringReplacement(bug, strContents, strFilePath, respErrorMessage)

                                Case BugKillType.BothReplaceAndAppend
                                    blnReturn = (KillVia_AppendText(bug, strContents, strFilePath, respErrorMessage) And _
                                            KillVia_StringReplacement(bug, strContents, strFilePath, respErrorMessage))

                                Case BugKillType.NONE
                            End Select
                        Else
                            bug.Status = BugStatusType.NotFound
                            StampFile(bug, strFilePath, "tried to kill but couldn't find")
                        End If
                    Else
                        bug.Status = BugStatusType.Unknown
                    End If
                    'Logging
                    If LoggingON(PortalId) Then
                        Dim lstLogEntry As New List(Of String)
                        lstLogEntry.Add("==================================================================================")
                        lstLogEntry.Add("Entry Made: " & Now)
                        lstLogEntry.Add("Bug ID " & bug.ID)
                        lstLogEntry.Add("Function: Kill")
                        lstLogEntry.Add("bug.AffectedFilePath.Count = " & bug.AffectedFilePath.Count)
                        lstLogEntry.Add("Accessed file: " & strFilePath)
                        lstLogEntry.Add("bug.KillType = " & [Enum].GetName(GetType(BugKillType), bug.KillType))
                        lstLogEntry.Add("respErrorMessage from KillVia_ function = '" & respErrorMessage & "'")
                        lstLogEntry.Add("bug.Status after KillVia_ function = " & [Enum].GetName(GetType(BugStatusType), bug.Status))
                        lstLogEntry.Add("bug.Notes = '" & bug.Notes & "'")
                        lstLogEntry.Add("chkCreateBackups.Checked=" & CStr(chkCreateBackups.Checked))
                        Call AppendTextToFile(String.Join(vbCrLf, lstLogEntry.ToArray), LogsDirectoryPhysicalPath & "BugHunter.log")
                    End If
                Next
            Else
                bug.Notes = "Not for DotNetNuke Version " & DotNetNukeVersion
                bug.Status = BugStatusType.Unknown
                blnReturn = True 'There is no problem, this is just for a different version
            End If
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return blnReturn
    End Function

    Private Function KillVia_AppendText(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "") As Boolean
        'Make sure was not already appended before
        If Not CBool(InStr(strContents, bug.AppendText)) Then
            If AppendTextToFile(bug.AppendText, strFilePath, respErrorMessage) Then
                bug.Status = BugStatusType.Dead
                StampFile(bug, strFilePath, "killed")
                Return True
            Else
                bug.Status = BugStatusType.Unknown
                StampFile(bug, strFilePath, "tried to kill")
                Return False
            End If
        Else
            bug.Status = BugStatusType.Unknown
            StampFile(bug, strFilePath, "tried to kill, but already dead,")
            Return False
        End If
    End Function

    Private Function KillVia_StringReplacement(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "") As Boolean
        'Sometimes the Error Text is a sub string of the ReplacementText, so don't look for error string
        'Make sure not already dead
        If Not CBool(InStr(strContents, bug.ReplacementText)) Then
            strContents = Replace(strContents, bug.ErrorText, bug.ReplacementText)
            If SaveTextToFile(strContents, strFilePath, respErrorMessage) Then
                bug.Status = BugStatusType.Dead
                StampFile(bug, strFilePath, "killed")
                Return True
            Else
                bug.Status = BugStatusType.Unknown
                StampFile(bug, strFilePath, "tried to kill")
                Return False
            End If
        End If
        Return False
    End Function


    'RESTORE
    Public Function Restore(bug As SkinTweaker.Bug, blnBackupFiles As Boolean, Optional ByRef respErrorMessage As String = "") As Boolean
        'For each file in list:
        '1. Check if bug kill will work with this version of DNN
        '2. Access File
        '3. Identify Bug Fix  - use fixes (ReplacementText and AppendText) NOT bug id text
        '4. Restore Bug & Append Stamp to File
        Dim blnReturn As Boolean = False 'Do NOT do a return inside the for loop...
        Dim strContents As String = String.Empty
        Try
            '1. Check if bug kill will work with this version of DNN
            If IsForThisDnnVersion(bug) Then
                If blnBackupFiles Then Call BackupFiles(New List(Of Bug) From {bug})
                For Each strFilePath As String In bug.AffectedFilePath
                    '2. Access File
                    strContents = GetTextFileContents(strFilePath, respErrorMessage)
                    If bug.NormalizeCarriageReturns Then strContents = NormalizeCarriageReturns(strContents)
                    If respErrorMessage = String.Empty Then
                        '3. Identify Bug Fix
                        If (CBool(InStr(strContents, bug.ReplacementText)) And bug.KillType = BugKillType.StringReplacement) Or _
                           (CBool(InStr(strContents, bug.AppendText) And bug.KillType = BugKillType.AppendText)) Then
                            '4. Restore Bug & Stamp File
                            Select Case bug.KillType
                                Case BugKillType.AppendText
                                    blnReturn = RestoreFrom_AppendText(bug, strContents, strFilePath, respErrorMessage)

                                Case BugKillType.StringReplacement
                                    blnReturn = RestoreFrom_StringReplacement(bug, strContents, strFilePath, respErrorMessage)

                                Case BugKillType.BothReplaceAndAppend
                                    blnReturn = (RestoreFrom_AppendText(bug, strContents, strFilePath, respErrorMessage) And _
                                            RestoreFrom_StringReplacement(bug, strContents, strFilePath, respErrorMessage))

                                Case BugKillType.NONE
                            End Select
                            'Check error message just incase one of the functions above returned true and an error message by mistake.
                            If respErrorMessage = String.Empty Then
                                bug.Status = BugStatusType.Dead
                                StampFile(bug, strFilePath, "Restored")
                                blnReturn = True
                            Else
                                bug.Status = BugStatusType.Unknown
                                StampFile(bug, strFilePath, "tried to Restore")
                            End If
                        Else
                            StampFile(bug, strFilePath, "tried to Restore but couldn't find")
                            bug.Status = BugStatusType.Unknown
                        End If
                        bug.Status = BugStatusType.Unknown
                    End If
                    'Logging
                    If LoggingON(PortalId) Then
                        Dim lstLogEntry As New List(Of String)
                        lstLogEntry.Add("==================================================================================")
                        lstLogEntry.Add("Entry Made: " & Now)
                        lstLogEntry.Add("Bug ID " & bug.ID)
                        lstLogEntry.Add("Function: Restore")
                        lstLogEntry.Add("bug.AffectedFilePath.Count = " & bug.AffectedFilePath.Count)
                        lstLogEntry.Add("Accessed file: " & strFilePath)
                        lstLogEntry.Add("bug.KillType = " & [Enum].GetName(GetType(BugKillType), bug.KillType))
                        lstLogEntry.Add("respErrorMessage from RestoreFrom_ function = '" & respErrorMessage & "'")
                        lstLogEntry.Add("bug.Status after RestoreFrom_ function = " & [Enum].GetName(GetType(BugStatusType), bug.Status))
                        lstLogEntry.Add("bug.Notes = '" & bug.Notes & "'")
                        lstLogEntry.Add("chkCreateBackups.Checked=" & CStr(chkCreateBackups.Checked))
                        Call AppendTextToFile(String.Join(vbCrLf, lstLogEntry.ToArray), LogsDirectoryPhysicalPath & "BugHunter.log")
                    End If
                Next
            Else
                bug.Notes = "Not for DotNetNuke Version " & DotNetNukeVersion
                bug.Status = BugStatusType.Unknown
                blnReturn = True 'There is no problem, this is just for a different version
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return blnReturn

    End Function

    Private Function RestoreFrom_AppendText(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "") As Boolean
        strContents = Replace(strContents, bug.AppendText, "")
        If SaveTextToFile(strContents, strFilePath, respErrorMessage) Then
            bug.Status = BugStatusType.Alive
            StampFile(bug, strFilePath, "restored")
            Return True
        Else
            bug.Status = BugStatusType.Unknown
            StampFile(bug, strFilePath, "tried to restore")
            Return False
        End If
        Return False
    End Function

    Private Function RestoreFrom_StringReplacement(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "") As Boolean
        strContents = Replace(strContents, bug.ReplacementText, bug.ErrorText)
        If SaveTextToFile(strContents, strFilePath, respErrorMessage) Then
            bug.Status = BugStatusType.Alive
            StampFile(bug, strFilePath, "restored")
            Return True
        Else
            bug.Status = BugStatusType.Unknown
            StampFile(bug, strFilePath, "tried to restore")
            Return False
        End If
    End Function


    'CHECK STATUS
    Private Function CheckStatus(bug As SkinTweaker.Bug, Optional ByRef respErrorMessage As String = "") As Boolean
        '1. Check if bug kill will work with this version of DNN
        '2. Access Each File
        '3. Check Status
        Dim blnReturn As Boolean = False
        Dim strContents As String = String.Empty
        Try
            '1. Check if bug kill will work with this version of DNN
            If IsForThisDnnVersion(bug) Then
                '2. Access Each File
                If bug.AffectedFilePath.Count > 0 Then
                    Dim blnFoundDeadBug As Boolean = False
                    Dim blnAlreadyNormalized As Boolean = False
                    For Each strFilePath As String In bug.AffectedFilePath
                        strContents = GetTextFileContents(strFilePath, respErrorMessage)
                        If bug.NormalizeCarriageReturns Then strContents = NormalizeCarriageReturns(strContents, blnAlreadyNormalized)
                        If (Not blnAlreadyNormalized) And bug.NormalizeCarriageReturns Then
                            'Update the file so it will be ready next time
                            Call SaveTextToFile(strContents, strFilePath)
                        End If
                        '3. Check Satus
                        Select Case bug.KillType
                            Case BugKillType.AppendText
                                Call CheckStatus_AppendText(bug, strContents, strFilePath, respErrorMessage)
                            Case BugKillType.StringReplacement
                                Call CheckStatus_StringReplacement(bug, strContents, strFilePath, respErrorMessage)
                            Case BugKillType.BothReplaceAndAppend
                                Call CheckStatus_BothReplaceAndAppend(bug, strContents, strFilePath, respErrorMessage)
                            Case BugKillType.NONE
                        End Select
                        'Logging
                        If LoggingON(PortalId) Then
                            Dim lstLogEntry As New List(Of String)
                            lstLogEntry.Add("==================================================================================")
                            lstLogEntry.Add("Entry Made: " & Now)
                            lstLogEntry.Add("Bug ID " & bug.ID)
                            lstLogEntry.Add("Function: CheckStatus")
                            lstLogEntry.Add("bug.AffectedFilePath.Count = " & bug.AffectedFilePath.Count)
                            lstLogEntry.Add("Accessed file: " & strFilePath)
                            lstLogEntry.Add("bug.KillType = " & [Enum].GetName(GetType(BugKillType), bug.KillType))
                            lstLogEntry.Add("respErrorMessage from CheckStatus_ function = '" & respErrorMessage & "'")
                            lstLogEntry.Add("bug.Status after CheckStatus_ function = " & [Enum].GetName(GetType(BugStatusType), bug.Status))
                            lstLogEntry.Add("bug.Notes = '" & bug.Notes & "'")
                            Call AppendTextToFile(String.Join(vbCrLf, lstLogEntry.ToArray), LogsDirectoryPhysicalPath & "BugHunter.log")
                        End If
                        'If ANY of these files have a bug that is alive, then report it
                        If bug.Status = BugStatusType.Alive Then
                            blnFoundDeadBug = False 'Have to reset since a previous could have been alive
                            Exit For
                        End If
                        'If ANY of the bugs are found dead AND NONE are found alive, then set Dead as the status for the whole group
                        If bug.Status = BugStatusType.Dead Then blnFoundDeadBug = True
                    Next
                    If blnFoundDeadBug Then bug.Status = BugStatusType.Dead
                Else
                    'No files to search, could be something like the theme directory being empty, so no big deal
                    bug.Notes = "No files were found for this bug."
                    bug.Status = BugStatusType.Unknown
                    Return True 'There is no problem, there just weren't any files to check
                End If
            Else
                bug.Notes = "Not for DotNetNuke Version " & DotNetNukeVersion
                bug.Status = BugStatusType.NotVersion
                Return True 'There is no problem, this is just for a different version
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return blnReturn
    End Function

    Private Sub CheckStatus_AppendText(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "")
        Try
            If CBool(InStr(strContents, bug.AppendText)) Then
                bug.Status = BugStatusType.Dead
            Else
                bug.Status = BugStatusType.Alive
            End If
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub CheckStatus_StringReplacement(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "")
        'Sometimes the Error Text is a sub string of the ReplacementText
        Try
            If CBool(InStr(strContents, bug.ReplacementText)) Then
                bug.Status = BugStatusType.Dead
            Else
                If CBool(InStr(strContents, bug.ErrorText)) Then
                    bug.Status = BugStatusType.Alive
                Else
                    bug.Status = BugStatusType.Unknown
                End If
            End If
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub CheckStatus_BothReplaceAndAppend(bug As SkinTweaker.Bug, strContents As String, strFilePath As String, Optional ByRef respErrorMessage As String = "")
        Dim bugAppendStatus As BugStatusType
        Try
            Call CheckStatus_AppendText(bug, strContents, strFilePath, respErrorMessage)
            bugAppendStatus = bug.Status
            Call CheckStatus_StringReplacement(bug, strContents, strFilePath, respErrorMessage)
            If bug.Status <> bugAppendStatus Then
                'If they are not in agreement, then the bug is either alive or unknown
                If bug.Status <> BugStatusType.Unknown Then
                    bug.Status = BugStatusType.Alive
                End If
            End If
        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

#End Region


#Region "PRIVATE SUBS"

    Private Function IsForThisDnnVersion(bug As SkinTweaker.Bug) As Boolean
        If Not bug.KillWorksForDnnVersions Is Nothing Then
            Dim strDnnMajorVersionNumber As String = Split(DotNetNukeVersion, ".")(0)
            If bug.KillWorksForDnnVersions.Contains(DotNetNukeVersion) Then
                Return True
            ElseIf bug.KillWorksForDnnVersions.Contains("DNNALL") Then
                Return True
            ElseIf bug.KillWorksForDnnVersions.Contains("DNN6ALL") And strDnnMajorVersionNumber = "6" Then
                Return True
            ElseIf bug.KillWorksForDnnVersions.Contains("DNN5ALL") And strDnnMajorVersionNumber = "5" Then
                Return True
            ElseIf bug.KillWorksForDnnVersions.Contains("DNN4ALL") And strDnnMajorVersionNumber = "4" Then
                Return True
            End If
        End If
        Return False 'Hasn't been specified, so don't risk it.
    End Function

    Private Sub StampFile(bug As SkinTweaker.Bug, strCurrentFilePath As String, strActionPerformed As String)
        Try
            If bug.UseStamps Then
                Dim strStampMessage As String = String.Empty
                strStampMessage = "SkinTweaker " & strActionPerformed & " Bug ID " & CStr(bug.ID) & " on " & FormatDateTime(Now, DateFormat.GeneralDate)
                strStampMessage = CommentOutThis(strStampMessage, strCurrentFilePath)
                'Append Stamp to File
                If Not AppendTextToFile(strStampMessage, strCurrentFilePath) Then
                    SetMessage(Me, "Append Stamp to File FAILED For: " & strStampMessage, MessageType.Warning)
                End If
            End If

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Function CommentOutThis(strCodeOrMessage As String, strFilePath As String) As String
        'Determine file type so can format for comment
        Select Case Path.GetExtension(strFilePath)
            Case ".css"
                strCodeOrMessage = vbCr & "/* " & strCodeOrMessage & " */"
            Case ".js"
                strCodeOrMessage = vbCr & "// " & strCodeOrMessage
            Case ".ascx"
                strCodeOrMessage = vbCr & "<%--" & strCodeOrMessage & " --%>"
            Case ".aspx"
                strCodeOrMessage = vbCr & "<%--" & strCodeOrMessage & " --%>"
            Case ".vb"
                strCodeOrMessage = vbCr & "'" & strCodeOrMessage
        End Select
        Return strCodeOrMessage
    End Function

    Private Function NormalizeCarriageReturns(strContents As String, Optional ByRef respAlreadyNormal As Boolean = False) As String
        'Microsoft (CR LF)
        'Unix (LF)
        'Unicode Line Seperator (LS)
        'Unicode Paragraph Seperator (PS)
        'Apple (CR)
        If CBool(InStr(strContents, vbCrLf)) Then
            respAlreadyNormal = True
        ElseIf CBool(InStr(strContents, vbLf)) Then
            Return Replace(strContents, vbLf, vbCrLf)
        ElseIf CBool(InStr(strContents, ChrW(UnicodeCategory.LineSeparator))) Then
            Return Replace(strContents, ChrW(UnicodeCategory.LineSeparator), vbCrLf)
        ElseIf CBool(InStr(strContents, ChrW(UnicodeCategory.ParagraphSeparator))) Then
            Return Replace(strContents, ChrW(UnicodeCategory.ParagraphSeparator), vbCrLf)
        ElseIf CBool(InStr(strContents, vbCr)) Then
            Return Replace(strContents, vbCr, vbCrLf) 'Needs to be the last one sought because used by StampFile()
        End If
        Return strContents
    End Function

    Private Sub SetupHelpTextAndToolTips()
        'Use the text from HelpText since the help text feature in DNN 5 sucks and the tool tip works better
        If Split(DotNetNukeVersion, ".")(0) <> "6" Then
            chkCreateBackups.ToolTip = lblCreateBackups.HelpText
        End If
    End Sub

    Private Function GetAllThemeCssFilePaths() As List(Of String)
        Dim lstAllThemeCssFilePaths As New List(Of String)
        Try
            'Add all Theme css file paths
            Dim objDir As New DirectoryInfo(ThemesCssDirectoryPhysicalPath)
            For Each objSubFolder As DirectoryInfo In objDir.GetDirectories
                For Each objFileInfo As FileInfo In objSubFolder.GetFiles
                    If objFileInfo.Extension = ".css" Then lstAllThemeCssFilePaths.Add(objFileInfo.FullName)
                Next
            Next

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return lstAllThemeCssFilePaths
    End Function

#End Region


#Region "BUGS"

    '====================================
    '       Define All Bugs in Here
    '====================================

    Private Function GetBugs() As List(Of SkinTweaker.Bug)
        Dim bug As SkinTweaker.Bug
        Dim Bugs As New List(Of SkinTweaker.Bug)
        Dim strSkinTweakerBugPath As String = "DesktopModules/SkinTweaker/Controls/Bug.ascx"
        Dim strDefaultCssFilePath As String = HttpContext.Current.Request.PhysicalApplicationPath & "Portals\_Default\Default.css"
        Dim lstAllThemeCssFilePaths As List(Of String) = GetAllThemeCssFilePaths()

        Try
            'Do not worry about adding code to select just one bug because they all have to be displayed anyway.
            'NOTE: IdentifyingText is used to find the bug itself, but for append it is just fluff. Identifyingtext
            '      usually contains the error surround by other relevant text.
            '      Do NOT need IdentifyingText for Restore, just for killing

            '=======================================================================================================================
            '                                       BUG ID 1: DNN Button Bug
            '=======================================================================================================================
            '
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = "1"
            bug.Name = "DNN Button Bug"
            bug.Description = "This bug would not allow an admin to use new jQuery buttons styles in DNN 6. This css would override any " & _
                              "new jQuery button styles so that all your buttons looked like DDN 6's dark grey buttons."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN6ALL"}
            bug.KillType = BugKillType.StringReplacement
            bug.AffectedFilePath.Add(strDefaultCssFilePath)
            bug.ErrorText = ".ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only"
            bug.IdentifyingText = "a.dnnPrimaryAction, a.dnnPrimaryAction:link, a.dnnPrimaryAction:visited, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only{"
            bug.ReplacementText = ".dnnForm .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only, .ui-dialog-buttonset .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)

            '=======================================================================================================================
            '                                       BUG ID 2: DNN Tabs Bug
            '=======================================================================================================================
            '
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = "2"
            bug.Name = "DNN Tabs Bug"
            bug.Description = "This bug is a css collision. Some of the possible values in the .dnnForm scope are not covered " & _
                              "for the tabs, so the tabs pickup the values from Skin Tweaker's selected theme. However, this " & _
                              "only occurs if the tabs are in the Skin Tweaker's scope, otherwise there is no problem at all."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN6ALL"}
            bug.KillType = BugKillType.AppendText
            bug.AffectedFilePath.Add(strDefaultCssFilePath)
            bug.IdentifyingText = ""
            bug.AppendText = ".dnnForm.ui-tabs.ui-widget.ui-widget-content.ui-corner-all {border:none; background:none;}" & _
                ".dnnForm .dnnAdminTabNav.ui-tabs-nav.ui-helper-reset.ui-helper-clearfix.ui-widget-header.ui-corner-all {border-radius: 0 0 0 0;background: none;border-bottom: 4px solid #292929;padding: 0 0 0 0;}" & _
                ".dnnForm .ui-state-default.ui-corner-top.ui-tabs-selected.ui-state-active, .dnnForm .ui-state-default.ui-corner-top {border: none;}"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)

            '=======================================================================================================================
            '                                       BUG ID 3: jQuery UI Input Button Bug
            '=======================================================================================================================
            '
            'This bug seems to be the result of an error by the jQuery Theme Roller. When the Theme Roller creates the new theme,
            '  it simple forgets to add the scope name to this selector. Just adding the scope name seems to fix the problem.

            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = "3"
            bug.Name = "jQuery UI Button Bug"
            bug.Description = "Bug found in some jQuery UI CSS Themes that makes all the input buttons look small."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNNALL"} 'DNN version does not matter
            bug.KillType = BugKillType.StringReplacement
            bug.NormalizeCarriageReturns = True
            'Get all Theme css file paths
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.ErrorText = "/* no icon support for input elements, provide padding by default */" & vbCrLf & "input.ui-button { padding: .4em 1em; }"
            bug.IdentifyingText = bug.ErrorText
            bug.ReplacementText = "/* no icon support for input elements, provide padding by default */" & vbCrLf & ".SkinTweaker input.ui-button { padding: .4em 1em; }"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)

            '=======================================================================================================================
            '                                       BUG ID 4: Autocomplete List Bug
            '=======================================================================================================================
            '
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 4
            bug.Name = "Autocomplete List Bug"
            bug.Description = "This bug affects the way that the drop-down lists of jQuery autocomplete and comboboxes look. This bug fix " & _
                "works together with the Autocomplete List Tweak that you find in the jQuery Script Editor."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNNALL"}
            bug.KillType = BugKillType.AppendText
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath.Add(strDefaultCssFilePath)
            bug.AppendText = ".SkinTweaker [role=""menuitem""] {list-style: none outside none;}"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 5: Help Icon Bug
            '=======================================================================================================================
            '
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 5
            bug.Name = "Help Icon Bug"
            bug.Description = "This bug would let the text run over top of the DNN 6 help icon image. Use this fix if you want to make use " & _
                "of DNN's Label control and use the HelpText attribute."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN6ALL"}
            bug.KillType = BugKillType.AppendText
            bug.NormalizeCarriageReturns = True
            bug.AffectedFilePath.Add(strDefaultCssFilePath)
            bug.AppendText = ".SkinTweaker a.dnnFormHelp { padding-right:20px;}"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 6: Widget Text Color Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 6
            bug.Name = "Widget Text Color Bug"
            bug.Description = "A clash between user selected skins and jQuery UI CSS Theme - The content text of many widgets is the wrong " & _
                "color often making it difficult to read. This also affects the day name headers of the datepicker's calendar."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNNALL"}
            bug.KillType = BugKillType.StringReplacement
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.IdentifyingText = ".SkinTweaker .ui-widget-content a { color:"
            bug.ErrorText = ".SkinTweaker .ui-widget-content a {"
            bug.ReplacementText = ".SkinTweaker .ui-widget-content a , .SkinTweaker .ui-widget-content th, .SkinTweaker .ui-widget-content p {"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 7: DatePicker Title Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 7
            bug.Name = "DatePicker Title Bug"
            bug.Description = "A clash between user selected skins and jQuery UI CSS Theme - The title of the datepicker's calendar is " & _
                "the wrong color often making it hard to see."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN5ALL"}
            bug.KillType = BugKillType.StringReplacement
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.IdentifyingText = ".SkinTweaker .ui-widget-header a { color:"
            bug.ErrorText = ".SkinTweaker .ui-widget-header a {"
            bug.ReplacementText = ".SkinTweaker .ui-widget-header a , .SkinTweaker .ui-widget-header div {"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 8: Highlight Text Color Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 8
            bug.Name = "Highlight Text Color Bug"
            bug.Description = "A clash between user selected skins and jQuery UI CSS Theme - The highlight text is the wrong color and " & _
                "can often be difficult to see."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN5ALL"}
            bug.KillType = BugKillType.StringReplacement
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.IdentifyingText = ".SkinTweaker .ui-state-highlight a, .SkinTweaker .ui-widget-content .ui-state-highlight a,.ui-widget-header .ui-state-highlight a { color:"
            bug.ErrorText = ".SkinTweaker .ui-state-highlight a, .SkinTweaker .ui-widget-content .ui-state-highlight a,.ui-widget-header .ui-state-highlight a {"
            bug.ReplacementText = ".SkinTweaker .ui-state-highlight a, .SkinTweaker .ui-widget-content .ui-state-highlight a,.ui-widget-header .ui-state-highlight a, .SkinTweaker .ui-widget .ui-state-highlight p {"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 9: Error Text Color Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 9
            bug.Name = "Error Text Color Bug"
            bug.Description = "A clash between user selected skins and jQuery UI CSS Theme - The error text is the wrong color and " & _
                "can often be difficult to see."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN5ALL"}
            bug.KillType = BugKillType.StringReplacement
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.IdentifyingText = ".SkinTweaker .ui-state-error a, .SkinTweaker .ui-widget-content .ui-state-error a, .SkinTweaker .ui-widget-header .ui-state-error a { color:"
            bug.ErrorText = ".SkinTweaker .ui-state-error a, .SkinTweaker .ui-widget-content .ui-state-error a, .SkinTweaker .ui-widget-header .ui-state-error a {"
            bug.ReplacementText = ".SkinTweaker .ui-state-error a, .SkinTweaker .ui-widget-content .ui-state-error a, .SkinTweaker .ui-widget-header .ui-state-error a, .SkinTweaker .ui-state-error p {"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            '=======================================================================================================================
            '                                       BUG ID 10: Forum Help Icon Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 10
            bug.Name = "Forum Help Icon Bug"
            bug.Description = "This bug is just like Bug ID 5, except this fix is specifically for DNN's Core Forum."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNN6ALL"} 'Options: DNNALL, DNN6ALL, DNN5ALL, DNN4ALL
            bug.KillType = BugKillType.AppendText
            bug.NormalizeCarriageReturns = False
            bug.AffectedFilePath.Add(strDefaultCssFilePath) 'Put fix in default.css because do not know where they have installed Forum
            bug.AppendText = ".DnnModule-DNN_Forum a.dnnFormHelp {padding-right: 20px;}"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)

            '=======================================================================================================================
            '                                       BUG ID 11: Highlight & Error Dimensions Bug
            '=======================================================================================================================
            '
            'NEW BUG TEMPLATE
            bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            bug.ID = 11
            bug.Name = "Highlight & Error Size Bug"
            bug.Description = "This bug distorts the dimensions of the the Highlight and Error message boxes of a jQuery CSS Theme."
            'Kill Info
            bug.KillWorksForDnnVersions = New List(Of String) From {"DNNALL"} 'Options: DNNALL, DNN6ALL, DNN5ALL, DNN4ALL
            bug.KillType = BugKillType.AppendText
            bug.NormalizeCarriageReturns = True
            bug.AffectedFilePath = lstAllThemeCssFilePaths
            bug.AppendText = ".SkinTweaker .ui-widget .ui-state-highlight p { margin:1em}" & vbCrLf & ".SkinTweaker .ui-widget .ui-state-error p { margin:1em}"
            'Stamp Info
            bug.UseStamps = True
            Bugs.Add(bug)
            phBugs.Controls.Add(bug)


            ''=======================================================================================================================
            ''                                       BUG ID 0: BUG TEMPLATE
            ''=======================================================================================================================
            ''
            ''NEW BUG TEMPLATE
            'bug = CType(Page.LoadControl(strSkinTweakerBugPath), SkinTweaker.Bug)
            'bug.ID = 0
            'bug.Name = "BUG TEMPLATE"
            'bug.Description = ""
            ''Kill Info
            'bug.KillWorksForDnnVersions = New List(Of String) From {"6.1.3", "6.1.5"} 'Options: DNNALL, DNN6ALL, DNN5ALL, DNN4ALL
            'bug.KillType = BugKillType.NONE
            'bug.NormalizeCarriageReturns = False
            'bug.AffectedFilePath.Add("PATH TO SOME FILE")
            'bug.IdentifyingText = "TEXT THAT IDENTIFIES THE BUG ITSELF" 'This is just fluff for Append
            'bug.ErrorText = ""
            'bug.ReplacementText = ""
            'bug.AppendText = ""
            ''Stamp Info
            'bug.UseStamps = True
            'bug.CustomKillStamp = ""
            'bug.CustomRestoreStamp = ""
            'Bugs.Add(bug)
            'phBugs.Controls.Add(bug)



            ''================================================================================================
            ''                                  NEW BUGS QUICK NOTES
            ''================================================================================================

            ''NEW BUG: 
            ''DESCRIPTION:


            ''------------------------------------------------------------------------------------------------
            ''NEW BUG: 
            ''DESCRIPTION:


            ''------------------------------------------------------------------------------------------------
            ''NEW BUG: 
            ''DESCRIPTION:


            ''------------------------------------------------------------------------------------------------
            ''NEW BUG: 
            ''DESCRIPTION:

        Catch exc As Exception        'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
        Return Bugs
    End Function

#End Region



End Class


#Region "OLD BUG NOTES"

'    '================================================================================================
'    '                               DNN_DefaultCSS_ButtonBug
'    '================================================================================================
'    'DESCRIPTION: This bug would not allow an admin to use new jQuery buttons styles. This css would override any
'    '  new jQuery button styles so that all your buttons looked like DDN's dark grey buttons.
'    '
'    'Bug in the ~\Portals\_Default\default.css file that occurs 2 times: 
'    'ERROR: .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only
'    'NEW CORRECTION: Remove it and clenaup any remaining commas.
'    'OLD CORRECTION: .ui-button .ui-widget .ui-state-default .ui-corner-all .ui-button-text-only
'    '
'    'Old correction was to add spaces between classes. The new correction simply add the scope .dnnForm
'    ' which was probably the original intent of the programmer. They were probably trying to stop 
'    ' collisions with other buttons, but if they want to do they then they need to stay in thier own
'    ' scope rather than force everyone to use the same buttons everywhere on a website.

'    Dim strBug As String = ".ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only"
'    Dim strCorrection As String = ".dnnForm .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only"


'    '================================================================================================
'    '                               DNN_DefaultCSS_ButtonBug
'    '================================================================================================

'    'This bug is a css collision. Some of the possible values in the .dnnForm scope are not covered 
'    ' for the tabs, so the tabs pickup the values from Skin Tweaker's selected theme. However, this 
'    ' only occurs if the tabs are in the Skin Tweaker's scope, otherwise there is no problem at all.
'    '
'    'The actual 'collisions' or 'overwrites' are too numerous to list here and I didn't find any 
'    ' mistakes in DNN's css for this. Thus, I will only list the lines to be add to fix this.
'    ' This isn't the greatest fix, but it will work for now.


'#End Region

#End Region

