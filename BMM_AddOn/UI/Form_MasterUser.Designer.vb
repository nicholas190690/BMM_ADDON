<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_MasterUser
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgv_user = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.c_active = New System.Windows.Forms.CheckBox()
        Me.txt_conf = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_user = New System.Windows.Forms.TextBox()
        Me.User = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.c_rpt_adm = New System.Windows.Forms.CheckBox()
        Me.c_bag_adm = New System.Windows.Forms.CheckBox()
        Me.c_raw_adm = New System.Windows.Forms.CheckBox()
        Me.c_wb_adm = New System.Windows.Forms.CheckBox()
        Me.c_config_adm = New System.Windows.Forms.CheckBox()
        Me.c_rpt_v = New System.Windows.Forms.CheckBox()
        Me.c_rpt_a = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.c_config_v = New System.Windows.Forms.CheckBox()
        Me.c_config_a = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.c_bag_v = New System.Windows.Forms.CheckBox()
        Me.c_bag_a = New System.Windows.Forms.CheckBox()
        Me.c_raw_v = New System.Windows.Forms.CheckBox()
        Me.c_raw_a = New System.Windows.Forms.CheckBox()
        Me.c_wb_v = New System.Windows.Forms.CheckBox()
        Me.c_wb_a = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btn_new = New System.Windows.Forms.Button()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dgv_report = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv_user, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgv_report, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.dgv_user)
        Me.Panel1.Location = New System.Drawing.Point(2, 199)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(561, 392)
        Me.Panel1.TabIndex = 0
        '
        'dgv_user
        '
        Me.dgv_user.AllowUserToAddRows = False
        Me.dgv_user.AllowUserToDeleteRows = False
        Me.dgv_user.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_user.Location = New System.Drawing.Point(3, 15)
        Me.dgv_user.Name = "dgv_user"
        Me.dgv_user.Size = New System.Drawing.Size(555, 374)
        Me.dgv_user.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(848, 39)
        Me.Panel2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(833, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master User Login"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.c_active)
        Me.Panel3.Controls.Add(Me.txt_conf)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txt_password)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.txt_user)
        Me.Panel3.Controls.Add(Me.User)
        Me.Panel3.Location = New System.Drawing.Point(2, 48)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(394, 145)
        Me.Panel3.TabIndex = 3
        '
        'c_active
        '
        Me.c_active.AutoSize = True
        Me.c_active.Location = New System.Drawing.Point(340, 27)
        Me.c_active.Name = "c_active"
        Me.c_active.Size = New System.Drawing.Size(56, 17)
        Me.c_active.TabIndex = 54
        Me.c_active.Text = "Active"
        Me.c_active.UseVisualStyleBackColor = True
        '
        'txt_conf
        '
        Me.txt_conf.Location = New System.Drawing.Point(153, 101)
        Me.txt_conf.Name = "txt_conf"
        Me.txt_conf.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_conf.Size = New System.Drawing.Size(183, 20)
        Me.txt_conf.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(137, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Confirm Password"
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(153, 60)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_password.Size = New System.Drawing.Size(183, 20)
        Me.txt_password.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'txt_user
        '
        Me.txt_user.Location = New System.Drawing.Point(153, 24)
        Me.txt_user.Name = "txt_user"
        Me.txt_user.Size = New System.Drawing.Size(183, 20)
        Me.txt_user.TabIndex = 1
        '
        'User
        '
        Me.User.AutoSize = True
        Me.User.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.User.Location = New System.Drawing.Point(11, 22)
        Me.User.Name = "User"
        Me.User.Size = New System.Drawing.Size(43, 20)
        Me.User.TabIndex = 0
        Me.User.Text = "User"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.c_rpt_adm)
        Me.Panel4.Controls.Add(Me.c_bag_adm)
        Me.Panel4.Controls.Add(Me.c_raw_adm)
        Me.Panel4.Controls.Add(Me.c_wb_adm)
        Me.Panel4.Controls.Add(Me.c_config_adm)
        Me.Panel4.Controls.Add(Me.c_rpt_v)
        Me.Panel4.Controls.Add(Me.c_rpt_a)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.c_config_v)
        Me.Panel4.Controls.Add(Me.c_config_a)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.c_bag_v)
        Me.Panel4.Controls.Add(Me.c_bag_a)
        Me.Panel4.Controls.Add(Me.c_raw_v)
        Me.Panel4.Controls.Add(Me.c_raw_a)
        Me.Panel4.Controls.Add(Me.c_wb_v)
        Me.Panel4.Controls.Add(Me.c_wb_a)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Location = New System.Drawing.Point(402, 48)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(448, 145)
        Me.Panel4.TabIndex = 40
        '
        'c_rpt_adm
        '
        Me.c_rpt_adm.AutoSize = True
        Me.c_rpt_adm.Location = New System.Drawing.Point(230, 118)
        Me.c_rpt_adm.Name = "c_rpt_adm"
        Me.c_rpt_adm.Size = New System.Drawing.Size(55, 17)
        Me.c_rpt_adm.TabIndex = 62
        Me.c_rpt_adm.Text = "Admin"
        Me.c_rpt_adm.UseVisualStyleBackColor = True
        '
        'c_bag_adm
        '
        Me.c_bag_adm.AutoSize = True
        Me.c_bag_adm.Location = New System.Drawing.Point(230, 94)
        Me.c_bag_adm.Name = "c_bag_adm"
        Me.c_bag_adm.Size = New System.Drawing.Size(55, 17)
        Me.c_bag_adm.TabIndex = 61
        Me.c_bag_adm.Text = "Admin"
        Me.c_bag_adm.UseVisualStyleBackColor = True
        '
        'c_raw_adm
        '
        Me.c_raw_adm.AutoSize = True
        Me.c_raw_adm.Location = New System.Drawing.Point(230, 72)
        Me.c_raw_adm.Name = "c_raw_adm"
        Me.c_raw_adm.Size = New System.Drawing.Size(55, 17)
        Me.c_raw_adm.TabIndex = 60
        Me.c_raw_adm.Text = "Admin"
        Me.c_raw_adm.UseVisualStyleBackColor = True
        '
        'c_wb_adm
        '
        Me.c_wb_adm.AutoSize = True
        Me.c_wb_adm.Location = New System.Drawing.Point(230, 43)
        Me.c_wb_adm.Name = "c_wb_adm"
        Me.c_wb_adm.Size = New System.Drawing.Size(55, 17)
        Me.c_wb_adm.TabIndex = 59
        Me.c_wb_adm.Text = "Admin"
        Me.c_wb_adm.UseVisualStyleBackColor = True
        '
        'c_config_adm
        '
        Me.c_config_adm.AutoSize = True
        Me.c_config_adm.Location = New System.Drawing.Point(230, 14)
        Me.c_config_adm.Name = "c_config_adm"
        Me.c_config_adm.Size = New System.Drawing.Size(55, 17)
        Me.c_config_adm.TabIndex = 58
        Me.c_config_adm.Text = "Admin"
        Me.c_config_adm.UseVisualStyleBackColor = True
        '
        'c_rpt_v
        '
        Me.c_rpt_v.AutoSize = True
        Me.c_rpt_v.Location = New System.Drawing.Point(167, 118)
        Me.c_rpt_v.Name = "c_rpt_v"
        Me.c_rpt_v.Size = New System.Drawing.Size(49, 17)
        Me.c_rpt_v.TabIndex = 57
        Me.c_rpt_v.Text = "View"
        Me.c_rpt_v.UseVisualStyleBackColor = True
        '
        'c_rpt_a
        '
        Me.c_rpt_a.AutoSize = True
        Me.c_rpt_a.Location = New System.Drawing.Point(102, 118)
        Me.c_rpt_a.Name = "c_rpt_a"
        Me.c_rpt_a.Size = New System.Drawing.Size(45, 17)
        Me.c_rpt_a.TabIndex = 56
        Me.c_rpt_a.Text = "Add"
        Me.c_rpt_a.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 16)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Report"
        '
        'c_config_v
        '
        Me.c_config_v.AutoSize = True
        Me.c_config_v.Location = New System.Drawing.Point(167, 14)
        Me.c_config_v.Name = "c_config_v"
        Me.c_config_v.Size = New System.Drawing.Size(49, 17)
        Me.c_config_v.TabIndex = 54
        Me.c_config_v.Text = "View"
        Me.c_config_v.UseVisualStyleBackColor = True
        '
        'c_config_a
        '
        Me.c_config_a.AutoSize = True
        Me.c_config_a.Location = New System.Drawing.Point(102, 14)
        Me.c_config_a.Name = "c_config_a"
        Me.c_config_a.Size = New System.Drawing.Size(45, 17)
        Me.c_config_a.TabIndex = 53
        Me.c_config_a.Text = "Add"
        Me.c_config_a.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 16)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Configuration"
        '
        'c_bag_v
        '
        Me.c_bag_v.AutoSize = True
        Me.c_bag_v.Location = New System.Drawing.Point(167, 94)
        Me.c_bag_v.Name = "c_bag_v"
        Me.c_bag_v.Size = New System.Drawing.Size(49, 17)
        Me.c_bag_v.TabIndex = 51
        Me.c_bag_v.Text = "View"
        Me.c_bag_v.UseVisualStyleBackColor = True
        '
        'c_bag_a
        '
        Me.c_bag_a.AutoSize = True
        Me.c_bag_a.Location = New System.Drawing.Point(102, 94)
        Me.c_bag_a.Name = "c_bag_a"
        Me.c_bag_a.Size = New System.Drawing.Size(45, 17)
        Me.c_bag_a.TabIndex = 50
        Me.c_bag_a.Text = "Add"
        Me.c_bag_a.UseVisualStyleBackColor = True
        '
        'c_raw_v
        '
        Me.c_raw_v.AutoSize = True
        Me.c_raw_v.Location = New System.Drawing.Point(167, 72)
        Me.c_raw_v.Name = "c_raw_v"
        Me.c_raw_v.Size = New System.Drawing.Size(49, 17)
        Me.c_raw_v.TabIndex = 49
        Me.c_raw_v.Text = "View"
        Me.c_raw_v.UseVisualStyleBackColor = True
        '
        'c_raw_a
        '
        Me.c_raw_a.AutoSize = True
        Me.c_raw_a.Location = New System.Drawing.Point(102, 72)
        Me.c_raw_a.Name = "c_raw_a"
        Me.c_raw_a.Size = New System.Drawing.Size(45, 17)
        Me.c_raw_a.TabIndex = 48
        Me.c_raw_a.Text = "Add"
        Me.c_raw_a.UseVisualStyleBackColor = True
        '
        'c_wb_v
        '
        Me.c_wb_v.AutoSize = True
        Me.c_wb_v.Location = New System.Drawing.Point(167, 43)
        Me.c_wb_v.Name = "c_wb_v"
        Me.c_wb_v.Size = New System.Drawing.Size(49, 17)
        Me.c_wb_v.TabIndex = 47
        Me.c_wb_v.Text = "View"
        Me.c_wb_v.UseVisualStyleBackColor = True
        '
        'c_wb_a
        '
        Me.c_wb_a.AutoSize = True
        Me.c_wb_a.Location = New System.Drawing.Point(102, 43)
        Me.c_wb_a.Name = "c_wb_a"
        Me.c_wb_a.Size = New System.Drawing.Size(45, 17)
        Me.c_wb_a.TabIndex = 46
        Me.c_wb_a.Text = "Add"
        Me.c_wb_a.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 16)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "Bagging"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 16)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "Raw Sugar"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 16)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "WeighBridge"
        '
        'btn_new
        '
        Me.btn_new.Location = New System.Drawing.Point(5, 626)
        Me.btn_new.Name = "btn_new"
        Me.btn_new.Size = New System.Drawing.Size(75, 23)
        Me.btn_new.TabIndex = 41
        Me.btn_new.Text = "New"
        Me.btn_new.UseVisualStyleBackColor = True
        '
        'btn_save
        '
        Me.btn_save.Location = New System.Drawing.Point(96, 626)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(75, 23)
        Me.btn_save.TabIndex = 42
        Me.btn_save.Text = "Save"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.dgv_report)
        Me.Panel5.Location = New System.Drawing.Point(569, 199)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(281, 392)
        Me.Panel5.TabIndex = 1
        '
        'dgv_report
        '
        Me.dgv_report.AllowUserToAddRows = False
        Me.dgv_report.AllowUserToDeleteRows = False
        Me.dgv_report.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_report.Location = New System.Drawing.Point(3, 15)
        Me.dgv_report.Name = "dgv_report"
        Me.dgv_report.Size = New System.Drawing.Size(275, 374)
        Me.dgv_report.TabIndex = 0
        '
        'Form_MasterUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 661)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.btn_new)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_MasterUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form_MasterUser"
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgv_user, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.dgv_report, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgv_user As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txt_conf As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_password As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_user As System.Windows.Forms.TextBox
    Friend WithEvents User As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents c_bag_v As System.Windows.Forms.CheckBox
    Friend WithEvents c_bag_a As System.Windows.Forms.CheckBox
    Friend WithEvents c_raw_v As System.Windows.Forms.CheckBox
    Friend WithEvents c_raw_a As System.Windows.Forms.CheckBox
    Friend WithEvents c_wb_v As System.Windows.Forms.CheckBox
    Friend WithEvents c_wb_a As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btn_new As System.Windows.Forms.Button
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents c_rpt_v As System.Windows.Forms.CheckBox
    Friend WithEvents c_rpt_a As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents c_config_v As System.Windows.Forms.CheckBox
    Friend WithEvents c_config_a As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents c_rpt_adm As System.Windows.Forms.CheckBox
    Friend WithEvents c_bag_adm As System.Windows.Forms.CheckBox
    Friend WithEvents c_raw_adm As System.Windows.Forms.CheckBox
    Friend WithEvents c_wb_adm As System.Windows.Forms.CheckBox
    Friend WithEvents c_config_adm As System.Windows.Forms.CheckBox
    Friend WithEvents c_active As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgv_report As System.Windows.Forms.DataGridView
End Class
