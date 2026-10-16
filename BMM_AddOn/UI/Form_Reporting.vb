Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine


Public Class Form_Reporting
    Dim dt_list As New DataTable
    Dim index As Integer
    Dim dt_param As New DataTable

    Private Sub connect()


        If Not oDBConnection Is Nothing And Not oDBConnection.Connected Then
            oDBConnection.Connect()
        End If



        If Not oDBConnection.Connected Then
            Throw New Exception("Database Connection Is Not Connected, Please Check!!!")
        End If


        If Not oSBOConnection Is Nothing And Not oSBOConnection.Connected Then
            oSBOConnection.Connect()
        End If


        If Not oSBOConnection.Connected Then
            Throw New Exception("Database Connection Is Not Connected, Please Check!!!")
        End If



    End Sub

    Private Sub loaddgv()
        dgv_list_rpt.DataSource = Nothing
        dgv_list_rpt.Rows.Clear()
        dgv_list_rpt.Columns.Clear()
        Dim sql As String
        sql = " SELECT " &
            " T1.""Name"" as ""Report"", " &
            " T1.""U_BMM_DESC"" AS ""Description"", " &
            " T1.""U_BMM_PATH"" AS ""Path"" " &
            " FROM " &
             oSBOConnection.DatabaseName & " .""@BMM_RPT_MAP"" T0 " &
            " INNER JOIN " & oSBOConnection.DatabaseName & ".""@BMM_REPORT"" T1 ON T0.""U_BMM_RPT"" = T1.""Code"" " &
            " where " &
            " T0.""U_BMM_USER"" = '" & ClassUser.UNAME & "' AND " &
            " T1.""U_BMM_ACTIVE"" = 1  and T0.""U_BMM_ACTIVE"" = 1 "

          
        dt_list = oDBConnection.executeQuery(sql)
        dgv_list_rpt.DataSource = dt_list
        If dgv_list_rpt.Columns.Count > 1 Then
            For Each column As DataColumn In dt_list.Columns
                dgv_list_rpt.Columns(column.ColumnName).ReadOnly = True
                dgv_list_rpt.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                If column.ColumnName = "Path" Then
                    dgv_list_rpt.Columns(column.ColumnName).Visible = False
                End If
            Next
        End If
    End Sub

  

    Private Sub Form_Reporting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oConfigXMLParser.read()
        isInit = True
        If oConfigXMLParser.isConfigFileExists Then
            If oConfigXMLParser.read Then
                isInit = False
            End If
        End If

        connect()
        loaddgv()

    End Sub

    Private Sub btn_generate_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_generate.Click
        Try

            Dim filename As String = ""
            Dim param2 As String
            Dim param As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition
            Dim sPath As String
            Dim CrTable As Table
            Dim CrTables As Tables
            Dim crtableLogoninfo As New TableLogOnInfo()
            Dim dt = DateTime.ParseExact("20190101", "yyyyMMdd", Nothing)
            Dim crConnectionInfo As New ConnectionInfo
            Dim crReport As New ReportDocument

            index = dgv_list_rpt.CurrentRow.Index
            crReport.Load(dgv_list_rpt.Rows(index).Cells("Path").Value)

            ' crReport.SetParameterValue(0, TextBox1.Text)
            'crReport.SetParameterValue(1, dt1)
            'crReport.SetParameterValue(2, dt2)
            'For Each param In crReport.DataDefinition.ParameterFields
            '    crReport.SetParameterValue(param.Name, "Your Value")
            'Next
            For i As Integer = 0 To dgv_list_param.RowCount - 1
                If dgv_list_param.Rows(i).Cells("Type").Value.ToString = "NumberField" And IsNumeric(dgv_list_param.Rows(i).Cells("Value").Value) = False Then
                    Throw New Exception("Mohon di input angka untuk parameter : " & dgv_list_param.Rows(i).Cells("Parameter").Value.ToString)
                End If
              
                If dgv_list_param.Rows(i).Cells("Type").Value.ToString = "DateField" Then
                    Try
                        dt = DateTime.ParseExact(dgv_list_param.Rows(i).Cells("Value").Value.ToString, "yyyyMMdd", Nothing)
                    Catch ex As Exception
                        Throw New Exception("Format tanggal harus  4 digit tahun 2 digit tanggal 2 digit tanggal, contoh : 20201231. Untuk parameter : " & dgv_list_param.Rows(i).Cells("Parameter").Value.ToString)
                    End Try
                End If
                If dgv_list_param.Rows(i).Cells("Type").Value.ToString = "DateField" Then
                    crReport.SetParameterValue(i, dt)
                Else
                    crReport.SetParameterValue(i, dgv_list_param.Rows(i).Cells("Value").Value)
                End If

            Next

            crConnectionInfo.ServerName = oSBOConnection.ServerName & ":" & oSBOConnection.Port
            crConnectionInfo.UserID = oSBOConnection.DBUserName.ToString
            crConnectionInfo.Password = oSBOConnection.DBPassword.ToString

            crConnectionInfo.Type = ConnectionInfoType.Unknown
            CrTables = crReport.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            CrystalReportViewer1.ReportSource = crReport
            CrystalReportViewer1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            CrystalReportViewer1.ReportSource = Nothing
            Return
        End Try
    End Sub

   

   

    Private Sub dgv_list_rpt_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_list_rpt.SelectionChanged

        If (dgv_list_rpt.Focused) Then
            Dim crReport As New ReportDocument
            Dim param As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition
            dgv_list_param.Rows.Clear()
            CrystalReportViewer1.ReportSource = Nothing
            index = dgv_list_rpt.CurrentRow.Index
            If Not IsDBNull(dgv_list_rpt.Rows(index).Cells("Path").Value) Then
                crReport.Load(dgv_list_rpt.Rows(index).Cells("Path").Value)
                For Each param In crReport.DataDefinition.ParameterFields
                    dgv_list_param.Rows.Add(param.Name.ToString, "", param.ValueType.ToString)
                Next
                crReport.Close()
            End If

        End If


    End Sub

  

End Class