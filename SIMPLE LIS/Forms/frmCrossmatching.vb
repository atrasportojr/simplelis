Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Management.Instrumentation
Imports System.Management
Imports SIMPLE_LIS.Utility
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Public Class frmCrossmatching
     
#Region "Variables"
    Public myFormStatus As enFormStatus

    Public Enum enFormStatus
        browsing = 0
        add = 1
        edit = 2
        view = 3
    End Enum
    Dim dtHospitalInfo As New DataTable

    '    'Royette 2021-07-29
    Private afterload As Boolean
    Private isformedit As Boolean
    Private laboratoryid As Long
    Private labname, Vbloodtype As String
    Private requestdetailno As Long
    Public medtech As Long = 0
    Public verifiedby As Long = 0
    Public patho As Long = 0
    Private dtPatientDetails As New DataTable
    Private dtNewBornResults As New DataTable
    Public isLock As Boolean
    Private baseForm As frmResultDesigner
    Private labformatid As LabFormat
    Public myFormaction As formaction

    Public admissionid As Long
    Public itemcode As String
    Public laboratoryresultid As Long
    Public labexaminationno As Long
    Private gender As String

    '--- jay 06022026
    Public mlaboratoryresultcrossmatchingno As Long
    Public mpatientrequestdetailno As Long
    Public mlaboratoryid As Long
    Public madmissionid As Long
    Public mbloodunitserialno As String
    Public mexpirydate As Date
    Public mbloodtype As String
    Public missuedby As String
    Public mwholeblood As Boolean
    Public mpackedredbloodcells As Boolean
    Public mplasma As Boolean
    Public mplateletconcentrate As Boolean
    Public mcryoprecipitate As Boolean
    Public mothers As String
    Public mresultofcrossmatching As String
    Public mcrossmatchdoneby As Long
    Public mcrossmatchdate As Date
    Public mcrossmatchtime As Date
    Public memergencytesting As Boolean
    Public muncrossmatched As Boolean
    Public mcrossmatched As Boolean
    Public msalinephaseonly As Boolean
    Public msalineantiglobulinephase As Boolean
    Public maborhcompatibility As Boolean
    Public mreceivedby As Long
    Public mreceiveddate As Date
    Public mreceivedtime As Date
    Public mcheckedbystaffnurse As String
    Public mcheckedbyheadnurse As String
    Public mcheckedbysupervisor As String
    Public mcheckedbypod As String
    Public mcheckedbypoddate As Date
    Public mcheckedbypodtime As Date
    Public mpvsbp As String
    Public mpvspr As String
    Public mpvsrr As String
    Public mpvstemp As String
    Public mtransfusionstartedby As String
    Public mtransfusionstartedbydate As Date
    Public mtransfusionstartedbytime As Date
    Public mtransfusioncompletedby As String
    Public mtransfusioncompletedbydate As Date
    Public mtransfusioncompletedbytime As Date
    Public mtransfusionremovedby As String
    Public mtransfusionremovedbydate As Date
    Public mtransfusionremovedbytime As Date
    Public mremarkstc As Boolean
    Public mremarksts As Boolean
    Public mremarksfever As Boolean
    Public mremarkschills As Boolean
    Public mremarksnausea As Boolean
    Public mremarksvomiting As Boolean
    Public mremarksflushes As Boolean
    Public mremarksrashes As Boolean
    Public mothervsbp As String
    Public mothervspr As String
    Public mothervsrr As String
    Public mothervstemp As String
    Public mremarksfc As Boolean
    Public mpreparedby As String
    Public mpreparedbydatetime As Date
    Private requestStatus As Integer
    'default grid height
    Public isSave As Boolean
    Private rowheight As Integer = 20
    Public Enum signatory
        medtech
        verifiedby
        patho
    End Enum

    Enum formaction
        NONE = 0
        updateFormat = 1
        manageResult = 2
        Release = 3
        View = 4
    End Enum
#End Region
#Region "Constructor"
    Public Sub New(ByVal baseForm As frmResultDesigner, ByVal isformedit As Boolean, ByVal laboratoryid As Long, ByVal labname As String, ByVal isLock As Boolean, ByVal format As LabFormat)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.isformedit = isformedit
        Me.laboratoryid = laboratoryid
        Me.labname = labname
        Me.isLock = Not isLock
        Me.baseForm = baseForm
        Me.labformatid = format
    End Sub

