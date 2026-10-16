Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form_BaggingToSAP
    Dim oCompany As SAPbobsCOM.Company
    Dim oGeneralService As SAPbobsCOM.GeneralService
    Dim oGeneralData As SAPbobsCOM.GeneralData
    Dim oCompService As SAPbobsCOM.CompanyService
    Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
    Dim oRec As SAPbobsCOM.Recordset
    Dim oRec2 As SAPbobsCOM.Recordset
    Dim mesin As String
    Dim tanggal As String
    Dim countererror As Integer = 0
    Dim counterbagging As Integer = 0
    Dim counterweight As Double = 0
    Dim iserror As Integer = 0
    Dim status As String = "O"
    Dim basetype As String = "-"
    Dim baseentry As String = "-"
    Dim oDraft As SAPbobsCOM.Documents
    Dim shift As String = ""
    Dim operator_id As String = ""
    Dim docentry As Integer
    Dim mode As String = ""
    Dim oChild As SAPbobsCOM.GeneralData
    Dim oChildren As SAPbobsCOM.GeneralDataCollection
    Dim dt_trans As New DataTable
    Dim dt_operator As New DataTable
    Dim dt_mesin As New DataTable
    Dim typereceipt As String = oApplicationConfig.TipeReceipt
    Dim itemcode As String = oApplicationConfig.ItemReceiptCode
    Dim itemname As String = oApplicationConfig.ItemReceiptName
    Dim date1 As Date
    Dim dt_shift As DataTable
    Dim dt_bagging As DataTable
    Dim counter As Integer = 0
    Dim localpath As String = oApplicationConfig.PathBag
    Dim isvalid As Boolean
    Dim filename As String
    Dim dfi As Globalization.DateTimeFormatInfo = New Globalization.DateTimeFormatInfo()
    Dim dateinsert As DateTime
    Dim datecheck As String
    Dim mesincheck As String
    Dim timecheck As String
    Dim checkdate As DateTime
    Dim counterlast As Double
    Dim counterover As Double
    Dim counterunder As Double
    Public Sub loadcombo()

        Dim i As Integer
        Dim sql As String
        sql = "SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" WHERE ""U_SOL_TYPE"" = '2' ORDER BY ""Name"" ASC"
        dt_operator = oDBConnection.executeQuery(sql)
        cmb_operator.Items.Clear()
        cmb_operator.DataSource = dt_operator
        cmb_operator.SelectedIndex() = 0
        cmb_operator.DisplayMember = "Name"
        cmb_operator.ValueMember = "Code"

        sql = "SELECT 'All' as ""Code"", 'All' as ""Name"" FROM DUMMY UNION ALL SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" WHERE ""U_SOL_TYPE"" = '2' ORDER BY ""Name"" ASC"
        dt_mesin = oDBConnection.executeQuery(sql)
        cmb_mesin.Items.Clear()
        cmb_mesin.DataSource = dt_mesin
        cmb_mesin.SelectedIndex() = 0
        cmb_mesin.DisplayMember = "Name"
        cmb_mesin.ValueMember = "Code"


    End Sub

    Sub emptyvalue()
        countererror = 0
        counterbagging = 0
        counterweight = 0
        status = "Confirm"
    End Sub

    Sub setvalue()
        mesin = cmb_mesin.SelectedValue()
        tanggal = dtp_trans.ToString("yyyyMMdd")
        shift = cmb_shift.Text
        operator_id = cmb_operator.SelectedValue()
        date_now = Date.Now
        time_now = TimeOfDay.ToString("hhmm")
        itemcode = oApplicationConfig.ItemReceiptCode
        itemname = oApplicationConfig.ItemReceiptName
        typereceipt = oApplicationConfig.TipeReceipt
        date1 = dtp_trans.Value
    End Sub

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



    Private Sub Form_BaggingToSAP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        connect()

        SQLHandler = oSBOConnection.SQLHandler
        oCompany = oSBOConnection.Company

        Dim childtables(0) As String
        childtables(0) = "SOL_BAGGING_D"

        dt_shift = oDBConnection.executeQuery(SQLHandler.GetShift())
        dt_bagging = oDBConnection.executeQuery(SQLHandler.GetMesin("2"))

        loadcombo()

        'Open = start shift
        'Confirm  = confirm shift
        'Draft = Receipt Draft
        'Receipt = Receipt
        Utils.CreateUDT(oSBOConnection.Company, "SOL_BAGGING_H", "Bagging Header", SAPbobsCOM.BoUTBTableType.bott_Document)
        Utils.CreateUDT(oSBOConnection.Company, "SOL_BAGGING_D", "Bagging Detail", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
        Utils.CreateUDT(oSBOConnection.Company, "SOL_BAGGING", "Bagging", SAPbobsCOM.BoUTBTableType.bott_Document)

  
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_DATE", "Date", SAPbobsCOM.BoFieldTypes.db_Date)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_WEIGHT", "Berat Total", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_SUM", "Jumlah Karung Total", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_SUM2", "Jumlah Karung Final", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_CLEANING", "Jumlah Karung Cleaning", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_SUMOVER", "Jumlah Karung Over", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_SUMUNDER", "Jumlah Karung Under", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG1", "Bagging 1", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG2", "Bagging 2", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG3", "Bagging 3", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG4", "Bagging 4", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG5", "Bagging 5", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_BAG6", "Bagging 6", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_STAT", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_SHIFT", "Shift", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_RECEIPT", "Type Receipt", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_ITEMCODE", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_H", "SOL_ITEMNAME", "ItemName", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)


        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING_D", "SOL_BASEENTRY", "Base Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)


        Utils.CreateUDF(oSBOConnection.Company, "OIGN", "SOL_BAGGING", "Nomor Urut Bagging", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
        Utils.CreateUDF(oSBOConnection.Company, "OIGN", "SOL_QTY_BAG", "Jumlah Karung", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity)


        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_WEIGHT", "Berat Total", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_SUM", "Jumlah Karung Total", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_SUM2", "Jumlah Karung Total Confirm", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_SUMOVER", "Jumlah Karung Over", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_SUMUNDER", "Jumlah Karung Under", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_CLEANING", "Jumlah Karung Cleaning", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_STAT", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_DT_IMPORT", "Date Import", SAPbobsCOM.BoFieldTypes.db_Date)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_T_IMPORT", "Time Import", SAPbobsCOM.BoFieldTypes.db_Numeric)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_DT_CONF", "Date Confirm", SAPbobsCOM.BoFieldTypes.db_Date)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_T_CONF", "Time Confirm", SAPbobsCOM.BoFieldTypes.db_Numeric)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_MESIN", "Mesin", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_OPERATOR", "Operator", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_SHIFT", "Shift", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_RECEIPT", "Type Receipt", SAPbobsCOM.BoFieldTypes.db_Alpha, 5)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_ITEMCODE", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_ITEMNAME", "ItemName", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_FILE", "File Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_DATE", "Date", SAPbobsCOM.BoFieldTypes.db_Date)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_TIME", "Time", SAPbobsCOM.BoFieldTypes.db_Numeric)
        Utils.CreateUDF(oSBOConnection.Company, "@SOL_BAGGING", "SOL_MAXCOUNT", "Last Counter", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)

        Utils.CreateUDO(oSBOConnection.Company, "UDO_SOL_BAG_TRX", SAPbobsCOM.BoUDOObjType.boud_Document, "SOL_BAGGING", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO)
        Utils.CreateUDO(oSBOConnection.Company, "UDO_SOL_BAGGING", SAPbobsCOM.BoUDOObjType.boud_Document, "SOL_BAGGING_H", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tYES, SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tNO, childtables)

        oCompService = oCompany.GetCompanyService()
        oGeneralService = oCompService.GetGeneralService("UDO_SOL_BAG_TRX")

        dt_trans.Columns.Add("Weight", GetType(Double))
        dt_trans.Columns.Add("Sum", GetType(Double))
        dt_trans.Columns.Add("Fail", GetType(Double))
        dt_trans.Columns.Add("FileName", GetType(String))


    End Sub

    Private Shared Function IsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

    Private Sub IsFileOpen(ByVal file As FileInfo)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception

            If TypeOf ex Is IOException AndAlso IsFileLocked(ex) Then
                MessageBox.Show(ex.ToString)
                Return
            End If
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        setvalue()
        If Directory.Exists(oApplicationConfig.PathBag + mesin + "\") = False Then
            MessageBox.Show("Directory tidak ditemukan")
            Return

        End If
        Dim fileproses As String() = Directory.GetFiles(oApplicationConfig.PathBag + mesin + "\", "*.txt")
        Dim counter As Integer = 0


        iserror = 1
        mode = "A"


        For i As Integer = 0 To fileproses.Length - 1
            emptyvalue()
            dt_trans.Rows.Clear()

            If fileproses(i).ToString.Contains(tanggal & "-" & mesin) = True Then

                IsFileOpen(New FileInfo(fileproses(i)))
                Dim alltext As String() = File.ReadAllLines(fileproses(i))

                For Each line As String In alltext
                    If line.Contains("ERR DEVICE") = False Then
                        Dim linesplit As String() = line.Split(vbTab)
                        If IsNumeric(linesplit(3)) = True Then

                        End If
                        If linesplit.Length = 7 Then
                            counterbagging = counterbagging + 1
                            counterweight = counterweight + Replace(linesplit(2), SBODecSep, WinDecSep)
                            iserror = 0

                        End If
                    End If
                    If iserror = 1 Then
                        countererror = countererror + 1
                    End If


                Next
            End If
            If counterbagging > 0 Then
                dt_trans.Rows.Add()
                dt_trans.Rows(counter).Item("Weight") = counterweight
                dt_trans.Rows(counter).Item("Sum") = counterbagging
                dt_trans.Rows(counter).Item("Fail") = countererror
                dt_trans.Rows(counter).Item("FileName") = Path.GetFileNameWithoutExtension(fileproses(i).ToString())
                counter = counter + 1
            End If
        Next


        If dt_trans.Rows.Count > 0 Then
            Try
                emptyvalue()
                setvalue()


                Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk konfirmasi timbangan ?", "Konfirmasi Timbangan", 2)
                If result = "Confirm" Then

                    oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRec.DoQuery(SQLHandler.GetCurrent)
                    oCompany.StartTransaction()

                    For i As Integer = 0 To dt_trans.Rows.Count - 1
                        oGeneralData.SetProperty("U_SOL_WEIGHT", dt_trans.Rows(i).Item("Weight"))
                        oGeneralData.SetProperty("U_SOL_SUM", dt_trans.Rows(i).Item("Sum"))
                        oGeneralData.SetProperty("U_SOL_STAT", "Open")
                        oGeneralData.SetProperty("U_SOL_OPERATOR", operator_id)
                        oGeneralData.SetProperty("U_SOL_MESIN", mesin)
                        oGeneralData.SetProperty("U_SOL_SHIFT", shift)
                        oGeneralData.SetProperty("U_SOL_RECEIPT", typereceipt)
                        oGeneralData.SetProperty("U_SOL_ITEMCODE", itemcode)
                        oGeneralData.SetProperty("U_SOL_ITEMNAME", itemname)
                        oGeneralData.SetProperty("U_SOL_FILE", dt_trans.Rows(i).Item("FileName"))
                        oGeneralData.SetProperty("U_SOL_DATE", CType(dtp_trans.Value, Date))
                        oGeneralData.SetProperty("U_SOL_TIME", CType(oRec.Fields.Item("CTime").Value, Integer))
                        oGeneralData.SetProperty("U_SOL_MAXCOUNT", CType(oRec.Fields.Item("CTime").Value, Integer))
                    Next
                    docentry = oGeneralService.Add(oGeneralData).GetProperty("DocEntry").ToString
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
            Catch ex As Exception
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                MessageBox.Show(ex.Message.ToString())
            Finally
                Utils.releaseobject(oGeneralData)
            End Try

        End If


    End Sub
    Private Sub checkandcreatedirectory(ByVal pathfrom As String)

        Dim stringpath() As String
        Dim Path1 As String = ""


        If Directory.Exists(pathfrom) = False Then
            stringpath = pathfrom.Split("\")
            For i As Integer = 0 To stringpath.Length - 1
                Path1 = Path1 & stringpath(i) & "\"
                If System.IO.Directory.Exists(Path1) = False Then
                    System.IO.Directory.CreateDirectory(Path1)
                End If
            Next
        End If

    End Sub

    Private Sub checkandcopyfile(ByVal filefrom As String, ByVal fileto As String)
        If File.Exists(fileto) = False Then
            File.Copy(filefrom, fileto)
        End If
    End Sub

    Private Sub checkandmovefile(ByVal filefrom As String, ByVal fileto As String)
        If File.Exists(fileto) = False Then
            File.Move(filefrom, fileto)
        End If
    End Sub



    Private Sub checkanddeletefile(ByVal filefrom As String)

        If File.Exists(filefrom) = True Then
            File.Delete(filefrom)
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            For i As Integer = 0 To dt_bagging.Rows.Count - 1
                'manage file proses & directory nya

                mesin = dt_bagging.Rows(i).Item("U_SOL_PREFIX").ToString()
                If Directory.Exists(dt_bagging.Rows(i).Item("U_SOL_PATH")) = True Then
                    Dim fileproses As String() = Directory.GetFiles(dt_bagging.Rows(i).Item("U_SOL_PATH"), "*.bmm")

                    '20190101-01-01'
                    For j As Integer = 0 To fileproses.Length - 1
                        isvalid = True
                        If Path.GetFileNameWithoutExtension(fileproses(j)).Length = 14 Then
                            '20190101-01-01'
                            filename = Path.GetFileNameWithoutExtension(fileproses(j))
                            datecheck = Mid(filename, 1, 8)
                            mesincheck = Mid(filename, 10, 2)
                            timecheck = Mid(filename, 13, 2)
                            If DateTime.TryParseExact(datecheck, "yyyyMMdd", dfi, System.Globalization.DateTimeStyles.None, checkdate) = False Or mesin <> mesincheck Or DateTime.TryParseExact(timecheck, "HH", dfi, System.Globalization.DateTimeStyles.None, checkdate) = False Then
                                isvalid = False
                            End If
                        Else
                            isvalid = False
                        End If
                        If (DateTime.Now.ToString("yyyMMdd") = datecheck And DateTime.Now.ToString("HH") = timecheck) = False Then
                            If isvalid = True Then
                                checkandcreatedirectory(dt_bagging.Rows(i).Item("U_SOL_PATH") & " \Copied")
                                checkandcreatedirectory(localpath)
                                checkandcreatedirectory(localpath & "\" & mesin & "\")
                                checkandcreatedirectory(localpath & "\" & mesin & "\Exported")
                                checkandcopyfile(fileproses(j), dt_bagging.Rows(i).Item("U_SOL_PATH") & " \Copied\" & Path.GetFileName(fileproses(j)))
                                checkandmovefile(fileproses(j), localpath & "\" & mesin & "\" & Path.GetFileName(fileproses(j)))
                            ElseIf isvalid = False Then
                                checkandcreatedirectory(dt_bagging.Rows(i).Item("U_SOL_PATH") & " \Failed")
                                checkandmovefile(fileproses(j), dt_bagging.Rows(i).Item("U_SOL_PATH") & " \Failed\" & Path.GetFileName(fileproses(j)))
                            End If
                        End If

                    Next
                End If
            Next

            For i As Integer = 0 To dt_bagging.Rows.Count - 1
                mesin = dt_bagging.Rows(i).Item("U_SOL_PREFIX").ToString()
                If Directory.Exists(localpath & "\" & mesin & "\") = True Then

                    Dim fileproses2 As String() = Directory.GetFiles(localpath & "\" & mesin & "\", "*.bmm")
                    For k As Integer = 0 To fileproses2.Length - 1
                        Try


                            counter = 0
                            counterweight = 0
                            counterbagging = 0
                            countererror = 0
                            counterover = 0
                            filename = Path.GetFileNameWithoutExtension(fileproses2(k))
                            datecheck = Mid(filename, 1, 8)
                            mesincheck = Mid(filename, 10, 2)
                            timecheck = Mid(filename, 13, 2)
                            dateinsert = DateTime.ParseExact(datecheck, "yyyyMMdd", dfi)
                            Dim result() As DataRow = dt_shift.Select("type = 2 ")
                            ' "SELECT ""U_SOL_SHIFT"" AS ""Shift"",""U_SOL_START"" AS ""START"" ,""U_SOL_END"" as ""End"", ""U_SOL_TYPE"" as ""Type"" FROM " & oSBOConnection.DatabaseName.ToString & ".""@SOL_SHIFT"" "
                            For Each row As DataRow In result
                                If row(1) = 24 Then
                                    row(1) = 0
                                End If
                                If timecheck >= row(1) And timecheck < row(2) Then
                                    shift = row(0)
                                End If

                            Next
                            IsFileOpen(New FileInfo(fileproses2(k)))
                            Dim alltext As String() = File.ReadAllLines(fileproses2(k))
                            For Each line As String In alltext
                                If line.Contains("ERR DEVICE") = False Then
                                    Dim linesplit As String() = line.Split(vbTab)
                                    If linesplit.Length = 7 Then
                                        If IsNumeric(linesplit(3)) = True Then
                                            counterbagging = counterbagging + 1
                                            counterweight = counterweight + Replace(linesplit(3), ".", WinDecSep)
                                            iserror = 0
                                            counterlast = linesplit(1)
                                        End If
                                    End If
                                End If
                            Next
                            If counterbagging > 0 Then
                                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                                If oCompany.InTransaction = False Then
                                    oCompany.StartTransaction()
                                End If
                                oGeneralData.SetProperty("U_SOL_WEIGHT", Math.Round(counterweight, 4))
                                oGeneralData.SetProperty("U_SOL_SUM", counterbagging)
                                oGeneralData.SetProperty("U_SOL_STAT", "Open")
                                oGeneralData.SetProperty("U_SOL_MESIN", dt_bagging.Rows(i).Item("Code").ToString())
                                oGeneralData.SetProperty("U_SOL_SHIFT", shift)
                                oGeneralData.SetProperty("U_SOL_RECEIPT", typereceipt)
                                oGeneralData.SetProperty("U_SOL_ITEMCODE", itemcode)
                                oGeneralData.SetProperty("U_SOL_ITEMNAME", itemname)
                                oGeneralData.SetProperty("U_SOL_FILE", Path.GetFileNameWithoutExtension(fileproses2(k).ToString()))
                                oGeneralData.SetProperty("U_SOL_DATE", dateinsert)
                                oGeneralData.SetProperty("U_SOL_TIME", timecheck)
                                oGeneralData.SetProperty("U_SOL_MAXCOUNT", counterlast)

                                oGeneralService.Add(oGeneralData)
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                                checkandcreatedirectory(localpath & "\" & mesin & "\" & " \Exported")
                                checkandmovefile(fileproses2(k), localpath & "\" & mesin & " \Exported\" & Path.GetFileName(fileproses2(k)))
                            End If
                        Catch ex As Exception
                            If oCompany.InTransaction = False Then
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            End If
                            checkandcreatedirectory(localpath & "\" & mesin & "\" & " \Failed")
                            checkandmovefile(fileproses2(k), localpath & "\" & mesin & " \Failed\" & Path.GetFileName(fileproses2(k)))
                        End Try
                    Next
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
        Utils.releaseobject(oCompService)
        MessageBox.Show("Done Process")
        Application.Exit()

    End Sub

    

    Private Sub btn_generate_Click(sender As System.Object, e As System.EventArgs) Handles btn_generate.Click
        If cmb_shift.Text = "" Then
            MessageBox.Show("Shift Harus di pilih")
            Return
        End If
        Dim dt_rekap As New DataTable
        Dim dt_detail As New DataTable
        Try


            dt_detail = oDBConnection.executeQuery(SQLHandler.GetBaggingConfirmList(cmb_mesin.SelectedValue, dtp_trans.Value.ToString("yyyyMMdd"), cmb_shift.Text))
            dgv_detail.DataSource = dt_detail
            If dt_detail.Columns.Count > 1 Then
                For Each column As DataColumn In dt_detail.Columns

                    dgv_detail.Columns(column.ColumnName).ReadOnly = True
                    dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                    If column.ColumnName.ToString() = "Jumlah Karung Confirmed" Or column.ColumnName.ToString() = "Jumlah Karung" Or column.ColumnName.ToString() = "Counter" Or column.ColumnName.ToString() = "Jumlah Karung Cleaning" Or column.ColumnName.ToString() = "Jumlah Karung Over" Or column.ColumnName.ToString() = "Jumlah Karung Under" Then
                        dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        dgv_detail.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"


                    End If

                    If column.ColumnName.ToString() = "Jumlah Karung Confirmed" Or column.ColumnName.ToString() = "Comments" Or column.ColumnName.ToString() = "Jumlah Karung Cleaning" Then
                        dgv_detail.Columns(column.ColumnName).ReadOnly = False
                    End If
                Next
            End If

            hitungdgv()
            '   dgv_rekap.DataSource = dt_rekap
            'If dt_rekap.Columns.Count > 1 Then
            '    For Each column As DataColumn In dt_rekap.Columns

            '        dgv_rekap.Columns(column.ColumnName).ReadOnly = True
            '        dgv_rekap.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            '        If column.ColumnName.ToString() = "DocEntry" Then
            '            dgv_rekap.Columns(column.ColumnName).Visible = False
            '        End If
            '        If column.ColumnName.ToString() = "Jumlah Karung Confirmed" Or column.ColumnName.ToString() = "Jumlah Karung" Then
            '            dgv_rekap.Columns(column.ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '            dgv_rekap.Columns(column.ColumnName).DefaultCellStyle.Format = "N2"
            '        End If
            '    Next
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub


    Private Sub btn_add_Click(sender As System.Object, e As System.EventArgs) Handles btn_add.Click

        Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk konfirmasi bagging ?", "Konfirmasi bagging", 2)
        btn_add.Enabled = False
        btn_generate.Enabled = False
        'dgv_rekap.Enabled = False
        Try
            If result = "Confirm" Then
                Dim stringupdate As String = ""
                oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRec2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRec.DoQuery(SQLHandler.GetCurrent)
                If dgv_detail.Rows.Count > 1 Then

                    If oCompany.InTransaction = False Then
                        oCompany.StartTransaction()
                    End If
                    For Each row As DataGridViewRow In dgv_detail.Rows
                        If row.Index = dgv_detail.Rows.Count - 1 Then
                            Exit For
                        End If
                        oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
                        oGeneralParams.SetProperty("DocEntry", dgv_detail.Rows(row.Index).Cells("DocEntry").Value)
                        oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                        oGeneralData.SetProperty("U_SOL_STAT", "Confirm")
                        oGeneralData.SetProperty("U_SOL_SUM2", CType(dgv_detail.Rows(row.Index).Cells("Jumlah Karung Confirmed").Value, Integer))
                        oGeneralData.SetProperty("U_SOL_DT_CONF", CType(oRec.Fields.Item("CDate2").Value, Date).ToString("yyyyMMdd"))
                        oGeneralData.SetProperty("U_SOL_T_CONF", CType(oRec.Fields.Item("Ctime").Value, Integer))
                        oGeneralData.SetProperty("U_SOL_OPERATOR", CType(cmb_operator.SelectedValue.ToString, String))
                        oGeneralData.SetProperty("U_SOL_CLEANING", CType(dgv_detail.Rows(row.Index).Cells("Jumlah Karung Cleaning").Value, Integer))
                        oGeneralData.SetProperty("Remark", CType(dgv_detail.Rows(row.Index).Cells("Comments").Value, String))
                        oGeneralService.Update(oGeneralData)

                    Next
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                    MessageBox.Show("Konfirmasi Bagging selesai  ")
                    dgv_detail.DataSource = Nothing
                    ' dgv_rekap.DataSource = Nothing
              


            End If
        Utils.releaseobject(oRec)
        Utils.releaseobject(oRec2)
        End If

        Catch ex As Exception
            MessageBox.Show(ex.Message())
            If oCompany.InTransaction = True Then
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
        Finally
            btn_add.Enabled = True
            btn_generate.Enabled = True
            '  dgv_rekap.Enabled = True
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dgv_detail.DataSource = Nothing
        ' dgv_rekap.DataSource = Nothing
    End Sub

    Sub hitungdgv()
        Dim bagging1 As Integer = 0
        Dim bagging2 As Integer = 0
        Dim bagging3 As Integer = 0
        Dim bagging4 As Integer = 0
        Dim bagging5 As Integer = 0
        Dim bagging6 As Integer = 0

        For i As Integer = 0 To dgv_detail.RowCount - 2
            If dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 1" Then
                bagging1 = bagging1 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            ElseIf dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 2" Then
                bagging2 = bagging2 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            ElseIf dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 3" Then
                bagging3 = bagging3 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            ElseIf dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 4" Then
                bagging4 = bagging4 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            ElseIf dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 5" Then
                bagging5 = bagging5 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            ElseIf dgv_detail.Rows(i).Cells.Item("Mesin Bagging").Value.ToString() = "Bagging 6" Then
                bagging6 = bagging6 + dgv_detail.Rows(i).Cells.Item("Jumlah Karung Confirmed").Value
            End If
        Next

        lbl1.Text = bagging1.ToString
        lbl2.Text = bagging2.ToString
        lbl3.Text = bagging3.ToString
        lbl4.Text = bagging4.ToString
        lbl5.Text = bagging5.ToString
        lbl6.Text = bagging6.ToString
        lbla.Text = (bagging1 + bagging2).ToString
        lblb.Text = (bagging3 + bagging4).ToString
        lblc.Text = (bagging5 + bagging6).ToString
        lblttl.Text = (bagging1 + bagging2 + bagging3 + bagging4 + bagging5 + bagging6).ToString
    End Sub


    Private Sub dgv_detail_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_detail.CellEndEdit
        hitungdgv()
    End Sub

    Private Sub dgv_detail_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgv_detail.EditingControlShowing
        Dim iCurCol As Integer = dgv_detail.CurrentCell.ColumnIndex
        Select Case iCurCol
            Case 4, 5, 6
                'only allow numerics
                If TypeOf e.Control Is TextBox Then
                    Dim tb As TextBox = TryCast(e.Control, TextBox)
                    AddHandler tb.KeyPress, AddressOf dgv_detail_KeyPress
                End If
            Case Else

        End Select
    End Sub


    

    Private Sub dgv_detail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgv_detail.KeyPress

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                If Asc(e.KeyChar) <> 46 Then
                    e.Handled = True
                End If

            End If
        End If

    End Sub
End Class