Imports System.Drawing.Drawing2D
'***********
'*    Author: Ronald Jumao-as
'*    Date Created: 06-05-12
'*
'**********

Imports System.Drawing.Imaging
Imports System.IO
Public Class frmResultBaseDesign
#Region "Variables"
    Dim dtHospitalInfo As New DataTable

    '    'Royette 2021-07-29
    Private afterload As Boolean
    Private isformedit As Boolean
    Private laboratoryid As Long
    Private labname As String
    Private requestdetailno As Long
    Public medtech As Long = 0
    Public verifiedby As Long = 0
    Public patho As Long = 0
    Private dtPatientDetails As New DataTable
    Private dtNewBornResults As New DataTable
    Public isLock As Boolean
    Private baseForm As frmResultDesigner
    Private labformatid As LabFormat

    Public admissionid As Long
    Public itemcode As String
    Public laboratoryresultid As Long
    Public labexaminationno As Long
    Private gender As String

    'default grid height
    Private rowheight As Integer = 20
    Public Enum signatory
        medtech
        verifiedby
        patho
    End Enum
#End Region
#Region "Constructor"
    Public Sub New(baseForm As frmResultDesigner, ByVal isformedit As Boolean, ByVal laboratoryid As Long, ByVal labname As String, ByVal isLock As Boolean, ByVal format As LabFormat)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.isformedit = isformedit
        Me.laboratoryid = laboratoryid
        Me.labname = labname
        Me.isLock = isLock
        Me.baseForm = baseForm
        Me.labformatid = format
    End Sub

#End Region
#Region "Events"

    Private Sub frmMiscellaneous_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = getLabname()

        initForm()
        Me.lblMisc.Text = Me.getLabname
        

        If Me.laboratoryid = LabFormat.ECGREPORT Then
            Me.lblpathodesignation.Text = "Cardiologist"
        ElseIf Me.laboratoryid = LabFormat.NEWBORNSCREENING Then
            Me.panelnewborn.Visible = True
            Me.lblpathodesignation.Text = "Pediatrician"
            Me.dtNewBornResults = clsNewBornScreening.getNBSresults()
        End If
        Call LoadCombo()
    End Sub
#End Region

