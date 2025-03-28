<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmtemplateRTF
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmtemplateRTF))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbResult = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tbllayoutpanel = New System.Windows.Forms.TableLayoutPanel()
        Me.panelpreviousresult = New System.Windows.Forms.Panel()
        Me.txtpreviousresult = New System.Windows.Forms.RichTextBox()
        Me.AxAcroPDFPrevious = New AxAcroPDFLib.AxAcroPDF()
        Me.panelcurrentresult = New System.Windows.Forms.Panel()
        Me.txtResult = New System.Windows.Forms.RichTextBox()
        Me.AxAcroPDFCurrent = New AxAcroPDFLib.AxAcroPDF()
        Me.paneleditortools = New System.Windows.Forms.Panel()
        Me.tscontainerrtfeditor = New System.Windows.Forms.ToolStrip()
        Me.tsfontfamily = New System.Windows.Forms.ToolStripComboBox()
        Me.tsfonsize = New System.Windows.Forms.ToolStripComboBox()
        Me.tsfontcolor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbold = New System.Windows.Forms.ToolStripButton()
        Me.tsunderline = New System.Windows.Forms.ToolStripButton()
        Me.tsitalize = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsalignleft = New System.Windows.Forms.ToolStripButton()
        Me.tsaligncenter = New System.Windows.Forms.ToolStripButton()
        Me.tsalignright = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsundo = New System.Windows.Forms.ToolStripButton()
        Me.tsredo = New System.Windows.Forms.ToolStripButton()
        Me.tscontainerwordeditor = New System.Windows.Forms.ToolStrip()
        Me.tseditresultpdf = New System.Windows.Forms.ToolStripButton()
        Me.tsreloadresultpdf = New System.Windows.Forms.ToolStripButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvImageAddress = New System.Windows.Forms.DataGridView()
        Me.tsImageTools = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonbrowswimage = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonremovethisimage = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.CenterImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StretchImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Picture = New System.Windows.Forms.PictureBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.paneladdfilm = New System.Windows.Forms.Panel()
        Me.btnAddToList = New System.Windows.Forms.Button()
        Me.cmbItemCodeFilm = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.DGVFilm = New System.Windows.Forms.DataGridView()
        Me.colremove = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.tsSave = New System.Windows.Forms.ToolStripButton()
        Me.tsCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsClose = New System.Windows.Forms.ToolStripButton()
        Me.cmbRadTech = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPatientname = New System.Windows.Forms.Label()
        Me.lblPatientname = New System.Windows.Forms.Label()
        Me.txtGender = New System.Windows.Forms.Label()
        Me.txtAge = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.dtDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.LineShape3 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape4 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.lblexamination = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbradiologist = New System.Windows.Forms.ComboBox()
        Me.cmbpreviousresult = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblchiefcomplaint = New System.Windows.Forms.Label()
        Me.lblrequestedby = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkesig = New System.Windows.Forms.CheckBox()
        Me.lblward = New System.Windows.Forms.Label()
        Me.txtward = New System.Windows.Forms.Label()
        Me.lblcaseno = New System.Windows.Forms.Label()
        Me.txtcaseno = New System.Windows.Forms.TextBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colimagename = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colimagedesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.collocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colimageid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colitemcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colfilmname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colnooffilms = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colchargedetailsid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbResult.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.tbllayoutpanel.SuspendLayout()
        Me.panelpreviousresult.SuspendLayout()
        CType(Me.AxAcroPDFPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelcurrentresult.SuspendLayout()
        CType(Me.AxAcroPDFCurrent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.paneleditortools.SuspendLayout()
        Me.tscontainerrtfeditor.SuspendLayout()
        Me.tscontainerwordeditor.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvImageAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsImageTools.SuspendLayout()
        CType(Me.Picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.paneladdfilm.SuspendLayout()
        CType(Me.DGVFilm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Animate.png")
        Me.ImageList1.Images.SetKeyName(1, "note-add.png")
        Me.ImageList1.Images.SetKeyName(2, "admin.png")
        Me.ImageList1.Images.SetKeyName(3, "InsertHyperlinkHS.png")
        Me.ImageList1.Images.SetKeyName(4, "image_png.png")
        Me.ImageList1.Images.SetKeyName(5, "delete.png")
        Me.ImageList1.Images.SetKeyName(6, "collapsed.gif")
        Me.ImageList1.Images.SetKeyName(7, "delete.png")
        Me.ImageList1.Images.SetKeyName(8, "view1.png")
        '
        'tbResult
        '
        Me.tbResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbResult.Controls.Add(Me.TabPage1)
        Me.tbResult.Controls.Add(Me.TabPage2)
        Me.tbResult.Controls.Add(Me.TabPage3)
        Me.tbResult.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tbResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbResult.ImageList = Me.ImageList1
        Me.tbResult.Location = New System.Drawing.Point(3, 100)
        Me.tbResult.Name = "tbResult"
        Me.tbResult.SelectedIndex = 0
        Me.tbResult.Size = New System.Drawing.Size(1132, 510)
        Me.tbResult.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.tbResult.TabIndex = 37
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.tbllayoutpanel)
        Me.TabPage1.ImageIndex = 6
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1124, 483)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "      Result      "
        '
        'tbllayoutpanel
        '
        Me.tbllayoutpanel.ColumnCount = 2
        Me.tbllayoutpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tbllayoutpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tbllayoutpanel.Controls.Add(Me.panelpreviousresult, 1, 0)
        Me.tbllayoutpanel.Controls.Add(Me.panelcurrentresult, 0, 0)
        Me.tbllayoutpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbllayoutpanel.Location = New System.Drawing.Point(3, 3)
        Me.tbllayoutpanel.Name = "tbllayoutpanel"
        Me.tbllayoutpanel.RowCount = 2
        Me.tbllayoutpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tbllayoutpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tbllayoutpanel.Size = New System.Drawing.Size(1118, 477)
        Me.tbllayoutpanel.TabIndex = 53
        '
        'panelpreviousresult
        '
        Me.panelpreviousresult.Controls.Add(Me.txtpreviousresult)
        Me.panelpreviousresult.Controls.Add(Me.AxAcroPDFPrevious)
        Me.panelpreviousresult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelpreviousresult.Location = New System.Drawing.Point(562, 3)
        Me.panelpreviousresult.Name = "panelpreviousresult"
        Me.panelpreviousresult.Size = New System.Drawing.Size(553, 451)
        Me.panelpreviousresult.TabIndex = 281
        '
        'txtpreviousresult
        '
        Me.txtpreviousresult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtpreviousresult.Location = New System.Drawing.Point(0, 0)
        Me.txtpreviousresult.Name = "txtpreviousresult"
        Me.txtpreviousresult.ReadOnly = True
        Me.txtpreviousresult.Size = New System.Drawing.Size(553, 451)
        Me.txtpreviousresult.TabIndex = 53
        Me.txtpreviousresult.Text = ""
        '
        'AxAcroPDFPrevious
        '
        Me.AxAcroPDFPrevious.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxAcroPDFPrevious.Enabled = True
        Me.AxAcroPDFPrevious.Location = New System.Drawing.Point(0, 0)
        Me.AxAcroPDFPrevious.Name = "AxAcroPDFPrevious"
        Me.AxAcroPDFPrevious.OcxState = CType(resources.GetObject("AxAcroPDFPrevious.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxAcroPDFPrevious.Size = New System.Drawing.Size(553, 451)
        Me.AxAcroPDFPrevious.TabIndex = 234
        Me.AxAcroPDFPrevious.Visible = False
        '
        'panelcurrentresult
        '
        Me.panelcurrentresult.Controls.Add(Me.txtResult)
        Me.panelcurrentresult.Controls.Add(Me.AxAcroPDFCurrent)
        Me.panelcurrentresult.Controls.Add(Me.paneleditortools)
        Me.panelcurrentresult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelcurrentresult.Location = New System.Drawing.Point(3, 3)
        Me.panelcurrentresult.Name = "panelcurrentresult"
        Me.panelcurrentresult.Size = New System.Drawing.Size(553, 451)
        Me.panelcurrentresult.TabIndex = 280
        '
        'txtResult
        '
        Me.txtResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtResult.EnableAutoDragDrop = True
        Me.txtResult.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResult.Location = New System.Drawing.Point(0, 24)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.Size = New System.Drawing.Size(553, 427)
        Me.txtResult.TabIndex = 52
        Me.txtResult.Text = ""
        '
        'AxAcroPDFCurrent
        '
        Me.AxAcroPDFCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxAcroPDFCurrent.Enabled = True
        Me.AxAcroPDFCurrent.Location = New System.Drawing.Point(0, 24)
        Me.AxAcroPDFCurrent.Name = "AxAcroPDFCurrent"
        Me.AxAcroPDFCurrent.OcxState = CType(resources.GetObject("AxAcroPDFCurrent.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxAcroPDFCurrent.Size = New System.Drawing.Size(553, 427)
        Me.AxAcroPDFCurrent.TabIndex = 234
        '
        'paneleditortools
        '
        Me.paneleditortools.Controls.Add(Me.tscontainerrtfeditor)
        Me.paneleditortools.Controls.Add(Me.tscontainerwordeditor)
        Me.paneleditortools.Dock = System.Windows.Forms.DockStyle.Top
        Me.paneleditortools.Location = New System.Drawing.Point(0, 0)
        Me.paneleditortools.Name = "paneleditortools"
        Me.paneleditortools.Size = New System.Drawing.Size(553, 24)
        Me.paneleditortools.TabIndex = 53
        '
        'tscontainerrtfeditor
        '
        Me.tscontainerrtfeditor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsfontfamily, Me.tsfonsize, Me.tsfontcolor, Me.ToolStripSeparator5, Me.tsbold, Me.tsunderline, Me.tsitalize, Me.ToolStripSeparator3, Me.tsalignleft, Me.tsaligncenter, Me.tsalignright, Me.ToolStripSeparator4, Me.tsundo, Me.tsredo})
        Me.tscontainerrtfeditor.Location = New System.Drawing.Point(0, 0)
        Me.tscontainerrtfeditor.Name = "tscontainerrtfeditor"
        Me.tscontainerrtfeditor.Size = New System.Drawing.Size(553, 25)
        Me.tscontainerrtfeditor.TabIndex = 0
        Me.tscontainerrtfeditor.Text = "ToolStrip1"
        '
        'tsfontfamily
        '
        Me.tsfontfamily.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tsfontfamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tsfontfamily.Name = "tsfontfamily"
        Me.tsfontfamily.Size = New System.Drawing.Size(121, 25)
        '
        'tsfonsize
        '
        Me.tsfonsize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tsfonsize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tsfonsize.Name = "tsfonsize"
        Me.tsfonsize.Size = New System.Drawing.Size(75, 25)
        Me.tsfonsize.ToolTipText = "Accepts 7 to 72 font sizes"
        '
        'tsfontcolor
        '
        Me.tsfontcolor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsfontcolor.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_text_color_16
        Me.tsfontcolor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsfontcolor.Name = "tsfontcolor"
        Me.tsfontcolor.Size = New System.Drawing.Size(23, 22)
        Me.tsfontcolor.Text = "A"
        Me.tsfontcolor.ToolTipText = "Font Color"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'tsbold
        '
        Me.tsbold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbold.Image = CType(resources.GetObject("tsbold.Image"), System.Drawing.Image)
        Me.tsbold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbold.Name = "tsbold"
        Me.tsbold.Size = New System.Drawing.Size(23, 22)
        Me.tsbold.Text = "ToolStripButton2"
        Me.tsbold.ToolTipText = "Bold"
        '
        'tsunderline
        '
        Me.tsunderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsunderline.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_underline_16
        Me.tsunderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsunderline.Name = "tsunderline"
        Me.tsunderline.Size = New System.Drawing.Size(23, 22)
        Me.tsunderline.Text = "ToolStripButton2"
        Me.tsunderline.ToolTipText = "Underline"
        '
        'tsitalize
        '
        Me.tsitalize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsitalize.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_italic_16
        Me.tsitalize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsitalize.Name = "tsitalize"
        Me.tsitalize.Size = New System.Drawing.Size(23, 22)
        Me.tsitalize.Text = "ToolStripButton2"
        Me.tsitalize.ToolTipText = "Italize"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsalignleft
        '
        Me.tsalignleft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsalignleft.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_align_left_16
        Me.tsalignleft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsalignleft.Name = "tsalignleft"
        Me.tsalignleft.Size = New System.Drawing.Size(23, 22)
        Me.tsalignleft.Text = "ToolStripButton2"
        Me.tsalignleft.ToolTipText = "Align Left"
        '
        'tsaligncenter
        '
        Me.tsaligncenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsaligncenter.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_align_center_16
        Me.tsaligncenter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsaligncenter.Name = "tsaligncenter"
        Me.tsaligncenter.Size = New System.Drawing.Size(23, 22)
        Me.tsaligncenter.Text = "ToolStripButton2"
        Me.tsaligncenter.ToolTipText = "Align Center"
        '
        'tsalignright
        '
        Me.tsalignright.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsalignright.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_align_right_16
        Me.tsalignright.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsalignright.Name = "tsalignright"
        Me.tsalignright.Size = New System.Drawing.Size(23, 22)
        Me.tsalignright.Text = "ToolStripButton2"
        Me.tsalignright.ToolTipText = "Align Right"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsundo
        '
        Me.tsundo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsundo.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_undo_16
        Me.tsundo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsundo.Name = "tsundo"
        Me.tsundo.Size = New System.Drawing.Size(23, 22)
        Me.tsundo.Text = "ToolStripButton2"
        Me.tsundo.ToolTipText = "Undo"
        '
        'tsredo
        '
        Me.tsredo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsredo.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_redo_16
        Me.tsredo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsredo.Name = "tsredo"
        Me.tsredo.Size = New System.Drawing.Size(23, 22)
        Me.tsredo.Text = "ToolStripButton2"
        Me.tsredo.ToolTipText = "Redo"
        '
        'tscontainerwordeditor
        '
        Me.tscontainerwordeditor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tseditresultpdf, Me.tsreloadresultpdf})
        Me.tscontainerwordeditor.Location = New System.Drawing.Point(0, 0)
        Me.tscontainerwordeditor.Name = "tscontainerwordeditor"
        Me.tscontainerwordeditor.Size = New System.Drawing.Size(553, 25)
        Me.tscontainerwordeditor.TabIndex = 1
        Me.tscontainerwordeditor.Text = "ToolStrip2"
        Me.tscontainerwordeditor.Visible = False
        '
        'tseditresultpdf
        '
        Me.tseditresultpdf.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_edit_16
        Me.tseditresultpdf.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tseditresultpdf.Name = "tseditresultpdf"
        Me.tseditresultpdf.Size = New System.Drawing.Size(82, 22)
        Me.tseditresultpdf.Text = "Edit Result"
        Me.tseditresultpdf.ToolTipText = "Underline"
        Me.tseditresultpdf.Visible = False
        '
        'tsreloadresultpdf
        '
        Me.tsreloadresultpdf.Image = Global.SIMPLE_LIS.My.Resources.Resources.ic_redo_16
        Me.tsreloadresultpdf.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsreloadresultpdf.Name = "tsreloadresultpdf"
        Me.tsreloadresultpdf.Size = New System.Drawing.Size(98, 22)
        Me.tsreloadresultpdf.Text = "Reload Result"
        Me.tsreloadresultpdf.ToolTipText = "Underline"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage2.ImageIndex = 6
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1124, 483)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "     Image     "
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Picture, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1118, 477)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgvImageAddress)
        Me.Panel1.Controls.Add(Me.tsImageTools)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(413, 451)
        Me.Panel1.TabIndex = 0
        '
        'dgvImageAddress
        '
        Me.dgvImageAddress.AllowUserToAddRows = False
        Me.dgvImageAddress.AllowUserToDeleteRows = False
        Me.dgvImageAddress.AllowUserToResizeColumns = False
        Me.dgvImageAddress.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvImageAddress.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvImageAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvImageAddress.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvImageAddress.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(75, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvImageAddress.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvImageAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvImageAddress.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colimagename, Me.colimagedesc, Me.collocation, Me.colimageid})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvImageAddress.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvImageAddress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvImageAddress.Location = New System.Drawing.Point(0, 30)
        Me.dgvImageAddress.Name = "dgvImageAddress"
        Me.dgvImageAddress.RowHeadersVisible = False
        Me.dgvImageAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvImageAddress.Size = New System.Drawing.Size(413, 421)
        Me.dgvImageAddress.TabIndex = 6
        '
        'tsImageTools
        '
        Me.tsImageTools.AutoSize = False
        Me.tsImageTools.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsImageTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonbrowswimage, Me.ToolStripSeparator1, Me.ToolStripButtonremovethisimage, Me.ToolStripSeparator2, Me.ToolStripButton1})
        Me.tsImageTools.Location = New System.Drawing.Point(0, 0)
        Me.tsImageTools.Name = "tsImageTools"
        Me.tsImageTools.Size = New System.Drawing.Size(413, 30)
        Me.tsImageTools.TabIndex = 7
        Me.tsImageTools.Text = "ToolStrip2"
        '
        'ToolStripButtonbrowswimage
        '
        Me.ToolStripButtonbrowswimage.Image = CType(resources.GetObject("ToolStripButtonbrowswimage.Image"), System.Drawing.Image)
        Me.ToolStripButtonbrowswimage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonbrowswimage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonbrowswimage.Name = "ToolStripButtonbrowswimage"
        Me.ToolStripButtonbrowswimage.Size = New System.Drawing.Size(67, 27)
        Me.ToolStripButtonbrowswimage.Text = "Browse"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 30)
        '
        'ToolStripButtonremovethisimage
        '
        Me.ToolStripButtonremovethisimage.Image = CType(resources.GetObject("ToolStripButtonremovethisimage.Image"), System.Drawing.Image)
        Me.ToolStripButtonremovethisimage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonremovethisimage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonremovethisimage.Name = "ToolStripButtonremovethisimage"
        Me.ToolStripButtonremovethisimage.Size = New System.Drawing.Size(71, 27)
        Me.ToolStripButtonremovethisimage.Text = "Remove"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 30)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CenterImageToolStripMenuItem, Me.NormalToolStripMenuItem, Me.StretchImageToolStripMenuItem, Me.AutoSizeToolStripMenuItem, Me.ZoomToolStripMenuItem})
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(76, 27)
        Me.ToolStripButton1.Text = "Option"
        '
        'CenterImageToolStripMenuItem
        '
        Me.CenterImageToolStripMenuItem.Name = "CenterImageToolStripMenuItem"
        Me.CenterImageToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.CenterImageToolStripMenuItem.Text = "Center Image"
        '
        'NormalToolStripMenuItem
        '
        Me.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem"
        Me.NormalToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.NormalToolStripMenuItem.Text = "Normal"
        '
        'StretchImageToolStripMenuItem
        '
        Me.StretchImageToolStripMenuItem.Name = "StretchImageToolStripMenuItem"
        Me.StretchImageToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.StretchImageToolStripMenuItem.Text = "Stretch Image"
        '
        'AutoSizeToolStripMenuItem
        '
        Me.AutoSizeToolStripMenuItem.Name = "AutoSizeToolStripMenuItem"
        Me.AutoSizeToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.AutoSizeToolStripMenuItem.Text = "Auto Size"
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ZoomToolStripMenuItem.Text = "Zoom"
        '
        'Picture
        '
        Me.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Picture.Image = CType(resources.GetObject("Picture.Image"), System.Drawing.Image)
        Me.Picture.Location = New System.Drawing.Point(422, 3)
        Me.Picture.Name = "Picture"
        Me.Picture.Size = New System.Drawing.Size(693, 451)
        Me.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Picture.TabIndex = 8
        Me.Picture.TabStop = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.paneladdfilm)
        Me.TabPage3.Controls.Add(Me.DGVFilm)
        Me.TabPage3.ImageIndex = 6
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1124, 483)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "         Film          "
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'paneladdfilm
        '
        Me.paneladdfilm.Controls.Add(Me.btnAddToList)
        Me.paneladdfilm.Controls.Add(Me.cmbItemCodeFilm)
        Me.paneladdfilm.Controls.Add(Me.Label18)
        Me.paneladdfilm.Location = New System.Drawing.Point(16, 416)
        Me.paneladdfilm.Name = "paneladdfilm"
        Me.paneladdfilm.Size = New System.Drawing.Size(644, 43)
        Me.paneladdfilm.TabIndex = 53
        '
        'btnAddToList
        '
        Me.btnAddToList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToList.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddToList.Location = New System.Drawing.Point(418, 9)
        Me.btnAddToList.Name = "btnAddToList"
        Me.btnAddToList.Size = New System.Drawing.Size(133, 24)
        Me.btnAddToList.TabIndex = 52
        Me.btnAddToList.Text = "Add to list "
        Me.btnAddToList.UseVisualStyleBackColor = True
        '
        'cmbItemCodeFilm
        '
        Me.cmbItemCodeFilm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemCodeFilm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItemCodeFilm.FormattingEnabled = True
        Me.cmbItemCodeFilm.Location = New System.Drawing.Point(36, 10)
        Me.cmbItemCodeFilm.Name = "cmbItemCodeFilm"
        Me.cmbItemCodeFilm.Size = New System.Drawing.Size(377, 22)
        Me.cmbItemCodeFilm.TabIndex = 47
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label18.ImageList = Me.ImageList1
        Me.Label18.Location = New System.Drawing.Point(3, 14)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 14)
        Me.Label18.TabIndex = 50
        Me.Label18.Text = "Film:      "
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DGVFilm
        '
        Me.DGVFilm.AllowUserToAddRows = False
        Me.DGVFilm.AllowUserToDeleteRows = False
        Me.DGVFilm.AllowUserToResizeColumns = False
        Me.DGVFilm.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DGVFilm.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVFilm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGVFilm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGVFilm.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DGVFilm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(75, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(75, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVFilm.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVFilm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVFilm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colremove, Me.colitemcode, Me.colfilmname, Me.colnooffilms, Me.Column14, Me.colchargedetailsid})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVFilm.DefaultCellStyle = DataGridViewCellStyle7
        Me.DGVFilm.GridColor = System.Drawing.Color.WhiteSmoke
        Me.DGVFilm.Location = New System.Drawing.Point(16, 14)
        Me.DGVFilm.MultiSelect = False
        Me.DGVFilm.Name = "DGVFilm"
        Me.DGVFilm.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(75, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(75, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVFilm.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGVFilm.RowHeadersVisible = False
        Me.DGVFilm.RowHeadersWidth = 30
        Me.DGVFilm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVFilm.Size = New System.Drawing.Size(1025, 383)
        Me.DGVFilm.TabIndex = 46
        '
        'colremove
        '
        Me.colremove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colremove.HeaderText = ""
        Me.colremove.Name = "colremove"
        Me.colremove.Width = 30
        '
        'Column14
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.NullValue = False
        Me.Column14.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column14.HeaderText = "Is Used?"
        Me.Column14.Name = "Column14"
        Me.Column14.Visible = False
        '
        'tsSave
        '
        Me.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsSave.Margin = New System.Windows.Forms.Padding(0)
        Me.tsSave.Name = "tsSave"
        Me.tsSave.Size = New System.Drawing.Size(35, 35)
        Me.tsSave.Text = "Save"
        Me.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsCancel
        '
        Me.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsCancel.Name = "tsCancel"
        Me.tsCancel.Size = New System.Drawing.Size(47, 35)
        Me.tsCancel.Text = "Cancel"
        Me.tsCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsPrint
        '
        Me.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsPrint.Name = "tsPrint"
        Me.tsPrint.Size = New System.Drawing.Size(36, 35)
        Me.tsPrint.Text = "Print"
        Me.tsPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsClose
        '
        Me.tsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsClose.Name = "tsClose"
        Me.tsClose.Size = New System.Drawing.Size(40, 35)
        Me.tsClose.Text = "Close"
        Me.tsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cmbRadTech
        '
        Me.cmbRadTech.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.cmbRadTech.FormattingEnabled = True
        Me.cmbRadTech.Location = New System.Drawing.Point(109, 41)
        Me.cmbRadTech.Name = "cmbRadTech"
        Me.cmbRadTech.Size = New System.Drawing.Size(285, 26)
        Me.cmbRadTech.TabIndex = 51
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(10, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 18)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Performed By:"
        '
        'txtPatientname
        '
        Me.txtPatientname.BackColor = System.Drawing.Color.Transparent
        Me.txtPatientname.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtPatientname.ForeColor = System.Drawing.Color.Black
        Me.txtPatientname.Location = New System.Drawing.Point(61, 14)
        Me.txtPatientname.Name = "txtPatientname"
        Me.txtPatientname.Size = New System.Drawing.Size(323, 18)
        Me.txtPatientname.TabIndex = 259
        Me.txtPatientname.Text = "NAME"
        Me.txtPatientname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientname
        '
        Me.lblPatientname.AutoSize = True
        Me.lblPatientname.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientname.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblPatientname.ForeColor = System.Drawing.Color.Black
        Me.lblPatientname.Location = New System.Drawing.Point(12, 14)
        Me.lblPatientname.Name = "lblPatientname"
        Me.lblPatientname.Size = New System.Drawing.Size(49, 18)
        Me.lblPatientname.TabIndex = 258
        Me.lblPatientname.Text = "Name:"
        '
        'txtGender
        '
        Me.txtGender.BackColor = System.Drawing.Color.Transparent
        Me.txtGender.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtGender.ForeColor = System.Drawing.Color.Black
        Me.txtGender.Location = New System.Drawing.Point(535, 14)
        Me.txtGender.Name = "txtGender"
        Me.txtGender.Size = New System.Drawing.Size(61, 18)
        Me.txtGender.TabIndex = 265
        Me.txtGender.Text = "Female"
        Me.txtGender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAge
        '
        Me.txtAge.BackColor = System.Drawing.Color.Transparent
        Me.txtAge.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtAge.ForeColor = System.Drawing.Color.Black
        Me.txtAge.Location = New System.Drawing.Point(431, 14)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(46, 18)
        Me.txtAge.TabIndex = 264
        Me.txtAge.Text = "Age"
        Me.txtAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.BackColor = System.Drawing.Color.Transparent
        Me.lblGender.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblGender.ForeColor = System.Drawing.Color.Black
        Me.lblGender.Location = New System.Drawing.Point(499, 14)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(34, 18)
        Me.lblGender.TabIndex = 262
        Me.lblGender.Text = "Sex:"
        '
        'lblAge
        '
        Me.lblAge.AutoSize = True
        Me.lblAge.BackColor = System.Drawing.Color.Transparent
        Me.lblAge.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblAge.ForeColor = System.Drawing.Color.Black
        Me.lblAge.Location = New System.Drawing.Point(396, 14)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(36, 18)
        Me.lblAge.TabIndex = 261
        Me.lblAge.Text = "Age:"
        '
        'dtDate
        '
        Me.dtDate.CustomFormat = ""
        Me.dtDate.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDate.Location = New System.Drawing.Point(1037, 40)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.Size = New System.Drawing.Size(156, 26)
        Me.dtDate.TabIndex = 269
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.White
        Me.lblDate.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(973, 44)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(69, 18)
        Me.lblDate.TabIndex = 268
        Me.lblDate.Text = "Test Date:"
        '
        'LineShape3
        '
        Me.LineShape3.Name = "LineShape3"
        Me.LineShape3.X1 = 528
        Me.LineShape3.X2 = 595
        Me.LineShape3.Y1 = 33
        Me.LineShape3.Y2 = 33
        '
        'LineShape2
        '
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 430
        Me.LineShape2.X2 = 483
        Me.LineShape2.Y1 = 32
        Me.LineShape2.Y2 = 32
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 61
        Me.LineShape1.X2 = 394
        Me.LineShape1.Y1 = 32
        Me.LineShape1.Y2 = 32
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape4, Me.LineShape1, Me.LineShape2, Me.LineShape3})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1138, 611)
        Me.ShapeContainer1.TabIndex = 271
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape4
        '
        Me.LineShape4.Name = "LineShape4"
        Me.LineShape4.X1 = 1034
        Me.LineShape4.X2 = 1138
        Me.LineShape4.Y1 = 34
        Me.LineShape4.Y2 = 34
        '
        'lblexamination
        '
        Me.lblexamination.BackColor = System.Drawing.Color.Transparent
        Me.lblexamination.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblexamination.ForeColor = System.Drawing.Color.Black
        Me.lblexamination.Location = New System.Drawing.Point(106, 73)
        Me.lblexamination.Name = "lblexamination"
        Me.lblexamination.Size = New System.Drawing.Size(288, 18)
        Me.lblexamination.TabIndex = 273
        Me.lblexamination.Text = "Exam"
        Me.lblexamination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(10, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 18)
        Me.Label4.TabIndex = 272
        Me.Label4.Text = "Examination:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(402, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 18)
        Me.Label1.TabIndex = 277
        Me.Label1.Text = "Radiologist/Sonologist:"
        '
        'cmbradiologist
        '
        Me.cmbradiologist.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.cmbradiologist.FormattingEnabled = True
        Me.cmbradiologist.Location = New System.Drawing.Point(555, 41)
        Me.cmbradiologist.Name = "cmbradiologist"
        Me.cmbradiologist.Size = New System.Drawing.Size(304, 26)
        Me.cmbradiologist.TabIndex = 276
        '
        'cmbpreviousresult
        '
        Me.cmbpreviousresult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbpreviousresult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbpreviousresult.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.cmbpreviousresult.FormattingEnabled = True
        Me.cmbpreviousresult.Location = New System.Drawing.Point(831, 94)
        Me.cmbpreviousresult.Name = "cmbpreviousresult"
        Me.cmbpreviousresult.Size = New System.Drawing.Size(304, 26)
        Me.cmbpreviousresult.TabIndex = 278
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(718, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 18)
        Me.Label2.TabIndex = 279
        Me.Label2.Text = "Previous Results:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(402, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 18)
        Me.Label3.TabIndex = 281
        Me.Label3.Text = "Chief Complaint:"
        '
        'lblchiefcomplaint
        '
        Me.lblchiefcomplaint.BackColor = System.Drawing.Color.Transparent
        Me.lblchiefcomplaint.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblchiefcomplaint.ForeColor = System.Drawing.Color.Black
        Me.lblchiefcomplaint.Location = New System.Drawing.Point(520, 73)
        Me.lblchiefcomplaint.Name = "lblchiefcomplaint"
        Me.lblchiefcomplaint.Size = New System.Drawing.Size(451, 18)
        Me.lblchiefcomplaint.TabIndex = 282
        Me.lblchiefcomplaint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblrequestedby
        '
        Me.lblrequestedby.BackColor = System.Drawing.Color.Transparent
        Me.lblrequestedby.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblrequestedby.ForeColor = System.Drawing.Color.Black
        Me.lblrequestedby.Location = New System.Drawing.Point(706, 15)
        Me.lblrequestedby.Name = "lblrequestedby"
        Me.lblrequestedby.Size = New System.Drawing.Size(244, 18)
        Me.lblrequestedby.TabIndex = 284
        Me.lblrequestedby.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(607, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 18)
        Me.Label6.TabIndex = 283
        Me.Label6.Text = "Requested By:"
        '
        'chkesig
        '
        Me.chkesig.AutoSize = True
        Me.chkesig.Checked = True
        Me.chkesig.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkesig.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.chkesig.Location = New System.Drawing.Point(867, 45)
        Me.chkesig.Name = "chkesig"
        Me.chkesig.Size = New System.Drawing.Size(57, 22)
        Me.chkesig.TabIndex = 280
        Me.chkesig.Text = "E-Sig"
        Me.chkesig.UseVisualStyleBackColor = True
        '
        'lblward
        '
        Me.lblward.AutoSize = True
        Me.lblward.BackColor = System.Drawing.Color.White
        Me.lblward.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblward.ForeColor = System.Drawing.Color.Black
        Me.lblward.Location = New System.Drawing.Point(973, 69)
        Me.lblward.Name = "lblward"
        Me.lblward.Size = New System.Drawing.Size(44, 18)
        Me.lblward.TabIndex = 285
        Me.lblward.Text = "Ward:"
        '
        'txtward
        '
        Me.txtward.BackColor = System.Drawing.Color.Transparent
        Me.txtward.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtward.ForeColor = System.Drawing.Color.Black
        Me.txtward.Location = New System.Drawing.Point(1037, 69)
        Me.txtward.Name = "txtward"
        Me.txtward.Size = New System.Drawing.Size(156, 18)
        Me.txtward.TabIndex = 286
        Me.txtward.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblcaseno
        '
        Me.lblcaseno.AutoSize = True
        Me.lblcaseno.BackColor = System.Drawing.Color.White
        Me.lblcaseno.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblcaseno.ForeColor = System.Drawing.Color.Black
        Me.lblcaseno.Location = New System.Drawing.Point(981, 14)
        Me.lblcaseno.Name = "lblcaseno"
        Me.lblcaseno.Size = New System.Drawing.Size(51, 18)
        Me.lblcaseno.TabIndex = 287
        Me.lblcaseno.Text = "Case #:"
        '
        'txtcaseno
        '
        Me.txtcaseno.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtcaseno.Location = New System.Drawing.Point(1037, 18)
        Me.txtcaseno.Name = "txtcaseno"
        Me.txtcaseno.Size = New System.Drawing.Size(100, 14)
        Me.txtcaseno.TabIndex = 288
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "old location..."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 113
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "image name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "New location..."
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 265
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Column5"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 547
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "LabID"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 547
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Item Code"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 132
        '
        'colimagename
        '
        Me.colimagename.HeaderText = "Image Name"
        Me.colimagename.Name = "colimagename"
        Me.colimagename.ReadOnly = True
        Me.colimagename.Width = 160
        '
        'colimagedesc
        '
        Me.colimagedesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colimagedesc.HeaderText = "Description"
        Me.colimagedesc.Name = "colimagedesc"
        '
        'collocation
        '
        Me.collocation.HeaderText = "Location"
        Me.collocation.Name = "collocation"
        Me.collocation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.collocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.collocation.Visible = False
        '
        'colimageid
        '
        Me.colimageid.HeaderText = "colimageid"
        Me.colimageid.Name = "colimageid"
        Me.colimageid.ReadOnly = True
        Me.colimageid.Visible = False
        '
        'colitemcode
        '
        Me.colitemcode.HeaderText = "Item Code"
        Me.colitemcode.Name = "colitemcode"
        Me.colitemcode.ReadOnly = True
        Me.colitemcode.Visible = False
        '
        'colfilmname
        '
        Me.colfilmname.HeaderText = "Film"
        Me.colfilmname.Name = "colfilmname"
        Me.colfilmname.ReadOnly = True
        '
        'colnooffilms
        '
        Me.colnooffilms.HeaderText = "No. of Film Used"
        Me.colnooffilms.Name = "colnooffilms"
        '
        'colchargedetailsid
        '
        Me.colchargedetailsid.HeaderText = "colchargedetailsid"
        Me.colchargedetailsid.Name = "colchargedetailsid"
        Me.colchargedetailsid.ReadOnly = True
        Me.colchargedetailsid.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Film"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 132
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "No. of Film Used"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 131
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Column13"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 132
        '
        'frmtemplateRTF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1138, 611)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtcaseno)
        Me.Controls.Add(Me.lblcaseno)
        Me.Controls.Add(Me.txtward)
        Me.Controls.Add(Me.lblward)
        Me.Controls.Add(Me.lblrequestedby)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblchiefcomplaint)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbpreviousresult)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbradiologist)
        Me.Controls.Add(Me.lblexamination)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtDate)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.txtGender)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.lblGender)
        Me.Controls.Add(Me.lblAge)
        Me.Controls.Add(Me.txtPatientname)
        Me.Controls.Add(Me.lblPatientname)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbRadTech)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbResult)
        Me.Controls.Add(Me.chkesig)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frmtemplateRTF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tbResult.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.tbllayoutpanel.ResumeLayout(False)
        Me.panelpreviousresult.ResumeLayout(False)
        CType(Me.AxAcroPDFPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelcurrentresult.ResumeLayout(False)
        CType(Me.AxAcroPDFCurrent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.paneleditortools.ResumeLayout(False)
        Me.paneleditortools.PerformLayout()
        Me.tscontainerrtfeditor.ResumeLayout(False)
        Me.tscontainerrtfeditor.PerformLayout()
        Me.tscontainerwordeditor.ResumeLayout(False)
        Me.tscontainerwordeditor.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvImageAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsImageTools.ResumeLayout(False)
        Me.tsImageTools.PerformLayout()
        CType(Me.Picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.paneladdfilm.ResumeLayout(False)
        Me.paneladdfilm.PerformLayout()
        CType(Me.DGVFilm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbResult As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents tsSave As System.Windows.Forms.ToolStripButton
    Public WithEvents tsCancel As System.Windows.Forms.ToolStripButton
    Public WithEvents tsPrint As System.Windows.Forms.ToolStripButton
    Public WithEvents tsClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvImageAddress As System.Windows.Forms.DataGridView
    Friend WithEvents Picture As System.Windows.Forms.PictureBox
    Friend WithEvents tsImageTools As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonbrowswimage As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButtonremovethisimage As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents CenterImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StretchImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents btnAddToList As System.Windows.Forms.Button
    Friend WithEvents DGVFilm As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbItemCodeFilm As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbRadTech As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPatientname As System.Windows.Forms.Label
    Friend WithEvents lblPatientname As System.Windows.Forms.Label
    Friend WithEvents txtGender As System.Windows.Forms.Label
    Friend WithEvents txtAge As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents LineShape3 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents lblexamination As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents colremove As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colitemcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colfilmname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colnooffilms As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colchargedetailsid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents paneladdfilm As System.Windows.Forms.Panel
    Friend WithEvents txtResult As System.Windows.Forms.RichTextBox
    Friend WithEvents tbllayoutpanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtpreviousresult As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbradiologist As System.Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents colimagename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colimagedesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents collocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colimageid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbpreviousresult As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents panelcurrentresult As System.Windows.Forms.Panel
    Friend WithEvents paneleditortools As System.Windows.Forms.Panel
    Friend WithEvents tscontainerrtfeditor As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbold As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblchiefcomplaint As System.Windows.Forms.Label
    Friend WithEvents tsfontfamily As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tsfonsize As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tsfontcolor As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsunderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsitalize As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsalignleft As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsaligncenter As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsalignright As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsundo As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsredo As System.Windows.Forms.ToolStripButton
    Friend WithEvents panelpreviousresult As System.Windows.Forms.Panel
    Friend WithEvents AxAcroPDFPrevious As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents AxAcroPDFCurrent As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents tscontainerwordeditor As System.Windows.Forms.ToolStrip
    Friend WithEvents tseditresultpdf As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblrequestedby As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tsreloadresultpdf As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkesig As System.Windows.Forms.CheckBox
    Friend WithEvents lblward As System.Windows.Forms.Label
    Friend WithEvents txtward As System.Windows.Forms.Label
    Friend WithEvents LineShape4 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents lblcaseno As System.Windows.Forms.Label
    Friend WithEvents txtcaseno As System.Windows.Forms.TextBox
End Class
