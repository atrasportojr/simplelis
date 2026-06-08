<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportHandler
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.crvPrinting = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'crvPrinting
        '
        Me.crvPrinting.ActiveViewIndex = -1
        Me.crvPrinting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvPrinting.Cursor = System.Windows.Forms.Cursors.Default
        Me.crvPrinting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvPrinting.Location = New System.Drawing.Point(0, 0)
        Me.crvPrinting.Name = "crvPrinting"
        Me.crvPrinting.ShowGroupTreeButton = False
        Me.crvPrinting.ShowParameterPanelButton = False
        Me.crvPrinting.ShowRefreshButton = False
        Me.crvPrinting.Size = New System.Drawing.Size(1343, 663)
        Me.crvPrinting.TabIndex = 0
        Me.crvPrinting.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'frmReportHandler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1343, 663)
        Me.Controls.Add(Me.crvPrinting)
        Me.Name = "frmReportHandler"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reports"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvPrinting As CrystalDecisions.Windows.Forms.CrystalReportViewer 
End Class