#Region "Methods"
     
    Private Function getLabname() As String
        If Me.labname = "" Then
            Return "Laboratory"
        End If
        Return Me.labname
    End Function
    Private Sub initForm()
        dtHospitalInfo = clsLaboratoryResult.getHospitalInfo()
        Me.lblHeader.Text = dtHospitalInfo.Rows(0).Item("Hospital").ToString()
        Me.lblAddress.Text = dtHospitalInfo.Rows(0).Item("Address3").ToString()
        Me.lablTelNo.Text = dtHospitalInfo.Rows(0).Item("Telephone").ToString()
        Me.lblMisc.BackColor = SystemColors.Control
        If modGlobal.hospitalcode = Constant.Facility.lhi Then
            lbldateencoded.Text = "Date:"
            txtdateencoded.Location = New Point(616, 114)
            txtdateencoded.Size = New Size(131, 18)
            lbltimeencoded.Text = "Room:"
            txttimeencoded.Location = New Point(620, 135)
            txttimeencoded.Size = New Size(127, 18)
            paneltimeinfo.Visible = False
            If Me.laboratoryid = 40 And Me.txtgridremarks.Text = "" Then
                txtgridremarks.Text = "   REFERENCE RANGE		" & vbNewLine & _
                                             "      PREGNANT WOMEN	        TOTAL ß-hCG Level" & vbNewLine & _
                                                 "      Weeks since LMP	                          (mIU/mL)" & vbNewLine & _
                                                 "      NEGATIVE		        < 5.00" & vbNewLine & _
                                                 "      3			        5-50" & vbNewLine & _
                                                 "      4			        5-426" & vbNewLine & _
                                                 "      5			        18-7,340" & vbNewLine & _
                                                 "      6			        1,080-56,500" & vbNewLine & _
                                                 "      7 to 8			        7,650-229,000" & vbNewLine & _
                                                 "      9 to 12			        25,700-288,000" & vbNewLine & _
                                                 "      13 to 16		        13,300-254,000" & vbNewLine & _
                                                 "      17 to 24		        4,060-164,400" & vbNewLine & _
                                                 "      25 to 40		        3,640-117,000"
            End If
        ElseIf modGlobal.hospitalcode = Constant.Facility.hipolfamily Then
            lbldateencoded.Text = "Date:"
            txtdateencoded.Location = New Point(616, 114)
            txtdateencoded.Size = New Size(131, 18)
            lbltimeencoded.Text = "Room:"
            txttimeencoded.Location = New Point(620, 135)
            txttimeencoded.Size = New Size(127, 18)
        ElseIf modGlobal.hospitalcode = Constant.Facility.qualilabdiag Then             '1/23/2026 jay
            Select Case Me.getLabname
                Case "HEMATOLOGY"
                    lblMisc.BackColor = Color.Pink
                Case "URINALYSIS"
                    lblMisc.BackColor = Color.Yellow
                Case "FECALYSIS"
                    lblMisc.BackColor = Color.LightBlue
                Case "BLOOD CHEMISTRY"
                    lblMisc.BackColor = Color.Green
                Case "MISCELLANEOUS FORM"
                    lblMisc.BackColor = Color.Gray
            End Select

        End If

        Me.paneltopmargin.Visible = appSetting.labheadermargin > 0
        Me.paneltopmargin.Height = appSetting.labheadermargin
        Me.Height = Me.Height + appSetting.labheadermargin
        Try
            Dim tempphoto As Byte() = dtHospitalInfo.Rows(0).Item("hospitallogo")
            If IsDBNull(dtHospitalInfo.Rows(0).Item("hospitallogo")) Or tempphoto.Length = 0 Then
                pctrLogo.Image = Nothing
            Else
                pctrLogo.Image = Utility.convertImage(dtHospitalInfo.Rows(0).Item("hospitallogo")) 'image here
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub loadRequestDetails(ByVal requestdetailno As Long)
        Me.requestdetailno = requestdetailno
        dtPatientDetails = clsLaboratoryResult.genericcls(14, requestdetailno)
        Me.admissionid = Utility.NullToZero(dtPatientDetails.Rows(0).Item("admissionid"))
        Me.laboratoryresultid = Utility.NullToZero(dtPatientDetails.Rows(0).Item("laboratoryresultid"))
        Me.labexaminationno = Utility.NullToZero(dtPatientDetails.Rows(0).Item("labexaminationno"))
        Me.itemcode = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("itemcode").ToString)
        If Me.labname = "" And Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("itemspecs")) <> "" Then
            Me.labname = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("itemspecs"))
            Me.lblMisc.Text = getLabname()
        End If
        Me.txtRequestedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("requestedby").ToString)
        Me.txtPatientName.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("patient").ToString)
        Me.txtAge.Text = dtPatientDetails.Rows(0).Item("age").ToString
        Me.gender = dtPatientDetails.Rows(0).Item("gender").ToString
        Me.txtGender.Text = Me.gender

        Me.lblMisc.Text = getLabname()
        Me.lbltransdate.Text = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("daterequested")).ToString(modGlobal.defaultdateformat)
        Me.lbltranstime.Text = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("daterequested")).ToString(modGlobal.defaulttimeformat)
        Me.txtdateencoded.Text = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("resultdatesubmitted")).ToString(modGlobal.defaultdateformat)
        If modGlobal.hospitalcode = Constant.Facility.lhi Then
            Me.txttimeencoded.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("ward"))
        ElseIf modGlobal.hospitalcode = Constant.Facility.hipolfamily Then
            Me.txttimeencoded.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("room"))
        Else
            Me.txttimeencoded.Text = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("resultdatesubmitted")).ToString(modGlobal.defaulttimeformat)
        End If
        Me.medtech = dtPatientDetails.Rows(0).Item("medicaltechnologist")
        Me.verifiedby = Utility.NullToZero(dtPatientDetails.Rows(0).Item("verifiedby"))
        Me.patho = dtPatientDetails.Rows(0).Item("pathologist")
        Me.txtmother.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("mothername").ToString)
        Me.txtcontactno.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("mobileno").ToString)
        Me.lblptno.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("ptno").ToString)
        Me.lblbirthdate.Text = Utility.NullToDefaultDateFormat(dtPatientDetails.Rows(0).Item("Birth Date"))
        Me.lblpatientaddress.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("homeaddress").ToString)
        Me.txtgridremarks.Text = dtPatientDetails.Rows(0).Item("remarks")
        Me.chkesigmedtech.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("esigmedtech"))
        Me.chkesigverifiedby.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("esigverifiedby"))
        Me.chkesigpatho.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("esigpatho"))
    End Sub
    Private Sub LoadCombo()
        afterload = False
        If Me.laboratoryid = LabFormat.NEWBORNSCREENING Then
            Me.cmbPathologist.DataSource = clsNewBornScreening.getPediatrician()
            Me.cmbPathologist.DisplayMember = "cname"
            Me.cmbPathologist.ValueMember = "employeeid"
        Else
            Me.cmbPathologist.DataSource = clsLaboratoryResult.getPathologist(clsModel.EmployeeTypes.pathologist)
            Me.cmbPathologist.DisplayMember = "radiologist"
            Me.cmbPathologist.ValueMember = "employeeid"
        End If
        Me.cmbPathologist.SelectedIndex = -1
        afterload = True
        If patho > 0 Then
            Me.cmbPathologist.SelectedValue = patho
        ElseIf Me.cmbPathologist.Items.Count > 0 Then
            Me.cmbPathologist.SelectedIndex = 0
        End If

        afterload = False
        Me.cmbMedtech.DataSource = clsLaboratoryResult.getPathologist(clsModel.EmployeeTypes.medtech)
        Me.cmbMedtech.DisplayMember = "radiologist"
        Me.cmbMedtech.ValueMember = "employeeid"
        Me.cmbMedtech.SelectedIndex = -1
        afterload = True
        If medtech > 0 Then
            Me.cmbMedtech.SelectedValue = medtech
        ElseIf Me.cmbMedtech.Items.Count > 0 Then
            Me.cmbMedtech.SelectedValue = modGlobal.userid
        End If
        afterload = False
        Me.cmbverifiedby.DataSource = clsLaboratoryResult.getPathologist(clsModel.EmployeeTypes.medtech)
        Me.cmbverifiedby.DisplayMember = "radiologist"
        Me.cmbverifiedby.ValueMember = "employeeid"
        Me.cmbverifiedby.SelectedIndex = -1
        afterload = True
        If modGlobal.hospitalcode = Constant.Facility.qualilabdiag Then         '1/23/2026 jay
            If verifiedby > 0 Then
                Me.cmbverifiedby.SelectedValue = verifiedby
            End If
        Else
            If verifiedby > 0 Then
                Me.cmbverifiedby.SelectedValue = verifiedby
            ElseIf Me.cmbverifiedby.Items.Count > 0 Then
                Me.cmbverifiedby.SelectedValue = modGlobal.userid
            End If
        End If
        
        If Not isLock Then
            FormatSignatory(signatory.medtech, True, Utility.getReference(Constant.ReferenceKey.lab_showmedtech_sig) = 1)
            FormatSignatory(signatory.verifiedby, Utility.getReference(Constant.ReferenceKey.lab_showverifiedby) = 1, Utility.getReference(Constant.ReferenceKey.lab_showverifiedby_sig) = 1)
            FormatSignatory(signatory.patho, True, Utility.getReference(Constant.ReferenceKey.lab_showpatho_sig) = 1)
        End If
    End Sub
    Private Sub checkHighlight(ByVal ctrl As Control, ByVal dgcell As DataGridViewCell, ByVal texthighlight As String)
        If texthighlight = "" Then
            Exit Sub
        End If
        Dim value As String
        If ctrl Is Nothing Then
            value = Trim(dgcell.Value)
        Else
            value = Trim(ctrl.Text)
        End If
        Dim opt As String() = texthighlight.Split(";")
        For Each str As String In opt
            Dim optval As String() = str.Split(":")
            Try
                Dim isGender = ""
                If optval(0).Substring(0, 2) = "M(" Or optval(0).Substring(0, 2) = "F(" Then
                    isGender = optval(0).Substring(0, 1)
                    optval(0) = optval(0).Replace("M(", "").Replace("F(", "")
                    optval(1) = optval(1).Replace(")", "")
                End If
                If (optval(0).Contains(">") Or optval(0).Contains("<") Or optval(0).Contains("<>") Or optval(0).Contains("-") Or optval(0).Contains("!-")) Then
                    If (processCondition(value, Trim(optval(0))) And (isGender = Me.gender Or isGender = "")) Then
                        If Utility.IsColor(optval(1)) Then
                            If ctrl Is Nothing Then
                                dgcell.Style.ForeColor = System.Drawing.Color.FromName(optval(1))
                            Else
                                ctrl.ForeColor = System.Drawing.Color.FromName(optval(1))
                            End If
                        Else
                            If ctrl Is Nothing Then
                                dgcell.Value = optval(1) & dgcell.Value
                            Else
                                ctrl.Text = optval(1) & ctrl.Text
                            End If
                        End If
                    End If
                ElseIf value = Trim(optval(0)) Then
                    If (Utility.IsColor(optval(1)) And (isGender = Me.gender Or isGender = "")) Then
                        If ctrl Is Nothing Then
                            dgcell.Style.ForeColor = System.Drawing.Color.FromName(optval(1))
                        Else
                            ctrl.ForeColor = System.Drawing.Color.FromName(optval(1))
                        End If
                    Else
                        If ctrl Is Nothing Then
                            dgcell.Value = optval(1) & dgcell.Value
                        Else
                            ctrl.Text = optval(1) & ctrl.Text
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub
    Private Function processCondition(ByVal value As String, ByVal condition As String) As Boolean
        Try
            value = value.Replace("%", "")
            If condition.Contains(">") Then
                condition = condition.Replace(">", "")
                If CDbl(value) > CDbl(condition) Then
                    Return True
                End If
            ElseIf condition.Contains("<") Then
                condition = condition.Replace("<", "")
                If CDbl(value) < CDbl(condition) Then
                    Return True
                End If
            ElseIf condition.Contains("<>") Then
                condition = condition.Replace("<>", "")
                If CDbl(value) <> CDbl(condition) Then
                    Return True
                End If
            ElseIf condition.Contains("!-") Then
                condition = condition.Replace("!-", "-")
                Dim range = condition.Split("-")
                If (CDbl(value) < CDbl(range(0)) Or CDbl(value) > CDbl(range(1))) Then
                    Return True
                End If
            ElseIf condition.Contains("-") Then
                Dim range = condition.Split("-")
                If (CDbl(value) >= CDbl(range(0)) And CDbl(value) <= CDbl(range(1))) Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Sub AddControl(ByVal ctr As clsModel.LabControl)
        Dim panel As New Panel()
        If Not isformedit AndAlso ctr.defaultvalue <> "" AndAlso dtPatientDetails.Columns.Contains(ctr.defaultvalue) Then
            ctr.value = Me.dtPatientDetails.Rows(0).Item(ctr.defaultvalue).ToString
            isLock = True
            panel.Tag = "1" 'used to disregard lock fields
        End If
        panel.Size = New Size(IIf(ctr.panelwidth = 0, clsModel.ControlTypes.DefaultPanelWidth, ctr.panelwidth), clsModel.ControlTypes.DefaultPanelHeight)
        If Me.isformedit Then
            panel.BorderStyle = BorderStyle.FixedSingle
        End If
        panel.Font = New Font("Cambria", 9)
        panel.Name = "panel_" & ctr.uuid
        Me.panelresult.Controls.Add(panel)
        If ctr.ctrtype = clsModel.ControlTypes.Dropdown Then
            Dim locleft As Integer = 137
            If ctr.labeltext = "" Then
                locleft = 19
                'panel.Width = 202
            Else
                Dim lbl As New Label
                lbl.Text = ctr.labeltext
                lbl.AutoSize = False
                lbl.Width = 118
                lbl.TextAlign = ContentAlignment.MiddleRight
                panel.Controls.Add(lbl)

                lbl.Left = 19
                lbl.Top = 2
                lbl.Name = "lbl_" & ctr.uuid
                If Me.isformedit Then
                    AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
                End If
            End If

            If isLock Then
                Dim lbldetail As New Label
                lbldetail.AutoSize = False
                lbldetail.Size = New Size(panel.Width - locleft - 3, 20)
                lbldetail.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                lbldetail.Font = New Font("Cambria", 9, FontStyle.Bold)
                lbldetail.TextAlign = ContentAlignment.MiddleCenter
                If Me.laboratoryid = LabFormat.NEWBORNSCREENING AndAlso ctr.labeltext.ToLower().Contains("result") Then
                    For Each row As DataRow In Me.dtNewBornResults.Rows
                        If row.Item("newbornscreeningresultid").ToString = ctr.value Then
                            ctr.value = row.Item("results")
                            Exit For
                        End If
                    Next
                End If
                lbldetail.Text = ctr.value
                panel.Controls.Add(lbldetail)

                lbldetail.Left = locleft
                lbldetail.Top = 4
                checkHighlight(lbldetail, Nothing, ctr.texthighlight)
                Dim line1 As New PowerPacks.LineShape
                line1.X1 = lbldetail.Location.X
                line1.X2 = lbldetail.Location.X + lbldetail.Width
                line1.Y1 = lbldetail.Location.Y + lbldetail.Height
                line1.Y2 = lbldetail.Location.Y + lbldetail.Height
                Dim sh As New PowerPacks.ShapeContainer
                sh.Margin = New System.Windows.Forms.Padding(0)
                sh.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line1})
                sh.Size = New System.Drawing.Size(lbldetail.Width, 24)
                panel.Controls.Add(sh)
            Else
                Dim cmb As New ComboBox
                cmb.Name = "cmb_" & ctr.uuid
                cmb.Size = New Size(panel.Width - locleft - 3, 20)
                cmb.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                panel.Controls.Add(cmb)
                If Me.laboratoryid = LabFormat.NEWBORNSCREENING AndAlso ctr.labeltext.ToLower().Contains("result") Then
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList
                    cmb.DataSource = Me.dtNewBornResults
                    cmb.ValueMember = "newbornscreeningresultid"
                    cmb.DisplayMember = "results"
                    cmb.SelectedValue = Val(ctr.value)
                Else
                    Dim opt As String() = ctr.optvalue.Split(";")
                    For Each str As String In opt
                        cmb.Items.Add(str)
                    Next
                    cmb.Text = ctr.value
                End If
                cmb.Left = locleft
                cmb.Top = 1
            End If
        ElseIf ctr.ctrtype = clsModel.ControlTypes.DateTimePicker Then
            Dim locleft As Integer = 137
            If ctr.labeltext = "" Then
                locleft = 19
                'panel.Width = 202
            Else
                Dim lbl As New Label
                lbl.Text = ctr.labeltext
                lbl.AutoSize = False
                lbl.Width = 118
                lbl.TextAlign = ContentAlignment.MiddleRight
                panel.Controls.Add(lbl)

                lbl.Left = 19
                lbl.Top = 2
                lbl.Name = "lbl_" & ctr.uuid

                If Me.isformedit Then
                    AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
                End If
            End If

            If isLock Then
                Dim lbldetail As New Label
                lbldetail.AutoSize = False
                lbldetail.Size = New Size(panel.Width - locleft - 3, 20)
                lbldetail.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                lbldetail.Font = New Font("Cambria", 9, FontStyle.Bold)
                lbldetail.TextAlign = ContentAlignment.MiddleCenter
                Try
                    lbldetail.Text = CDate(ctr.value).ToString(clsModel.ControlTypes.yyyyMMddhhmmtt)
                Catch ex As Exception
                    lbldetail.Text = ""
                End Try
                panel.Controls.Add(lbldetail)

                lbldetail.Left = locleft
                lbldetail.Top = 4
                checkHighlight(lbldetail, Nothing, ctr.texthighlight)
                Dim line1 As New PowerPacks.LineShape
                line1.X1 = lbldetail.Location.X
                line1.X2 = lbldetail.Location.X + lbldetail.Width
                line1.Y1 = lbldetail.Location.Y + lbldetail.Height
                line1.Y2 = lbldetail.Location.Y + lbldetail.Height
                Dim sh As New PowerPacks.ShapeContainer
                sh.Margin = New System.Windows.Forms.Padding(0)
                sh.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line1})
                sh.Size = New System.Drawing.Size(lbldetail.Width, 24)
                panel.Controls.Add(sh)
            Else
                Dim dtp As New DateTimePicker
                dtp.Size = New Size(panel.Width - locleft - 3, 20)
                dtp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                dtp.Format = DateTimePickerFormat.Custom
                dtp.CustomFormat = clsModel.ControlTypes.yyyyMMddhhmmtt
                Try
                    dtp.Value = CDate(ctr.value)
                Catch ex As Exception

                End Try
                panel.Controls.Add(dtp)

                dtp.Left = locleft
                dtp.Top = 1
                dtp.Name = "dtp_" & ctr.uuid
            End If
        ElseIf ctr.isLabel Then
            Dim lbl As New Label
            lbl.Text = ctr.labeltext
            lbl.AutoSize = True
            lbl.TextAlign = ContentAlignment.MiddleCenter

            If ctr.ctrtype = clsModel.ControlTypes.LabelH1 Then
                panel.Font = New Font("Cambria", 14, FontStyle.Bold)
            ElseIf ctr.ctrtype = clsModel.ControlTypes.LabelH2 Then
                panel.Font = New Font("Cambria", 12, FontStyle.Bold)
            ElseIf ctr.ctrtype = clsModel.ControlTypes.LabelH3 Then
                panel.Font = New Font("Cambria", 10, FontStyle.Bold)
            ElseIf ctr.ctrtype = clsModel.ControlTypes.LabelH4 Then
                panel.Font = New Font("Cambria", 10, FontStyle.Regular)
            ElseIf ctr.ctrtype = clsModel.ControlTypes.LabelH5 Then
                panel.Font = New Font("Cambria", 8, FontStyle.Regular)
            End If
            panel.Controls.Add(lbl)
            panel.Width = lbl.Width + 2

            lbl.Left = 5
            lbl.Top = 5
            lbl.Name = "lbl_" & ctr.uuid
            If Me.isformedit Then
                AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
            End If
        ElseIf ctr.ctrtype = clsModel.ControlTypes.DoubleTextField Then
            panel.Height = clsModel.ControlTypes.DoubleTextFieldHeight
            Dim locleft As Integer = 137
            If ctr.labeltext = "" Then
                locleft = 19
                panel.Width = 202
            Else
                Dim lbl As New Label
                lbl.Text = ctr.labeltext
                lbl.AutoSize = False
                lbl.Width = 118
                lbl.TextAlign = ContentAlignment.MiddleRight
                panel.Controls.Add(lbl)
                lbl.Left = 19
                lbl.Top = 2
                lbl.Name = "lbl_" & ctr.uuid
                If Me.isformedit Then
                    AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
                End If
            End If
            If isLock Then
                Dim lbldetail As New Label
                lbldetail.AutoSize = False
                lbldetail.Size = New Size(180, 40)
                lbldetail.TextAlign = ContentAlignment.MiddleCenter
                lbldetail.Text = ctr.value
                panel.Controls.Add(lbldetail)

                lbldetail.Left = locleft
                lbldetail.Top = 4
                checkHighlight(lbldetail, Nothing, ctr.texthighlight)
                Dim line1 As New PowerPacks.LineShape
                line1.X1 = lbldetail.Location.X
                line1.X2 = lbldetail.Location.X + lbldetail.Width
                line1.Y1 = lbldetail.Location.Y + lbldetail.Height
                line1.Y2 = lbldetail.Location.Y + lbldetail.Height
                Dim sh As New PowerPacks.ShapeContainer
                sh.Margin = New System.Windows.Forms.Padding(0)
                sh.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line1})
                sh.Size = New System.Drawing.Size(lbldetail.Width, 24)
                panel.Controls.Add(sh)
            Else
                Dim txt As New TextBox
                txt.Multiline = True
                txt.Size = New Size(180, 40)
                panel.Controls.Add(txt)
                txt.Text = ctr.value
                txt.Left = locleft
                txt.Top = 1
                txt.Name = "txt_" & ctr.uuid
            End If
        ElseIf ctr.ctrtype = clsModel.ControlTypes.ResizableTextField Then
            panel.Height = IIf(ctr.panelheight = 0, clsModel.ControlTypes.ResizableTextFieldHeight, ctr.panelheight)
            Dim loctop As Integer = 20
            Dim locl As Integer = 19
            If ctr.labeltext = "" Then
                loctop = 1
            Else
                Dim lbl As New Label
                lbl.AutoSize = True
                lbl.Text = ctr.labeltext
                lbl.Width = 118
                lbl.TextAlign = ContentAlignment.MiddleLeft
                lbl.Font = New Font("Cambria", 10, FontStyle.Bold)
                panel.Controls.Add(lbl)
                lbl.Left = 18
                lbl.Top = 2
                lbl.Name = "lbl_" & ctr.uuid

                If Me.isformedit Then
                    AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
                End If
            End If

            Dim txt As New TextBox
            txt.Multiline = True
            txt.Size = New Size(panel.Width - locl - 4, panel.Height - loctop - 4)
            txt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            panel.Controls.Add(txt)
            txt.Text = ctr.value
            txt.Left = locl
            txt.Top = loctop
            txt.Name = "txt_" & ctr.uuid
            txt.BringToFront()
            If isLock Then
                txt.BackColor = Color.White
                txt.ReadOnly = True
                txt.BorderStyle = BorderStyle.None
                checkHighlight(txt, Nothing, ctr.texthighlight)
            End If
        ElseIf ctr.ctrtype = clsModel.ControlTypes.ParagraphField Then
            panel.Height = IIf(ctr.panelheight = 0, clsModel.ControlTypes.ResizableTextFieldHeight, ctr.panelheight)
            Dim loctop As Integer = 20
            Dim locl As Integer = 19

            Dim txt As New RichTextBox
            txt.Size = New Size(panel.Width - locl - 4, panel.Height - loctop - 4)
            txt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            panel.Controls.Add(txt)

            txt.Rtf = ctr.labeltext
            txt.Left = locl
            txt.Top = loctop
            txt.Name = "txt_" & ctr.uuid
            txt.BringToFront()
            'If islock Then
            txt.BackColor = Color.White
            txt.ReadOnly = True
            txt.BorderStyle = BorderStyle.None
            'End If
        Else
            Dim locl As Integer = 137
            If ctr.labeltext = "" Then
                locl = 19
                'panel.Width = 202
            Else
                Dim lbl As New Label
                lbl.Text = ctr.labeltext
                lbl.AutoSize = False
                lbl.Width = 118
                lbl.TextAlign = ContentAlignment.MiddleRight
                panel.Controls.Add(lbl)
                lbl.Left = 19
                lbl.Top = 2
                lbl.Name = "lbl_" & ctr.uuid
                If Me.isformedit Then
                    AddHandler lbl.DoubleClick, AddressOf label_MouseDoubleClick
                End If
            End If

            If isLock Then
                Dim lbldetail As New Label
                lbldetail.AutoSize = False
                lbldetail.Size = New Size(panel.Width - locl - 3, 20)
                lbldetail.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                lbldetail.TextAlign = ContentAlignment.MiddleCenter
                lbldetail.Text = ctr.value
                panel.Controls.Add(lbldetail)

                lbldetail.Left = locl
                lbldetail.Top = 4
                checkHighlight(lbldetail, Nothing, ctr.texthighlight)
                Dim line1 As New PowerPacks.LineShape
                line1.X1 = lbldetail.Location.X
                line1.X2 = lbldetail.Location.X + lbldetail.Width
                line1.Y1 = lbldetail.Location.Y + lbldetail.Height
                line1.Y2 = lbldetail.Location.Y + lbldetail.Height
                Dim sh As New PowerPacks.ShapeContainer
                sh.Margin = New System.Windows.Forms.Padding(0)
                sh.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line1})
                sh.Size = New System.Drawing.Size(lbldetail.Width, 24)
                panel.Controls.Add(sh)
            Else
                Dim txt As New TextBox
                txt.Size = New Size(panel.Width - locl - 3, 20)
                txt.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                panel.Controls.Add(txt)
                txt.Text = ctr.value
                txt.Left = locl
                txt.Top = 1
                txt.Name = "txt_" & ctr.uuid
            End If
            'Dim rezi As ResizeableControl = New ResizeableControl(txt)
        End If
        panel.Location = ctr.loc
        If Me.isformedit Then
            AddHandler panel.DoubleClick, AddressOf panel_MouseDoubleClick
            Dim rezi2 As ResizeableControl = New ResizeableControl(panel)
        End If
    End Sub
    Private Sub panel_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim panel As Control = CType(sender, Control)
        Me.baseForm.onPanelDoubleClick(panel.Name.Replace("panel_", ""), panel.Location.X, panel.Location.Y, panel.Width, panel.Height)
    End Sub
    Private Sub label_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim panel As Control = CType(sender, Control).Parent
        Me.baseForm.onPanelDoubleClick(panel.Name.Replace("panel_", ""), panel.Location.X, panel.Location.Y, panel.Width, panel.Height)
    End Sub
    Private Sub getEmployeeInfo(ByVal employeeid As String, ByVal t As signatory)
        Dim dt As DataTable = clsRadiology.genericcls(8, employeeid)
        If dt.Rows.Count = 0 Then
            Exit Sub
        End If
        If t = signatory.patho Then 'Patho
            Me.lblpatho.Text = dt.Rows(0).Item("employeename")
            Me.lblpatholicense.Text = "License No. " & dt.Rows(0).Item("prcno")
            If Utility.NullToEmptyString(dt.Rows(0).Item("designation")) = "" Then
                Me.lblpathodesignation.Text = "Clinical Pathologist"
            Else
                Me.lblpathodesignation.Text = dt.Rows(0).Item("designation")
            End If
            If Me.isLock AndAlso Me.chkesigpatho.Checked Then
                Try
                    Dim tempphoto As Byte() = dt.Rows(0).Item("usersign")
                    If IsDBNull(dt.Rows(0).Item("usersign")) Or tempphoto.Length = 0 Then
                        panelpatho.BackgroundImage = Nothing
                    Else
                        panelpatho.BackgroundImage = Utility.convertImage(dt.Rows(0).Item("usersign")) 'image here
                    End If
                Catch ex As Exception
                End Try
            Else
                panelpatho.BackgroundImage = Nothing
            End If
        ElseIf t = signatory.verifiedby Then
            Me.lblverifiedby.Text = Me.cmbverifiedby.Text
            Me.lblverifiedbylicense.Text = "License No. " & dt.Rows(0).Item("prcno")
            If Utility.NullToEmptyString(dt.Rows(0).Item("designation")) = "" Then
                Me.lblverifiedbydesignation.Text = "Medical Technologist"
            Else
                Me.lblverifiedbydesignation.Text = dt.Rows(0).Item("designation")
            End If
            If Me.isLock AndAlso Me.chkesigverifiedby.Checked Then
                Try
                    Dim tempphoto As Byte() = dt.Rows(0).Item("usersign")
                    If IsDBNull(dt.Rows(0).Item("usersign")) Or tempphoto.Length = 0 Then
                        panelverifiedby.BackgroundImage = Nothing
                    Else
                        panelverifiedby.BackgroundImage = Utility.convertImage(dt.Rows(0).Item("usersign")) 'image here
                    End If
                Catch ex As Exception
                End Try
            Else
                panelverifiedby.BackgroundImage = Nothing
            End If
        Else 'Medtech
            Me.lblmedtech.Text = Me.cmbMedtech.Text
            Me.lblmedtechlicense.Text = "License No. " & dt.Rows(0).Item("prcno")
            If Utility.NullToEmptyString(dt.Rows(0).Item("designation")) = "" Then
                Me.lblmedtechdesignation.Text = "Medical Technologist"
            Else
                Me.lblmedtechdesignation.Text = dt.Rows(0).Item("designation")
            End If
            If Me.isLock AndAlso Me.chkesigmedtech.Checked Then
                Try
                    Dim tempphoto As Byte() = dt.Rows(0).Item("usersign")
                    If IsDBNull(dt.Rows(0).Item("usersign")) Or tempphoto.Length = 0 Then
                        panelmedtech.BackgroundImage = Nothing
                    Else
                        panelmedtech.BackgroundImage = Utility.convertImage(dt.Rows(0).Item("usersign")) 'image here
                    End If
                Catch ex As Exception
                End Try
            Else
                panelmedtech.BackgroundImage = Nothing
            End If
        End If
    End Sub
    Public Function getResultFileName() As String
        Return "RadLab_" & Utility.RemoveIllegalFileNameChars(Me.labname, "_") & "_" & requestdetailno & ".jpg"
    End Function
    Public Sub DisplayPrintPreview()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Text = ""
        Call clsadmissiondocuments.SaveLabResultImage(requestdetailno, Me.admissionid, GetFormImage(False), Me.getResultFileName(), False)
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            MiscPrintDocu.PrinterSettings = PrintDialog1.PrinterSettings
            MiscPrintDocu.Print()
        End If

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = getLabname()
    End Sub
    Public Function GetFormImage(ByVal include_borders As Boolean) As Bitmap
        If Not include_borders Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        End If
        'Me.Text = ""
        ' Make the bitmap.
        Dim wid As Integer = Me.Width
        Dim hgt As Integer = Me.Height
        Dim bm As New Bitmap(wid, hgt)
        ' Draw the form onto the bitmap.
        Me.DrawToBitmap(bm, New Rectangle(0, 0, wid, hgt))
        ' Make a smaller bitmap without borders.
        wid = Me.ClientSize.Width
        hgt = Me.ClientSize.Height
        Dim bm2 As New Bitmap(wid, hgt)
        ' Get the offset from the window's corner to its client
        ' area's corner.
        Dim pt As New Point(0, 0)
        pt = PointToScreen(pt)
        Dim dx As Integer = pt.X - Me.Left
        Dim dy As Integer = pt.Y - Me.Top
        ' Copy the part of the original bitmap that we want
        ' into the bitmap.
        Dim gr As Graphics = Graphics.FromImage(bm2)
        gr.DrawImage(bm, 0, 0, New Rectangle(dx, dy, wid, hgt), GraphicsUnit.Pixel)
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        'Me.Text = Labname
        If Not include_borders Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        End If
        Return bm
    End Function
    Public Sub FormatSignatory(ByVal t As signatory, ByVal show As Boolean, ByVal showEsig As Boolean)
        If t = signatory.medtech Then
            chkesigmedtech.Visible = showEsig
            If Not show Then
                tblpanelsignatory.ColumnStyles(t).SizeType = SizeType.Absolute
                tblpanelsignatory.ColumnStyles(t).Width = 0
            End If
        ElseIf t = signatory.verifiedby Then
            chkesigverifiedby.Visible = showEsig
            If Not show Then
                tblpanelsignatory.ColumnStyles(t).SizeType = SizeType.Absolute
                tblpanelsignatory.ColumnStyles(t).Width = 0
                tblpanelsignatory.ColumnStyles(signatory.medtech).Width = IIf(tblpanelsignatory.ColumnStyles(signatory.medtech).Width > 0, 50, 0)
                tblpanelsignatory.ColumnStyles(signatory.patho).Width = 50
            End If
        Else
            chkesigpatho.Visible = showEsig
        End If
    End Sub
