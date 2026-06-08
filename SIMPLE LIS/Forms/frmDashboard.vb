Imports System.IO
Public Class frmDashboard

#Region "Vars"
    Private afterload As Boolean
    Private selectedModule As enModule = enModule.diagnostics
    Enum enFilterBy
        forresultentry = 0
        viewAll = 4
        Today = 5
        gotopreviousdate = 6
    End Enum
    Enum enModule
        diagnostics = 0
        examinationschema = 1 
    End Enum
#End Region
    Private Sub frmDashboard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadPrivileges()
        setFormTitle()
    End Sub
#Region "Methods"
    Private Sub loadPrivileges()
        Me.tsExaminationSchema.Visible = modGlobal.GetUserSubModulesVisibility(modGlobal.AccountSubModule.smodSchemaExamination)
        Me.tsDiagnosticTestMapping.Visible = modGlobal.GetUserSubModulesVisibility(modGlobal.AccountSubModule.smodDiagnosticTestMapping)
    End Sub
    Private Sub setFormTitle()
        If selectedModule = enModule.diagnostics Then
            Me.lblmodulename.Text = "Diagnostic Tests"
        ElseIf selectedModule = enModule.examinationschema Then
            Me.lblmodulename.Text = "Examination Schema"
        End If
        loadFilterBy()
        displayList()
        setToolStrip()
    End Sub
    Private Sub setToolStrip()
        Me.tsview.Text = "View"
        If selectedModule = enModule.diagnostics Then
            If Me.dgMain.SelectedRows().Count > 0 Then
                If Me.dgMain.SelectedRows(0).Cells("status").Value = 3 Then
                    Me.tsnew.Enabled = True
                    Me.tsedit.Enabled = False
                    Me.tsview.Enabled = False
                ElseIf Me.dgMain.SelectedRows(0).Cells("status").Value = 4 Then
                    Me.tsnew.Enabled = False
                    Me.tsedit.Enabled = True
                    Me.tsview.Enabled = True
                    Me.tsview.Text = "Release"
                ElseIf Me.dgMain.SelectedRows(0).Cells("status").Value = 5 Or Me.dgMain.SelectedRows(0).Cells("status").Value = 6 Then
                    Me.tsnew.Enabled = False
                    Me.tsedit.Enabled = True
                    Me.tsview.Enabled = True
                End If
            Else
                Me.tsnew.Enabled = False
                Me.tsedit.Enabled = False
                Me.tsview.Enabled = False
            End If
        ElseIf selectedModule = enModule.examinationschema Then
            Me.tsnew.Enabled = False
            Me.tsedit.Enabled = True
            Me.tsview.Enabled = False
        End If
    End Sub
    Private Sub loadFilterBy()
        afterload = False
        Me.tsfilteryby.Visible = True
        Me.tslblfilterby.Visible = True
        Me.tsfilteryby.ComboBox.DataSource = Nothing
        If selectedModule = enModule.diagnostics Then
            With tsfilteryby.ComboBox
                .Items.Add("For Result Entry")
                .Items.Add("For Update/Releasing")
                .Items.Add("Released Examinations")
                '.Items.Add("Cancelled")
                '.Items.Add("View All")
                '.Items.Add("Today")
                '.Items.Add("Go To Previous Date")
                .DataSource = .Items
                afterload = True
                .SelectedIndex = 0
            End With
        Else
            Me.lblpreviousdate.Text = ""
            Me.tsfilteryby.Visible = False
            Me.tslblfilterby.Visible = False
        End If
        afterload = True
    End Sub
    Private Sub displayList()
        If Not afterload Then
            Exit Sub
        End If
        With dgMain
            If Me.selectedModule = enModule.diagnostics Then
                .DataSource = clsExaminationUpshots.getExaminationUpshots(txtsearch.Text, modGlobal.sourceOfficeCode, tsfilteryby.SelectedIndex) ', IIf(lblpreviousdate.Text = "", Date.Now, lblpreviousdate.Text))
                formatColumn("prdno", "Request No.", 60, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("patientrequestno", "", 0)
                formatColumn("ptno", "PTNo", 80, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("hospitalno", "", 0)
                formatColumn("patientname", "Patient Name", -1)
                formatColumn("DOB", "DOB", 80, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("Age", "Age", 60, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("Gender", "Sex", 60, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("daterequested", "Date Requested", 120)
                formatColumn("office", "Requesting Department", 160)
                formatColumn("Request", "Request", -1)
                formatColumn("admissionid", "", 0)
                'If tsfilteryby.SelectedIndex < 5 And tsfilteryby.SelectedIndex <> enFilterBy.viewAll Then
                '    formatColumn("requestStatus", "Status", 0)
                'Else
                '    formatColumn("requestStatus", "Status", 120, DataGridViewContentAlignment.MiddleCenter)
                'End If
                formatColumn("Paid", "Paid", 40, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("status", "", 0)
            ElseIf Me.selectedModule = enModule.examinationschema Then
                .DataSource = clsExamination.genericcls(3, "")
                formatColumn("Code", "Code", 120, DataGridViewContentAlignment.MiddleCenter)
                formatColumn("Description", "Description", -1)
            End If
        End With
    End Sub
    Private Sub formatColumn(columnName As String, headerText As String, width As Integer, Optional alignment As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft)
        dgMain.Columns(columnName).HeaderText = headerText
        If width = -1 Then
            dgMain.Columns(columnName).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgMain.Columns(columnName).Visible = True
        ElseIf width = 0 Then
            dgMain.Columns(columnName).Visible = False
        Else
            dgMain.Columns(columnName).Width = width
            dgMain.Columns(columnName).Visible = True
        End If
        dgMain.Columns(columnName).DefaultCellStyle.Alignment = alignment
    End Sub
#End Region

    Private Sub lblpreviousdate_Click(sender As System.Object, e As System.EventArgs) Handles lblpreviousdate.Click
        Dim f As New frmCustomDialog
        f.dtpdate.Value = Me.lblpreviousdate.Text
        f.ShowDialog()
        If f.issave Then
            Me.lblpreviousdate.Text = f.dtpdate.Value.ToString(modGlobal.defaultdateformat)
            displayList()
        End If
    End Sub

    Private Sub tsfilteryby_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tsfilteryby.SelectedIndexChanged
        If tsfilteryby.SelectedIndex = enFilterBy.gotopreviousdate Then
            Me.lblpreviousdate.Text = Utility.GetServerDate().AddDays(-1).ToString(modGlobal.defaultdateformat)
        Else
            Me.lblpreviousdate.Text = ""
        End If
        displayList()
    End Sub

    Private Sub DiagnosticTestMappingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsDiagnosticTestMapping.Click
        Dim f As New frmLaboratoryItems
        f.ShowDialog()
    End Sub

    Private Sub dgMain_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles dgMain.SelectionChanged
        Try
            If Me.selectedModule = enModule.diagnostics AndAlso Me.dgMain.SelectedRows().Count > 0 Then
                setToolStrip()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExaminationSchemaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsExaminationSchema.Click
        Me.selectedModule = enModule.examinationschema
        setFormTitle()
    End Sub

    Private Sub DiagnosticTestsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsDiagnosticTests.Click
        Me.selectedModule = enModule.diagnostics
        setFormTitle()
    End Sub

    Private Sub tsClose_Click(sender As System.Object, e As System.EventArgs) Handles tsClose.Click
        Me.Close()
    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click
        Select Case Me.selectedModule
            Case enModule.diagnostics
                Dim frmDesigner As frmResultDesigner
                Dim dt As DataTable = clsExaminationUpshots.getPatientRequestStatus(dgMain.SelectedRows(0).Cells("prdno").Value)
                If dgMain.SelectedRows(0).Cells("status").Value = clsModel.RequestStatus.resultencoded Then
                    frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.Release, dgMain.SelectedRows(0).Cells("prdno").Value, 0, dt.Rows(0)(0))
                Else
                    frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.View, dgMain.SelectedRows(0).Cells("prdno").Value, 0, dt.Rows(0)(0))
                End If
                frmDesigner.ShowDialog()
                displayList()
            Case enModule.examinationschema

        End Select
    End Sub

    Private Sub tsnew_Click(sender As System.Object, e As System.EventArgs) Handles tsnew.Click
        Select Case Me.selectedModule
            Case enModule.diagnostics
                Dim dt As DataTable = clsExaminationUpshots.getPatientRequestStatus(dgMain.SelectedRows(0).Cells("prdno").Value)
                Dim frmDesigner As frmResultDesigner
                frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.manageResult, dgMain.SelectedRows(0).Cells("prdno").Value, 0, dt.Rows(0)(0))
                frmDesigner.ShowDialog()
                displayList()
            
        End Select
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click
        Select Case Me.selectedModule
            Case enModule.diagnostics
                Dim dt As DataTable = clsExaminationUpshots.getPatientRequestStatus(dgMain.SelectedRows(0).Cells("prdno").Value)
                If dgMain.SelectedRows(0).Cells("status").Value = clsModel.RequestStatus.released Then
                    Dim foverride As New frmRequestedBy(frmRequestedBy.enPurpose.editResult, modGlobal.sourceOfficeCode)
                    foverride.ShowDialog()
                    If Not foverride.isLoginValid Then
                        Exit Sub
                    End If
                    modGlobal.SaveLog("Diagnostics", "Attempt to edit a released examination ReqNo: " & dgMain.SelectedRows(0).Cells("prdno").Value & " (Override By: " & foverride.RequestedByID & ")")
                End If
                Dim frmDesigner As frmResultDesigner
                frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.manageResult, dgMain.SelectedRows(0).Cells("prdno").Value, 0, dt.Rows(0)(0))
                frmDesigner.ShowDialog()
                displayList()
            Case enModule.examinationschema
                Dim f As New frmResultDesigner(frmResultDesigner.formaction.updateFormat, 0, dgMain.SelectedRows(0).Cells("Code").Value, 0)
                f.ShowDialog()
        End Select
    End Sub

    Private Sub tsbtnsearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnsearch.Click
        displayList()
    End Sub

    'Private Sub frmDashboard_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
    '    frmNotif.StartBroadCast()
    'End Sub
End Class