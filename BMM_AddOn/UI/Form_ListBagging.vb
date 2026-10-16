Public Class Form_ListBagging

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
    Dim incluelist As New List(Of String)
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

    Private Sub Form_ListBagging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmb_stat.Text = "All"
        excludelist.Add("Choose")
        excludelist.Add("DocEntryDraft")
        excludelist.Add("DocEntryReceipt")

        incluelist.Add("Jumlah Karung")
        incluelist.Add("Jumlah Karung Confirm")
        incluelist.Add("Jumlah Karung Over")
        incluelist.Add("Jumlah Karung Under")
        incluelist.Add("Jumlah Karung Cleaning")
        incluelist.Add("Berat Karung")
        incluelist.Add("Bagging 1")
        incluelist.Add("Bagging 2")
        incluelist.Add("Line A")
        incluelist.Add("Bagging 3")
        incluelist.Add("Bagging 4")
        incluelist.Add("Line B")
        incluelist.Add("Bagging 5")
        incluelist.Add("Bagging 6")
        incluelist.Add("Line C")
        incluelist.Add("Berat Karung")
        incluelist.Add("Counter")
    End Sub

    Private Sub btn_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_1.Click
        Dim dt_transaksi As New DataTable
        Try

            connect()
            oCompany = oSBOConnection.Company
            SQLHandler = oSBOConnection.SQLHandler
            dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetAllBagging(cmb_stat.Text.ToString(), dtp1.Value.ToString("yyyyMMdd"), dtp2.Value.ToString("yyyyMMdd")))
            dgv_list.DataSource = dt_transaksi
            If dt_transaksi.Columns.Count > 1 Then
                For Each column As DataColumn In dt_transaksi.Columns
                    dgv_list.Columns(column.ColumnName).ReadOnly = True
                    dgv_list.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                    If incluelist.Contains(column.ColumnName.ToString) Then
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

    Private Sub dgv_list_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_list.CellClick
        Dim dt_detail As New DataTable
        If dgv_list.CurrentRow.Index <> dgv_list.RowCount - 1 Then
            shift = dgv_list.Item("Shift", dgv_list.CurrentRow.Index).Value
            date2 = dgv_list.Item("Tanggal Bagging", dgv_list.CurrentRow.Index).Value
            docentry = dgv_list.Item("Nomor Bagging", dgv_list.CurrentRow.Index).Value
            docentry = Replace(Replace(dgv_list.Item("Nomor Bagging", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
            If docentry = "" Then
                docentry = "0"
            End If
            dt_detail = oDBConnection.executeQuery(SQLHandler.GetBaggingList(shift, date2.ToString("yyyyMMdd"), docentry))
            dgv_detail.DataSource = dt_detail
            If dt_detail.Columns.Count > 1 Then
                For Each column As DataColumn In dt_detail.Columns
                    dgv_detail.Columns(column.ColumnName).ReadOnly = True
                    dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                    If incluelist.Contains(column.ColumnName.ToString) Then
                        dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                    End If

                Next
            End If
        Else
            dgv_detail.Rows.Clear()
        End If
     
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filepath As String
        Try
            filepath = System.IO.Path.Combine(
                  My.Computer.FileSystem.SpecialDirectories.MyDocuments, "BMM-ListBagging-" & Now.ToString("yyyyMMddHHmmss") & ".xlsx")
            cFunction.ExportToExcel(dt_detail, filepath)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class