#End Region

    Private Sub cmbMedtech_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbMedtech.SelectedValueChanged
        If afterload AndAlso cmbMedtech.SelectedIndex >= 0 Then
            Call getEmployeeInfo(cmbMedtech.SelectedValue, signatory.medtech)
        End If
    End Sub

    Private Sub cmbverifiedby_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbverifiedby.SelectedValueChanged
        If afterload AndAlso cmbverifiedby.SelectedIndex >= 0 Then
            Call getEmployeeInfo(cmbverifiedby.SelectedValue, signatory.verifiedby)
        End If
    End Sub

    Private Sub txtPathologist_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbPathologist.SelectedValueChanged
        If afterload AndAlso cmbPathologist.SelectedIndex >= 0 Then
            Call getEmployeeInfo(cmbPathologist.SelectedValue, signatory.patho)
        End If
    End Sub

    Private Sub MiscPrintDocu_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles MiscPrintDocu.PrintPage
        e.Graphics.DrawImage(GetFormImage(False), New Point(15, 0))
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        Dim f As New frmManageResultParams(Me.laboratoryid, Me.labname, Me.labformatid)
        f.ShowDialog()
        If f.issave Then
            Dim dt As DataTable = clsExamination.getLabdetails(Me.laboratoryid)
            dt.Columns.Add("result")
            dt.Columns.Add("resultconversion")
            dt.Columns.Add("laboratoryresultdetailsid")
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To Me.dgvResult.Rows.Count - 1
                    If dt.Rows(i).Item("laboratorydetailsid") = Me.dgvResult.Rows(j).Cells(collabdetailid.Index).Value Then
                        dt.Rows(i).Item("result") = Utility.NullToEmptyString(Me.dgvResult.Rows(j).Cells(colresult.Index).Value)
                        dt.Rows(i).Item("resultconversion") = Utility.NullToEmptyString(Me.dgvResult.Rows(j).Cells(colresultconversion.Index).Value)
                        dt.Rows(i).Item("laboratoryresultdetailsid") = Utility.NullToZero(Me.dgvResult.Rows(j).Cells(collabresultdetailid.Index).Value)
                        Me.dgvResult.Rows.RemoveAt(j)
                        reduceForm()
                        Exit For
                    End If
                Next
            Next
            For j As Integer = Me.dgvResult.Rows.Count - 1 To 0 Step -1
                Me.dgvResult.Rows.RemoveAt(j)
                reduceForm()
            Next
            For Each row As DataRow In dt.Rows
                If row.Item("visible") Then
                    dgvResult.Rows.Add(1)
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(collabdetailid.Index).Value = row.Item("laboratorydetailsid")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(colparameter.Index).Value = row.Item("description")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(colref.Index).Value = row.Item("normalvalues")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(colresult.Index).Value = Utility.NullToEmptyString(row.Item("result"))
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(collabresultdetailid.Index).Value = Utility.NullToZero(row.Item("laboratoryresultdetailsid"))
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(colunits.Index).Value = row.Item("unit")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(coltexthighlight.Index).Value = row.Item("texthighlight")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(Me.colresultconversion.Index).Value = row.Item("resultconversion")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(Me.colrefconversion.Index).Value = Utility.NullToEmptyString(row.Item("normalvaluessi"))
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(Me.colunitsconversion.Index).Value = row.Item("unitsi")
                    Me.dgvResult.Rows(Me.dgvResult.Rows.Count - 1).Cells(Me.colconversion.Index).Value = row.Item("siconversion")
                    Call adjustForm()
                End If
            Next
        End If
    End Sub

    Public Sub adjustForm()
        If Me.dgvResult.Rows.Count > 15 Then
            Exit Sub
        End If
        Me.dgvResult.Height = Me.dgvResult.Height + rowheight
        Me.panelresultgrid.Height = Me.panelresultgrid.Height + rowheight
        Me.panelmain.Height = Me.panelmain.Height + rowheight
        Me.Height = Me.Height + rowheight
    End Sub
    Public Sub reduceForm()
        If Me.dgvResult.Rows.Count > 15 Or Me.dgvResult.Rows.Count = 0 Then
            Exit Sub
        End If
        Me.dgvResult.Height = Me.dgvResult.Height - rowheight
        Me.panelresultgrid.Height = Me.panelresultgrid.Height - rowheight
        Me.panelmain.Height = Me.panelmain.Height - rowheight
        Me.Height = Me.Height - rowheight
    End Sub

    Public Sub lock()
        Me.isLock = True
        Dim dtdatenow As Date = Utility.GetServerDate()
        Me.lblprinttime.Text = dtdatenow.ToString(modGlobal.defaulttimeformat)
        Me.lblprintdate.Text = dtdatenow.ToString(modGlobal.defaultdateformat)
        Me.cmbPathologist.Visible = False
        Me.cmbMedtech.Visible = False
        Me.cmbverifiedby.Visible = False
        Me.lblpatho.Visible = True
        Me.lblmedtech.Visible = True
        Me.lblverifiedby.Visible = True
        Me.chkesigmedtech.Visible = False
        Me.chkesigverifiedby.Visible = False
        Me.chkesigpatho.Visible = False

        getEmployeeInfo(Me.medtech, signatory.medtech)
        If Me.verifiedby > 0 Then
            getEmployeeInfo(Me.cmbverifiedby.SelectedValue, signatory.verifiedby)
        Else
            FormatSignatory(signatory.verifiedby, False, False)
        End If
        getEmployeeInfo(Me.patho, signatory.patho)
        If panelresultgrid.Visible = True Then
            For i As Integer = Me.dgvResult.Rows.Count - 1 To 0 Step -1
                If Utility.NullToEmptyString(Me.dgvResult.Rows(i).Cells(colresult.Index).Value) = "" Then
                    If Utility.NullToEmptyString(Me.dgvResult.Rows(i).Cells(colref.Index).Value) = "-" AndAlso
                        i < Me.dgvResult.Rows.Count - 1 AndAlso
                        Utility.NullToEmptyString(Me.dgvResult.Rows(i + 1).Cells(colparameter.Index).Value).Contains("   ") Then
                        Me.dgvResult.Rows(i).Cells(colref.Index).Value = ""
                    Else
                        If Me.labformatid = LabFormat.GRID_WITH_CONVERSION And Utility.NullToEmptyString(Me.dgvResult.Rows(i).Cells(colresultconversion.Index).Value) <> "" Then
                            Me.dgvResult.Rows(i).Cells(colref.Index).Value = Me.dgvResult.Rows(i).Cells(colref.Index).Value & " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                            Me.dgvResult.Rows(i).Cells(colunits.Index).Value = Me.dgvResult.Rows(i).Cells(colresult.Index).Value '& " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                            Me.dgvResult.Rows(i).Cells(colrefconversion.Index).Value = Me.dgvResult.Rows(i).Cells(colrefconversion.Index).Value & " " & Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Value
                            Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Value = Me.dgvResult.Rows(i).Cells(colresultconversion.Index).Value '& " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                            Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Style.ForeColor = Me.dgvResult.Rows(i).Cells(colunits.Index).Style.ForeColor
                        Else
                            Me.dgvResult.Rows.RemoveAt(i)
                            Call reduceForm()
                        End If
                    End If
                Else
                    Me.dgvResult.Rows(i).Cells(colref.Index).Value = Me.dgvResult.Rows(i).Cells(colref.Index).Value & " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                    Me.dgvResult.Rows(i).Cells(colunits.Index).Value = Me.dgvResult.Rows(i).Cells(colresult.Index).Value '& " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                    checkHighlight(Nothing, Me.dgvResult.Rows(i).Cells(colunits.Index), Me.dgvResult.Rows(i).Cells(coltexthighlight.Index).Value)
                    If labformatid = LabFormat.GRID_WITH_CONVERSION Then
                        Me.dgvResult.Rows(i).Cells(colrefconversion.Index).Value = Me.dgvResult.Rows(i).Cells(colrefconversion.Index).Value & " " & Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Value
                        Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Value = Me.dgvResult.Rows(i).Cells(colresultconversion.Index).Value '& " " & Me.dgvResult.Rows(i).Cells(colunits.Index).Value
                        Me.dgvResult.Rows(i).Cells(colunitsconversion.Index).Style.ForeColor = Me.dgvResult.Rows(i).Cells(colunits.Index).Style.ForeColor
                    End If
                End If
            Next
            Me.panelmanageparams.Visible = False
            Me.panelresultgrid.Height = Me.panelresultgrid.Height - Me.panelmanageparams.Height
            Me.Height = Me.Height - Me.panelmanageparams.Height
            Me.txtgridremarks.ReadOnly = True
            Me.txtgridremarks.BorderStyle = BorderStyle.None
            Me.dgvResult.Columns(colresult.Index).Visible = False
            Me.dgvResult.Columns(colunits.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.dgvResult.Columns(colunits.Index).DefaultCellStyle.Font = New Font("Cambria", 9, FontStyle.Bold)
            Me.dgvResult.Columns(colunits.Index).HeaderText = "Result"
            If Me.labformatid = LabFormat.GRID_SI Then
                Me.dgvResult.Columns(colunits.Index).Width = CInt(Me.dgvResult.Columns(colunits.Index).Width + (Me.dgvResult.Columns(colresult.Index).Width / 2))
            Else
                Me.dgvResult.Columns(colunits.Index).Width = CInt(Me.dgvResult.Columns(colunits.Index).Width + (Me.dgvResult.Columns(colresult.Index).Width))
                Me.dgvResult.Columns(colunitsconversion.Index).Width = CInt(Me.dgvResult.Columns(colunitsconversion.Index).Width + (Me.dgvResult.Columns(colresultconversion.Index).Width))
                Me.dgvResult.Columns(colresultconversion.Index).Visible = False
                Me.dgvResult.Columns(colunitsconversion.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Me.dgvResult.Columns(colunitsconversion.Index).DefaultCellStyle.Font = New Font("Cambria", 9, FontStyle.Bold)
                Me.dgvResult.Columns(colunitsconversion.Index).HeaderText = "Result"
            End If
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If modGlobal.hospitalcode = Constant.Facility.ecomed Then
            If (keyData = Keys.Enter Or keyData = Keys.Down Or keyData = Keys.Up Or keyData = Keys.Tab) And Not dgvResult.EditingControl Is Nothing Then
                Return True
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub dgvResult_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvResult.CellEndEdit
        If dgvResult.CurrentCell.RowIndex >= 0 AndAlso (e.ColumnIndex = colresult.Index Or e.ColumnIndex = colresultconversion.Index) Then
            Try
                Dim conversionFactor As Double = Val(dgvResult.CurrentRow.Cells(colconversion.Index).Value)
                If conversionFactor > 0 Then
                    If dgvResult.CurrentCell.ColumnIndex = colresult.Index Then
                        dgvResult.Rows(e.RowIndex).Cells(colresultconversion.Index).Value = Math.Round(Convert.ToDouble(dgvResult.CurrentCell.Value) * conversionFactor, 2)
                    ElseIf dgvResult.CurrentCell.ColumnIndex = colresultconversion.Index Then
                        dgvResult.Rows(e.RowIndex).Cells(colresult.Index).Value = Math.Round(Convert.ToDouble(dgvResult.CurrentCell.Value) / conversionFactor, 2)
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtgridremarks_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgridremarks.TextChanged
        Dim size As Size = TextRenderer.MeasureText(txtgridremarks.Text, txtgridremarks.Font)
        txtgridremarks.Height = size.Height + 10
    End Sub
End Class