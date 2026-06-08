Imports System.Data.SqlClient
Imports System.Configuration
Imports Newtonsoft.Json

Public Class frmMain
#Region "Variables"

    Private laboratoryid As String
    Private employeeid As Long
    Private requestdetailno As Long
    Private target As targetmodule
    Private myformaction As enformstatus
    Public dbSettings As New DatabaseSettings

    Public Class DatabaseSettings
        Public Host As String
        Public DatabaseName As String
        Public AuthType As String
        Public UserId As String
        Public Password As String
    End Class

    Enum targetmodule
        manageresult = 0 'Encode/View/Release Result
        updateformat = 1 'Modify Test Format
        manageprocedure = 2 'Create or Update Lab/Rad Name
        LISDashboard = 3
    End Enum
    Enum enformstatus
        add = 0
        edit = 1
        view_release = 3
    End Enum
#End Region
#Region "Constructors"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        With dbSettings
            .Host = ConfigurationManager.AppSettings("gServername")
            .DatabaseName = ConfigurationManager.AppSettings("gDatabasename")
            .AuthType = ConfigurationManager.AppSettings("gAuthType")
            .UserId = ConfigurationManager.AppSettings("gUsername")
            .Password = Utility.Decrypt(ConfigurationManager.AppSettings("gPassword"))
        End With
        laboratoryid = 1
        requestdetailno = 1628 ''maceda '1156 maceda '1569 cabebe '1572 PERPETUA '1575 Martinez '1582 Nobleza
        employeeid = 1693 'ecomed@lab 1031 'ecomed@rad 1011 'lhi@lab 1693 'lhi@rad 1361 'hipol@lab 1081 'lhirad 1361
        myformaction = enformstatus.edit
        target = targetmodule.LISDashboard
        modGlobal.userid = employeeid
    End Sub
    Public Sub New(ByVal Host As String,
         ByVal DatabaseName As String,
         ByVal AuthType As String,
         ByVal UserId As String,
         ByVal Password As String,
         ByVal target As Integer,
         ByVal myformaction As Integer,
         ByVal employeeid As Long,
         ByVal requestdetailno As Long,
         ByVal laboratoryid As Long)

        ' This call is required by the designer.
        InitializeComponent()
        Me.target = target
        Me.myformaction = myformaction
        Me.laboratoryid = laboratoryid 'Supply Only If Editing Result Format or update procedure name/ 0 if create new procedure
        Me.requestdetailno = requestdetailno 'Supply Only If Encoding result, Edit, View and Release
        With dbSettings
            .Host = Host
            .DatabaseName = DatabaseName
            .AuthType = AuthType
            .UserId = UserId
            .Password = Password
        End With
        modGlobal.userid = employeeid
        'MsgBox(Me.myformaction.ToString() & " " & Me.target.ToString() & " labid:" & Me.laboratoryid & " detail:" & requestdetailno)
    End Sub
#End Region
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If System.IO.File.Exists(jsonConfigFileName) Then
            Dim reader As New System.IO.StreamReader(jsonConfigFileName)
            appSetting = JsonConvert.DeserializeObject(Of AppSettingsModule.appSettings)(reader.ReadToEnd())
        End If

        With dbSettings
            If .AuthType = 1 Then
                modGlobal.gconnectionstring = "Data Source=" & .Host & _
                                        ";Database=" & .DatabaseName & _
                                        ";User ID=" & .UserId & _
                                        ";Password=" & .Password & "; Pooling=True; Max Pool Size=30;Min Pool Size=0; Pooling=True;Connect Timeout=90;"
            Else
                modGlobal.gconnectionstring = "Data Source=" & .Host & ";Initial Catalog=" & .DatabaseName & ";Trusted_Connection=True;Connection Timeout=60"
            End If
        End With
        'If Me.requestdetailno > 0 Then
        '    target = targetmodule.manageresult
        'End If
        'frmNotif.Show()
        'frmNotif.StartBroadCast()
        setUserInfo()
        LoadUserSubModules()
        Me.Hide()
        If target = targetmodule.manageresult Then
