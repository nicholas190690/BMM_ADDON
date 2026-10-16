Public Class Form_Search
    Dim oRec As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim dt_transaksi As DataTable
    Dim index As Integer

    Private kodeform As Integer
    Private callerForm As Object
    Private keysdown As New List(Of Keys)
    Dim parentform As String
    Dim excludePO As New List(Of String)
    Dim excluderaw As New List(Of String)
    Dim excludeSales As New List(Of String)
    Dim excludeall As New List(Of String)

    Public Sub New(ByVal formName As String, ByRef form As Object)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        callerForm = form
        parentform = formName
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByRef form As Object)
        InitializeComponent()
        callerForm = form
    End Sub

    Sub choose()
        ClassWeighbridge = New oWeighBridge
        index = dgv_trans.CurrentRow.Index
        If parentform = "BMMWeighBridge" Then

            'ClassWeighbridge.UCARDCODE = dgv_trans.Rows(index).Cells("CardCode").Value
            'ClassWeighbridge.UCARDNAME = dgv_trans.Rows(index).Cells("CardName").Value
            'ClassWeighbridge.UITEMCODE = dgv_trans.Rows(index).Cells("ItemCode").Value
            'ClassWeighbridge.UITEMNAME = dgv_trans.Rows(index).Cells("ItemName").Value
            'ClassWeighbridge.UNOSPT = dgv_trans.Rows(index).Cells("SPT").Value
            'ClassWeighbridge.UNOPO = dgv_trans.Rows(index).Cells("PO").Value
            'ClassWeighbridge.UNOSO = dgv_trans.Rows(index).Cells("SO").Value
            'ClassWeighbridge.UNOSPPB = dgv_trans.Rows(index).Cells("SPPB").Value
            'ClassWeighbridge.USURATJALAN = dgv_trans.Rows(index).Cells("NoSJ").Value
            'ClassWeighbridge.UKENDARAAN = dgv_trans.Rows(index).Cells("Kendaraan").Value
            'ClassWeighbridge.UNOPOL = dgv_trans.Rows(index).Cells("Nopol").Value
            'ClassWeighbridge.USUPIR = dgv_trans.Rows(index).Cells("Supir").Value
            'ClassWeighbridge.UTRANSPORTIR = dgv_trans.Rows(index).Cells("Transporter").Value
            'ClassWeighbridge.UBATCH = dgv_trans.Rows(index).Cells("Batch").Value
            'ClassWeighbridge.UQTYSPM = dgv_trans.Rows(index).Cells("QtySPM").Value
            'ClassWeighbridge.UQTYSJ = dgv_trans.Rows(index).Cells("QtySJ").Value

          
        End If




    End Sub

    Sub loaddgv()
        Try
            Dim i As Integer

            If parentform = "BMMWeighBridge" Then
                dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetTransaksi(Global_tipeP, Now.ToString("yyyyMMdd")))
            Else
                dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetListSPMSPB("20000101", "29991231", "F,C", "Keluar"))
            End If
            dgv_trans.DataSource = dt_transaksi
            If dt_transaksi.Columns.Count > 1 Then
                For Each column As DataColumn In dt_transaksi.Columns
                    dgv_trans.Columns(column.ColumnName).ReadOnly = True
                    dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    If column.DataType.ToString() <> "System.String" And column.DataType.ToString() <> "System.DateTime" Then
                        If column.ColumnName <> "SPPB" And column.ColumnName <> "SPT" And column.ColumnName <> "SO" And parentform = "BMMWeighBridge" Then
                            dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                        End If
                    End If
                    If excludePO.Contains(column.ColumnName.ToString) And Global_tipeP = "4" And parentform = "BMMWeighBridge" Then
                        dgv_trans.Columns(column.ColumnName).Visible = False
                    End If
                    If excluderaw.Contains(column.ColumnName.ToString) And Global_tipeP = "3" And parentform = "BMMWeighBridge" Then
                        dgv_trans.Columns(column.ColumnName).Visible = False
                    End If
                    If excludeSales.Contains(column.ColumnName.ToString) And (Global_tipeP = "2" Or Global_tipeP = "1") And parentform = "BMMWeighBridge" Then
                        dgv_trans.Columns(column.ColumnName).Visible = False
                    End If
                    If excludeall.Contains(column.ColumnName.ToString) And parentform = "BMMWeighBridge" Then
                        dgv_trans.Columns(column.ColumnName).Visible = False
                    End If
                    dgv_trans.AutoResizeColumn(i)
                    i += 1
                Next
            End If
            Dim btn As New DataGridViewButtonColumn
            btn.HeaderText = ""
            btn.Text = "Select"
            btn.Name = "btn"
            btn.UseColumnTextForButtonValue = True
            dgv_trans.Columns.Insert(0, btn)
            dgv_trans.AllowUserToAddRows = False


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try

       
    End Sub

    Private Sub Search_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        excludePO.Add("SPPB")
        excludePO.Add("SPt")
        excludePO.Add("SO")
        excludePO.Add("Kapal")
        excludePO.Add("NoSJ")
        excludePO.Add("Kendaraan")
        excludePO.Add("Nopol")
        excludePO.Add("Transporter")
        excludePO.Add("Supir")
        excludePO.Add("QtySPM")

        excluderaw.Add("SO")
        excluderaw.Add("SPPB")
        excluderaw.Add("QtySPM")


        excludeSales.Add("NoSJ")
        excludeSales.Add("QtySJ")

          

        excludeall.Add("NoSystem")
        excludeall.Add("NoSpm")
      


        loaddgv()
    End Sub


    Private Sub dgv_trans_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_trans.CellClick
        Try
            If e.ColumnIndex = 0 Then
                choose()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

 
    Private Sub dgv_trans_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_trans.CellContentClick

    End Sub
End Class