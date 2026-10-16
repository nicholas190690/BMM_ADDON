Public Class Form_ListRaw
    Dim oRec As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim dt_transaksi As DataTable
    Dim dt_detail As DataTable
    Dim index As Integer
    Dim shift As String
    Dim date1 As String
    Dim date2 As Date
    Dim docentry As String
    Dim excludelist As New List(Of String)
    Dim cFunction As New oFunctional
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

    Private Sub Form_ListRaw_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cmb_stat.Text = "All"
        excludelist.Add("Choose")
        excludelist.Add("DocEntryDraft")
        excludelist.Add("DocEntryIssue")


    End Sub

    Private Sub btn_1_Click(sender As System.Object, e As System.EventArgs) Handles btn_1.Click
        Dim dt_transaksi As New DataTable
        Try

            connect()
            oCompany = oSBOConnection.Company
            SQLHandler = oSBOConnection.SQLHandler
            dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetRawSugarList(cmb_stat.Text.ToString(), dtp1.Value.ToString("yyyyMMdd"), dtp2.Value.ToString("yyyyMMdd")))
            dgv_list.DataSource = dt_transaksi
            If dt_transaksi.Columns.Count > 1 Then
                For Each column As DataColumn In dt_transaksi.Columns
                    dgv_list.Columns(column.ColumnName).ReadOnly = True
                    dgv_list.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                    If column.ColumnName.ToString() = "Berat Raw Sugar" Or column.ColumnName.ToString() = "Berat Raw Sugar Confirm" Or column.ColumnName.ToString() = "Timbangan L" Or column.ColumnName.ToString() = "Timbangan R" Then
                        dgv_list.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        dgv_list.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                    End If
                    If excludelist.Contains(column.ColumnName.ToString) Then
                        dgv_list.Columns(column.ColumnName).Visible = False
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub dgv_list_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_list.CellClick
        Dim dt_detail As New DataTable

        shift = dgv_list.Item("Shift", dgv_list.CurrentRow.Index).Value
        date2 = dgv_list.Item("Tanggal Timbang", dgv_list.CurrentRow.Index).Value
        docentry = Replace(Replace(dgv_list.Item("Nomor Timbangan", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
        If docentry = "" Then
            docentry = "0"
        End If

        dt_detail = oDBConnection.executeQuery(SQLHandler.GetRawSugarDetail(shift, date2.ToString("yyyyMMdd"), docentry))
        dgv_detail.DataSource = dt_detail
        If dt_detail.Columns.Count > 1 Then
            For Each column As DataColumn In dt_detail.Columns
                dgv_detail.Columns(column.ColumnName).ReadOnly = True
                dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                If column.ColumnName.ToString() = "Berat Raw Sugar" Or column.ColumnName.ToString() = "Berat Raw Sugar Confirm" Or column.ColumnName.ToString() = "Counter Awal" Or column.ColumnName.ToString() = "Counter Akhir" Then
                    dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                End If

            Next
        End If
        dgv_detail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgv_detail.AutoResizeColumns()
    End Sub

   
    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub dgv_list_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_list.CellContentClick

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filepath As String
        Try
            filepath = System.IO.Path.Combine(
                  My.Computer.FileSystem.SpecialDirectories.MyDocuments, "BMM-ListRaw-" & Now.ToString("yyyyMMddHHmmss") & ".xlsx")
            cFunction.ExportToExcel(dt_transaksi, filepath)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class