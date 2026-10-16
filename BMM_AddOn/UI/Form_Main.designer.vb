<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Main))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.LogOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurationToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.WeighBridgeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnScale = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimbanganKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopLoadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RekapTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimbanganToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RawSugarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfirmRawSugarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListTransaksiRawSugarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BaggingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfirmBaggingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListTransaksiBaggingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogOutToolStripMenuItem, Me.ChangePasswordToolStripMenuItem, Me.ConfigurationToolStripMenuItem, Me.WeighBridgeToolStripMenuItem, Me.RawSugarToolStripMenuItem, Me.BaggingToolStripMenuItem, Me.ReportToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(701, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'LogOutToolStripMenuItem
        '
        Me.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem"
        Me.LogOutToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.LogOutToolStripMenuItem.Text = "Log Out"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(113, 20)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'ConfigurationToolStripMenuItem
        '
        Me.ConfigurationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurationToolStripMenuItem1, Me.UserToolStripMenuItem1})
        Me.ConfigurationToolStripMenuItem.Name = "ConfigurationToolStripMenuItem"
        Me.ConfigurationToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.ConfigurationToolStripMenuItem.Text = "Configuration"
        Me.ConfigurationToolStripMenuItem.Visible = False
        '
        'ConfigurationToolStripMenuItem1
        '
        Me.ConfigurationToolStripMenuItem1.Name = "ConfigurationToolStripMenuItem1"
        Me.ConfigurationToolStripMenuItem1.Size = New System.Drawing.Size(148, 22)
        Me.ConfigurationToolStripMenuItem1.Text = "Configuration"
        '
        'UserToolStripMenuItem1
        '
        Me.UserToolStripMenuItem1.Name = "UserToolStripMenuItem1"
        Me.UserToolStripMenuItem1.Size = New System.Drawing.Size(148, 22)
        Me.UserToolStripMenuItem1.Text = "Master User"
        '
        'WeighBridgeToolStripMenuItem
        '
        Me.WeighBridgeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TimbanganToolStripMenuItem, Me.mnScale, Me.TimbanganKeluarToolStripMenuItem, Me.LoadingToolStripMenuItem, Me.StopLoadingToolStripMenuItem, Me.RekapTransaksiToolStripMenuItem})
        Me.WeighBridgeToolStripMenuItem.Name = "WeighBridgeToolStripMenuItem"
        Me.WeighBridgeToolStripMenuItem.Size = New System.Drawing.Size(87, 20)
        Me.WeighBridgeToolStripMenuItem.Text = "WeighBridge"
        Me.WeighBridgeToolStripMenuItem.Visible = False
        '
        'mnScale
        '
        Me.mnScale.Name = "mnScale"
        Me.mnScale.Size = New System.Drawing.Size(172, 22)
        Me.mnScale.Text = "Timbangan Masuk"
        Me.mnScale.Visible = False
        '
        'TimbanganKeluarToolStripMenuItem
        '
        Me.TimbanganKeluarToolStripMenuItem.Name = "TimbanganKeluarToolStripMenuItem"
        Me.TimbanganKeluarToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.TimbanganKeluarToolStripMenuItem.Text = "Timbangan Keluar"
        Me.TimbanganKeluarToolStripMenuItem.Visible = False
        '
        'LoadingToolStripMenuItem
        '
        Me.LoadingToolStripMenuItem.Name = "LoadingToolStripMenuItem"
        Me.LoadingToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.LoadingToolStripMenuItem.Text = "Start Loading"
        '
        'StopLoadingToolStripMenuItem
        '
        Me.StopLoadingToolStripMenuItem.Name = "StopLoadingToolStripMenuItem"
        Me.StopLoadingToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.StopLoadingToolStripMenuItem.Text = "Stop Loading"
        '
        'RekapTransaksiToolStripMenuItem
        '
        Me.RekapTransaksiToolStripMenuItem.Name = "RekapTransaksiToolStripMenuItem"
        Me.RekapTransaksiToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.RekapTransaksiToolStripMenuItem.Text = "List Transaksi"
        '
        'TimbanganToolStripMenuItem
        '
        Me.TimbanganToolStripMenuItem.Name = "TimbanganToolStripMenuItem"
        Me.TimbanganToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.TimbanganToolStripMenuItem.Text = "Timbangan"
        '
        'RawSugarToolStripMenuItem
        '
        Me.RawSugarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfirmRawSugarToolStripMenuItem, Me.ListTransaksiRawSugarToolStripMenuItem})
        Me.RawSugarToolStripMenuItem.Name = "RawSugarToolStripMenuItem"
        Me.RawSugarToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.RawSugarToolStripMenuItem.Text = "Raw Sugar"
        Me.RawSugarToolStripMenuItem.Visible = False
        '
        'ConfirmRawSugarToolStripMenuItem
        '
        Me.ConfirmRawSugarToolStripMenuItem.Name = "ConfirmRawSugarToolStripMenuItem"
        Me.ConfirmRawSugarToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ConfirmRawSugarToolStripMenuItem.Text = "Confirm Raw Sugar"
        '
        'ListTransaksiRawSugarToolStripMenuItem
        '
        Me.ListTransaksiRawSugarToolStripMenuItem.Name = "ListTransaksiRawSugarToolStripMenuItem"
        Me.ListTransaksiRawSugarToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ListTransaksiRawSugarToolStripMenuItem.Text = "List Transaksi Raw Sugar"
        '
        'BaggingToolStripMenuItem
        '
        Me.BaggingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfirmBaggingToolStripMenuItem, Me.ListTransaksiBaggingToolStripMenuItem})
        Me.BaggingToolStripMenuItem.Name = "BaggingToolStripMenuItem"
        Me.BaggingToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.BaggingToolStripMenuItem.Text = "Bagging"
        Me.BaggingToolStripMenuItem.Visible = False
        '
        'ConfirmBaggingToolStripMenuItem
        '
        Me.ConfirmBaggingToolStripMenuItem.Name = "ConfirmBaggingToolStripMenuItem"
        Me.ConfirmBaggingToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ConfirmBaggingToolStripMenuItem.Text = "Confirm Bagging"
        '
        'ListTransaksiBaggingToolStripMenuItem
        '
        Me.ListTransaksiBaggingToolStripMenuItem.Name = "ListTransaksiBaggingToolStripMenuItem"
        Me.ListTransaksiBaggingToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ListTransaksiBaggingToolStripMenuItem.Text = "List Transaksi Bagging"
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ReportToolStripMenuItem.Text = "Report"
        Me.ReportToolStripMenuItem.Visible = False
        '
        'Form_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(701, 261)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PT Berkah Manis Makmur - SAP Business One AddOn "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents WeighBridgeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnScale As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimbanganKeluarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopLoadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RawSugarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BaggingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfirmRawSugarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListTransaksiRawSugarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfirmBaggingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListTransaksiBaggingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RekapTransaksiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimbanganToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
