<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_BaggingToSAP
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgv_detail = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dtp_trans = New System.Windows.Forms.DateTimePicker()
        Me.cmb_mesin = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmb_shift = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_operator = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblc = New System.Windows.Forms.Label()
        Me.lblb = New System.Windows.Forms.Label()
        Me.lbla = New System.Windows.Forms.Label()
        Me.lbl6 = New System.Windows.Forms.Label()
        Me.lbl5 = New System.Windows.Forms.Label()
        Me.lbl4 = New System.Windows.Forms.Label()
        Me.lbl3 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.lblttl = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgv_detail)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 224)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(860, 368)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dgv_detail
        '
        Me.dgv_detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_detail.Location = New System.Drawing.Point(21, 14)
        Me.dgv_detail.Name = "dgv_detail"
        Me.dgv_detail.Size = New System.Drawing.Size(833, 348)
        Me.dgv_detail.TabIndex = 21
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(344, 204)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(132, 29)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dtp_trans
        '
        Me.dtp_trans.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_trans.Location = New System.Drawing.Point(103, 19)
        Me.dtp_trans.Name = "dtp_trans"
        Me.dtp_trans.Size = New System.Drawing.Size(299, 26)
        Me.dtp_trans.TabIndex = 18
        '
        'cmb_mesin
        '
        Me.cmb_mesin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_mesin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_mesin.FormattingEnabled = True
        Me.cmb_mesin.Location = New System.Drawing.Point(103, 97)
        Me.cmb_mesin.Name = "cmb_mesin"
        Me.cmb_mesin.Size = New System.Drawing.Size(299, 28)
        Me.cmb_mesin.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(14, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 27)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Bagging"
        '
        'cmb_shift
        '
        Me.cmb_shift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_shift.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_shift.FormattingEnabled = True
        Me.cmb_shift.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cmb_shift.Location = New System.Drawing.Point(103, 54)
        Me.cmb_shift.Name = "cmb_shift"
        Me.cmb_shift.Size = New System.Drawing.Size(299, 28)
        Me.cmb_shift.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 27)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Shift"
        '
        'cmb_operator
        '
        Me.cmb_operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_operator.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_operator.FormattingEnabled = True
        Me.cmb_operator.Location = New System.Drawing.Point(115, 598)
        Me.cmb_operator.Name = "cmb_operator"
        Me.cmb_operator.Size = New System.Drawing.Size(194, 28)
        Me.cmb_operator.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 598)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 27)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Operator"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(461, 602)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 24)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Manual Process"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 27)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Date"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblttl)
        Me.GroupBox2.Controls.Add(Me.lblc)
        Me.GroupBox2.Controls.Add(Me.lblb)
        Me.GroupBox2.Controls.Add(Me.lbla)
        Me.GroupBox2.Controls.Add(Me.lbl6)
        Me.GroupBox2.Controls.Add(Me.lbl5)
        Me.GroupBox2.Controls.Add(Me.lbl4)
        Me.GroupBox2.Controls.Add(Me.lbl3)
        Me.GroupBox2.Controls.Add(Me.lbl2)
        Me.GroupBox2.Controls.Add(Me.lbl1)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btn_generate)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmb_mesin)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cmb_shift)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.dtp_trans)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(859, 206)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'lblc
        '
        Me.lblc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblc.Location = New System.Drawing.Point(712, 110)
        Me.lblc.Name = "lblc"
        Me.lblc.Size = New System.Drawing.Size(136, 23)
        Me.lblc.TabIndex = 40
        Me.lblc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblb
        '
        Me.lblb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblb.Location = New System.Drawing.Point(576, 110)
        Me.lblb.Name = "lblb"
        Me.lblb.Size = New System.Drawing.Size(136, 23)
        Me.lblb.TabIndex = 39
        Me.lblb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbla
        '
        Me.lbla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbla.Location = New System.Drawing.Point(440, 110)
        Me.lbla.Name = "lbla"
        Me.lbla.Size = New System.Drawing.Size(136, 23)
        Me.lbla.TabIndex = 38
        Me.lbla.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl6
        '
        Me.lbl6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl6.Location = New System.Drawing.Point(780, 87)
        Me.lbl6.Name = "lbl6"
        Me.lbl6.Size = New System.Drawing.Size(68, 23)
        Me.lbl6.TabIndex = 37
        Me.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl5
        '
        Me.lbl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl5.Location = New System.Drawing.Point(712, 87)
        Me.lbl5.Name = "lbl5"
        Me.lbl5.Size = New System.Drawing.Size(68, 23)
        Me.lbl5.TabIndex = 36
        Me.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl4
        '
        Me.lbl4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl4.Location = New System.Drawing.Point(644, 87)
        Me.lbl4.Name = "lbl4"
        Me.lbl4.Size = New System.Drawing.Size(68, 23)
        Me.lbl4.TabIndex = 35
        Me.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl3
        '
        Me.lbl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl3.Location = New System.Drawing.Point(576, 87)
        Me.lbl3.Name = "lbl3"
        Me.lbl3.Size = New System.Drawing.Size(68, 23)
        Me.lbl3.TabIndex = 34
        Me.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl2
        '
        Me.lbl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl2.Location = New System.Drawing.Point(508, 87)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(68, 23)
        Me.lbl2.TabIndex = 31
        Me.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl1
        '
        Me.lbl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl1.Location = New System.Drawing.Point(440, 87)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(68, 23)
        Me.lbl1.TabIndex = 30
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Location = New System.Drawing.Point(780, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 23)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "Bagging 6"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Location = New System.Drawing.Point(712, 64)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 23)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Bagging 5"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Location = New System.Drawing.Point(644, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 23)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Bagging 4"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Location = New System.Drawing.Point(576, 64)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 23)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Bagging 3"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(508, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 23)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Bagging 2"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(440, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 23)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Bagging 1"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(712, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 23)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Line C"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(576, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 23)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Line B"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(440, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Line A"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(617, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Rekap"
        '
        'btn_generate
        '
        Me.btn_generate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_generate.Location = New System.Drawing.Point(102, 150)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(300, 33)
        Me.btn_generate.TabIndex = 23
        Me.btn_generate.Text = "Generate"
        Me.btn_generate.UseVisualStyleBackColor = True
        '
        'btn_add
        '
        Me.btn_add.Location = New System.Drawing.Point(330, 602)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(113, 23)
        Me.btn_add.TabIndex = 22
        Me.btn_add.Text = "Add to SAP"
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'lblttl
        '
        Me.lblttl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblttl.Location = New System.Drawing.Point(440, 133)
        Me.lblttl.Name = "lblttl"
        Me.lblttl.Size = New System.Drawing.Size(408, 23)
        Me.lblttl.TabIndex = 41
        Me.lblttl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form_BaggingToSAP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 661)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmb_operator)
        Me.Name = "Form_BaggingToSAP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Post to SAP"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmb_mesin As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmb_shift As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_operator As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dtp_trans As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv_detail As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_generate As System.Windows.Forms.Button
    Friend WithEvents btn_add As System.Windows.Forms.Button
    Friend WithEvents lblc As System.Windows.Forms.Label
    Friend WithEvents lblb As System.Windows.Forms.Label
    Friend WithEvents lbla As System.Windows.Forms.Label
    Friend WithEvents lbl6 As System.Windows.Forms.Label
    Friend WithEvents lbl5 As System.Windows.Forms.Label
    Friend WithEvents lbl4 As System.Windows.Forms.Label
    Friend WithEvents lbl3 As System.Windows.Forms.Label
    Friend WithEvents lbl2 As System.Windows.Forms.Label
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblttl As System.Windows.Forms.Label
End Class