#End Region
#Region "New Methods"
    Sub New(ByVal FormStatus As enFormStatus)
        InitializeComponent()
        myFormStatus = FormStatus
        
    End Sub
    Public Sub loadRequestDetails(ByVal requestdetailno As Long)
        Me.requestdetailno = requestdetailno
        dtPatientDetails = clsCrossmatching.genericcls(2, requestdetailno)
        Me.admissionid = Utility.NullToZero(dtPatientDetails.Rows(0).Item("admissionid"))
        Me.mlaboratoryresultcrossmatchingno = Utility.NullToZero(dtPatientDetails.Rows(0).Item("laboratoryresultcrossmatchingno"))
        Me.laboratoryresultid = Utility.NullToZero(dtPatientDetails.Rows(0).Item("laboratoryresultid"))
        Me.itemcode = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("itemcode").ToString)
        Call BlankForm()
        Me.lblfname.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("firstname").ToString)
        Me.lblmname.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("middlename").ToString)
        Me.lbllname.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("lastname").ToString)
        Me.lblcaseno.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("hospitalno").ToString)
        Me.lblage.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("age").ToString)
        Me.lblgender.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("gender").ToString)
        Me.lblroom.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("room").ToString)
        Me.cmbbloodtype.SelectedValue = Trim(Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("bloodtype").ToString))
        Me.cmbbloodtyperh.SelectedValue = Trim(Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("bloodtyperh").ToString))
        Me.txtserialno.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("bloodunitserialno").ToString)
        Me.dtpserialno.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("expirydate").ToString)
        Me.txtissuedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("issuedby").ToString)
        Me.chkwholeblood.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("wholeblood"))
        Me.chkredbloodcells.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("packedredbloodcells"))
        Me.chkplasma.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("plasma"))
        Me.chkplate.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("plateletconcentrate"))
        Me.chkcryo.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("cryoprecipitate"))
        Me.txtothers.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("others").ToString)
        Me.txtresult.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("resultofcrossmatching").ToString)
        Me.cmbmedtech.SelectedValue = Utility.NullToZero(dtPatientDetails.Rows(0).Item("crossmatchdoneby"))
        Me.dtpcrossmatchdate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("crossmatchdate").ToString)
        Me.dtpcrossmatchtime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("crossmatchtime").ToString)
        Me.chkemergencytest.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("emergencytesting"))
        Me.chkuncrossmatched.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("uncrossmatched"))
        Me.chkcrossmatched.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("crossmatched"))
        Me.chksaline.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("salinephaseonly"))
        Me.chkantiglobulin.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("salineantiglobulinephase"))
        Me.chkaborh.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("aborhcompatibility"))
        Me.txtreceivedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("receivedby").ToString)
        Me.dtpreceivedbydate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("receiveddate").ToString)
        Me.dtpreceivedbytime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("receivedtime").ToString)
        Me.txtSN.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("checkedbystaffnurse").ToString)
        Me.txtHN.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("checkedbyheadnurse").ToString)
        Me.txtNS.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("checkedbysupervisor").ToString)
        Me.txtPOD.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("checkedbypod").ToString)
        Me.dtppoddate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("checkedbypoddate").ToString)
        Me.dtppodtime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("checkedbypodtime").ToString)
        Me.txtpbp.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("pvsbp").ToString)
        Me.txtppr.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("pvspr").ToString)
        Me.txtprr.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("pvsrr").ToString)
        Me.txtptemp.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("pvstemp").ToString)
        Me.txtstartedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("transfusionstartedby").ToString)
        Me.dtpstartedbydate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusionstartedbydate").ToString)
        Me.dtpstartedbytime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusionstartedbytime").ToString)
        Me.txtcompletedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("transfusioncompletedby").ToString)
        Me.dtpcompletedbydate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusioncompletedbydate").ToString)
        Me.dtpcompletedbytime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusioncompletedbytime").ToString)
        Me.txtremovedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("transfusionremovedby").ToString)
        Me.dtpremovedbydate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusionremovedbydate").ToString)
        Me.dtpremovedbytime.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("transfusionremovedbytime").ToString)
        Me.chkcompleted.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarkstc"))
        Me.chkstopped.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksts"))
        Me.chkfever.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksfever"))
        Me.chkchills.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarkschills"))
        Me.chknausea.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksnausea"))
        Me.chkvomiting.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksvomiting"))
        Me.chkflushes.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksflushes"))
        Me.chkrashes.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksrashes"))
        Me.txtvsbp.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("othervsbp").ToString)
        Me.txtvspr.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("othervspr").ToString)
        Me.txtvsrr.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("othervsrr").ToString)
        Me.txtvstemp.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("othervstemp").ToString)
        Me.chkreactionstudies.Checked = Utility.NullToBoolean(dtPatientDetails.Rows(0).Item("remarksfc"))
        Me.txtpreparedby.Text = Utility.NullToEmptyString(dtPatientDetails.Rows(0).Item("preparedby").ToString)
        Me.dtpprepareddate.Value = Utility.NullToCurrentDate(dtPatientDetails.Rows(0).Item("preparedbydatetime").ToString)
        Call lock()
    End Sub

    Private Sub BlankForm()
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("UNKNOWN", "U"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("A", "A"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("B", "B"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("O", "O"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("AB", "AB"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""A"" POSITIVE", "A+"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""A"" NEGATIVE", "A-"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""B"" POSITIVE", "B+"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""B"" NEGATIVE", "B-"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""O"" POSITIVE", "O+'"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""O"" NEGATIVE", "O-"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""AB"" POSITIVE", "AB+"))
        Me.cmbbloodtype.Items.Add(New DictionaryEntry("""AB"" NEGATIVE", "AB-"))

        Me.cmbbloodtype.DisplayMember = "Key"
        Me.cmbbloodtype.ValueMember = "Value"
        Me.cmbbloodtype.DataSource = Me.cmbbloodtype.Items
        If laboratoryresultid = 0 Then
            Me.cmbbloodtype.SelectedIndex = 0
        End If

        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("UNKNOWN", "U"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""A"" Rh Positive", "ARh+"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""A"" Rh Negative", "ARh-"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""B"" Rh Positive", "BRh+"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""B"" Rh Negative", "BRh-"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""O"" Rh Positive", "ORh+'"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""O"" Rh Negative", "ORh-"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""AB"" Rh Positive", "ABRh+"))
        Me.cmbbloodtyperh.Items.Add(New DictionaryEntry("""AB"" Rh Negative", "ABRh-"))

        Me.cmbbloodtyperh.DisplayMember = "Key"
        Me.cmbbloodtyperh.ValueMember = "Value"
        Me.cmbbloodtyperh.DataSource = Me.cmbbloodtyperh.Items
        If laboratoryresultid = 0 Then
            Me.cmbbloodtyperh.SelectedIndex = 0
        End If

        Me.cmbmedtech.DataSource = clsLaboratoryResult.getPathologist(clsModel.EmployeeTypes.medtech)
        Me.cmbmedtech.DisplayMember = "radiologist"
        Me.cmbmedtech.ValueMember = "employeeid"
        If laboratoryresultid = 0 Then
            Me.cmbmedtech.SelectedIndex = 0
        End If
    End Sub
    Public Sub saveNow(ByRef action As String)
        'If MsgBox("Are you sure you want to " & action & " this laboratory result?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, modGlobal.msgboxTitle) = MsgBoxResult.Yes Then
        isSave = True
        Call SaveRecord() 
        MsgBox("Crossmatching result successfully " + action + "d.", MsgBoxStyle.OkOnly, modGlobal.msgboxTitle)
        Call lock()

        'End If
    End Sub

    Private Sub SaveRecord()
        '******************** save labresult
        Dim myLaboratoryResult As New clsLaboratoryResult
        With myLaboratoryResult

            .laboratoryid = Me.laboratoryid
            .admissionid = Me.admissionid
            .patientrequestno = Me.requestdetailno 
            .datesubmitted = GetServerDate()
            .dateencoded = GetServerDate()
            .itemcode = itemcode
            .encodedby = userid
            .medtech = cmbmedtech.SelectedValue
            .medicaltechnologist = .medtech
            .esigpatho = 1
            .releasedby = 1
            .datereleased = GetServerDate() '"01/01/1990"  
            If Me.laboratoryresultid = 0 Then
                .Save(True)
                Call SaveLog("Crossmatching", "New Crossmatching result:" & .patientrequestno, userid)
            Else
                .Oldlaboratoryid = Me.laboratoryresultid
                .Save(False)
                Call SaveLog("Crossmatching", "Update Crossmatching result:" & .patientrequestno, userid)
            End If
        End With

        Dim myCrossmatchingResult As New clsCrossmatching
        With myCrossmatchingResult
            .mlaboratoryresultcrossmatchingno = Me.mlaboratoryresultcrossmatchingno
            .mpatientrequestno = Me.requestdetailno
            .mlaboratoryid = Me.laboratoryresultid
            .mbloodunitserialno = Me.txtserialno.Text
            .mexpirydate = Me.dtpserialno.Value
            .mbloodtype = Me.cmbbloodtyperh.SelectedValue
            .missuedby = Me.txtissuedby.Text
            .mwholeblood = Me.chkwholeblood.Checked
            .mpackedredbloodcells = Me.chkredbloodcells.Checked
            .mplasma = Me.chkplasma.Checked
            .mplateletconcentrate = Me.chkplate.Checked
            .mcryoprecipitate = Me.chkcryo.Checked
            .mothers = Me.txtothers.Text
            .mresultofcrossmatching = Me.txtresult.Text
            .mcrossmatchdoneby = Me.cmbmedtech.SelectedValue
            .mcrossmatchdate = Me.dtpcrossmatchdate.Value
            .mcrossmatchtime = Me.dtpcrossmatchtime.Value
            .memergencytesting = Me.chkemergencytest.Checked
            .muncrossmatched = Me.chkuncrossmatched.Checked
            .mcrossmatched = Me.chkcrossmatched.Checked
            .msalinephaseonly = Me.chksaline.Checked
            .msalineantiglobulinephase = Me.chkantiglobulin.Checked
            .maborhcompatibility = Me.chkaborh.Checked
            .mreceivedby = Me.txtreceivedby.Text
            .mreceiveddate = Me.dtpreceivedbydate.Value
            .mreceivedtime = Me.dtpreceivedbytime.Value
            .mcheckedbystaffnurse = Me.txtSN.Text
            .mcheckedbyheadnurse = Me.txtHN.Text
            .mcheckedbysupervisor = Me.txtNS.Text
            .mcheckedbypod = Me.txtPOD.Text
            .mcheckedbypoddate = Me.dtppoddate.Value
            .mcheckedbypodtime = Me.dtppodtime.Value
            .mpvsbp = Me.txtpbp.Text
            .mpvspr = Me.txtppr.Text
            .mpvsrr = Me.txtprr.Text
            .mpvstemp = Me.txtptemp.Text
            .mtransfusionstartedby = Me.txtstartedby.Text
            .mtransfusionstartedbydate = Me.dtpstartedbydate.Value
            .mtransfusionstartedbytime = Me.dtpstartedbytime.Value
            .mtransfusioncompletedby = Me.txtcompletedby.Text
            .mtransfusioncompletedbydate = Me.dtpcompletedbydate.Value
            .mtransfusioncompletedbytime = Me.dtpcompletedbytime.Value
            .mtransfusionremovedby = Me.txtremovedby.Text
            .mtransfusionremovedbydate = Me.dtpremovedbydate.Value
            .mtransfusionremovedbytime = Me.dtpremovedbytime.Value
            .mremarkstc = Me.chkcompleted.Checked
            .mremarksts = Me.chkstopped.Checked
            .mremarksfever = Me.chkfever.Checked
            .mremarkschills = Me.chkchills.Checked
            .mremarksnausea = Me.chknausea.Checked
            .mremarksvomiting = Me.chkvomiting.Checked
            .mremarksflushes = Me.chkflushes.Checked
            .mremarksrashes = Me.chkrashes.Checked
            .mothervsbp = Me.txtvsbp.Text
            .mothervspr = Me.txtvspr.Text
            .mothervsrr = Me.txtvsrr.Text
            .mothervstemp = Me.txtvstemp.Text
            .mremarksfc = Me.chkreactionstudies.Checked
            .mpreparedby = Me.txtpreparedby.Text
            .mpreparedbydatetime = Me.dtpprepareddate.Value

            If Me.laboratoryresultid = 0 Then
                .SaveDetails(True)
                Call SaveLog("Crossmatching", "New Crossmatching result:" & .mpatientrequestno, userid)
            Else
                .mOldlaboratoryid = Me.laboratoryresultid
                .SaveDetails(False)
                Call SaveLog("Crossmatching", "Update Crossmatching result:" & .mpatientrequestno, userid)
            End If
            isLock = False
        End With
    End Sub
    Public Sub lock()
        Me.grppatientdetails.Enabled = isLock
        Me.grpresultdetails.Enabled = isLock
        Me.grpbloodtransfusion.Enabled = isLock
    End Sub
#Region "Printing"
    Public Sub PrintPreview(Optional ByVal tool As Integer = 0)
        Dim handler As New frmReportHandler
        handler.varno = Me.requestdetailno
        handler.printType = "Crossmatching"
        handler.ShowDialog()
        handler = Nothing
    End Sub
#End Region
#End Region
End Class