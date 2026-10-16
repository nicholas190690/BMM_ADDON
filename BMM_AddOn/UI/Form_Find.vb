Public Class Form_Find

    Dim oRec As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim dt_transaksi As DataTable
    Dim index As Integer
    Dim parentform As String
    Private kodeform As Integer
    Private callerForm As Object
    Private keysdown As New List(Of Keys)

    Dim TypeSearch As String
    Dim NomorSearch As String

    Sub loaddgv()
        Try
            Dim i As Integer
            If TypeSearch = "DocNum" Then
                dt_transaksi = oDBConnection.executeQuery(" CALL " & oSBOConnection.DatabaseName & ".SOL_GET_TRANSAKSISPBSPM('20000101', '29991231')")
            ElseIf TypeSearch = "SPT" Then
                dt_transaksi = oDBConnection.executeQuery(" CALL " & oSBOConnection.DatabaseName & ".SOL_GET_TRANSAKSISPT('" & ClassWeighbridge.UDTPOSTDATE & "')")
            ElseIf TypeSearch = "PO" Then
                dt_transaksi = oDBConnection.executeQuery(" CALL " & oSBOConnection.DatabaseName & ".SOL_GET_TRANSAKSIPO('" & ClassWeighbridge.UDTPOSTDATE & "')")
            ElseIf TypeSearch = "TG" Then
                dt_transaksi = oDBConnection.executeQuery(" CALL " & oSBOConnection.DatabaseName & ".SOL_GET_TRANSAKSITG('" & ClassWeighbridge.UDTPOSTDATE & "')")
            End If
            dgv_trans.DataSource = dt_transaksi
            If dt_transaksi.Columns.Count > 1 Then
                For Each column As DataColumn In dt_transaksi.Columns
                    dgv_trans.Columns(column.ColumnName).ReadOnly = True
                    dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    If column.DataType.ToString() <> "System.String" And column.DataType.ToString() <> "System.DateTime" Then
                        If column.ColumnName <> "SPPB" And column.ColumnName <> "SPT" And column.ColumnName <> "Kendaraan" And column.ColumnName <> "SO" And column.ColumnName <> "No SPTA" And column.ColumnName <> "No SPTA" And column.ColumnName <> "No SPTA" Then
                            dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            dgv_trans.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
                        End If
                    End If
                    dgv_trans.AutoResizeColumn(i)
                    If column.ColumnName = "Tipe" Then
                        dgv_trans.Columns(column.ColumnName).Visible = False
                    End If
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
    Public Sub New(ByVal formName As String, ByRef form As Object, ByRef P_TypeSearch As String, ByRef P_NomorSearch As String)
        InitializeComponent()
        callerForm = form
        parentform = formName
        TypeSearch = P_TypeSearch
        NomorSearch = P_NomorSearch
    End Sub
    Public Sub New(ByRef form As Object)
        InitializeComponent()
        callerForm = form
    End Sub

    Private Sub Form_Find_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SQLHandler = New HANAVersion(oSBOConnection.DatabaseName)
        loaddgv()
    End Sub
    Sub choose()
        ClassWeighbridge = New oWeighBridge
        index = dgv_trans.CurrentRow.Index
       If TypeSearch = "PO" Then
            ClassWeighbridge.UNOPO = If(dgv_trans.Columns.Contains("PO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("PO").Value), "-", dgv_trans.Rows(index).Cells("PO").Value), "-")
            ClassWeighbridge.UQTYSJ = If(dgv_trans.Columns.Contains("Jumlah PO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Jumlah PO").Value), 0, dgv_trans.Rows(index).Cells("Jumlah PO").Value), 0)
            ClassWeighbridge.UKAPAL = If(IsDBNull(dgv_trans.Rows(index).Cells("Kapal").Value), 0, dgv_trans.Rows(index).Cells("Kapal").Value)
        ElseIf TypeSearch = "SPT" Then
            ClassWeighbridge.UNOPOL = If(dgv_trans.Columns.Contains("Nopol") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nopol").Value), "", dgv_trans.Rows(index).Cells("Nopol").Value), "")
            ClassWeighbridge.UNOSPPB = If(dgv_trans.Columns.Contains("SPPB") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("SPPB").Value), "-", dgv_trans.Rows(index).Cells("SPPB").Value), "-")
            ClassWeighbridge.UNOSPT = If(dgv_trans.Columns.Contains("SPT") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("SPT").Value), "-", dgv_trans.Rows(index).Cells("SPT").Value), "-")
            ClassWeighbridge.UNOPO = If(dgv_trans.Columns.Contains("PO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("PO").Value), "-", dgv_trans.Rows(index).Cells("PO").Value), "-")
            ClassWeighbridge.UNOSO = If(dgv_trans.Columns.Contains("SO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("SO").Value), "-", dgv_trans.Rows(index).Cells("SO").Value), "-")
            ClassWeighbridge.UKENDARAAN = If(dgv_trans.Columns.Contains("Kendaraan") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Kendaraan").Value), "1", dgv_trans.Rows(index).Cells("Kendaraan").Value), "1")
            ClassWeighbridge.USUPIR = If(dgv_trans.Columns.Contains("Supir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Supir").Value), "", dgv_trans.Rows(index).Cells("Supir").Value), "")
            ClassWeighbridge.UBATCH = If(dgv_trans.Columns.Contains("Batch") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Batch").Value), "", dgv_trans.Rows(index).Cells("Batch").Value), "")
            ClassWeighbridge.UQTYSPM = If(dgv_trans.Columns.Contains("Jumlah SPT") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Jumlah SPT").Value), 0, dgv_trans.Rows(index).Cells("Jumlah SPT").Value), 0)
            ClassWeighbridge.UTRANSPORTIR = If(dgv_trans.Columns.Contains("Transportir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Transportir").Value), "", dgv_trans.Rows(index).Cells("Transportir").Value), "")

            If Not IsDBNull(dgv_trans.Rows(index).Cells("DocEntry").Value) Then
                ClassWeighbridge.DocEntry = If(dgv_trans.Columns.Contains("DocEntry") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("DocEntry").Value), 0, dgv_trans.Rows(index).Cells("DocEntry").Value), 0)
                ClassWeighbridge.DocNum = If(dgv_trans.Columns.Contains("DocNum") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("DocNum").Value), 0, dgv_trans.Rows(index).Cells("DocNum").Value), 0)
                ClassWeighbridge.USURATJALAN = If(dgv_trans.Columns.Contains("U_SOL_SURATJALAN") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_SURATJALAN").Value), "", dgv_trans.Rows(index).Cells("U_SOL_SURATJALAN").Value), "")
                ClassWeighbridge.USUPIR = If(dgv_trans.Columns.Contains("U_SOL_SUPIR") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_SUPIR").Value), "", dgv_trans.Rows(index).Cells("U_SOL_SUPIR").Value), "")
                ClassWeighbridge.UQTYSPM = If(dgv_trans.Columns.Contains("U_SOL_QTY_SPM") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_QTY_SPM").Value), 0, dgv_trans.Rows(index).Cells("U_SOL_QTY_SPM").Value), 0)
                ClassWeighbridge.UQTYSJ = If(dgv_trans.Columns.Contains("U_SOL_QTY_SJ") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_QTY_SJ").Value), 0, dgv_trans.Rows(index).Cells("U_SOL_QTY_SJ").Value), 0)
                ClassWeighbridge.UQTY1 = If(dgv_trans.Columns.Contains("U_SOL_QTY_WEIGH1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_QTY_WEIGH1").Value), 0, dgv_trans.Rows(index).Cells("U_SOL_QTY_WEIGH1").Value), 0)
                ClassWeighbridge.UDTWEIGH1 = If(dgv_trans.Columns.Contains("U_SOL_DT_WEIGH1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value), "19000101", dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(6, 4) & dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(3, 2) & dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(0, 2)), "19000101")
                ClassWeighbridge.Remarks = If(dgv_trans.Columns.Contains("U_SOL_REMARKS") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_REMARKS").Value), "", dgv_trans.Rows(index).Cells("U_SOL_REMARKS").Value), "")
                ClassWeighbridge.CreateDate = If(dgv_trans.Columns.Contains("CreateDate") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateDate").Value), "19000101", dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(6, 4) & dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(3, 2) & dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(0, 2)), "19000101")
                ClassWeighbridge.CreateTime = If(dgv_trans.Columns.Contains("CreateTime") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateTime").Value), "", dgv_trans.Rows(index).Cells("CreateTime").Value), "")
                ClassWeighbridge.UOP1 = If(dgv_trans.Columns.Contains("U_SOL_OPERATORW1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_OPERATORW1").Value), "", dgv_trans.Rows(index).Cells("U_SOL_OPERATORW1").Value), "")
                'ClassWeighbridge.UPERCENTAGE = If(IsDBNull(dgv_trans.Rows(index).Cells("Summary").Value), 0, dgv_trans.Rows(index).Cells("Summary").Value)
            End If

        ElseIf TypeSearch = "DocNum" Then

            '_DocEntry = 0
            '_DocNum = 0
            '_CreateDate = "19000101"
            '_CreateTime = 0
            '_Remarks = ""
            '_UCARDCODE = ""
            '_UCARDNAME = ""
            '_UDTWEIGH1 = "19000101"
            '_UDTWEIGH2 = "19000101"
            '_UITEMCODE = ""
            '_UITEMNAME = ""
            '_UKAPAL = ""
            '_UKENDARAAN = "1"
            '_UNOPO = "-"
            '_UNOPOL = ""
            '_UNOSO = "-"
            '_UNOSPPB = "-"
            '_UNOSPT = "-"
            '_UOP1 = ""
            '_UOP2 = ""
            '_UPERCENTAGE = 0
            '_UQTY0 = 0
            '_UQTY1 = 0
            '_UQTY2 = 0
            '_UQTYSJ = 0
            '_UQTYSPM = 0
            '_UREMARKS = ""
            '_USERIES = 0
            '_USTAT = ""
            '_USUPIR = ""
            '_USURATJALAN = ""
            '_UTIPE = ""
            '_UTIPETIMBANG1 = ""
            '_UTIPETIMBANG2 = ""
            '_UTRANSPORTIR = ""
            '_UTWEIGH1 = 0
            '_UTWEIGH2 = 0
            '_UBATCH = ""
            '_USUMMARY = 0
            '_UDTLOAD1 = "19000101"
            '_UDTLOAD2 = "19000101"
            '_UTLOAD1 = 0
            '_UTLOAD2 = 0
            '_UQTYKAPAL = 0

            ClassWeighbridge.UNOSPT = If(dgv_trans.Columns.Contains("Nomor SPT") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor SPT").Value), "-", dgv_trans.Rows(index).Cells("Nomor SPT").Value), "-")
            ClassWeighbridge.DocNum = If(dgv_trans.Columns.Contains("Nomor SPM/SPB") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor SPM/SPB").Value), 0, dgv_trans.Rows(index).Cells("Nomor SPM/SPB").Value), 0)
            ClassWeighbridge.USURATJALAN = If(dgv_trans.Columns.Contains("Nomor Surat Jalan") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor Surat Jalan").Value), "", dgv_trans.Rows(index).Cells("Nomor Surat Jalan").Value), "")
            ClassWeighbridge.UNOPOL = If(dgv_trans.Columns.Contains("Nopol") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nopol").Value), "", dgv_trans.Rows(index).Cells("Nopol").Value), "")
            ClassWeighbridge.UKENDARAAN = If(dgv_trans.Columns.Contains("Kendaraan") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Kendaraan").Value), "1", dgv_trans.Rows(index).Cells("Kendaraan").Value), "1")
            ClassWeighbridge.UTRANSPORTIR = If(dgv_trans.Columns.Contains("Transportir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Transportir").Value), "", dgv_trans.Rows(index).Cells("Transportir").Value), "")
            ClassWeighbridge.USUPIR = If(dgv_trans.Columns.Contains("Supir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Supir").Value), "", dgv_trans.Rows(index).Cells("Supir").Value), "")
            ClassWeighbridge.UQTYSPM = If(dgv_trans.Columns.Contains("Qty SPM") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Qty SPM").Value), 0, dgv_trans.Rows(index).Cells("Qty SPM").Value), 0)
            ClassWeighbridge.UQTYSJ = If(dgv_trans.Columns.Contains("Qty SJ") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Qty SJ").Value), 0, dgv_trans.Rows(index).Cells("Qty SJ").Value), 0)
            ClassWeighbridge.UQTY1 = If(dgv_trans.Columns.Contains("Timbang Masuk") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Timbang Masuk").Value), 0, dgv_trans.Rows(index).Cells("Timbang Masuk").Value), 0)
            ClassWeighbridge.UDTWEIGH1 = If(dgv_trans.Columns.Contains("Tanggal Masuk") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Tanggal Masuk").Value), "19000101", dgv_trans.Rows(index).Cells("Tanggal Masuk").Value), "19000101")
            ClassWeighbridge.UBATCH = If(dgv_trans.Columns.Contains("Batch") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Batch").Value), "", dgv_trans.Rows(index).Cells("Batch").Value), "")
            ClassWeighbridge.UREMARKS = If(dgv_trans.Columns.Contains("Remarks") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Remarks").Value), "", dgv_trans.Rows(index).Cells("Remarks").Value), "")
            ClassWeighbridge.UNOPO = If(dgv_trans.Columns.Contains("Nomor PO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor PO").Value), "-", dgv_trans.Rows(index).Cells("Nomor PO").Value), "-")
            ClassWeighbridge.UNOSO = If(dgv_trans.Columns.Contains("Nomor SO") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor SO").Value), "-", dgv_trans.Rows(index).Cells("Nomor SO").Value), "-")
            ClassWeighbridge.CreateDate = If(dgv_trans.Columns.Contains("CreateDate") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateDate").Value), "", dgv_trans.Rows(index).Cells("CreateDate").Value), "")
            ClassWeighbridge.CreateTime = If(dgv_trans.Columns.Contains("CreateTime") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateTime").Value), "", dgv_trans.Rows(index).Cells("CreateTime").Value), "")
            ClassWeighbridge.UKAPAL = If(dgv_trans.Columns.Contains("Kapal") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Kapal").Value), "", dgv_trans.Rows(index).Cells("Kapal").Value), "")
            ClassWeighbridge.UQTYKAPAL = If(dgv_trans.Columns.Contains("TimbanganPelabuhan") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("TimbanganPelabuhan").Value), 0, dgv_trans.Rows(index).Cells("TimbanganPelabuhan").Value), 0)
            ClassWeighbridge.DocEntry = dgv_trans.Rows(index).Cells("NoSystem").Value
            ClassWeighbridge.UOP1 = If(dgv_trans.Columns.Contains("Operator1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Operator1").Value), "", dgv_trans.Rows(index).Cells("Operator1").Value), "")
            ClassWeighbridge.UNOTG = If(dgv_trans.Columns.Contains("Nomor Tukar Guling") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor Tukar Guling").Value), "", dgv_trans.Rows(index).Cells("Nomor Tukar Guling").Value), "")
            ClassWeighbridge.UQTYTG = If(dgv_trans.Columns.Contains("Jumlah Tukar Guling") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Jumlah Tukar Guling").Value), 0, dgv_trans.Rows(index).Cells("Jumlah Tukar Guling").Value), 0)
            ClassWeighbridge.UNOSPPB = If(dgv_trans.Columns.Contains("Nomor SPPB") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor SPPB").Value), "-", dgv_trans.Rows(index).Cells("Nomor SPPB").Value), "-")
        ElseIf TypeSearch = "TG" Then
            ClassWeighbridge.UTRANSPORTIR = If(dgv_trans.Columns.Contains("Transportir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Transportir").Value), "", dgv_trans.Rows(index).Cells("Transportir").Value), "")
            ClassWeighbridge.USUPIR = If(dgv_trans.Columns.Contains("Supir") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Supir").Value), "", dgv_trans.Rows(index).Cells("Supir").Value), "")
            ClassWeighbridge.UNOTG = If(dgv_trans.Columns.Contains("Nomor Tukar Guling") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nomor Tukar Guling").Value), "", dgv_trans.Rows(index).Cells("Nomor Tukar Guling").Value), "")
            ClassWeighbridge.UQTYTG = If(dgv_trans.Columns.Contains("Jumlah Tukar Guling") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Jumlah Tukar Guling").Value), 0, dgv_trans.Rows(index).Cells("Jumlah Tukar Guling").Value), 0)
            ClassWeighbridge.UNOPOL = If(dgv_trans.Columns.Contains("Nopol") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("Nopol").Value), "", dgv_trans.Rows(index).Cells("Nopol").Value), "")

            If Not IsDBNull(dgv_trans.Rows(index).Cells("DocEntry").Value) Then
                ClassWeighbridge.DocEntry = If(dgv_trans.Columns.Contains("DocEntry") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("DocEntry").Value), 0, dgv_trans.Rows(index).Cells("DocEntry").Value), 0)
                ClassWeighbridge.DocNum = If(dgv_trans.Columns.Contains("DocNum") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("DocNum").Value), 0, dgv_trans.Rows(index).Cells("DocNum").Value), 0)
                ClassWeighbridge.UQTY1 = If(dgv_trans.Columns.Contains("U_SOL_QTY_WEIGH1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_QTY_WEIGH1").Value), 0, dgv_trans.Rows(index).Cells("U_SOL_QTY_WEIGH1").Value), 0)
                ClassWeighbridge.UDTWEIGH1 = If(dgv_trans.Columns.Contains("U_SOL_DT_WEIGH1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value), "19000101", dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(6, 4) & dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(3, 2) & dgv_trans.Rows(index).Cells("U_SOL_DT_WEIGH1").Value.ToString().Substring(0, 2)), "19000101")
                ClassWeighbridge.Remarks = If(dgv_trans.Columns.Contains("U_SOL_REMARKS") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_REMARKS").Value), "", dgv_trans.Rows(index).Cells("U_SOL_REMARKS").Value), "")
                ClassWeighbridge.CreateDate = If(dgv_trans.Columns.Contains("CreateDate") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateDate").Value), "19000101", dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(6, 4) & dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(3, 2) & dgv_trans.Rows(index).Cells("CreateDate").Value.ToString().Substring(0, 2)), "19000101")
                ClassWeighbridge.CreateTime = If(dgv_trans.Columns.Contains("CreateTime") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("CreateTime").Value), "", dgv_trans.Rows(index).Cells("CreateTime").Value), "")
                ClassWeighbridge.UOP1 = If(dgv_trans.Columns.Contains("U_SOL_OPERATORW1") = True, If(IsDBNull(dgv_trans.Rows(index).Cells("U_SOL_OPERATORW1").Value), "", dgv_trans.Rows(index).Cells("U_SOL_OPERATORW1").Value), "")
                'ClassWeighbridge.UPERCENTAGE = If(IsDBNull(dgv_trans.Rows(index).Cells("Summary").Value), 0, dgv_trans.Rows(index).Cells("Summary").Value)
            End If

        End If

        ClassWeighbridge.UTIPE = dgv_trans.Rows(index).Cells("Tipe").Value
        ClassWeighbridge.UCARDCODE = If(dgv_trans.Columns.Contains("CardCode") = True, If(dt_transaksi.Columns.Contains("CardCode") = True, dgv_trans.Rows(index).Cells("CardCode").Value, "-"), "-")
        ClassWeighbridge.UCARDNAME = If(dgv_trans.Columns.Contains("CardName") = True, If(dt_transaksi.Columns.Contains("CardName") = True, dgv_trans.Rows(index).Cells("CardName").Value, "-"), "-")
        ClassWeighbridge.UITEMCODE = If(dgv_trans.Columns.Contains("ItemCode") = True, If(dt_transaksi.Columns.Contains("ItemCode") = True, dgv_trans.Rows(index).Cells("ItemCode").Value, "-"), "-")
        ClassWeighbridge.UITEMNAME = If(dgv_trans.Columns.Contains("ItemName") = True, If(dt_transaksi.Columns.Contains("ItemName") = True, dgv_trans.Rows(index).Cells("ItemName").Value, "-"), "-")


        If (ClassWeighbridge.UTIPE = 1 Or ClassWeighbridge.UTIPE = 2) And ClassWeighbridge.UQTY1 > 0 Then
            ClassWeighbridge.checkNopolOutstanding()
        ElseIf (ClassWeighbridge.UTIPE = 1 Or ClassWeighbridge.UTIPE = 2) And ClassWeighbridge.UQTY1 = 0 Then
            ClassWeighbridge.UQTYSPMSUM = ClassWeighbridge.UQTYSPM
        End If

        Form_WeighBridge.bindingtextbox()
        Form_WeighBridge.propertychanged()
    End Sub



    Private Sub dgv_trans_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_trans.CellClick
        Try
            If e.ColumnIndex = 0 Then
                Choose()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

 


    Private Sub dgv_trans_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_trans.ColumnHeaderMouseClick
        If (dgv_trans.SortOrder.ToString() = "Descending") Then
            dt_transaksi.DefaultView.Sort = dgv_trans.SortedColumn.Name + " DESC"
        Else
            dt_transaksi.DefaultView.Sort = dgv_trans.SortedColumn.Name + " ASC"
        End If
        dt_transaksi = dt_transaksi.DefaultView.ToTable()
    End Sub
End Class