getResultDetails:
            Dim dt As DataTable = clsExaminationUpshots.getPatientRequestStatus(requestdetailno)
            '' if status is 3
            'If dt.Rows(0)(0) = 3 Then
            '    Dim frmDesigner As New frmResultDesigner(frmResultDesigner.formaction.manageResult, requestdetailno, 0, dt.Rows(0)(0))
            '    frmDesigner.ShowDialog()
            'ElseIf Me.myformaction = enformstatus.edit And (dt.Rows(0)(0) = 4 Or dt.Rows(0)(0) = 5) Then
            '    Dim frmDesigner As New frmResultDesigner(frmResultDesigner.formaction.manageResult, requestdetailno, 0, dt.Rows(0)(0))
            '    frmDesigner.ShowDialog()
            'ElseIf Me.myformaction = enformstatus.view_release Then
            '    If dt.Rows(0)(0) = 4 Then
            '        Dim frmDesigner As New frmResultDesigner(frmResultDesigner.formaction.Release, requestdetailno, 0, dt.Rows(0)(0))
            '        frmDesigner.ShowDialog()
            '    ElseIf dt.Rows(0)(0) = 5 Or dt.Rows(0)(0) = 6 Then
            '        Dim frmDesigner As New frmResultDesigner(frmResultDesigner.formaction.View, requestdetailno, 0, dt.Rows(0)(0))
            '        frmDesigner.ShowDialog()
            '    ElseIf dt.Rows(0)(0) = 3 Then
            '        MsgBox("Request doesn't have result yet", MsgBoxStyle.Information)
            '    End If
            'End If
            Dim frmDesigner As frmResultDesigner
            If Me.myformaction = enformstatus.add AndAlso dt.Rows(0)(0) = 3 Then
                frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.manageResult, requestdetailno, 0, dt.Rows(0)(0))
                frmDesigner.ShowDialog()
            ElseIf Me.myformaction = enformstatus.edit AndAlso (dt.Rows(0)(0) = 4 Or dt.Rows(0)(0) = 5) Then
                If dt.Rows(0)(0) = 5 Then
                    Dim foverride As New frmRequestedBy(frmRequestedBy.enPurpose.editResult, modGlobal.sourceOfficeCode)
                    foverride.ShowDialog()
                    If Not foverride.isLoginValid Then
                        Me.Close()
                        Exit Sub
                    End If
                    modGlobal.SaveLog("Diagnostics", "Attempt to edit a released examination ReqNo: " & Me.requestdetailno & " (Override By: " & foverride.RequestedByID & ")")
                End If
                frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.manageResult, requestdetailno, 0, dt.Rows(0)(0))
                frmDesigner.ShowDialog()
            ElseIf Me.myformaction = enformstatus.view_release Then
                If dt.Rows(0)(0) = 4 And modGlobal.sourceOfficeCode = dt.Rows(0).Item("destinationoffice") Then
                    frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.Release, requestdetailno, 0, dt.Rows(0)(0))
                    frmDesigner.ShowDialog()
                ElseIf dt.Rows(0)(0) = 5 Or dt.Rows(0)(0) = 6 Then
                    frmDesigner = New frmResultDesigner(frmResultDesigner.formaction.View, requestdetailno, 0, dt.Rows(0)(0))
                    frmDesigner.ShowDialog()
                Else 'If dt.Rows(0)(0) = 3 Then
                    MsgBox("Request doesn't have result yet", MsgBoxStyle.Information)
                End If
            End If
            If Not frmDesigner Is Nothing AndAlso frmDesigner.mergeToogle = 1 Then
                Me.myformaction = enformstatus.view_release
                GoTo getResultDetails
            ElseIf Not frmDesigner Is Nothing AndAlso frmDesigner.mergeToogle = 2 Then
                Me.myformaction = enformstatus.add
                GoTo getResultDetails
            End If
        ElseIf target = targetmodule.updateformat Then
            Dim f As New frmResultDesigner(frmResultDesigner.formaction.updateFormat, 0, laboratoryid, 0)
            f.ShowDialog()
        ElseIf target = targetmodule.manageprocedure Then
            Dim f As New frmLaboratory(IIf(laboratoryid > 0, frmLaboratory.formstatus.edit, frmLaboratory.formstatus.add), laboratoryid)
            f.ShowDialog()
        ElseIf target = targetmodule.LISDashboard Then
            Dim f As New frmDashboard
            f.ShowDialog()
        End If
        Me.Close()
    End Sub
End Class
