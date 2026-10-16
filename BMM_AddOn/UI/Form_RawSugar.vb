Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.VisualBasic

Public Class Form_RawSugar
    Dim oCompany As SAPbobsCOM.Company
    Dim oGeneralService As SAPbobsCOM.GeneralService
    Dim oGeneralData As SAPbobsCOM.GeneralData
    Dim oCompService As SAPbobsCOM.CompanyService
    Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
    Dim oRec As SAPbobsCOM.Recordset
    Dim start As String
    Dim dt_operator As DataTable
    Dim dt_mesin As DataTable
    Dim dt_shift As DataTable
    Dim end_id As String
    Dim shift As String
    Dim operator_id As String
    Dim qty1 As Double = 0
    Dim qty2 As Double = 0
    Dim itemcode As String
    Dim itemname As String
    Dim mesin As String
    Dim docentry As String
    Dim tipeissue As String
    Dim qtytoleransi As Double
    Dim oDraft As SAPbobsCOM.Documents
    Dim oChild As SAPbobsCOM.GeneralData
    Dim oChildren As SAPbobsCOM.GeneralDataCollection
    Dim startshift As Double = 0
    Dim endshift As Double = 0
    Dim s_startshift As String
    Dim s_endshift As String
    Dim counter1 As Double = 0
    Dim counter2 As Double = 0
    Dim remarks As String
    Private Sub EmptyValue()
        txt_qty1.Text = 0
        txt_qty2.Text = 0
        txt_start.Text = "-"
        txt_end.Text = "-"
        txt_toleransi.Text = 0
        txt_remarks.Text = "-"
    End Sub

    Private Sub setvalue()
        '   start = txt_start.Text
        '      end_id = txt_end.Text
        shift = cmb_shift.Text
        operator_id = cmb_operator.SelectedValue()
        qty1 = txt_qty1.Text
        qty2 = txt_qty2.Text
        itemcode = txt_muatan.Text
        itemname = txt_muatan_name.Text
        mesin = cmb_mesin.SelectedValue()
        tipeissue = oApplicationConfig.TipeIssue
        remarks = txt_remarks.Text

 
    End Sub

    Private Function Validation() As String
        If txt_start.Text = "-" Or txt_end.Text = "-" Then
            Return " Warning : 01 - Tekan tombol generate terlebih dahulu"
        End If
        Return "-"
    End Function

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



    Public Sub loadcombo()

        Dim index As Integer
        Dim sql As String
        sql = "SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" WHERE ""U_SOL_TYPE"" = '1'  ORDER BY ""Name"" ASC"
        dt_operator = oDBConnection.executeQuery(sql)
        cmb_operator.Items.Clear()
        cmb_operator.DataSource = dt_operator
        cmb_operator.SelectedIndex() = 0
        cmb_operator.DisplayMember = "Name"
        cmb_operator.ValueMember = "Code"

        sql = "SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" WHERE ""U_SOL_TYPE"" = '1'  ORDER BY ""Name"" ASC"
        dt_mesin = oDBConnection.executeQuery(sql)
        cmb_mesin.Items.Clear()
        cmb_mesin.DataSource = dt_mesin
        'If oApplicationConfig.Timbangan = "Timbangan L" Then
        cmb_mesin.SelectedIndex() = 0
        'Else
        'cmb_mesin.SelectedIndex() = 1
        ' End If
        cmb_mesin.DisplayMember = "Name"
        cmb_mesin.ValueMember = "Code"

        cmb_shift.Text = "1"
    End Sub


    Private Sub Form_RawSugar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            connect()

            Dim childtables(0) As String

            SQLHandler = oSBOConnection.SQLHandler
            oCompany = oSBOConnection.Company


            Utils.CreateUDT(oSBOConnection.Company, "SOL_OPERATOR", "Operator", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            Utils.CreateUDT(oSBOConnection.Company, "SOL_MESIN", "Mesin", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            Utils.CreateUDT(oSBOConnection.Company, "SOL_RAW", "RawSugar Scale", SAPbobsCOM.BoUTBTableType.bott_Document)
            ' Utils.CreateUDT(oSBOConnection.Company, "SOL_MAP_COA", "Mapping Issue", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            Utils.CreateUDT(oSBOConnection.Company, "SOL_MAP_USER", "User Access Mapping", SAPbobsCOM.BoUTBTableType.bott_NoObject)

            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_START", "Start Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_END", "End Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_SHIFT", "Shift", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_OPERATOR", "Operator", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_QTY1", "Quantity Scale", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_QTY2", "Quantity Confirm", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_ITEMCODE", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_ITEMNAME", "ItemName", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_DATE", "Date", SAPbobsCOM.BoFieldTypes.db_Date)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_TIME", "Time", SAPbobsCOM.BoFieldTypes.db_Numeric)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_MESIN", "Mesin", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_USER", "User Sign", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_USERNM", "User Sign Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_STAT", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_ISSUE", "Type Issue", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_COUNTER1", "Counter 1", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_RAW", "SOL_COUNTER2", "Counter 2", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)



            '1 untuk issue / 2 untuk receipt
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_MAP_USER", "SOL_TYPE", "Menu", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_MAP_USER", "SOL_USERID", "User ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_MAP_USER", "SOL_USERNM", "User Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)

            Utils.CreateUDF(oSBOConnection.Company, "OIGE", "SOL_RAW_NUMBER", "Nomor Urut Timbang", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            Utils.CreateUDF(oSBOConnection.Company, "OIGE", "SOL_RAW_WEIGHT", "Berat Timbang Raw Sugar Weight", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity)

            '1 timbangan raw, 2 bagging
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_MESIN", "SOL_TYPE", "Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)

            Utils.CreateUDO(oSBOConnection.Company, "UDO_SOL_RAW", SAPbobsCOM.BoUDOObjType.boud_Document, "SOL_RAW", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO)
            loadcombo()
            dt_shift = oDBConnection.executeQuery(SQLHandler.GetShift())
            oCompService = oCompany.GetCompanyService()
            oGeneralService = oCompService.GetGeneralService("UDO_SOL_RAW")
            txt_muatan.Text = oApplicationConfig.ItemIssueCode
            txt_muatan_name.Text = oApplicationConfig.ItemIssueName

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        Finally
            Utils.releaseobject(oCompService)
        End Try

    End Sub



    Private Sub btn_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_generate.Click
        Dim Table_ As String
        Dim query As String
        Dim MDBConnString_ As String
        Dim ds As New DataSet
        Dim tgl As String
        Dim PATH As String
        Try
            If cmb_mesin.SelectedValue() = "7" Then
                PATH = oApplicationConfig.PathTimbanganL.ToString
            Else
                PATH = oApplicationConfig.PathTimbanganR.ToString
            End If
            If File.Exists(PATH & "BeltScale.mdb") = False Then
                MessageBox.Show("File MDB tidak di temukan - " & PATH & "BeltScale.mdb , mohon di check menu configuration !")
                Return
            End If
            If cmb_shift.Text = "" Then
                MessageBox.Show("Shift Belum di pilih")
                Return
            End If
            Dim result() As DataRow = dt_shift.Select("Shift = " & cmb_shift.Text & " and type = 1 ")
            ' "SELECT ""U_SOL_SHIFT"" AS ""Shift"",""U_SOL_START"" AS ""START"" ,""U_SOL_END"" as ""End"", ""U_SOL_TYPE"" as ""Type"" FROM " & oSBOConnection.DatabaseName.ToString & ".""@SOL_SHIFT"" "

            For Each row As DataRow In result
                startshift = row(1)
                endshift = row(2)
            Next
            If startshift = 24 Then
                startshift = 0
            End If
            s_startshift = Strings.Right("00" & startshift.ToString, 2) & ":00:00"
            s_endshift = Strings.Right("00" & endshift.ToString, 2) & ":00:00"
            'tanggal untuk query SAP
            tgl = dtp_date.Value.ToString("yyyyMMdd")

            btn_add.Enabled = False
            btn_generate.Enabled = False
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec.DoQuery(SQLHandler.GetLastScale(cmb_mesin.SelectedValue(), cmb_shift.Text, tgl))
            If oRec.RecordCount < 1 Then
                start = 0
            Else

                start = oRec.Fields().Item(0).Value
            End If

            Table_ = "Scale"

            'query date #month/date/year#
            'tanggal untuk query access
            tgl = dtp_date.Value.ToString("MM/dd/yyyy")
            ' query = "SELECT MIN(IDAUTO) as MINID, MAX(IDAUTO) AS MAXID ,format(MIN(VALSTART), ""Standard"") AS START,format(MAX(VALSTOP), ""Standard"") AS STOP,format(SUM(NETWEIGHT), ""Standard"") AS WEIGHT FROM " & Table_ & " WHERE IDAUTO > " + start.ToString + " AND  DATESTOP = #" & tgl & "# AND TIMESTOP >= '" & s_startshift & "' AND TIMESTOP < '" & s_endshift & "' "
            query = "SELECT " _
                & "T04.MINID AS MINID " _
                & ",T04.MAXID AS MAXID " _
                & ",T04.WEIGHT  AS WEIGHT " _
                & ",format(T04.START, ""Standard"") AS START " _
                & ",format(T03.VALSTOP, ""Standard"") AS STOP " _
                & "FROM " _
                & "( " _
                & "SELECT " _
                & "T01.MINID AS MINID " _
                & ",T01.MAXID AS MAXID " _
                & ",T01.WEIGHT  AS WEIGHT " _
                & ",format(T02.VALSTART, ""Standard"") AS START " _
                & "FROM " _
                & "(SELECT MIN(T0.IDAUTO) as MINID, MAX(T0.IDAUTO) AS MAXID,format(SUM(T0.NETWEIGHT), ""Standard"") AS WEIGHT FROM Scale  " _
                & "T0  WHERE   T0.IDAUTO > " + start.ToString + " AND  T0.DATESTOP = #" & tgl & "# AND T0.TIMESTOP >= '" & s_startshift & "' AND T0.TIMESTOP < '" & s_endshift & "' ) T01 " _
                & "INNER JOIN SCALE T02 ON T02.IDAUTO = T01.MINID " _
                & ")T04 " _
                & "INNER JOIN SCALE T03 ON T03.IDAUTO = T04.MAXID "


            MDBConnString_ = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + PATH + "BeltScale.mdb;"

            Dim cnn As OleDbConnection = New OleDbConnection(MDBConnString_)
            cnn.Open()
            Dim cmd As New OleDbCommand(query, cnn)
            Dim da As New OleDbDataAdapter(cmd)
            da.Fill(ds, Table_)
            cnn.Close()
            Dim t1 As DataTable = ds.Tables(Table_)

            If t1.Rows.Count = 0 Then
                MessageBox.Show("Data timbangan tidak ditemukan")
                Return
            End If

            For i As Integer = 0 To t1.Rows.Count - 1
                If t1.Rows(i).Item("MINID").ToString() = "" Then
                    MessageBox.Show("Data timbangan tidak ditemukan")
                    Return
                End If

                txt_start.Text = t1.Rows(i).Item("START").ToString()
                txt_end.Text = t1.Rows(i).Item("STOP").ToString()
                txt_qty1.Text = t1.Rows(i).Item("WEIGHT").ToString()
                txt_qty2.Text = t1.Rows(i).Item("WEIGHT").ToString()
                counter1 = t1.Rows(i).Item("START")
                counter2 = t1.Rows(i).Item("STOP")
                start = t1.Rows(i).Item("MINID").ToString()
                end_id = t1.Rows(i).Item("MAXID").ToString()
                txt_toleransi.Text = txt_qty1.Text * toleransi / 100
            Next
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            tgl = dtp_date.Value.ToString("yyyyMMdd")
            oRec.DoQuery(SQLHandler.GetSumRawSugar(cmb_mesin.SelectedValue, tgl, cmb_shift.Text))
            If oRec.RecordCount > 0 Then
                oRec.MoveFirst()

            End If

        Catch ex As Exception
            Utils.releaseobject(oRec)
            MessageBox.Show(ex.Message.ToString())
        Finally
            Utils.releaseobject(oRec)
            btn_add.Enabled = True
            btn_generate.Enabled = True
        End Try

    End Sub

    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click

        Dim i As Integer
        Dim validasi As String = "Y"

        If Validation() <> "-" Then
            MessageBox.Show(Validation())
            Return
        End If
        Try
            btn_add.Enabled = False
            btn_generate.Enabled = False
            setvalue()
            qtytoleransi = qty1 * toleransi / 100

            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec.DoQuery(SQLHandler.GetToleransi())

            If oRec.RecordCount > 0 Then
                validasi = oRec.Fields.Item(4).Value
            End If
            Utils.releaseobject(oRec)
            If Math.Abs(qty2 - qty1) > qtytoleransi And validasi.ToUpper = "Y" Then
                MessageBox.Show("Melebihi Toleransi")
                btn_add.Enabled = True
                btn_generate.Enabled = True
                Return
            End If
            Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk konfirmasi timbangan ?", "Konfirmasi Timbangan", 2)
            If result = "Confirm" Then

                oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                oRec.DoQuery(SQLHandler.GetCurrent)
                If oRec.RecordCount > 0 Then
                    oRec.MoveFirst()
                End If
                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                oCompany.StartTransaction()


                oGeneralData.SetProperty("U_SOL_START", start)
                oGeneralData.SetProperty("U_SOL_END", end_id)
                oGeneralData.SetProperty("U_SOL_SHIFT", shift)
                oGeneralData.SetProperty("U_SOL_OPERATOR", operator_id)
                oGeneralData.SetProperty("U_SOL_QTY1", qty1)
                oGeneralData.SetProperty("U_SOL_QTY2", qty2)
                oGeneralData.SetProperty("U_SOL_ITEMCODE", itemcode)
                oGeneralData.SetProperty("U_SOL_ITEMNAME", itemname)
                oGeneralData.SetProperty("U_SOL_DATE", CType(dtp_date.Value, Date))
                oGeneralData.SetProperty("U_SOL_TIME", CType(oRec.Fields.Item("CTime").Value, Integer))
                oGeneralData.SetProperty("U_SOL_MESIN", mesin)
                oGeneralData.SetProperty("U_SOL_USER", "")
                oGeneralData.SetProperty("U_SOL_USERNM", "")
                oGeneralData.SetProperty("U_SOL_STAT", "Open")
                oGeneralData.SetProperty("U_SOL_ISSUE", tipeissue)
                oGeneralData.SetProperty("U_SOL_COUNTER1", counter1)
                oGeneralData.SetProperty("U_SOL_COUNTER2", counter2)
                oGeneralData.SetProperty("Remark", remarks)
                docentry = oGeneralService.Add(oGeneralData).GetProperty("DocEntry").ToString
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                MessageBox.Show("Konfirmasi Timbang Berhasil dengan nomor urut " & docentry)
                EmptyValue()
                ' End If
            End If
        Catch ex As Exception
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            MessageBox.Show(ex.Message.ToString())
        Finally
            Utils.releaseobject(oGeneralData)
            Utils.releaseobject(oRec)
            btn_add.Enabled = True
            btn_generate.Enabled = True
        End Try




    End Sub

    Private Sub txt_qty2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_qty2.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                If Asc(e.KeyChar) <> 46 Then
                    e.Handled = True
                End If

            End If
        End If
    End Sub

  
  
    Private Sub cmb_mesin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_mesin.SelectedIndexChanged
        EmptyValue()

    End Sub

    Private Sub cmb_shift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_shift.SelectedIndexChanged
        EmptyValue()
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

  
End Class