Public Class clsLaboratoryResult
#Region "Variables"
    Dim operation As Integer
    Public soperation As Integer
    Dim mOldlaboratoryid As Long
    Dim mlaboratoryid As Long
    Dim mitemcode As String
    Dim madmissionid As Long
    Dim mpatientrequestno As Long
    Dim mspecimen As String
    Dim mlabno As String
    Dim mdatesubmitted As Date
    Dim mdateencoded As Date
    Dim mencodedby As Long
    Dim mpathologist As Long
    Dim mmedicaltechnologist As Long
    Dim mmedtech As Long
    Dim mverifiedby As Long
    Dim mreleasedby As Long
    Dim mdatereleased As Date
    Dim mremarks As String
    Dim mesigmedtech As Boolean
    Dim mesigverifiedby As Boolean
    Dim mesigpatho As Boolean
    Public caseno As Long
#End Region
#Region "Properties"
    Public Property Oldlaboratoryid() As Long
        Get
            Return mOldlaboratoryid
        End Get
        Set(ByVal value As Long)
            mOldlaboratoryid = value
        End Set
    End Property
    Public Property laboratoryid() As Long
        Get
            Return mlaboratoryid
        End Get
        Set(ByVal value As Long)
            mlaboratoryid = value
        End Set
    End Property
    Public Property itemcode() As String
        Get
            Return mitemcode
        End Get
        Set(ByVal value As String)
            mitemcode = value
        End Set
    End Property
    Public Property itemcatcode() As String
        Get
            Return mitemcode
        End Get
        Set(ByVal value As String)
            mitemcode = value
        End Set
    End Property
    Public Property admissionid() As Long
        Get
            Return madmissionid
        End Get
        Set(ByVal value As Long)
            madmissionid = value
        End Set
    End Property
    Public Property patientrequestno() As Long
        Get
            Return mpatientrequestno
        End Get
        Set(ByVal value As Long)
            mpatientrequestno = value
        End Set
    End Property
    Public Property specimen() As String
        Get
            Return mspecimen
        End Get
        Set(ByVal value As String)
            mspecimen = value
        End Set
    End Property
    Public Property labno() As String
        Get
            Return mlabno
        End Get
        Set(ByVal value As String)
            mlabno = value
        End Set
    End Property
    Public Property datesubmitted() As Date
        Get
            Return mdatesubmitted
        End Get
        Set(ByVal value As Date)
            mdatesubmitted = value
        End Set
    End Property
    Public Property dateencoded() As Date
        Get
            Return mdateencoded
        End Get
        Set(ByVal value As Date)
            mdateencoded = value
        End Set
    End Property
    Public Property encodedby() As Long
        Get
            Return mencodedby
        End Get
        Set(ByVal value As Long)
            mencodedby = value
        End Set
    End Property
    Public Property pathologist() As Long
        Get
            Return mpathologist
        End Get
        Set(ByVal value As Long)
            mpathologist = value
        End Set
    End Property
    Public Property medicaltechnologist() As Long
        Get
            Return mmedicaltechnologist
        End Get
        Set(ByVal value As Long)
            mmedicaltechnologist = value
        End Set
    End Property
    Public Property medtech() As Long
        Get
            Return mmedtech
        End Get
        Set(ByVal value As Long)
            mmedtech = value
        End Set
    End Property
    Public Property verifiedby() As Long
        Get
            Return mverifiedby
        End Get
        Set(ByVal value As Long)
            mverifiedby = value
        End Set
    End Property
    Public Property releasedby() As Long
        Get
            Return mreleasedby
        End Get
        Set(ByVal value As Long)
            mreleasedby = value
        End Set
    End Property
    Public Property datereleased() As Date
        Get
            Return mdatereleased
        End Get
        Set(ByVal value As Date)
            mdatereleased = value
        End Set
    End Property
    Public Property remarks() As String
        Get
            Return mremarks
        End Get
        Set(ByVal value As String)
            mremarks = value
        End Set
    End Property
    Public Property esigmedtech() As Boolean
        Get
            Return mesigmedtech
        End Get
        Set(ByVal value As Boolean)
            mesigmedtech = value
        End Set
    End Property
    Public Property esigverifiedby() As Boolean
        Get
            Return mesigverifiedby
        End Get
        Set(ByVal value As Boolean)
            mesigverifiedby = value
        End Set
    End Property
    Public Property esigpatho() As Boolean
        Get
            Return mesigpatho
        End Get
        Set(ByVal value As Boolean)
            mesigpatho = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Shared Function genericcls(ByVal soperation As Integer, ByVal search As String) As DataTable
        Dim strPar() As String = {"operation", "soperation", "search"}
        Dim strVal() As Object = {0, soperation, search}
        Return GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 0)
    End Function
    Public Shared Function getLaboratoryID(ByVal requestdetailno As String, ByVal LabID As String, ByVal isNew As Integer) As DataTable
        Dim strPar() As String = {"operation", "soperation", "search", "search1"}
        Dim strVal() As Object = {0, isNew, requestdetailno, LabID}
        Return GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 0)
    End Function
    Public Shared Function getRadiologyResultDetails(ByVal requestdetailno As String, ByVal isNew As Integer) As DataTable
        Dim strPar() As String = {"operation", "soperation", "search"}
        Dim strVal() As Object = {0, isNew, requestdetailno}
        Return GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 0)
    End Function
    Public Shared Function getPathologist(ByVal vEmployeeID As String) As DataTable
        Dim strPar() As String = {"operation", "soperation", "search"}
        Dim strVal() As Object = {0, 3, vEmployeeID}
        Return GenericDA.ManageQuery(strPar, strVal, "spRadiology", 0)
    End Function
    Public Shared Function getHospitalInfo() As DataTable
        Dim strPar() As String = {"operation", "soperation"}
        Dim strVal() As Object = {0, 2}
        Return GenericDA.ManageQuery(strPar, strVal, "spimages", 0)
    End Function
    'For displaying MEDTECH
    Public Shared Function getMedtech(ByVal patientReqDetail As Long) As DataTable
        Dim strPar() As String = {"operation", "soperation", "search"}
        Dim strVal() As Object = {0, 11, patientReqDetail}

        Return GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 0)
    End Function
    Public Function Save(ByVal isNew As Boolean) As Long
        Dim operation As Integer
        If isNew Then
            operation = 1
        Else
            operation = 2
        End If
        Dim strPar() As String = {"@operation", "@soperation", "@Oldlaboratoryid", "@laboratoryid", "@itemcode", "@admissionid", "@patientrequestno", "@specimen", "@labno", "@datesubmitted", "@dateencoded", _
                                  "@encodedby", "@pathologist", "@medicaltechnologist", "@medtech", "@verifiedby", "@releasedby", "@datereleased", "@remarks", "esigmedtech", "esigverifiedby", "esigpatho", "@caseno", "NewPK"}
        Dim strVal() As Object = {operation, soperation, Me.Oldlaboratoryid, Me.laboratoryid, Me.itemcode, Me.admissionid, Me.patientrequestno, Me.specimen, Me.labno, Me.datesubmitted, Me.dateencoded, _
                                    Me.encodedby, Me.pathologist, Me.medicaltechnologist, Me.medtech, Me.verifiedby, Me.releasedby, Me.datereleased, Me.remarks, esigmedtech, esigverifiedby, esigpatho, caseno, 1}
        Return GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 2)
    End Function
    Public Shared Sub mergeResult(requestdetailno As Long, oldlabresultid As Long, newlabresultid As Long)
        Dim strPar() As String = {"@operation", "@soperation", "@search", "@search1"}
        Dim strVal() As Object = {2, 2, requestdetailno, newlabresultid}
        GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 1)
    End Sub
    Public Shared Sub unmergeResult(requestdetailno As Long, oldlabresultid As Long, newlabresultid As Long)
        Dim strPar() As String = {"@operation", "@soperation", "@Oldlaboratoryid", "@search", "@search1"}
        Dim strVal() As Object = {2, 3, oldlabresultid, requestdetailno, newlabresultid}
        GenericDA.ManageQuery(strPar, strVal, "spLaboratoryResult", 1)
    End Sub
#End Region
End Class
