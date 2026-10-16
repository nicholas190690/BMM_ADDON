Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Windows.Forms
Imports System.Management
Imports SAPbobsCOM
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Threading

Public Class Form_MasterUser
    Dim dt_report As New DataTable
    Dim dt_user As New DataTable
    Dim currdata As New oUser
    Dim index As Integer
    Dim isnew As Boolean = True
#Region "Function"
    Private Sub bindtoobject()
        currdata.UNAME = txt_user.Text
        currdata.UPASS = EncryptionAddOnvb.Encrypt(txt_password.Text, "soltius")
        currdata.UABAG = Convert.ToInt32(c_bag_a.Checked)
        currdata.UVBAG = Convert.ToInt32(c_bag_v.Checked)
        currdata.UADMBAG = Convert.ToInt32(c_bag_adm.Checked)
        currdata.UARAW = Convert.ToInt32(c_raw_a.Checked)
        currdata.UVRAW = Convert.ToInt32(c_raw_v.Checked)
        currdata.UADMRAW = Convert.ToInt32(c_raw_adm.Checked)
        currdata.UAWB = Convert.ToInt32(c_wb_a.Checked)
        currdata.UVWB = Convert.ToInt32(c_wb_v.Checked)
        currdata.UADMWB = Convert.ToInt32(c_wb_adm.Checked)
        currdata.UACONFIG = Convert.ToInt32(c_config_a.Checked)
        currdata.UVCONFIG = Convert.ToInt32(c_config_v.Checked)
        currdata.UADMCONFIG = Convert.ToInt32(c_config_adm.Checked)
        currdata.UAREPORT = Convert.ToInt32(c_rpt_a.Checked)
        currdata.UVREPORT = Convert.ToInt32(c_rpt_v.Checked)
        currdata.UADMREPORT = Convert.ToInt32(c_rpt_adm.Checked)
        currdata.UACTIVE = Convert.ToInt32(c_active.Checked)


        '   Sql = "select case when t1.""Code"" is null then 0 else 1 end AS ""Active"" ,t0.""Name"" as ""ReportName"",t0.""U_BMM_DESC"" as ""ReportDesc"",t0.""U_BMM_PATH"" as ""ReportPath"" ,t1.""Code"" ,t1.""U_BMM_ACTIVE"" from " & oSBOConnection.DatabaseName.ToString & ".""@BMM_REPORT"" T0 LEFT JOIN " & oSBOConnection.DatabaseName.ToString & ".""@BMM_RPT_MAP"" T1 ON (T0.""Code"" = t1.""U_BMM_RPT"" ) and  (t1.""U_BMM_USER"" = '" & currdata.UNAME & "' or   t1.""Code"" is null ) where T0.""U_BMM_ACTIVE"" = '1'"

        For i As Integer = 0 To dgv_report.Rows.Count - 1
            currdata.Lines.docEntry = dgv_report.Item("Code", i).Value
            currdata.Lines.active = dgv_report.Item("Active", i).Value
            currdata.Lines.rptCode = dgv_report.Item("ReportCode", i).Value
            currdata.Lines.Add()
        Next
    End Sub
    Private Sub bindtoui()
        If isnew = True Then
            txt_user.Enabled = True
            btn_save.Text = "Add"
        Else
            txt_user.Enabled = False
            btn_save.Text = "Update"
        End If
        txt_user.Text = currdata.UNAME
        txt_password.Text = If(currdata.UPASS = "-", "-", EncryptionAddOnvb.Decrypt(currdata.UPASS, "soltius"))
        txt_conf.Text = If(currdata.UPASS = "-", "-", EncryptionAddOnvb.Decrypt(currdata.UPASS, "soltius"))
        c_bag_a.Checked = currdata.UABAG
        c_bag_v.Checked = currdata.UVBAG
        c_bag_adm.Checked = currdata.UADMBAG

        c_raw_a.Checked = currdata.UARAW
        c_raw_v.Checked = currdata.UVRAW
        c_raw_adm.Checked = currdata.UADMRAW
     
        c_wb_a.Checked = currdata.UAWB
        c_wb_v.Checked = currdata.UVWB
        c_wb_adm.Checked = currdata.UADMWB


        c_config_a.Checked = currdata.UACONFIG
        c_config_v.Checked = currdata.UVCONFIG
        c_config_adm.Checked = currdata.UADMCONFIG

        c_rpt_a.Checked = currdata.UAREPORT
        c_rpt_v.Checked = currdata.UVREPORT
        c_rpt_adm.Checked = currdata.UADMREPORT

        c_active.Checked = currdata.UACTIVE

      

    End Sub
    Private Sub loaddgvReport()

        dgv_report.DataSource = Nothing

        dgv_report.Rows.Clear()
        dgv_report.Columns.Clear()
        Dim sql As String
        sql = "select case when t1.""Code"" is null then 0 else case when t1.""U_BMM_ACTIVE"" = 1 then 1 else 0 end end AS ""Active"" ,t0.""Name"" as ""ReportName"",t0.""U_BMM_DESC"" as ""ReportDesc"",t0.""U_BMM_PATH"" as ""ReportPath"" ,ifnull(t1.""Code"",'0') as ""Code"" ,ifnull(t1.""U_BMM_ACTIVE"",0) as ""U_BMM_ACTIVE"",t0.""Code"" as ""ReportCode"" from " & oSBOConnection.DatabaseName.ToString & ".""@BMM_REPORT"" T0 LEFT JOIN " & oSBOConnection.DatabaseName.ToString & ".""@BMM_RPT_MAP"" T1 ON (T0.""Code"" = t1.""U_BMM_RPT"" ) and  (t1.""U_BMM_USER"" = '" & currdata.UNAME & "' or   t1.""Code"" is null ) where T0.""U_BMM_ACTIVE"" = '1'"
        dt_report = oDBConnection.executeQuery(sql)
        dgv_report.DataSource = dt_report
        If dt_report.Columns.Count > 1 Then
            For Each column As DataColumn In dt_report.Columns
                If column.ColumnName = "Active" Then
                    dgv_report.Columns(column.ColumnName).ReadOnly = False
                    dgv_report.Columns(column.ColumnName).HeaderCell.Value = ""
                    dgv_report.Columns(column.ColumnName).Width = 25
                Else
                    dgv_report.Columns(column.ColumnName).ReadOnly = True
                End If

                dgv_report.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                If column.ColumnName = "ReportPath" Or column.ColumnName = "Code" Or column.ColumnName = "U_BMM_ACTIVE" Or column.ColumnName = "ReportCode" Then
                    dgv_report.Columns(column.ColumnName).Visible = False
                End If
            Next
        End If
        For i As Integer = 0 To dgv_report.Rows.Count - 1
            Dim CheckBoxCell = New DataGridViewCheckBoxCell()
            dgv_report.Item(0, i) = CheckBoxCell
        Next


    End Sub
    Private Sub loaddgv()
        dgv_user.DataSource = Nothing


      
        dgv_user.Rows.Clear()
        dgv_user.Columns.Clear()
        Dim sql As String
        sql = "SELECT ""U_BMM_USER"" AS ""User"" " &
            ", IFNULL(""U_BMM_PASS"",'-') AS ""Password"" " &
            ", IFNULL(""U_BMM_ACTIVE"",0) AS ""Active"" " &
            ", IFNULL(""U_BMM_AWB"",0) AS ""Add WeighBridge"" " &
            ", IFNULL(""U_BMM_VWB"",0) AS ""View WeighBridge"" " &
            ", IFNULL(""U_BMM_ADMWB"",0) AS ""Admin WeighBridge"" " &
            ", IFNULL(""U_BMM_ARAW"",0) AS ""Add Raw"" " &
            ", IFNULL(""U_BMM_VRAW"",0) AS ""View Raw"" " &
            ", IFNULL(""U_BMM_ADMRAW"",0) AS ""Admin Raw"" " &
            ", IFNULL(""U_BMM_ABAG"",0) AS ""Add Bagging"" " &
            ", IFNULL(""U_BMM_VBAG"",0) AS ""View Bagging"" " &
            ", IFNULL(""U_BMM_ADMBAG"",0) AS ""Admin Bagging"" " &
            ", IFNULL(""U_BMM_ACONFIG"",0) AS ""Add Config"" " &
            ", IFNULL(""U_BMM_VCONFIG"",0) AS ""View Config"" " &
            ", IFNULL(""U_BMM_ADMCONFIG"",0) AS ""Admin Config"" " &
            ", IFNULL(""U_BMM_ARPT"" ,0)AS ""Add Report"" " &
            ", IFNULL(""U_BMM_VRPT"",0) AS ""View Report"" " &
            ", IFNULL(""U_BMM_ADMRPT"",0) AS ""Admin Report"" " &
            " FROM " & oSBOConnection.DatabaseName.ToString & ".""@BMM_USER"" T0"
        dt_user = oDBConnection.executeQuery(sql)
        dgv_user.DataSource = dt_user
        If dt_user.Columns.Count > 1 Then
            For Each column As DataColumn In dt_user.Columns
                dgv_user.Columns(column.ColumnName).ReadOnly = True
                dgv_user.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                If column.ColumnName = "Password" Then
                    dgv_user.Columns(column.ColumnName).Visible = False
                End If
            Next
        End If


       
    End Sub

  

