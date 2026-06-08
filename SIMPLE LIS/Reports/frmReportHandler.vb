Imports System.Data
Imports System.Data.SqlClient 
Imports System.Configuration
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Web.Mail
Imports System.Collections
Imports System.Drawing.Printing
Imports System.Management
Public Class frmReportHandler
    Public varno As String
    Public printType As String = "History and PE"
    Dim resultrpt

    Private Sub Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call loadreport()
    End Sub

    Private Sub loadreport()
        Dim da As SqlDataAdapter

        Dim gconn As New clsDBConnection
        gconn.CreateOpenConnection()

        Dim conn As New SqlConnection
        conn = gconn.GDBConn

        Dim dt As New DataTable
        If printType = "Crossmatching" Then
            Dim rpt As New crptcrossmatch
            da = New SqlDataAdapter("Exec spLabResultDetailCrossmatching  0,2,0,'','','','','','','','" & varno & "' ", conn)
            da.SelectCommand.CommandTimeout = 1000 : da.Fill(dt)
            rpt.SetDataSource(dt)
            crvPrinting.ReportSource = rpt
            resultrpt = rpt
        End If
    End Sub
End Class