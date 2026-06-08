Imports System.IO
Public Class frmResultDesigner

#Region "Vars"
    Public requestdetailno, oldrequestdetailno As Long
    Private admissionid As Long
    Private itemdescription As String
    Private laboratoryresultid As Long
    Private laboratoryid As Integer
    Private laboratoryname As String
    Private laboratoryttitle As String
    Private testofficecode As String
     
    Private fbaseform As frmResultBaseDesign
    Private fBloodChem As frmtemplatewithconversion
    Private frmRadiology As frmtemplateRTF
    Private fcrossmatch As frmCrossmatching

    Private afterload As Boolean
    Private initpanelresultheight As Integer
    Private initpanelmainheight As Integer
    Private initformheight As Integer

    Private labformatid As LabFormat
    Private requestStatus As Integer
    Public myFormaction As formaction
    Public mergeToogle As Integer = 0
    Private ismergeresult As Boolean
    Private lstControls As New List(Of clsModel.LabControl)

    Enum formaction
        NONE = 0
        updateFormat = 1
        manageResult = 2
        Release = 3
        View = 4
    End Enum

    Public Sub New(ByVal myFormaction As formaction, ByVal requestdetailno As Long, ByVal laboratoryid As Long, ByVal status As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.myFormaction = myFormaction
        Me.requestdetailno = requestdetailno
        Me.oldrequestdetailno = requestdetailno
        Me.laboratoryid = laboratoryid
        Me.requestStatus = status
    End Sub
#End Region
    Private Sub frmResultDesigner_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        afterload = False
        Call getLabResultDetails()
        If myFormaction = formaction.updateFormat Then
            Me.Text = "Lab Result Designer"
            Select Case labformatid
                Case LabFormat.GRID_SI
                    Me.tsSave.Visible = False
                    Me.tsClose.Visible = False
                    Dim f As New frmManageResultParams(Me.laboratoryid, Me.laboratoryttitle, Me.labformatid)
                    f.ShowDialog()
                    Me.Close()
                    Exit Sub
                Case LabFormat.GRID_WITH_CONVERSION
                    Dim f As New frmManageResultParams(Me.laboratoryid, Me.laboratoryttitle, Me.labformatid)
                    f.ShowDialog()
                    Me.Close()
                    Exit Sub
                Case LabFormat.RADIOLOGY, LabFormat.ULTRASOUND, LabFormat.CROSSMATCHING, LabFormat.ECGREPORT, LabFormat.EchoForms
                    MsgBox("Diagnostic Format is not available for editing!", MsgBoxStyle.Information + MsgBoxStyle.Information, modGlobal.msgboxTitle)
                    Me.Close()
                    Exit Sub
                Case Else
                    fbaseform = New frmResultBaseDesign(Me, True, Me.laboratoryid, Me.laboratoryttitle, False, Me.labformatid)
                    loadForm()
            End Select
        ElseIf myFormaction = formaction.NONE Then
            Me.Close()
            Exit Sub
        Else
            Me.Text = Me.laboratoryname & Me.Text
            Me.panelsidebar.Visible = False
            Select Case Me.labformatid
                Case LabFormat.RADIOLOGY, LabFormat.ULTRASOUND, LabFormat.ECGREPORT, LabFormat.EchoForms
                    If Me.myFormaction = formaction.manageResult Then
                        tsradtemplatemain.Visible = True
                        loadTemplateList()
                    End If
                    frmRadiology = New frmtemplateRTF(requestdetailno, Me.laboratoryid, Me.laboratoryname, Me.myFormaction <> formaction.manageResult, Me.requestStatus, Me.labformatid)
                    frmRadiology.MdiParent = Me
                    frmRadiology.Dock = DockStyle.Fill
                    frmRadiology.Show()
                Case LabFormat.CROSSMATCHING
                    fcrossmatch = New frmCrossmatching(Me, False, Me.laboratoryid, Me.laboratoryttitle, Me.myFormaction <> formaction.manageResult, Me.labformatid)
                    fcrossmatch.loadRequestDetails(Me.requestdetailno)
                    loadFormCM()
                Case Else
                    fbaseform = New frmResultBaseDesign(Me, False, Me.laboratoryid, Me.laboratoryttitle, Me.myFormaction <> formaction.manageResult, Me.labformatid)
                    fbaseform.loadRequestDetails(Me.requestdetailno)
                    loadForm()
            End Select
            If Me.myFormaction = formaction.View Then
                Me.tsSave.Visible = False
                Me.tsPrint.Visible = testofficecode = modGlobal.sourceOfficeCode
                Me.tsprintas.Visible = Me.tsPrint.Visible
                Me.tsMerging.Visible = Me.tsPrint.Visible
                If Me.tsPrint.Visible And Me.isRTFForm() Then
                    If Me.labformatid = LabFormat.EchoForms Then
                        Me.CrystalReportToolStripMenuItem.Visible = False
                    End If
                ElseIf Me.tsPrint.Visible And Not Me.isRTFForm() Then
                    Me.DefaultPDFViewerToolStripMenuItem.Visible = False
                    Me.CrystalReportToolStripMenuItem.Visible = False
                End If
            ElseIf Me.myFormaction = formaction.Release Then
                Me.tsSave.Text = "Release"
            End If
        End If
        afterload = True
    End Sub
    Private Function isRTFForm() As Boolean
        Return Me.labformatid = LabFormat.RADIOLOGY Or
            Me.labformatid = LabFormat.ULTRASOUND Or
            Me.labformatid = LabFormat.ECGREPORT Or
            Me.labformatid = LabFormat.EchoForms
    End Function
    Private Sub getLabResultDetails()
        Dim dt As DataTable
        If Me.myFormaction = formaction.updateFormat Then
            dt = clsExamination.genericcls(8, Me.laboratoryid)
        Else
getLabDetails:
            dt = clsExamination.genericcls(9, Me.requestdetailno)
            If dt.Rows.Count = 0 Then
                MsgBox("Diagnostic Format not yet assigned!", MsgBoxStyle.Information + MsgBoxStyle.Information, modGlobal.msgboxTitle)
                myFormaction = formaction.NONE
                Exit Sub
            End If
            Me.admissionid = dt.Rows(0).Item("admissionid")
            Me.laboratoryresultid = dt.Rows(0).Item("laboratoryresultid")
            Me.itemdescription = dt.Rows(0).Item("itemdescription")
            Me.testofficecode = Utility.NullToEmptyString(dt.Rows(0).Item("destinationoffice"))
            If Not ismergeresult Then
                If dt.Rows(0).Item("mergetoresult") = 0 Then
                    checkMergeResults()
                    Me.tsMergeWithOtherResultToolStripMenuItem.Tag = "0"
                Else
                    Me.tsMergeWithOtherResultToolStripMenuItem.Tag = "1"
                    Me.tsMergeWithOtherResultToolStripMenuItem.Text = "Unmerge result"
                    Me.requestdetailno = dt.Rows(0).Item("mergetoresult")
                    Dim dtmerge As DataTable = clsExamination.genericcls(9, Me.requestdetailno)
                    Me.Text = " - Merged with " & dtmerge.Rows(0).Item("itemdescription")
                    Me.ismergeresult = True
                    GoTo getLabDetails
                End If
            End If
        End If
        Me.laboratoryid = dt.Rows(0).Item("laboratoryid")
        Me.labformatid = dt.Rows(0).Item("labformatid")
        Me.laboratoryname = dt.Rows(0).Item("description")
        Me.laboratoryttitle = IIf(Utility.NullToEmptyString(dt.Rows(0).Item("title")) = "", Me.laboratoryname, dt.Rows(0).Item("title"))
        Me.txtpanelheight.Text = Utility.NullToZero(dt.Rows(0).Item("panelsize"))
    End Sub
    Private Sub loadForm()
        fbaseform.MdiParent = Me
        fbaseform.Show()
        Me.initpanelresultheight = fbaseform.panelresult.Height
        Me.initpanelmainheight = fbaseform.panelmain.Height
        Me.initformheight = fbaseform.Height
        If Me.labformatid = LabFormat.GRID_WITH_CONVERSION Then
            Me.initpanelresultheight += fbaseform.panelresultwithconversion.Height
            Me.initpanelmainheight += fbaseform.panelresultwithconversion.Height
            Me.initformheight += fbaseform.panelresultwithconversion.Height
        End If
        Call adjustPanelSize()
        'Me.txtpanelheight.Text = Me.initpanelresultheight
        'fbaseform.Top = (Me.Height - fbaseform.Height) / 2
        Call loadDesign()
        recenterForm()
    End Sub
    Private Sub loadformcm()
        Dim islock As Boolean
        fcrossmatch.MdiParent = Me
        If Not myFormaction = formaction.manageResult Then
            islock = True
            Me.fcrossmatch.isLock = True
        End If
        fcrossmatch.Show()
       
    End Sub
    Private Sub adjustPanelSize()
        If CInt(Me.txtpanelheight.Text) >= 100 Then
            Me.fbaseform.panelresult.Height = CInt(Me.txtpanelheight.Text)
            Me.fbaseform.panelmain.Height = Me.initpanelmainheight + (CInt(Me.txtpanelheight.Text) - Me.initpanelresultheight)
            'Me.fbaseform.Height = Me.initformheight + (CInt(Me.txtpanelheight.Text) - Me.initpanelresultheight)
            Me.fbaseform.Height = 1080
            Call recenterForm()
        Else
            Me.txtpanelheight.Text = Me.initpanelresultheight
            adjustPanelSize()
        End If
    End Sub
    Private Sub recenterForm()
        fbaseform.Top = (Me.Height - fbaseform.Height) / 2
    End Sub
    Private Function getPanelCenter() As Point
        Return New Point(fbaseform.panelresult.Width / 2, fbaseform.panelresult.Height / 2)
    End Function
    Private Sub loadDesign()
        Me.lstControls = New List(Of clsModel.LabControl)
        If myFormaction = formaction.updateFormat Then
            Dim dt As DataTable = clsExamination.genericcls(10, Me.laboratoryid)
            fbaseform.panelresultgrid.Visible = False
            For Each row As DataRow In dt.Rows
                Dim field As New clsModel.LabControl(row)

                If field.ctrtype > 0 Then
                    If field.isvisible Then
                        fbaseform.AddControl(field)
                    End If
                    Me.lstControls.Add(field)
                End If

            Next
            If Me.labformatid = LabFormat.ECGREPORT Then
                Me.fbaseform.FormatSignatory(frmResultBaseDesign.signatory.medtech, False, False)
                Me.fbaseform.FormatSignatory(frmResultBaseDesign.signatory.verifiedby, False, False)
                Me.fbaseform.lblpathodesignation.Text = "Cardiologist"
            End If
            BindResultGrid()
            Me.fbaseform.chkesigmedtech.Visible = False
            Me.fbaseform.chkesigverifiedby.Visible = False
            Me.fbaseform.chkesigpatho.Visible = False
        Else
            Dim dt As DataTable = clsLaboratoryResultDetails.getLaboratoryResultDetails(Me.requestdetailno, "", Me.laboratoryid, 2)
            Dim islock As Boolean

            If Not myFormaction = formaction.manageResult Then
                islock = True
                Me.fbaseform.isLock = True
            End If
            If Me.labformatid = LabFormat.GRID_SI Then
                fbaseform.panelresultgrid.BorderStyle = BorderStyle.None
                fbaseform.panelresult.Visible = False
                fbaseform.panelresultwithconversion.Visible = False
                With fbaseform.dgvResult
                    .Columns(fbaseform.colresult.Index).Width = 160
                    .Columns(fbaseform.colunits.Index).Width = 100
                    .Columns(fbaseform.colref.Index).Width = 190
                    .Columns(fbaseform.colparameter.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .Columns(fbaseform.colresultconversion.Index).Visible = False
                    .Columns(fbaseform.colunitsconversion.Index).Visible = False
                    .Columns(fbaseform.colrefconversion.Index).Visible = False
                    For Each row As DataRow In dt.Rows
                        If row.Item("visible") Then
                            .Rows.Add(1)
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.collabdetailid.Index).Value = row.Item("laboratorydetailsid")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colparameter.Index).Value = row.Item("description")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colref.Index).Value = row.Item("normalvalues")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colresult.Index).Value = Utility.NullToEmptyString(row.Item("result"))
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.collabresultdetailid.Index).Value = row.Item("laboratoryresultdetailsid")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colunits.Index).Value = row.Item("unit")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.coltexthighlight.Index).Value = row.Item("texthighlight")
                            Call fbaseform.adjustForm()
                        End If
                    Next
                End With
            ElseIf Me.labformatid = LabFormat.GRID_WITH_CONVERSION Then
                fbaseform.panelresultgrid.BorderStyle = BorderStyle.None
                fbaseform.panelresult.Visible = False
                With (fbaseform.dgvResult)
                    .ColumnHeadersVisible = False
                    For Each row As DataRow In dt.Rows
                        If row.Item("visible") Then
                            .Rows.Add(1)
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.collabdetailid.Index).Value = row.Item("laboratorydetailsid")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colparameter.Index).Value = row.Item("description")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colref.Index).Value = row.Item("normalvalues")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colresult.Index).Value = Utility.NullToEmptyString(row.Item("result"))
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.collabresultdetailid.Index).Value = row.Item("laboratoryresultdetailsid")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colunits.Index).Value = row.Item("unit")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.coltexthighlight.Index).Value = row.Item("texthighlight")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colresultconversion.Index).Value = row.Item("resultconversion")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colrefconversion.Index).Value = Utility.NullToEmptyString(row.Item("normalvaluessi"))
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colunitsconversion.Index).Value = row.Item("unitsi")
                            .Rows(fbaseform.dgvResult.Rows.Count - 1).Cells(fbaseform.colconversion.Index).Value = row.Item("siconversion")
                            Call fbaseform.adjustForm()
                        End If
                    Next
                End With
            Else
                fbaseform.panelresult.BorderStyle = BorderStyle.None
                fbaseform.panelresultgrid.Visible = False
                If Me.labformatid = LabFormat.ECGREPORT Then
                    Me.fbaseform.cmbMedtech.Visible = False
                    Me.fbaseform.lblmedtechlicense.Visible = False
                    Me.fbaseform.lblmedtechdesignation.Visible = False
                    Me.fbaseform.lblmedtech.Visible = False
                End If
                For Each row As DataRow In dt.Rows
                    If row.Item("controltype") > 0 Then
                        Dim field As New clsModel.LabControl(row, row.Item("laboratoryresultdetailsid"), row.Item("result"))
                        If row.Item("visible") Then
                            fbaseform.AddControl(field)
                        End If
                        Me.lstControls.Add(field)
                    End If
                Next
            End If
            If islock Then
                fbaseform.lock() 
            End If
        End If
    End Sub
    Private Sub BindResultGrid()
        Me.dgvResult.DataSource = Nothing
        Me.dgvResult.DataSource = Me.lstControls
        For i As Integer = 0 To Me.dgvResult.Columns.Count - 1
            If Me.dgvResult.Columns(i).Name = "isvisible" Then
                Me.dgvResult.Columns(i).HeaderText = "Visible?"
                Me.dgvResult.Columns(i).Width = 50
            ElseIf Me.dgvResult.Columns(i).Name = "name" Then
                Me.dgvResult.Columns(i).HeaderText = "Field"
                Me.dgvResult.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ElseIf Me.dgvResult.Columns(i).Name = "controlTypeDescription" Then
                Me.dgvResult.Columns(i).HeaderText = "Fielt Type"
                Me.dgvResult.Columns(i).Width = 90
            Else
                Me.dgvResult.Columns(i).Visible = False
            End If
        Next
    End Sub
    Private Sub Save()
        If myFormaction = formaction.updateFormat Then
            If MsgBox("Do you want to update this format?", MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                Dim x As New clsExamination
                x.laboratoryid = Me.laboratoryid
                x.panelsize = Me.fbaseform.panelresult.Height
                x.saveLaboratory()
                Dim idx As Integer = 0
                For Each field As clsModel.LabControl In Me.lstControls
                    x.laboratorydetailsid = field.laboratorydetailsid
                    x.labdetailsdescription = field.name
                    x.visible = field.isvisible
                    x.normalvalues = ""
                    x.unit = ""
                    x.orderno = idx
                    Dim uuid As String = field.laboratorydetailsid
                    If uuid = "0" Then
                        uuid = field.uuid
                    End If
                    If x.visible Then
                        x.x2 = CType(Me.fbaseform.panelresult.Controls.Find("panel_" & uuid, True).First, Panel).Location.X
                        x.y2 = CType(Me.fbaseform.panelresult.Controls.Find("panel_" & uuid, True).First, Panel).Location.Y
                        x.width2 = CType(Me.fbaseform.panelresult.Controls.Find("panel_" & uuid, True).First, Panel).Size.Width
                        x.height2 = CType(Me.fbaseform.panelresult.Controls.Find("panel_" & uuid, True).First, Panel).Size.Height
                    Else
                        x.x2 = field.loc.X
                        x.y2 = field.loc.Y
                        x.width2 = field.panelwidth
                        x.height2 = field.panelheight
                    End If
                    x.optionvalues = field.optvalue
                    x.controltype = field.ctrtype
                    x.defaultvalue = field.defaultvalue
                    x.labeldescription = field.labeltext
                    x.texthighlight = field.texthighlight
                    If x.laboratorydetailsid = 0 Then
                        x.saveDetails(True)
                    Else
                        x.saveDetails(False)
                    End If
                Next
                Call SaveLog("Diagnostics", "Updated result format " & Me.laboratoryname)
                Me.Close()
            End If
        ElseIf myFormaction = formaction.manageResult Then
            If MsgBox("Are you sure you want to save this examination result?", MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                Select Case labformatid
                    Case LabFormat.RADIOLOGY, LabFormat.ULTRASOUND, LabFormat.ECGREPORT, LabFormat.EchoForms
                        frmRadiology.saveNow(Me.tsSave.Text)
                        If Not frmRadiology.isSave Then
                            Exit Sub
                        End If
                        frmRadiology.lock()
                    Case LabFormat.CROSSMATCHING        'jay
                        fcrossmatch.saveNow(Me.tsSave.Text)
                        If Not fcrossmatch.isSave Then
                            Exit Sub
                        End If
                        fcrossmatch.lock()
                    Case Else
                        If fbaseform.cmbMedtech.SelectedIndex = -1 Or fbaseform.cmbPathologist.SelectedIndex = -1 Then
                            MsgBox("Medical Tehnologist and Pathologist are required!", MsgBoxStyle.Critical, msgboxTitle)
                            Exit Sub
                        End If
                        Dim x As New clsLaboratoryResult
                        x.Oldlaboratoryid = fbaseform.laboratoryresultid  'Used as primarykey
                        x.laboratoryid = Me.laboratoryid
                        x.admissionid = fbaseform.admissionid
                        x.patientrequestno = requestdetailno 'dtResult.Rows(0).Item("patientrequestno").ToString
                        x.itemcode = fbaseform.itemcode
                        x.labno = fbaseform.labexaminationno
                        x.dateencoded = Utility.GetServerDate()
                        x.datesubmitted = x.dateencoded
                        x.encodedby = modGlobal.userid
                        fbaseform.medtech = fbaseform.cmbMedtech.SelectedValue
                        fbaseform.verifiedby = fbaseform.cmbverifiedby.SelectedValue
                        fbaseform.patho = fbaseform.cmbPathologist.SelectedValue
                        x.pathologist = fbaseform.patho
                        x.verifiedby = fbaseform.verifiedby
                        x.medtech = fbaseform.medtech
                        x.medicaltechnologist = x.medtech
                        x.releasedby = modGlobal.userid
                        x.datereleased = "01/01/1990"
                        x.remarks = fbaseform.txtgridremarks.Text
                        x.esigmedtech = fbaseform.chkesigmedtech.Checked
                        x.esigverifiedby = fbaseform.chkesigverifiedby.Checked
                        x.esigpatho = fbaseform.chkesigpatho.Checked
                        If x.Oldlaboratoryid = 0 Then
                            x.Oldlaboratoryid = x.Save(True)
                            Call SaveLog("Diagnostics", "New " & Me.laboratoryname & " result with request no.: " & x.patientrequestno & "", modGlobal.userid)
                        Else
                            x.soperation = 0
                            x.Save(False)
                            Call SaveLog("Diagnostics", "Update " & Me.laboratoryname & " result with request no.: " & x.patientrequestno & "", modGlobal.userid)
                        End If
                        If x.Oldlaboratoryid = 0 Then
                            Exit Sub
                        End If
                        Dim xdetails As New clsLaboratoryResultDetails
                        xdetails.laboratoryresultid = x.Oldlaboratoryid
                        If Me.labformatid = LabFormat.GRID_SI Or Me.labformatid = LabFormat.GRID_WITH_CONVERSION Then
                            fbaseform.dgvResult.EndEdit()
                            For Each row As DataGridViewRow In fbaseform.dgvResult.Rows
                                xdetails.laboratorydetailsid = row.Cells(fbaseform.collabdetailid.Index).Value
                                xdetails.Oldlaboratoryresultid = row.Cells(fbaseform.collabresultdetailid.Index).Value
                                xdetails.result = row.Cells(fbaseform.colresult.Index).Value
                                xdetails.resultconversion = row.Cells(fbaseform.colresultconversion.Index).Value
                                If xdetails.Oldlaboratoryresultid = 0 Then
                                    xdetails.Save(True)
                                Else
                                    xdetails.Save(False)
                                End If
                            Next
                            fbaseform.lock()
                        Else
                            For Each field As clsModel.LabControl In Me.lstControls
                                Dim uuid As String = field.laboratorydetailsid
                                If uuid = "0" Then
                                    uuid = field.uuid
                                End If
                                If field.isvisible AndAlso clsModel.ControlTypes.isInputField(field.ctrtype) AndAlso CType(Me.fbaseform.panelresult.Controls.Find("panel_" & uuid, True).First, Panel).Tag <> "1" Then
                                    xdetails.Oldlaboratoryresultid = field.laboratoryresultdetailid
                                    xdetails.laboratorydetailsid = field.laboratorydetailsid
                                    If field.ctrtype = clsModel.ControlTypes.TextField Or field.ctrtype = clsModel.ControlTypes.DoubleTextField Or
                                     field.ctrtype = clsModel.ControlTypes.ResizableTextField Then
                                        xdetails.result = CType(Me.fbaseform.panelresult.Controls.Find("txt_" & uuid, True).First, TextBox).Text
                                    ElseIf field.ctrtype = clsModel.ControlTypes.Dropdown Then
                                        If Me.laboratoryid = LabFormat.NEWBORNSCREENING Then
                                            xdetails.result = CType(Me.fbaseform.panelresult.Controls.Find("cmb_" & uuid, True).First, ComboBox).SelectedValue
                                        Else
                                            xdetails.result = CType(Me.fbaseform.panelresult.Controls.Find("cmb_" & uuid, True).First, ComboBox).Text
                                        End If
                                    ElseIf field.ctrtype = clsModel.ControlTypes.DateTimePicker Then
                                        xdetails.result = CType(Me.fbaseform.panelresult.Controls.Find("dtp_" & uuid, True).First, DateTimePicker).Value
                                    End If
                                    If xdetails.Oldlaboratoryresultid = 0 Then
                                        xdetails.Save(True)
                                    Else
                                        xdetails.Save(False)
                                    End If
                                End If
                            Next
                            Me.myFormaction = formaction.Release
                            fbaseform.panelresult.Controls.Clear()
                            fbaseform.lock()
                            loadDesign()

                        End If
                        If Me.requestStatus = clsModel.RequestStatus.released Then
                            Call clsadmissiondocuments.SaveLabResultImage(requestdetailno, Me.admissionid, fbaseform.GetFormImage(False), fbaseform.getResultFileName(), True)
                        End If

                End Select
                updateRequestStatus(4)
                MsgBox("Record successfully saved.", vbInformation, modGlobal.msgboxTitle)

                Me.myFormaction = formaction.Release
                Me.tsSave.Text = "Release"
                'Me.Close()
            End If
        ElseIf myFormaction = formaction.Release Then
            If MsgBox("Are you sure you want to release this examination?", MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                updateRequestStatus(5)
                modGlobal.SaveLog("Diagnostics", "Released examination ReqNo: " & Me.requestdetailno)
                Me.myFormaction = formaction.View
                Me.tsSave.Visible = False
                Me.tsPrint.Visible = True
                Me.tsprintas.Visible = Me.tsPrint.Visible
                Me.tsradtemplatemain.Visible = False
                If Me.isRTFForm() Then
                    If Me.labformatid = LabFormat.EchoForms Then
                        Me.CrystalReportToolStripMenuItem.Visible = False
                    End If
                Else
                    Me.DefaultPDFViewerToolStripMenuItem.Visible = False
                    Me.CrystalReportToolStripMenuItem.Visible = False
                    Me.tsMerging.Visible = True
                End If
            End If
        End If

    End Sub
    Public Sub updateRequestStatus(ByVal status As Integer)
        Dim Myrequest As New clsExaminationUpshots
        Myrequest.status = status
        Myrequest.patientreqdetailno = requestdetailno
        Myrequest.save(False)
    End Sub
    Private Sub loadTemplateList()
        If Directory.Exists("templates") = False Then
            MsgBox("Unable to locate template folder", MsgBoxStyle.Critical, msgboxTitle)
            Exit Sub
        End If
        If Me.labformatid = LabFormat.EchoForms Then
            generateTS("templates", LoadFromTemplateToolStripMenuItem, ".docx")
        Else
            generateTS("templates", LoadFromTemplateToolStripMenuItem, ".rtf")
        End If
    End Sub
    Private Sub generateTS(foldername As String, tsitem As ToolStripMenuItem, fileext As String)
        Dim di As New IO.DirectoryInfo(foldername)
        Dim arrFolders As IO.DirectoryInfo() = di.GetDirectories()
        For Each folder In arrFolders
            If folder.Name <> "temp" Then
                Dim tsfolder As New ToolStripMenuItem
                tsfolder.Text = folder.Name
                tsfolder.Image = My.Resources.ic_folder_16
                generateTS(foldername & "/" & folder.Name, tsfolder, fileext)
                If tsfolder.DropDownItems.Count > 0 Then
                    tsitem.DropDownItems.Add(tsfolder)
                End If
            End If
        Next
        Dim aryFi As IO.FileInfo() = di.GetFiles("*" & fileext)
        For Each fi In aryFi
            If Not fi.Name.Contains("headertemplatedoc") Then
                Dim tstemplate As New ToolStripMenuItem
                tstemplate.Text = fi.Name.Replace(fileext, "")
                tstemplate.Tag = fi.FullName
                tstemplate.Image = My.Resources.ic_template
                AddHandler tstemplate.Click, AddressOf tsTemplate_Click
                tsitem.DropDownItems.Add(tstemplate)
            End If
        Next
    End Sub
    Private Sub tsTemplate_Click(sender As System.Object, e As System.EventArgs)
        If Me.labformatid = LabFormat.EchoForms Then
            frmRadiology.processWordDocument(sender.Tag, True)
        Else
            frmRadiology.txtResult.LoadFile(sender.Tag, RichTextBoxStreamType.RichText)
        End If
    End Sub
    Private Sub tsClose_Click(sender As System.Object, e As System.EventArgs) Handles tsClose.Click
        Me.Close()
    End Sub
    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Dim f As New frmAddField(frmAddField.formstatus.add, New clsModel.LabControl())
        f.ShowDialog()
        If f.issave Then
            f.field.isvisible = True
            fbaseform.AddControl(f.field)
            Me.lstControls.Add(f.field)
            BindResultGrid()
        End If
    End Sub

    Private Sub txtpanelheight_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtpanelheight.TextChanged
        If afterload Then
            Try
                Call adjustPanelSize()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub tsSave_Click(sender As System.Object, e As System.EventArgs) Handles tsSave.Click
        Save()
    End Sub

    Private Sub btnpreview_Click(sender As System.Object, e As System.EventArgs) Handles btnpreview.Click
        Dim style As BorderStyle = BorderStyle.None
        If btnpreview.Text = "Close Preview" Then
            style = BorderStyle.FixedSingle
            btnpreview.Text = "Preview Design"
            btnpreview.BackColor = Color.CornflowerBlue
        Else
            btnpreview.Text = "Close Preview"
            btnpreview.BackColor = Color.MistyRose
        End If
        For Each field As clsModel.LabControl In Me.lstControls
            If field.isvisible Then
                CType(Me.fbaseform.panelresult.Controls.Find("panel_" & field.uuid, True).First, Panel).BorderStyle = style
            End If
        Next
        Me.fbaseform.panelresult.BorderStyle = style
    End Sub

    Public Sub removeControl(ByVal ctrname As String)
        Try
            Dim p As Panel = CType(Me.fbaseform.panelresult.Controls.Find(ctrname, True).First, Panel)
            Me.fbaseform.panelresult.Controls.Remove(p)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgvResult_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResult.CellContentClick
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvResult.Columns("name").Index Then
                Call showFieldForm(Me.dgvResult.Rows(e.RowIndex).Cells("uuid").Value)
            ElseIf e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
                Me.dgvResult.CommitEdit(DataGridViewDataErrorContexts.Commit)
                With Me.dgvResult.Rows(e.RowIndex)
                    Dim field As clsModel.LabControl = Me.lstControls.Where(Function(s) s.uuid = .Cells("uuid").Value).FirstOrDefault()
                    If .Cells(e.ColumnIndex).Value = False Then
                        removeControl("panel_" & field.uuid)
                    Else
                        fbaseform.AddControl(field)
                    End If
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub onPanelDoubleClick(uuid As String, locx As Integer, locy As Integer, width As Integer, length As Integer)
        Dim field = Me.lstControls.Where(Function(x) x.uuid = uuid).FirstOrDefault()
        field.loc.X = locx
        field.loc.Y = locy
        field.panelwidth = width
        field.panelheight = length
        showFieldForm(uuid)
    End Sub
    Private Sub showFieldForm(uuid As String)
        Dim field = Me.lstControls.Where(Function(x) x.uuid = uuid).FirstOrDefault()
        Dim f As New frmAddField(frmAddField.formstatus.edit, field)
        f.ShowDialog()
        If f.issave Then
            removeControl("panel_" & uuid)
            fbaseform.AddControl(field)
        End If
    End Sub

    Private Sub tsPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsPrint.Click
        Select Case Me.labformatid
            Case LabFormat.RADIOLOGY, LabFormat.ULTRASOUND, LabFormat.ECGREPORT, LabFormat.EchoForms
                frmRadiology.DisplayPrintPreview()
            Case LabFormat.CROSSMATCHING
                Me.fcrossmatch.PrintPreview()
            Case Else
                If Me.labformatid = LabFormat.GENERIC Then
                    For Each field As clsModel.LabControl In Me.lstControls
                        If field.isvisible AndAlso field.ctrtype = clsModel.ControlTypes.ParagraphField AndAlso CType(Me.fbaseform.panelresult.Controls.Find("panel_" & field.uuid, True).First, Panel).Tag <> "1" Then
                            Dim panel = CType(Me.fbaseform.panelresult.Controls.Find("panel_" & field.uuid, True).First, Panel)
                            If panel.Visible Then
                                Try
                                    Dim bmp As New Bitmap(panel.Width, panel.Height)
                                    Dim gr = Graphics.FromImage(bmp)
                                    gr.CopyFromScreen(panel.PointToScreen(Point.Empty), Point.Empty, panel.Size)
                                    Dim img As New PictureBox
                                    fbaseform.panelresult.Controls.Add(img)
                                    panel.Visible = False
                                    img.Location = panel.Location
                                    img.Size = panel.Size
                                    img.Image = bmp
                                Catch ex As Exception

                                End Try
                            End If
                        End If
                    Next
                End If
                Me.fbaseform.DisplayPrintPreview()
        End Select
    End Sub

    Private Sub DefaultPDFViewerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DefaultPDFViewerToolStripMenuItem.Click
        frmRadiology.DisplayPrintPreview(1)
    End Sub

    Private Sub CrystalReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CrystalReportToolStripMenuItem.Click
        frmRadiology.DisplayPrintPreview(2)
    End Sub

    Private Sub SaveAsNewTemplateToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveAsNewTemplateToolStripMenuItem.Click
        If frmRadiology.txtResult.Text <> "" Then
            Dim fd As New SaveFileDialog()
            fd.RestoreDirectory = True
            fd.InitialDirectory = Application.StartupPath & "\templates"
            fd.Filter = "Rich Text Files(*.rtf)|*rtf;"
            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim issave As Boolean = True
                Dim filename As String = fd.FileName
                If filename.Contains(".rtf") = False Then
                    filename = filename & ".rtf"
                End If
                Try
                    frmRadiology.txtResult.SaveFile(filename)
                Catch ex As Exception
                    MsgBox("Unable to save template. Existing template might be in use or you do not have access to folder.")
                End Try
            End If
        End If
    End Sub

    Private Sub ExternalTemplateManagementToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExternalTemplateManagementToolStripMenuItem.Click
        Process.Start(Application.StartupPath & "\templates")
    End Sub

    Private Sub tsMergeWithOtherResultToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsMergeWithOtherResultToolStripMenuItem.Click
        If tsMergeWithOtherResultToolStripMenuItem.Tag = "0" Then
            'Dim f As New frmSearchEngine
            'f.mModule = frmSearchEngine.ModuleName.MergeResult
            'f.mKey = Me.requestdetailno
            'f.ShowDialog()
            'If f.issave Then
            '    If MsgBox("Do you want to MERGE this result?", MsgBoxStyle.YesNo, msgboxTitle) = MsgBoxResult.Yes Then
            '        Me.mergeToogle = 1
            '        clsLaboratoryResult.mergeResult(Me.oldrequestdetailno, Me.laboratoryresultid, f.mKey)
            '        Me.Close()
            '    End If
            'End If
            Dim f As New frmSearchEngine
            f.mModule = frmSearchEngine.ModuleName.MergeResult
            f.mRequestDetailNo = Me.requestdetailno
            f.ShowDialog()
            If f.issave Then
                If MsgBox("Do you want to MERGE this result(s)?", MsgBoxStyle.YesNo, msgboxTitle) = MsgBoxResult.Yes Then
                    For Each row As DataRow In f.dtSelectedRecords.Rows
                        clsLaboratoryResult.mergeResult(Me.oldrequestdetailno, Me.laboratoryresultid, row.Item("patientrequestdetailno"))
                        Dim tsResult As New ToolStripMenuItem
                        tsResult.Text = "  ~ " & row.Item("itemspecs")
                        tsResult.Tag = row.Item("patientrequestdetailno")
                        Me.tsMerging.DropDownItems.Add(tsResult)
                        Me.tsMerging.Text = "Result Merging(" & Me.tsMerging.DropDownItems.Count - 1 & ")"
                    Next
                End If
            End If
        Else
            If MsgBox("Do you want to UNMERGE this result?", MsgBoxStyle.YesNo, msgboxTitle) = MsgBoxResult.Yes Then
                Me.mergeToogle = 2
                clsLaboratoryResult.unmergeResult(Me.oldrequestdetailno, Me.laboratoryresultid, 0)
                Me.Close()
            End If
        End If

    End Sub
    Private Sub checkMergeResults()
        Dim dt As DataTable = clsLaboratoryResult.genericcls(17, Me.requestdetailno)
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim tsResult As New ToolStripMenuItem
                tsResult.Text = "  ~ " & row.Item("itemspecs")
                tsResult.Tag = row.Item("patientrequestdetailno")
                Me.tsMerging.DropDownItems.Add(tsResult)
            Next
            Me.tsMerging.Text = "Result Merging(" & dt.Rows.Count & ")"
        Else
            Me.tsMerging.Text = "Result Merging"
        End If

    End Sub
    Private Sub exportasPDF(lock As Boolean)
        Dim fd As New SaveFileDialog()
        fd.RestoreDirectory = True
        fd.Filter = "PDF Files(*.pdf)|*pdf;"
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim filename As String = fd.FileName
            If filename.Contains(".pdf") = False Then
                filename = filename & ".pdf"
            End If
            Dim newPDFDoc As New PdfSharp.Pdf.PdfDocument
            Try
                Select Case Me.labformatid
                    Case LabFormat.RADIOLOGY, LabFormat.ULTRASOUND, LabFormat.ECGREPORT, LabFormat.EchoForms
                        If Not File.Exists(frmRadiology.resultpdflocation) Then
                            frmRadiology.DisplayPrintPreview(3)
                        End If
                        Dim sourcePDFDoc = PdfSharp.Pdf.IO.PdfReader.Open(frmRadiology.resultpdflocation, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import)
                        For Pg As Integer = 0 To sourcePDFDoc.Pages.Count - 1
                            newPDFDoc.AddPage(sourcePDFDoc.Pages(Pg))
                        Next
                        If lock Then
                            newPDFDoc.SecuritySettings.UserPassword = frmRadiology.patient.birthdate.ToString("yyyyMMdd")
                            newPDFDoc.SecuritySettings.OwnerPassword = "owner"
                        End If
                    Case Else
                        Dim page = newPDFDoc.AddPage()
                        If lock Then
                            newPDFDoc.SecuritySettings.OwnerPassword = "owner"
                            newPDFDoc.SecuritySettings.UserPassword = CDate(fbaseform.lblbirthdate.Text).ToString("yyyyMMdd")
                        End If
                        Dim sourceFileName = gDocumentLocationEMR & "\" & Me.admissionid & "\" & fbaseform.getResultFileName()
                        Call clsadmissiondocuments.SaveLabResultImage(requestdetailno, Me.admissionid, fbaseform.GetFormImage(False), fbaseform.getResultFileName(), True)
                        'Create XImage object from file.
                        Using xImg = PdfSharp.Drawing.XImage.FromFile(sourceFileName)
                            'Resize page Width and Height to fit image size.
                            page.Width = xImg.PixelWidth * 72 / xImg.HorizontalResolution
                            page.Height = xImg.PixelHeight * 72 / xImg.HorizontalResolution

                            'Draw current image file to page.
                            Dim GR = PdfSharp.Drawing.XGraphics.FromPdfPage(page)
                            GR.DrawImage(xImg, 0, 0, page.Width, page.Height)
                        End Using
                End Select
                newPDFDoc.Save(filename)
                MsgBox("PDF export successful!", MsgBoxStyle.OkOnly)
                Process.Start(New FileInfo(filename).Directory.FullName)
            Catch ex As Exception

            Finally
                newPDFDoc = Nothing
            End Try
        End If
    End Sub
    Private Sub ExportAsEmailAttachmentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExportAsEmailAttachmentToolStripMenuItem.Click
        Call exportasPDF(True)
    End Sub

    Private Sub ExportAsPDFToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExportAsPDFToolStripMenuItem.Click
        Call exportasPDF(False)
    End Sub
End Class