#End Region

#Region "Database"
    Private Sub connect()
        If Not oDBConnection Is Nothing And Not oDBConnection.Connected Then
            oDBConnection.Connect()
        End If
        If Not oDBConnection.Connected Then
            Throw New Exception("Database Connection Is Not Connected, Please Check!!!")
        End If
    
    End Sub
#End Region
#Region "Form Event"
    Private Sub Form_MasterUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connect()
        SQLHandler = New HANAVersion(oSBOConnection.DatabaseName)
        SQLHandler = oSBOConnection.SQLHandler
        loaddgv()
    End Sub

    Private Sub dgv_user_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_user.ColumnHeaderMouseClick
        If (dgv_user.SortOrder.ToString() = "Descending") Then
            dt_user.DefaultView.Sort = dgv_user.SortedColumn.Name + " DESC"
        Else
            dt_user.DefaultView.Sort = dgv_user.SortedColumn.Name + " ASC"
        End If
        dt_user = dt_user.DefaultView.ToTable()
    End Sub
    Private Sub dgv_user_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_user.SelectionChanged

        index = dgv_user.CurrentRow.Index
        If dgv_user.RowCount > 1 Then
            currdata.UABAG = dgv_user.Rows(index).Cells("Add Bagging").Value
            currdata.UACONFIG = dgv_user.Rows(index).Cells("Add Config").Value
            currdata.UARAW = dgv_user.Rows(index).Cells("Add Raw").Value
            currdata.UAWB = dgv_user.Rows(index).Cells("Add WeighBridge").Value
            currdata.UAREPORT = dgv_user.Rows(index).Cells("Add Report").Value
            currdata.UVBAG = dgv_user.Rows(index).Cells("View Bagging").Value
            currdata.UVCONFIG = dgv_user.Rows(index).Cells("View Config").Value
            currdata.UVRAW = dgv_user.Rows(index).Cells("View Raw").Value
            currdata.UVWB = dgv_user.Rows(index).Cells("View WeighBridge").Value
            currdata.UVREPORT = dgv_user.Rows(index).Cells("View Report").Value
            currdata.UNAME = dgv_user.Rows(index).Cells("User").Value
            currdata.UPASS = dgv_user.Rows(index).Cells("Password").Value
            currdata.UADMCONFIG = dgv_user.Rows(index).Cells("Admin Config").Value
            currdata.UADMRAW = dgv_user.Rows(index).Cells("Admin Raw").Value
            currdata.UADMWB = dgv_user.Rows(index).Cells("Admin WeighBridge").Value
            currdata.UADMREPORT = dgv_user.Rows(index).Cells("Admin Report").Value
            currdata.UACTIVE = dgv_user.Rows(index).Cells("Active").Value
            isnew = False
            bindtoui()
            loaddgvReport()
        End If
       
    End Sub

    Private Sub btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_new.Click
        currdata = New oUser
        isnew = True
        bindtoui()
        loaddgvReport()
    End Sub

#End Region

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Dim retval As Integer
        If Trim(txt_user.Text) = "" Then
            MessageBox.Show("User Name Belum Di isi")
            Return
        End If
        If Trim(txt_password.Text) <> Trim(txt_conf.Text) Then
            MessageBox.Show("Password dan Confirm Password tidak sesuai")
            Return
        End If

        If txt_password.Text.Length <= 4 Then
            MessageBox.Show("Password harus lebih dari 4 digit!")
            Return
        End If
        Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk menambah/edit master ?", "Konfirmasi", 2)
        If result = "Confirm" Then
            bindtoobject()
            If isnew = True Then
                retval = currdata.Add()
            Else
                retval = currdata.update()
            End If
            If retval = 0 Then
                currdata = New oUser
                isnew = True
                bindtoui()
                loaddgv()
            End If
        End If
    End Sub

   
   
  
End Class