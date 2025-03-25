Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Management.Instrumentation
Imports System.Management
Imports SIMPLE_LIS.Utility
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions

Public Class frmtemplateRTF

    Public Sub New(ByVal requestdetailno As Long, ByVal laboratoryid As Long, ByVal labname As String, ByVal Islock As Boolean, ByVal status As Integer, ByVal labformatid As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.requestdetailno = requestdetailno
        Me.laboratoryid = laboratoryid
        Me.labname = labname
        Me.labformatid = labformatid
        Me.Islock = Islock
        Me.requestStatus = status
    End Sub
#Region "Variables"
    Private requestdetailno As Long
    Private laboratoryid As Long
    Private labname As String
    Private itemcode As String
    Private labformatid As Integer
    Private Islock As Boolean

    Private admissionid As Long
    Private labradiologyid As Long
    Private laboratoryresultid As Long
    Private chargeid As Long
    Public patient As New Patient
    Private ptno As String
    Private radiologistdesignation As String
    Private radiologistlicenseno As String
    Private requestStatus As Integer
    Public resultpdflocation As String

    Private tempfilename As String
    Private tempsafefilaname As String

    Private ImageStorage As String
    Private DocumentLocation As String
    Public resultlocation As String

    Dim dtHospitalInfo As New DataTable

    Dim getTransNo As DataTable
    Dim img As Image
    Public isSave As Boolean
    Private afterload As Boolean
#End Region
#Region "Events"
    Private Sub btnAddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToList.Click
        If Me.cmbItemCodeFilm.Text <> "" Then
            Dim isexist As Boolean
            For Each row As DataGridViewRow In DGVFilm.Rows
                If Me.cmbItemCodeFilm.SelectedValue = row.Cells(colitemcode.Index).Value Then
                    isexist = True
                End If
            Next
            If isexist Then
                MsgBox("Item already exist in the list", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Else
                Me.DGVFilm.Rows.Add(1)
                Me.DGVFilm.Rows(DGVFilm.Rows.Count - 1).Cells(colitemcode.Index).Value = Me.cmbItemCodeFilm.SelectedValue
                Me.DGVFilm.Rows(DGVFilm.Rows.Count - 1).Cells(colfilmname.Index).Value = Me.cmbItemCodeFilm.Text
                Me.DGVFilm.Rows(DGVFilm.Rows.Count - 1).Cells(colnooffilms.Index).Value = 1
            End If
        Else
            MsgBox("Please Select Film First")
        End If
    End Sub
    Private Sub DGVFilm_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVFilm.CurrentCellDirtyStateChanged
        If Me.DGVFilm.IsCurrentCellDirty Then
            Me.DGVFilm.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub DGVFilm_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVFilm.CellContentClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex = colremove.Index Then
                If Me.DGVFilm.SelectedRows(0).Cells(colchargedetailsid.Index).Value > 0 Then
                    If MessageBox.Show("Are you sure you want to remove this film?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim okDelete As Boolean = False
                        okDelete = clsRadiology.removeChargeDetails(Me.DGVFilm.SelectedRows(0).Cells(colchargedetailsid.Index).Value)
                        If okDelete Then
                            ' moveDeletedImage(Me.DGVFilm.SelectedRows(0).Cells(5).Value, Me.DGVFilm.SelectedRows(0).Cells(3).Value, 0)
                            Me.DGVFilm.Rows.Remove(Me.DGVFilm.CurrentRow)
                            MsgBox("Successfully Deleted")
                            ' myCharges.callInventoryDump(dtOfficeCode.Rows(0).Item("officecode").ToString(), Utility.GetServerDate())
                        End If
                    End If
                Else
                    Me.DGVFilm.Rows.Remove(Me.DGVFilm.CurrentRow)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmRadiology_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtHospitalInfo = clsLaboratoryResult.getHospitalInfo()
        Me.tbResult.TabPages.RemoveAt(2)
        Call LoadCombo()
        Call LoadRecord()
        If Islock Then
            lock()
        Else
            loadFonts()
        End If
        Me.tsSave.Image = modGlobal.save_icon
        Me.tsClose.Image = modGlobal.close_icon
        Me.KeyPreview = True
    End Sub

    Private Sub CenterImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenterImageToolStripMenuItem.Click
        resizeImg(PictureBoxSizeMode.CenterImage)
    End Sub
    Private Sub NormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem.Click
        resizeImg(PictureBoxSizeMode.Normal)
    End Sub
    Private Sub StretchImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StretchImageToolStripMenuItem.Click
        resizeImg(PictureBoxSizeMode.StretchImage)
    End Sub
    Private Sub AutoSizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoSizeToolStripMenuItem.Click
        resizeImg(PictureBoxSizeMode.AutoSize)
    End Sub
    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        resizeImg(PictureBoxSizeMode.Zoom)
    End Sub
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvImageAddress.CurrentCellDirtyStateChanged
        If dgvImageAddress.IsCurrentCellDirty Then
            Me.dgvImageAddress.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvImageAddress.CellClick
        Call readImageName(Me.dgvImageAddress.SelectedRows(0).Cells(collocation.Index).Value)
    End Sub


    Private Sub Picture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Picture.Click
        If Me.dgvImageAddress.Rows.Count > 0 And Me.Picture.Image IsNot img Then
            Try
                System.Diagnostics.Process.Start(Me.dgvImageAddress.SelectedRows(0).Cells(collocation.Index).Value)
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub ToolStripButtonbrowswimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonbrowswimage.Click
        If BrowseFile() Then
            Dim ok As Boolean = False
            For i = 0 To Me.dgvImageAddress.Rows.Count - 1
                If Me.dgvImageAddress.Rows(i).Cells(collocation.Index).Value = tempfilename Then
                    ok = True
                    i = Me.dgvImageAddress.Rows.Count - 1
                Else
                    ok = False
                End If
            Next
            '**********************
            If ok = False Then
                Me.dgvImageAddress.Rows.Add(1)
                Me.dgvImageAddress.Rows(Me.dgvImageAddress.Rows.Count - 1).Cells(collocation.Index).Value = tempfilename
                Me.dgvImageAddress.Rows(Me.dgvImageAddress.Rows.Count - 1).Cells(colimagename.Index).Value = tempsafefilaname
                Me.dgvImageAddress.Rows(Me.dgvImageAddress.Rows.Count - 1).Cells(colimageid.Index).Value = 0
                Try
                    Dim imageData As Byte() = ReadFile(tempfilename)
                    Dim newImage As Image
                    Using ms As New MemoryStream(imageData, 0, imageData.Length)
                        ms.Write(imageData, 0, imageData.Length)
                        newImage = Image.FromStream(ms, True)
                    End Using
                    Me.Picture.Image = newImage
                    Me.dgvImageAddress.Rows(Me.dgvImageAddress.Rows.Count - 1).Selected = True
                    Me.dgvImageAddress.CurrentCell = Me.dgvImageAddress(0, Me.dgvImageAddress.Rows.Count - 1)
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try
            Else
                MessageBox.Show("This image is selected already", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            ok = False
        End If
    End Sub
    Private Sub ToolStripButtonremovethisimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonremovethisimage.Click
        Try
            If Me.dgvImageAddress.SelectedRows(0).Index >= 0 Then
                If MessageBox.Show("Are you sure you want to remove this image?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Me.dgvImageAddress.SelectedRows(0).Cells(colimageid.Index).Value > 0 Then
                        Dim okToDelete As Boolean = clsRadiology.removeImages(Me.dgvImageAddress.SelectedRows(0).Cells(colimageid.Index).Value)
                        If okToDelete Then
                            moveImage(Me.dgvImageAddress.SelectedRows(0).Cells(collocation.Index).Value, Me.dgvImageAddress.SelectedRows(0).Cells(colimagename.Index).Value, True)
                        End If
                    Else
                        Me.dgvImageAddress.Rows.RemoveAt(Me.dgvImageAddress.SelectedRows(0).Index)
                        Me.Picture.Image = img
                    End If
                End If
            Else
                MsgBox("Please select an image to remove")
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region
#Region "Methods"
    Private Function BrowseFile() As Boolean
        Dim dlg As New OpenFileDialog()
        dlg.Filter = "Image Files|*.jpg;*.bmp;*.png;*.jpeg"
        Dim dlgRes As DialogResult = dlg.ShowDialog()
        If dlgRes = DialogResult.OK Then
            Me.tempfilename = dlg.FileName
            Me.tempsafefilaname = dlg.SafeFileName
            Return True
        End If
        Return False
    End Function
    Public Sub saveNow(ByRef action As String)
        'If MsgBox("Are you sure you want to " & action & " this laboratory result?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, modGlobal.msgboxTitle) = MsgBoxResult.Yes Then
        If Me.cmbRadTech.Text = "" Then
            SetErrorProvider(cmbRadTech, "Select employee.")
            Exit Sub
        End If
        If Me.cmbradiologist.Text = "" Then
            SetErrorProvider(cmbradiologist, "Select doctor.")
            Exit Sub
        End If
        SetErrorProvider(cmbRadTech, "", False)
        SetErrorProvider(cmbradiologist, "", False)

        isSave = True
        Call SaveRecord()
        If Me.requestStatus = clsModel.RequestStatus.released Then
            If Me.labformatid <> LabFormat.EchoForms Then
                Call Me.generatePDF()
            End If
        End If
        'MsgBox("Laboratory result successfully " + action + "d.", MsgBoxStyle.OkOnly, modGlobal.msgboxTitle)
        Islock = True

        'End If
    End Sub
    Public Sub lock()
        Me.DGVFilm.ReadOnly = True
        Me.DGVFilm.Columns(colremove.Index).Visible = False
        Me.paneladdfilm.Visible = False
        Me.ToolStripButtonbrowswimage.Visible = False
        Me.ToolStripSeparator1.Visible = False
        Me.ToolStripButtonremovethisimage.Visible = False
        Me.ToolStripSeparator2.Visible = False
        Me.dgvImageAddress.ReadOnly = True
        Me.txtResult.ReadOnly = True
        Me.dtDate.Enabled = False
        Me.cmbRadTech.Enabled = False
        Me.cmbradiologist.Enabled = False
        Me.paneleditortools.Visible = False
    End Sub
    Public Function moveImage(ByVal sourcePath As String, ByVal imageName As String, ByVal isdelete As Boolean) As String
        Dim targetlocation As String = ImageStorage & "\Templates"
        If isdelete Then
            targetlocation = ImageStorage & "\Deleted"
            If Directory.Exists(targetlocation) = False Then
                Directory.CreateDirectory(targetlocation)
                Dim diMyDir As New DirectoryInfo(targetlocation)
                diMyDir.Attributes = FileAttributes.Hidden
            End If
            imageName = Me.requestdetailno & "_" & imageName
        End If
        If System.IO.File.Exists(sourcePath) Then
            Dim i As Integer = 1
            While System.IO.File.Exists(targetlocation & "\" & imageName)
                imageName = imageName & "_" + i
            End While
            System.IO.File.Copy(sourcePath, targetlocation & "\" & imageName)
        Else
            imageName = ""
        End If
        Return imageName
    End Function
    Private Sub resizeImg(ByVal sender As String)
        If Me.Picture.Image IsNot img Then
            Me.Picture.SizeMode = sender
        End If
    End Sub
    Private Function ReadFile(ByVal sPath As String) As Byte()
        Dim data As Byte() = Nothing
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fStream)
        data = br.ReadBytes(CInt(numBytes))
        Return data
    End Function
    Private Sub LoadRecord()
        Try
            Call LoadFilms()
            Dim dtResult As DataTable = clsRadiology.getRadiologyResultDetails(requestdetailno, 9)
            If Me.labformatid = LabFormat.EchoForms Then
                Me.Text = "VASCULAR"
                Me.chkesig.Visible = False
            ElseIf Me.laboratoryid = 10 Then
                Me.Text = "RADIOLOGY"
            Else
                Me.Text = "ULTRASOUND"
            End If
            If dtResult.Rows(0).Item("gender").ToString = "M" Then
                Me.txtGender.Text = "Male"
            Else
                Me.txtGender.Text = "Female"
            End If
            Me.admissionid = dtResult.Rows(0).Item("admissionid").ToString

            Me.txtPatientname.Text = dtResult.Rows(0).Item("patient").ToString
            Me.txtAge.Text = dtResult.Rows(0).Item("age").ToString
            Me.lblexamination.Text = dtResult.Rows(0).Item("itemspecs").ToString
            Me.lblchiefcomplaint.Text = dtResult.Rows(0).Item("chiefcomplaints").ToString
            Me.patient.mobileno = dtResult.Rows(0).Item("mobileno").ToString
            Me.lblrequestedby.Text = dtResult.Rows(0).Item("requestingphysician").ToString
            Me.laboratoryresultid = dtResult.Rows(0).Item("laboratoryresultid")
            Me.labradiologyid = dtResult.Rows(0).Item("labradiologyid")
            If Me.laboratoryresultid = 0 Then
                Me.cmbRadTech.SelectedValue = modGlobal.userid
            Else
                Me.cmbRadTech.SelectedValue = dtResult.Rows(0).Item("radtechid")
            End If
            Me.cmbradiologist.SelectedValue = dtResult.Rows(0).Item("radiologistid")
            Me.patient.patientid = dtResult.Rows(0).Item("patientid")
            Me.patient.patientfullname = dtResult.Rows(0).Item("patient")
            Me.patient.birthdate = Utility.NullToDefaultDateFormat(dtResult.Rows(0).Item("birthdate"))
            Me.ptno = Utility.NullToEmptyString(dtResult.Rows(0).Item("ptno"))
            Me.patient.hospitalno = Utility.NullToEmptyString(dtResult.Rows(0).Item("hospitalno"))
            Me.patient.homeaddress = Utility.NullToEmptyString(dtResult.Rows(0).Item("homeaddress"))
            Me.patient.email = Utility.NullToEmptyString(dtResult.Rows(0).Item("emailaddress"))
            Me.patient.nationality = Utility.NullToEmptyString(dtResult.Rows(0).Item("nationality"))
            Me.patient.height = Utility.NullToEmptyString(dtResult.Rows(0).Item("height"))
            Me.patient.weight = Utility.NullToEmptyString(dtResult.Rows(0).Item("weight"))
            Me.radiologistdesignation = Utility.NullToEmptyString(dtResult.Rows(0).Item("radiologistdesignation"))
            Me.radiologistlicenseno = Utility.NullToEmptyString(dtResult.Rows(0).Item("radiologistlicense"))
            Me.dtDate.Value = Utility.NullToCurrentDate(dtResult.Rows(0).Item("dateencoded"))
            Me.chkesig.Checked = Utility.NullToBoolean(dtResult.Rows(0).Item("esigradiologist"))
            Me.txtward.Text = Utility.NullToEmptyString(dtResult.Rows(0).Item("ward"))
            If modGlobal.hospitalcode = Constant.Facility.ecomed Then
                Me.lblward.Visible = False
                Me.txtward.Visible = False
            End If

            Dim dtResultImages As DataTable = clsRadiology.getRadiologyResultDetailsImages(requestdetailno, 4)
            For i = 0 To dtResultImages.Rows.Count - 1
                Me.dgvImageAddress.Rows.Add(1)
                Me.dgvImageAddress.Rows(i).Cells(colimageid.Index).Value = dtResultImages.Rows(i)("imageid")
                Me.dgvImageAddress.Rows(i).Cells(colimagename.Index).Value = dtResultImages.Rows(i)("image")
                Me.dgvImageAddress.Rows(i).Cells(colimagedesc.Index).Value = dtResultImages.Rows(i)("description")
                Me.dgvImageAddress.Rows(i).Cells(collocation.Index).Value = ImageStorage & "\Templates\" + dtResultImages.Rows(i)("image").ToString()
            Next

            Dim dtChargeResultDetails As DataTable = clsCharges.getResultCharges(8, "", requestdetailno)
            For ctr = 0 To dtChargeResultDetails.Rows.Count - 1
                Me.DGVFilm.Rows.Add(1)
                Me.DGVFilm.Rows(ctr).Cells(colchargedetailsid.Index).Value = dtChargeResultDetails.Rows(ctr).Item("chargedetailsid")
                Me.DGVFilm.Rows(ctr).Cells(colitemcode.Index).Value = dtChargeResultDetails.Rows(ctr).Item("itemcode").ToString()
                Me.DGVFilm.Rows(ctr).Cells(colfilmname.Index).Value = dtChargeResultDetails.Rows(ctr).Item("itemdescription").ToString()
                Me.DGVFilm.Rows(ctr).Cells(colnooffilms.Index).Value = dtChargeResultDetails.Rows(ctr).Item("quantity")
                Me.chargeid = dtChargeResultDetails.Rows(ctr).Item("chargeid")
            Next
            DocumentLocation = Utility.readSetting(modGlobal.appSettings.DocumentLocationEMR)
            If DocumentLocation <> "" Then
                ImageStorage = DocumentLocation & "\"
            End If
            Dim dt As DataTable = clsadmissiondocuments.getDocument(6, Me.requestdetailno, Me.admissionid)
            If (dt.Rows.Count > 0) Then
                resultpdflocation = String.Format("{0}{1}\{2}", ImageStorage, Me.admissionid, dt.Rows(0).Item("documentname"))
            Else
                resultpdflocation = String.Format("{0}{1}\{2}.pdf", ImageStorage, Me.admissionid, generateFileName())
            End If
            If Me.labformatid = LabFormat.EchoForms Then
                resultlocation = String.Format("{0}{1}\results\{2}.docx", ImageStorage, Me.admissionid, Me.requestdetailno)
                If File.Exists(resultpdflocation) Then
                    AxAcroPDFCurrent.LoadFile(resultpdflocation)
                    Me.tseditresultpdf.Visible = True
                ElseIf File.Exists(resultlocation) Then
                    Me.processWordDocument("", False)
                    Me.tscontainerwordeditor.Visible = True
                ElseIf File.Exists("templates\NoResult.pdf") Then
                    AxAcroPDFCurrent.LoadFile("templates\NoResult.pdf")
                End If
                Me.tscontainerwordeditor.Visible = True
                Me.txtResult.Visible = False
                Me.tscontainerrtfeditor.Visible = False
                Me.AxAcroPDFCurrent.Visible = True
            Else
                resultlocation = String.Format("{0}{1}\results\{2}.rtf", ImageStorage, Me.admissionid, Me.requestdetailno)
                If File.Exists(resultlocation) Then
                    txtResult.LoadFile(resultlocation)
                ElseIf File.Exists("templates\default.rtf") Then
                    txtResult.LoadFile("templates\default.rtf")
                End If
            End If
            ImageStorage = String.Format("{0}{1}\results\", ImageStorage, Me.admissionid)
            If Directory.Exists(ImageStorage) = False Then
                Directory.CreateDirectory(ImageStorage)
            End If
        Catch ex As Exception
        End Try
        img = Me.Picture.Image
        loadPreviousResult()
    End Sub
    Private Sub loadPreviousResult()
        Dim dt As DataTable = clsRadiology.getPreviousResults(Me.patient.patientid, Me.requestdetailno)
        With Me.cmbpreviousresult
            .DataSource = dt
            .DisplayMember = "testdesc"
            .ValueMember = "patientrequestdetailno"
            If .Items.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
    End Sub
    Private Sub readImageName(ByVal dgv As String)
        Try
            Dim imageData As Byte() = ReadFile(dgv)
            Dim newImage As Image
            Using ms As New MemoryStream(imageData, 0, imageData.Length)
                ms.Write(imageData, 0, imageData.Length)
                newImage = Image.FromStream(ms, True)
            End Using
            Me.Picture.Image = newImage
            Me.Picture.SizeMode = PictureBoxSizeMode.StretchImage
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub LoadCombo()
        Try
            If Me.labformatid = LabFormat.EchoForms Or labformatid = LabFormat.ECGREPORT Then
                Me.cmbradiologist.DataSource = clsRadiology.getRadiologist(clsModel.EmployeeTypes.cardiologist)
            Else
                Me.cmbradiologist.DataSource = clsRadiology.getRadiologist(clsModel.EmployeeTypes.radiologist)
            End If
            Me.cmbradiologist.DisplayMember = "radiologist"
            Me.cmbradiologist.ValueMember = "employeeid"
            Me.cmbradiologist.SelectedIndex = 0

            Me.cmbRadTech.DataSource = clsRadiology.getRadiologist(clsModel.EmployeeTypes.radtech)
            Me.cmbRadTech.DisplayMember = "radiologist"
            Me.cmbRadTech.ValueMember = "employeeid"
            Me.cmbRadTech.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadFilms()
        Me.cmbItemCodeFilm.DataSource = clsLabRadiology.getItemCodeFilm()
        Me.cmbItemCodeFilm.DisplayMember = "ItemDescription"
        Me.cmbItemCodeFilm.ValueMember = "ItemCode"
    End Sub

    Private Sub SaveRecord()
        '******************** save labresult
        Dim myLaboratoryResult As New clsLaboratoryResult
        With myLaboratoryResult

            .laboratoryid = Me.laboratoryid
            .admissionid = Me.admissionid
            .patientrequestno = Me.requestdetailno
            .specimen = Me.lblexamination.Text
            .datesubmitted = GetServerDate()
            .dateencoded = dtDate.Value
            .encodedby = userid
            .medtech = cmbRadTech.SelectedValue
            .medicaltechnologist = .medtech
            .esigpatho = Me.chkesig.Checked
            .releasedby = 1
            .datereleased = GetServerDate() '"01/01/1990"
            .pathologist = Me.cmbradiologist.SelectedValue
            If Me.laboratoryresultid = 0 Then
                .Save(True)
                Call SaveLog("Radiology", "New Radiology result:" & .patientrequestno, userid)
            Else
                .Oldlaboratoryid = Me.laboratoryresultid
                .Save(False)
                Call SaveLog("Radiology", "Update Radiology result:" & .patientrequestno, userid)
            End If
        End With

        Dim myRadiology As New clsRadiology
        Dim myCharges As New clsCharges

        '************************ save radiology
        myRadiology.labexaminationno = myLaboratoryResult.labno
        myRadiology.resultdescription = "" 'Me.txtResult.Text
        myRadiology.patientrequestdetailno = Me.requestdetailno
        myRadiology.admissionid = Me.admissionid
        myRadiology.remarks = ""
        If Me.labradiologyid = 0 Then
            myRadiology.Oldlabradiologyid = 0
            myRadiology.soperation = 0
            myRadiology.Save(True)
        Else
            myRadiology.Oldlabradiologyid = Me.labradiologyid
            myRadiology.soperation = 0
            myRadiology.Save(False)
        End If
        If Me.labformatid <> LabFormat.EchoForms Then
            Me.txtResult.SaveFile(resultlocation)
        End If

        If Me.DGVFilm.Rows.Count > 0 Then
            '******************* save charges
            myCharges.transtype = "XR"
            Dim officecode As DataTable = clsCharges.getOfficeCode(modGlobal.userid)
            myCharges.officecode = officecode.Rows(0).Item("officecode").ToString()
            getTransNo = clsCharges.getTransNo(myCharges.officecode, myCharges.transtype) 'datatTable get transNO
            myCharges.transno = getTransNo.Rows(0).Item("documentno").ToString()
            myCharges.preparedbyid = userid
            myCharges.dateprepared = GetServerDate()
            myCharges.dateapproved = GetServerDate() '"1/1/1900 0:00:00 PM"
            myCharges.remarks = "X-RAY/Ultrasound"

            myCharges.patientno = Me.patient.patientid
            myCharges.patientname = Me.patient.patientfullname
            myCharges.registryno = myLaboratoryResult.admissionid ' or admission id
            If Me.chargeid = 0 Then
                Me.chargeid = myCharges.Save(True)
            Else
                myCharges.SaveUpdate(False)
            End If

            '******************** save charge details
            myCharges.DeleteAllChargeDetailsForXray(requestdetailno)
            For ctr = 0 To Me.DGVFilm.Rows.Count - 1
                myCharges.itemcode = Me.DGVFilm.Rows(ctr).Cells(colitemcode.Index).Value 'myLaboratoryResult.itemcatcode
                myCharges.lotno = Me.DGVFilm.Rows(ctr).Cells(colitemcode.Index).Value
                myCharges.expirydate = GetServerDate() '"1/1/1900 0:00:00 PM"
                myCharges.quantity = Me.DGVFilm.Rows(ctr).Cells(colnooffilms.Index).Value
                'If Me.DGVFilm.Rows(ctr).Cells(3).Value = CheckState.Checked Or Me.DGVFilm.Rows(ctr).Cells(3).Value = True Then
                myCharges.remarks = "Film Used"
                'Else
                'myCharges.remarks = "Spoils"
                'End If
                myCharges.refadmissionchargeno = requestdetailno
                myCharges.search = Me.DGVFilm.Rows(ctr).Cells(colchargedetailsid.Index).Value
                myCharges.admissionchargeno = Me.chargeid
                If myCharges.search = 0 Then
                    myCharges.SaveDetails(True)
                Else
                    myCharges.SaveUpdateDetails(False)
                End If
            Next
        End If

        '******************** save images
        If Me.dgvImageAddress.Rows.Count > 0 AndAlso Directory.Exists(ImageStorage & "\Templates") = False Then
            Directory.CreateDirectory(ImageStorage)
            Directory.CreateDirectory(ImageStorage & "\Templates")
            Dim managementClass As New ManagementClass("Win32_Share")
            Dim inParams As ManagementBaseObject = managementClass.GetMethodParameters("Create")
            inParams.Item("Description") = "My Files Share"
            inParams.Item("Name") = "My Files Share"
            inParams.Item("Path") = ImageStorage
            inParams.Item("Type") = 0
            Dim diMyDir As New DirectoryInfo(ImageStorage)
            diMyDir.Attributes = FileAttributes.Hidden
            Dim diMyDir1 As New DirectoryInfo(ImageStorage & "\Templates")
            diMyDir1.Attributes = FileAttributes.Hidden
        End If
        For Each row As DataGridViewRow In Me.dgvImageAddress.Rows
            If row.Cells(colimageid.Index).Value = 0 Then
                myRadiology.image = moveImage(row.Cells(collocation.Index).Value, row.Cells(colimagename.Index).Value, False)
            Else
                myRadiology.image = row.Cells(colimagename.Index).Value
            End If
            If myRadiology.image = "" Then
                MsgBox("Unable to save file " & row.Cells(colimagename.Index).Value)
            Else
                myRadiology.imageid = row.Cells(colimageid.Index).Value
                myRadiology.description = row.Cells(colimagedesc.Index).Value
                myRadiology.saveImage(myRadiology.imageid = 0)
            End If
        Next
        isSave = True
        myRadiology = Nothing
        myLaboratoryResult = Nothing
    End Sub
#End Region
#Region "Printing"
    Public Sub DisplayPrintPreview(Optional tool As Integer = 0)
        'If Me.txtResult.Rtf.Contains("trowd") Then
        If Me.labformatid = LabFormat.EchoForms Then
            processWordDocument("")
        Else
            Dim dt As DataTable = clsadmissiondocuments.getDocument(6, Me.requestdetailno, Me.admissionid)
            If (dt.Rows.Count > 0) Then
                If File.Exists(DocumentLocation & "\" & dt.Rows(0).Item("documentlocation")) Then
                    resultpdflocation = DocumentLocation & "\" & dt.Rows(0).Item("documentlocation")
                Else
                    Call generatePDF()
                End If
            Else
                Call generatePDF()
            End If
        End If
        If tool = 0 Then
            Dim f As New frmPDFViewer(resultpdflocation, True, True)
            f.ShowDialog()
        ElseIf tool = 1 Then
            Process.Start(resultpdflocation)
        ElseIf tool = 2 Then
            Try
                Dim f As New frmReportHandler
                Dim crv As New crptRadTemplate
                Dim dt As DataTable = clsRadiology.genericcls(9, Me.requestdetailno)
                dt.Rows(0).Item("result") = Me.txtResult.Rtf
                crv.SetDataSource(dt)
                f.crvPrinting.ReportSource = crv
                f.ShowDialog()
            Catch ex As Exception

            End Try
        End If
        'Else

        'End If
    End Sub
    Public Function generateFileName() As String
        Return Utility.RemoveIllegalFileNameChars(Me.lblexamination.Text, "_") & "_" & requestdetailno
    End Function
    Public Sub generatePDF()
        Dim wordApplication As New Microsoft.Office.Interop.Word.Application
        Dim wordDocument As Microsoft.Office.Interop.Word.Document = Nothing
        Dim tempfilename = Application.StartupPath() & "\templates\temp\" & Utility.GetRandomString() & ".docx"
        Dim mastertemplate As String = "templates\headertemplatedoc_generic.docx"
        If Not chkesig.Checked And File.Exists("templates\headertemplatedoc_" & cmbradiologist.SelectedValue & "_noesig.docx") Then
            mastertemplate = "templates\headertemplatedoc_" & cmbradiologist.SelectedValue & "_noesig.docx"
        ElseIf File.Exists("templates\headertemplatedoc_" & cmbradiologist.SelectedValue & ".docx") Then
            mastertemplate = "templates\headertemplatedoc_" & cmbradiologist.SelectedValue & ".docx"
        End If
        Try
            File.Copy(mastertemplate, tempfilename, True)
            Me.txtResult.SelectAll()
            Me.txtResult.Copy()
            wordDocument = wordApplication.Documents.Open(tempfilename)
            wordDocument.Activate()

            FindAndReplaceDocument(wordDocument)

            wordApplication.Selection.Paste()
            wordDocument.Save()

            Dim fi As New FileInfo(resultpdflocation)


            If Not wordDocument Is Nothing Then
                wordDocument.ExportAsFixedFormat(fi.FullName, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument, 0, 0, Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent, True, True, Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks, True, True, False)
                Call clsadmissiondocuments.SaveAdmissionDocument(requestdetailno, Me.admissionid, resultpdflocation)
            End If
        Catch ex As Exception
            'TODO: handle exception
        Finally
            If Not wordDocument Is Nothing Then
                wordDocument.Close(False)
                wordDocument = Nothing
            End If

            If Not wordApplication Is Nothing Then
                wordApplication.Quit()
                wordApplication = Nothing
            End If
        End Try
        Try
            File.Delete(tempfilename)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub processWordDocument(sourceTemplate As String, Optional isEdit As Boolean = False)
        If sourceTemplate <> "" AndAlso File.Exists(resultlocation) Then
            If MsgBox("Do you want to replace existing result?", MsgBoxStyle.YesNo, msgboxTitle) <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        Dim wordApplication As New Microsoft.Office.Interop.Word.Application
        Dim wordDocument As Microsoft.Office.Interop.Word.Document = Nothing
        Dim tempfilename = resultlocation
        Try
            If sourceTemplate <> "" Then
                File.Copy(sourceTemplate, resultlocation, True)
            End If
            If Not isEdit Then
                tempfilename = Application.StartupPath() & "\templates\temp\" & Utility.GetRandomString() & ".docx"
                File.Copy(resultlocation, tempfilename, True)
            End If
            Dim finfo As New FileInfo(tempfilename)
            wordDocument = wordApplication.Documents.Open(finfo.FullName)
            wordDocument.Activate()
            Try
                With wordDocument
                    If isEdit Then
                        For Each field As Microsoft.Office.Interop.Word.FormField In wordDocument.FormFields
                            Select Case field.Name
                                Case "lblpatientname"
                                    field.Result = txtPatientname.Text
                                Case "lbldateprinted"
                                    field.Result = Utility.GetServerDate().ToString(defaultdateformat)
                                Case "lbltestdate"
                                    field.Result = Me.dtDate.Value.ToString(defaultdateformat)
                                Case "lblpatientaddress"
                                    field.Result = Me.patient.homeaddress
                                Case "lblptno"
                                    field.Result = Me.ptno
                                Case "lblhospno"
                                    field.Result = Me.patient.hospitalno
                                Case "lblward"
                                    field.Result = Me.txtward.Text
                                Case "lblchiefcomplaint"
                                    field.Result = Me.lblchiefcomplaint.Text
                                Case "lblage"
                                    field.Result = txtAge.Text
                                Case "lblgender"
                                    field.Result = txtGender.Text
                                Case "lblrequestedby"
                                    field.Result = Me.lblrequestedby.Text
                                Case "lblcontactno"
                                    field.Result = Me.patient.mobileno
                                Case "lblemail"
                                    field.Result = Me.patient.email
                                Case "lblnationality"
                                    field.Result = Me.patient.nationality
                                Case "lblheight"
                                    field.Result = Me.patient.height
                                Case "lblweight"
                                    field.Result = Me.patient.weight
                            End Select
                        Next
                    Else
                        For Each field As Microsoft.Office.Interop.Word.FormField In wordDocument.FormFields
                            Select Case field.Name
                                Case "lblpatientname"
                                    field.Range.Text = txtPatientname.Text
                                Case "lbldateprinted"
                                    field.Range.Text = Utility.GetServerDate().ToString(defaultdateformat)
                                Case "lbltestdate"
                                    field.Range.Text = Me.dtDate.Value.ToString(defaultdateformat)
                                Case "lblpatientaddress"
                                    field.Range.Text = Me.patient.homeaddress
                                Case "lblptno"
                                    field.Result = Me.ptno
                                Case "lblchiefcomplaint"
                                    field.Range.Text = Me.lblchiefcomplaint.Text
                                Case "lblage"
                                    field.Range.Text = txtAge.Text
                                Case "lblgender"
                                    field.Range.Text = txtGender.Text
                                Case "lblrequestedby"
                                    field.Range.Text = Me.lblrequestedby.Text
                                Case "lblcontactno"
                                    field.Range.Text = Me.patient.mobileno
                                Case "lblnationality"
                                    field.Range.Text = Me.patient.nationality
                                Case "lblemail"
                                    field.Range.Text = Me.patient.email
                                Case "lblnationality"
                                    field.Range.Text = Me.patient.nationality
                                Case "lblheight"
                                    field.Range.Text = Me.patient.height
                                Case "lblweight"
                                    field.Range.Text = Me.patient.weight
                            End Select
                        Next
                    End If

                    FindAndReplaceDocument(wordDocument)

                End With
            Catch ex As Exception

            End Try
            wordDocument.Save()
            If Not isEdit Then
                Dim fi As New FileInfo(resultpdflocation)
                wordDocument.ExportAsFixedFormat(fi.FullName, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, False, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument, 0, 0, Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent, True, True, Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks, True, True, False)
                Call clsadmissiondocuments.SaveAdmissionDocument(requestdetailno, Me.admissionid, resultpdflocation)
            End If
        Catch ex As Exception
            'TODO: handle exception
        Finally
            If Not wordDocument Is Nothing Then
                wordDocument.Close(False)
                wordDocument = Nothing
            End If

            If Not wordApplication Is Nothing Then
                wordApplication.Quit()
                wordApplication = Nothing
            End If
        End Try
        If isEdit Then
            Try
                System.Diagnostics.Process.Start(resultlocation)
            Catch ex As Exception
            End Try
        Else
            Try
                Me.AxAcroPDFCurrent.LoadFile("Empty")
                Me.AxAcroPDFCurrent.LoadFile(resultpdflocation)
                'Me.AxAcroPDFCurrent.src = outputFilename
                'Me.AxAcroPDFCurrent.Show()
            Catch ex As Exception

            End Try
        End If
        Try
            If resultlocation <> tempfilename Then
                File.Delete(tempfilename)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FindAndReplaceDocument(ByRef wordDocument As Microsoft.Office.Interop.Word.Document)
        For Each r As Microsoft.Office.Interop.Word.Range In wordDocument.StoryRanges
            With r.Find
                .Text = "{patientname}"
                .Replacement.Text = txtPatientname.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{age_sex}"
                .Replacement.Text = txtAge.Text & "/" & txtGender.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{age}"
                .Replacement.Text = txtAge.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{sex}"
                .Replacement.Text = txtGender.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{date}"
                .Replacement.Text = dtDate.Value.ToString("MM/dd/yyyy hh:mm tt")
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{dateonly}"
                .Replacement.Text = dtDate.Value.ToString("MM/dd/yyyy")
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{print_date}"
                .Replacement.Text = Utility.GetServerDate().ToString("MM/dd/yyyy hh:mm tt")
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{examination}"
                .Replacement.Text = Me.lblexamination.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{patientno}"
                .Replacement.Text = Me.ptno
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{hospitalno}"
                .Replacement.Text = Me.patient.hospitalno
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{birthdate}"
                .Replacement.Text = Me.patient.birthdate.ToShortDateString
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{patientaddress}"
                .Replacement.Text = Me.patient.homeaddress
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{nationality}"
                .Replacement.Text = Me.patient.nationality
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{civilstatus}"
                .Replacement.Text = Me.patient.civilstatus
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{reqphysician}"
                .Replacement.Text = Me.lblrequestedby.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{ward}"
                .Replacement.Text = Me.txtward.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{doctorsname}"
                .Replacement.Text = Me.cmbradiologist.Text
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{doctorsdesignation}"
                .Replacement.Text = Me.radiologistdesignation
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
            With r.Find
                .Text = "{doctorslicense}"
                .Replacement.Text = Me.radiologistlicenseno
                .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                .Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)
            End With
        Next
    End Sub
#End Region

    Private Sub DGVFilm_CellPainting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DGVFilm.CellPainting
        If e.ColumnIndex = colremove.Index AndAlso e.RowIndex >= 0 Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)
            Dim bmpFind As Bitmap = My.Resources.delete_16x16
            Dim ico As Icon = Icon.FromHandle(bmpFind.GetHicon)
            e.Graphics.DrawIcon(ico, e.CellBounds.Left + 8, e.CellBounds.Top + 3)
            e.Handled = True
        End If
    End Sub


    Private Sub cmbpreviousresult_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbpreviousresult.SelectedValueChanged
        Try
            If Me.cmbpreviousresult.SelectedValue > 0 Then
                Dim dtResult As DataTable = clsExamination.genericcls(9, Me.cmbpreviousresult.SelectedValue)
                If dtResult.Rows(0).Item("labformatid") = LabFormat.EchoForms Then
                    Dim dt As DataTable = clsadmissiondocuments.getDocument(6, Me.cmbpreviousresult.SelectedValue, Me.admissionid)
                    If (dt.Rows.Count > 0) Then
                        Dim prevrtfLocation = String.Format("{0}\{1}\{2}", DocumentLocation, Me.admissionid, dt.Rows(0).Item("documentname"))
                        If File.Exists(prevrtfLocation) Then
                            Me.AxAcroPDFPrevious.LoadFile(prevrtfLocation)
                        End If
                    End If
                    Me.AxAcroPDFPrevious.Visible = True
                    Me.txtpreviousresult.Visible = False
                Else
                    Dim prevrtfLocation = String.Format("{0}\{1}\results\{2}.rtf", DocumentLocation, dtResult.Rows(0).Item("admissionid"), Me.cmbpreviousresult.SelectedValue)
                    If File.Exists(prevrtfLocation) Then
                        Me.txtpreviousresult.LoadFile(prevrtfLocation)
                    End If
                    Me.AxAcroPDFPrevious.Visible = False
                    Me.txtpreviousresult.Visible = True
                End If

            End If
        Catch ex As Exception
            Me.txtpreviousresult.Text = ""
        End Try
    End Sub
#Region "Text editor"
    Private Enum formating
        bold
        underline
        italize
        alignleft
        aligncenter
        alignright
        undo
        redo
        fontfamily
        fontSize
        fontColor
    End Enum
    Private Sub loadFonts()
        afterload = False
        Dim installedFonts As New System.Drawing.Text.InstalledFontCollection
        Dim fontFamilies = installedFonts.Families()
        With Me.tsfontfamily.ComboBox
            .DataSource = fontFamilies
            .ValueMember = "name"
            .DisplayMember = "name"
        End With
        With Me.tsfonsize.ComboBox
            .Items.AddRange({"8", "9", "10", "11", "12", "14", "16", "18", "20", "24"})
        End With
        afterload = True
    End Sub
    Private Sub formatText(format As formating)
        Dim ffont As Font
        If txtResult.SelectionFont Is Nothing Then
            ffont = txtResult.Font
        Else
            ffont = txtResult.SelectionFont
        End If
        Dim fstyle = ffont.Style
        Dim isfontstyle As Boolean = False
        Select Case format
            Case formating.bold
                If ffont.Bold Then
                    fstyle = fstyle And Not FontStyle.Bold
                Else
                    fstyle = fstyle Or FontStyle.Bold
                End If
                isfontstyle = True
            Case formating.italize
                If ffont.Italic Then
                    fstyle = fstyle And Not FontStyle.Italic
                Else
                    fstyle = fstyle Or FontStyle.Italic
                End If
                isfontstyle = True
            Case formating.underline
                If ffont.Underline Then
                    fstyle = fstyle And Not FontStyle.Underline
                Else
                    fstyle = fstyle Or FontStyle.Underline
                End If
                isfontstyle = True
            Case formating.alignleft
                txtResult.SelectionAlignment = HorizontalAlignment.Left
            Case formating.aligncenter
                txtResult.SelectionAlignment = HorizontalAlignment.Center
            Case formating.alignright
                txtResult.SelectionAlignment = HorizontalAlignment.Right
            Case formating.undo
                txtResult.Undo()
            Case formating.redo
                txtResult.Redo()
            Case formating.fontfamily
                ffont = New Font(tsfontfamily.ComboBox.SelectedValue.ToString(), ffont.Size, fstyle)
                isfontstyle = True
            Case formating.fontSize
                ffont = New Font(tsfontfamily.ComboBox.SelectedValue.ToString(), CSng(tsfonsize.Text), fstyle)
                isfontstyle = True
            Case formating.fontColor
                Dim cd As New ColorDialog
                If Not txtResult.SelectionColor = Nothing Then
                    cd.Color = txtResult.SelectionColor
                End If
                If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    txtResult.SelectionColor = cd.Color
                End If
        End Select
        If isfontstyle Then
            Dim font = New Font(ffont, fstyle)
            txtResult.SelectionFont = font
        End If
    End Sub
    Private Sub tsbold_Click(sender As System.Object, e As System.EventArgs) Handles tsbold.Click
        formatText(formating.bold)
    End Sub

    Private Sub tsunderline_Click(sender As System.Object, e As System.EventArgs) Handles tsunderline.Click
        formatText(formating.underline)
    End Sub

    Private Sub tsitalize_Click(sender As System.Object, e As System.EventArgs) Handles tsitalize.Click
        formatText(formating.italize)
    End Sub

    Private Sub txtResult_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtResult.KeyDown
        If Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.B Then
            formatText(formating.bold)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.I Then
            formatText(formating.italize)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.U Then
            formatText(formating.underline)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.L Then
            formatText(formating.alignleft)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.E Then
            formatText(formating.aligncenter)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.R Then
            formatText(formating.alignright)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.Z Then
            formatText(formating.undo)
        ElseIf Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.Y Then
            formatText(formating.redo)
        End If
    End Sub

    Private Sub tsalignleft_Click(sender As System.Object, e As System.EventArgs) Handles tsalignleft.Click
        formatText(formating.alignleft)
    End Sub

    Private Sub tsaligncenter_Click(sender As System.Object, e As System.EventArgs) Handles tsaligncenter.Click
        formatText(formating.aligncenter)
    End Sub

    Private Sub tsalignright_Click(sender As System.Object, e As System.EventArgs) Handles tsalignright.Click
        formatText(formating.alignright)
    End Sub

    Private Sub tsundo_Click(sender As System.Object, e As System.EventArgs) Handles tsundo.Click
        formatText(formating.undo)
    End Sub

    Private Sub tsredo_Click(sender As System.Object, e As System.EventArgs) Handles tsredo.Click
        formatText(formating.redo)
    End Sub
    Private Sub txtResult_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles txtResult.SelectionChanged
        Try
            If afterload Then
                afterload = False
                tsfontfamily.ComboBox.SelectedValue = txtResult.SelectionFont.FontFamily.Name
                tsfonsize.ComboBox.Text = txtResult.SelectionFont.Size
                tsbold.Checked = txtResult.SelectionFont.Bold
                tsunderline.Checked = txtResult.SelectionFont.Underline
                tsitalize.Checked = txtResult.SelectionFont.Italic
                afterload = True
            End If
        Catch ex As Exception
            tsfontfamily.ComboBox.SelectedIndex = -1
            afterload = True
        End Try
    End Sub

    Private Sub tsfontfamily_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tsfontfamily.SelectedIndexChanged
        Try
            If afterload Then
                afterload = False
                formatText(formating.fontfamily)
                afterload = True
            End If
        Catch ex As Exception
            afterload = True
        End Try
    End Sub

    Private Sub tsfonsize_TextChanged(sender As System.Object, e As System.EventArgs) Handles tsfonsize.TextChanged
        Try
            If afterload Then
                If Val(tsfonsize.Text) > 7 And Val(tsfonsize.Text) < 72 Then
                    afterload = False
                    formatText(formating.fontSize)
                    afterload = True
                End If
            End If
        Catch ex As Exception
            afterload = True
        End Try
    End Sub

    Private Sub tsfontcolor_Click(sender As System.Object, e As System.EventArgs) Handles tsfontcolor.Click
        Try
            formatText(formating.fontColor)
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub tsreloadresultpdf_Click(sender As System.Object, e As System.EventArgs) Handles tsreloadresultpdf.Click
        Call processWordDocument("", False)
    End Sub

    Private Sub tseditresultpdf_Click(sender As System.Object, e As System.EventArgs) Handles tseditresultpdf.Click
        Call processWordDocument("", True)
    End Sub

    Private Sub cmbradiologist_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbradiologist.SelectedValueChanged
        If afterload AndAlso cmbradiologist.SelectedIndex >= 0 Then
            Call getEmployeeInfo(cmbradiologist.SelectedValue)
        End If
    End Sub
    Private Sub getEmployeeInfo(ByVal employeeid As String)
        Dim dt As DataTable = clsRadiology.genericcls(8, employeeid)
        If dt.Rows.Count = 0 Then
            Exit Sub
        End If
        Me.radiologistdesignation = Utility.NullToEmptyString(dt.Rows(0).Item("designation"))
        Me.radiologistlicenseno = Utility.NullToEmptyString(dt.Rows(0).Item("prcno"))
    End Sub

    Private Sub lblward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblward.Click

    End Sub
End Class