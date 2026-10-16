<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_RawSugar
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
        Me.cmb_mesin = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.cmb_shift = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_operator = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtp_date = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txt_remarks = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_toleransi = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.txt_qty2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_qty1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_muatan_name = New System.Windows.Forms.TextBox()
        Me.txt_muatan = New System.Windows.Forms.TextBox()
        Me.txt_end = New System.Windows.Forms.TextBox()
        Me.txt_start = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmb_mesin)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.btn_generate)
        Me.GroupBox1.Controls.Add(Me.cmb_shift)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmb_operator)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtp_date)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 205)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmb_mesin
        '
        Me.cmb_mesin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_mesin.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_mesin.FormattingEnabled = True
        Me.cmb_mesin.Location = New System.Drawing.Point(527, 29)
        Me.cmb_mesin.Name = "cmb_mesin"
        Me.cmb_mesin.Size = New System.Drawing.Size(194, 32)
        Me.cmb_mesin.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(414, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 27)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Timbangan"
        '
        'btn_generate
        '
        Me.btn_generate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_generate.Location = New System.Drawing.Point(10, 152)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(711, 33)
        Me.btn_generate.TabIndex = 8
        Me.btn_generate.Text = "Generate"
        Me.btn_generate.UseVisualStyleBackColor = True
        '
        'cmb_shift
        '
        Me.cmb_shift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_shift.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_shift.FormattingEnabled = True
        Me.cmb_shift.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cmb_shift.Location = New System.Drawing.Point(527, 88)
        Me.cmb_shift.Name = "cmb_shift"
        Me.cmb_shift.Size = New System.Drawing.Size(194, 32)
        Me.cmb_shift.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(414, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 27)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Shift"
        '
        'cmb_operator
        '
        Me.cmb_operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_operator.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_operator.FormattingEnabled = True
        Me.cmb_operator.Location = New System.Drawing.Point(134, 88)
        Me.cmb_operator.Name = "cmb_operator"
        Me.cmb_operator.Size = New System.Drawing.Size(194, 32)
        Me.cmb_operator.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 27)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Date"
        '
        'dtp_date
        '
        Me.dtp_date.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_date.CalendarMonthBackground = System.Drawing.SystemColors.ButtonFace
        Me.dtp_date.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_date.Location = New System.Drawing.Point(134, 28)
        Me.dtp_date.Name = "dtp_date"
        Me.dtp_date.Size = New System.Drawing.Size(192, 29)
        Me.dtp_date.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 27)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Operator"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txt_remarks)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txt_toleransi)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.btn_add)
        Me.GroupBox2.Controls.Add(Me.txt_qty2)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txt_qty1)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txt_muatan_name)
        Me.GroupBox2.Controls.Add(Me.txt_muatan)
        Me.GroupBox2.Controls.Add(Me.txt_end)
        Me.GroupBox2.Controls.Add(Me.txt_start)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 211)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(760, 338)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'txt_remarks
        '
        Me.txt_remarks.Location = New System.Drawing.Point(157, 221)
        Me.txt_remarks.Multiline = True
        Me.txt_remarks.Name = "txt_remarks"
        Me.txt_remarks.Size = New System.Drawing.Size(564, 61)
        Me.txt_remarks.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 221)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 29)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Comment"
        '
        'txt_toleransi
        '
        Me.txt_toleransi.Enabled = False
        Me.txt_toleransi.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_toleransi.Location = New System.Drawing.Point(547, 126)
        Me.txt_toleransi.Name = "txt_toleransi"
        Me.txt_toleransi.Size = New System.Drawing.Size(172, 29)
        Me.txt_toleransi.TabIndex = 21
        Me.txt_toleransi.Text = "0"
        Me.txt_toleransi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(422, 129)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(145, 27)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Toleransi"
        '
        'btn_add
        '
        Me.btn_add.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_add.Location = New System.Drawing.Point(10, 292)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(711, 33)
        Me.btn_add.TabIndex = 19
        Me.btn_add.Text = "Post To SAP"
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'txt_qty2
        '
        Me.txt_qty2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_qty2.Location = New System.Drawing.Point(157, 178)
        Me.txt_qty2.Name = "txt_qty2"
        Me.txt_qty2.Size = New System.Drawing.Size(257, 29)
        Me.txt_qty2.TabIndex = 18
        Me.txt_qty2.Text = "0"
        Me.txt_qty2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 178)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(145, 29)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Berat Confirmed"
        '
        'txt_qty1
        '
        Me.txt_qty1.Enabled = False
        Me.txt_qty1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_qty1.Location = New System.Drawing.Point(157, 126)
        Me.txt_qty1.Name = "txt_qty1"
        Me.txt_qty1.Size = New System.Drawing.Size(257, 29)
        Me.txt_qty1.TabIndex = 16
        Me.txt_qty1.Text = "0"
        Me.txt_qty1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(165, 27)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Berat Timbangan"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 27)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Item"
        '
        'txt_muatan_name
        '
        Me.txt_muatan_name.Enabled = False
        Me.txt_muatan_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_muatan_name.Location = New System.Drawing.Point(241, 70)
        Me.txt_muatan_name.Multiline = True
        Me.txt_muatan_name.Name = "txt_muatan_name"
        Me.txt_muatan_name.Size = New System.Drawing.Size(173, 30)
        Me.txt_muatan_name.TabIndex = 13
        '
        'txt_muatan
        '
        Me.txt_muatan.Enabled = False
        Me.txt_muatan.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_muatan.Location = New System.Drawing.Point(151, 70)
        Me.txt_muatan.Multiline = True
        Me.txt_muatan.Name = "txt_muatan"
        Me.txt_muatan.Size = New System.Drawing.Size(84, 30)
        Me.txt_muatan.TabIndex = 12
        '
        'txt_end
        '
        Me.txt_end.Enabled = False
        Me.txt_end.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_end.Location = New System.Drawing.Point(547, 19)
        Me.txt_end.Name = "txt_end"
        Me.txt_end.Size = New System.Drawing.Size(174, 29)
        Me.txt_end.TabIndex = 11
        Me.txt_end.Text = "-"
        Me.txt_end.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_start
        '
        Me.txt_start.Enabled = False
        Me.txt_start.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_start.Location = New System.Drawing.Point(151, 19)
        Me.txt_start.Name = "txt_start"
        Me.txt_start.Size = New System.Drawing.Size(263, 29)
        Me.txt_start.TabIndex = 9
        Me.txt_start.Text = "-"
        Me.txt_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(423, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(157, 51)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Counter Akhir"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 51)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Counter Awal"
        '
        'Form_RawSugar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_RawSugar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirm Raw Sugar"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtp_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_generate As System.Windows.Forms.Button
    Friend WithEvents cmb_shift As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_operator As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_end As System.Windows.Forms.TextBox
    Friend WithEvents txt_start As System.Windows.Forms.TextBox
    Friend WithEvents btn_add As System.Windows.Forms.Button
    Friend WithEvents txt_qty2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_qty1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txt_muatan_name As System.Windows.Forms.TextBox
    Public WithEvents txt_muatan As System.Windows.Forms.TextBox
    Friend WithEvents cmb_mesin As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_toleransi As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
