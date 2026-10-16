<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ListWB
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_plat = New System.Windows.Forms.TextBox()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.btn_printticket = New System.Windows.Forms.Button()
        Me.btn_printspm = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_type = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_stat = New System.Windows.Forms.ComboBox()
        Me.btn_search = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp2 = New System.Windows.Forms.DateTimePicker()
        Me.dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.dgv_list = New System.Windows.Forms.DataGridView()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgv_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txt_plat)
        Me.GroupBox2.Controls.Add(Me.btn_cancel)
        Me.GroupBox2.Controls.Add(Me.btn_printticket)
        Me.GroupBox2.Controls.Add(Me.btn_printspm)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cmb_type)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmb_stat)
        Me.GroupBox2.Controls.Add(Me.btn_search)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.dtp2)
        Me.GroupBox2.Controls.Add(Me.dtp1)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 15)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(1140, 103)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(842, 15)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(125, 49)
        Me.Button3.TabIndex = 16
        Me.Button3.Text = "Print  SJ SoftCopy"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(577, 62)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 28)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Resend WA"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(975, 62)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(148, 28)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Export to Excel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(423, 22)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "No Plat"
        '
        'txt_plat
        '
        Me.txt_plat.Location = New System.Drawing.Point(425, 42)
        Me.txt_plat.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_plat.Name = "txt_plat"
        Me.txt_plat.Size = New System.Drawing.Size(132, 22)
        Me.txt_plat.TabIndex = 12
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(975, 15)
        Me.btn_cancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(148, 28)
        Me.btn_cancel.TabIndex = 11
        Me.btn_cancel.Text = "Cancel SPM/SPB"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_printticket
        '
        Me.btn_printticket.Location = New System.Drawing.Point(685, 62)
        Me.btn_printticket.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_printticket.Name = "btn_printticket"
        Me.btn_printticket.Size = New System.Drawing.Size(148, 28)
        Me.btn_printticket.TabIndex = 10
        Me.btn_printticket.Text = "Print Ticket"
        Me.btn_printticket.UseVisualStyleBackColor = True
        '
        'btn_printspm
        '
        Me.btn_printspm.Location = New System.Drawing.Point(685, 15)
        Me.btn_printspm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_printspm.Name = "btn_printspm"
        Me.btn_printspm.Size = New System.Drawing.Size(148, 28)
        Me.btn_printspm.TabIndex = 9
        Me.btn_printspm.Text = "Print SPM/SPB"
        Me.btn_printspm.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(229, 74)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Type"
        '
        'cmb_type
        '
        Me.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_type.FormattingEnabled = True
        Me.cmb_type.Items.AddRange(New Object() {"All", "1-Penjualan", "2-Penjualan Loco", "3-Raw Sugar", "4-Supporting Operational", "5-Limbah", "6-Batu Bara"})
        Me.cmb_type.Location = New System.Drawing.Point(279, 70)
        Me.cmb_type.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_type.Name = "cmb_type"
        Me.cmb_type.Size = New System.Drawing.Size(117, 24)
        Me.cmb_type.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 74)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Status"
        '
        'cmb_stat
        '
        Me.cmb_stat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_stat.FormattingEnabled = True
        Me.cmb_stat.Items.AddRange(New Object() {"All", "O-Timbang Masuk", "L-Loading Mulai", "F-Loading Selesai", "C-Timbang Keluar & Not Confirm", "R-Timbang Keluar & Confirm", "V-Cancel"})
        Me.cmb_stat.Location = New System.Drawing.Point(99, 70)
        Me.cmb_stat.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_stat.Name = "cmb_stat"
        Me.cmb_stat.Size = New System.Drawing.Size(121, 24)
        Me.cmb_stat.TabIndex = 5
        '
        'btn_search
        '
        Me.btn_search.Location = New System.Drawing.Point(576, 15)
        Me.btn_search.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_search.Name = "btn_search"
        Me.btn_search.Size = New System.Drawing.Size(100, 28)
        Me.btn_search.TabIndex = 4
        Me.btn_search.Text = "Search"
        Me.btn_search.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(229, 27)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Date From "
        '
        'dtp2
        '
        Me.dtp2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp2.Location = New System.Drawing.Point(277, 22)
        Me.dtp2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtp2.Name = "dtp2"
        Me.dtp2.Size = New System.Drawing.Size(119, 22)
        Me.dtp2.TabIndex = 1
        '
        'dtp1
        '
        Me.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp1.Location = New System.Drawing.Point(99, 22)
        Me.dtp1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtp1.Name = "dtp1"
        Me.dtp1.Size = New System.Drawing.Size(121, 22)
        Me.dtp1.TabIndex = 0
        '
        'dgv_list
        '
        Me.dgv_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_list.Location = New System.Drawing.Point(17, 126)
        Me.dgv_list.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgv_list.Name = "dgv_list"
        Me.dgv_list.Size = New System.Drawing.Size(1140, 550)
        Me.dgv_list.TabIndex = 24
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(842, 66)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(125, 28)
        Me.Button4.TabIndex = 17
        Me.Button4.Text = "Print Clearance"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Form_ListWB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 690)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.dgv_list)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Form_ListWB"
        Me.Text = "Form_ListWB"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgv_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_stat As System.Windows.Forms.ComboBox
    Friend WithEvents btn_search As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtp2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgv_list As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_type As System.Windows.Forms.ComboBox
    Friend WithEvents btn_printticket As System.Windows.Forms.Button
    Friend WithEvents btn_printspm As System.Windows.Forms.Button
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_plat As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
