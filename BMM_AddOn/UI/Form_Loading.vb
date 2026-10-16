Public Class Form_Loading
    Dim oRec As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim oGeneralService As SAPbobsCOM.GeneralService
    Dim oGeneralData As SAPbobsCOM.GeneralData
    Dim oCompService As SAPbobsCOM.CompanyService
    Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
    Dim stat As String
    Dim index As Integer
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

    Sub loaddgv()
        Dim dt_transaksi As New DataTable
        Dim i As Integer
        Dim btn As New DataGridViewButtonColumn
        Dim chck As New DataGridViewCheckBoxColumn

        If load_stat = "Start" Then
            dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetListSPMSPB("20000101", "29991231", "O,C", "Loading"))
            stat = "L"
        ElseIf load_stat = "Stop" Then
            dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetListSPMSPB("20000101", "29991231", "L,C", "Loading"))
            stat = "F"
        End If


        dgv_trans.DataSource = dt_transaksi
        If dt_transaksi.Columns.Count > 1 Then
            For Each column As DataColumn In dt_transaksi.Columns
                dgv_trans.Columns(column.ColumnName).ReadOnly = True
                dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                If column.DataType.ToString() <> "System.String" And column.DataType.ToString() <> "System.DateTime" Then
                    dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                End If
                If column.ColumnName = "NoSystem" Or column.ColumnName = "Status" Then
                    dgv_trans.Columns(column.ColumnName).Visible = False
                End If
                i += 1
            Next
        End If
        dgv_trans.AllowUserToAddRows = False
        dgv_trans.Columns.Insert(0, chck)
        dgv_trans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgv_trans.AutoResizeColumns()
    End Sub

    Private Sub Form_Loading_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If load_stat = "Start" Then
                lbl_judul.Text = "Loading Awal"
            ElseIf load_stat = "Stop" Then
                lbl_judul.Text = "Loading Selesai"
            Else
                lbl_judul.Text = "List SPM/SPB"
                Button1.Visible = False
                chck_box.Visible = False
            End If
            connect()
            oCompany = oSBOConnection.Company
            SQLHandler = oSBOConnection.SQLHandler
            oCompService = oCompany.GetCompanyService()
            oGeneralService = oCompService.GetGeneralService("UDO_SOL_WB")
            loaddgv()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
      
    End Sub

    Sub UpdateStatus()
        Try
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oRec.DoQuery(SQLHandler.GetCurrent)
            If oRec.RecordCount > 0 Then
                oRec.MoveFirst()
            End If

            oCompany.StartTransaction()
            For Each row As DataGridViewRow In dgv_trans.Rows
                If row.Cells(0).Value IsNot Nothing And row.Cells(0).Value = True Then
                    oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
                    oGeneralParams.SetProperty("DocEntry", dgv_trans.Rows(row.Index).Cells("NoSystem").Value)
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                    If load_stat = "Start" Then
                        oGeneralData.SetProperty("U_SOL_DT_LOAD1", CType(oRec.Fields.Item("CDate2").Value, Date))
                        oGeneralData.SetProperty("U_SOL_T_LOAD1", CType(oRec.Fields.Item("Ctime").Value, Integer))
                    ElseIf load_stat = "Stop" Then
                        oGeneralData.SetProperty("U_SOL_DT_LOAD2", CType(oRec.Fields.Item("CDate2").Value, Date))
                        oGeneralData.SetProperty("U_SOL_T_LOAD2", CType(oRec.Fields.Item("Ctime").Value, Integer))
                    End If
                    
                    oGeneralData.SetProperty("U_SOL_STATUS", stat)
                    oGeneralService.Update(oGeneralData)
                End If
            Next
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
        Catch ex As Exception
            MessageBox.Show(ex.Message())
            If oCompany.InTransaction = True Then
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
        Finally
            Utils.releaseobject(oRec)
            Utils.releaseobject(oGeneralData)
            Utils.releaseobject(oGeneralParams)
        End Try
       
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chck_box.CheckedChanged
        For Each row As DataGridViewRow In dgv_trans.Rows

            row.Cells(0).Value = chck_box.Checked

        Next
        dgv_trans.EndEdit()
    End Sub

 

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk " + load_stat + " Loading ?", "Konfirmasi Loading", 2)
        If result = "Confirm" Then
            UpdateStatus()
            MessageBox.Show("Update Selesai")
            loaddgv()
        Else
            Return
        End If
    End Sub
End Class
