Public Class HANAVersion
    Implements Queries
    Private DatabaseName As String

    Public Sub New(ByVal DatabaseName As String)
        Me.DatabaseName = DatabaseName
    End Sub
    Public Function SeparatorSQL() As String Implements Queries.SeparatorSQL
        Return "SELECT ""DecSep"", ""ThousSep"" FROM ""OADM"""
    End Function

    Public Function CheckUDFSQL(ByVal tableName As String, ByVal fieldName As String) As String Implements Queries.CheckUDFSQL
        Return "SELECT ""TableID"" FROM ""CUFD"" WHERE ""TableID"" = '" + tableName.Trim + "' AND ""AliasID"" = '" + fieldName.Trim + "'"
    End Function

    Public Function CheckUDOSQL(ByVal tableName As String) As String Implements Queries.CheckUDOSQL
        Return "SELECT ""Code"" FROM ""OUDO"" WHERE ""Code"" = '" + tableName.Trim + "'"
    End Function

    Public Function CheckQueryCategorySQL(ByVal categoryName As String) As String Implements Queries.CheckQueryCategorySQL
        Return "SELECT ""CategoryId"" FROM ""OQCN"" WHERE ""CatName"" = '" + categoryName + "'"
    End Function

    Public Function CheckQuerySQL(ByVal categoryName As String, ByVal queryName As String) As String Implements Queries.CheckQuerySQL
        Return "SELECT ""IntrnalKey"" FROM ""OUQR"" WHERE ""QName"" = '" + queryName + "' AND ""QCategory"" IN (" + CheckQueryCategorySQL(categoryName) + ")"
    End Function

    Public Function CheckFMSSQL(ByVal formID As String, ByVal itemID As String) As String Implements Queries.CheckFMSSQL
        Return "SELECT ""IndexID"", ""QueryId"" FROM ""CSHS"" WHERE ""FormID"" = '" + formID + "' AND ""ItemID"" = '" + itemID + "'"
    End Function

    Public Function CheckFunctionExistsSQL(ByVal DBName As String, ByVal FunctionName As String) As String Implements Queries.CheckFunctionExistsSQL
        Return "SELECT TOP 1 1 FROM OBJECTS WHERE SCHEMA_NAME = '" + DBName + "' AND OBJECT_TYPE = 'FUNCTION' AND OBJECT_NAME = '" + FunctionName + "'"
    End Function

    Public Function CheckSPExistsSQL(ByVal DBName As String, ByVal SPName As String) As String Implements Queries.CheckSPExistsSQL
        Return "SELECT TOP 1 1 FROM OBJECTS WHERE SCHEMA_NAME = '" + DBName + "' AND OBJECT_TYPE = 'PROCEDURE' AND OBJECT_NAME = '" + SPName + "'"
    End Function


    Public Function GetSeries(ByVal SeriesName As String, ByVal ObjectCode As String) As String Implements Queries.GetSeries
        Return "SELECT  ""Series""  as ""Series"" FROM NNM1 WHERE ""SeriesName"" = '" + SeriesName + "' || (SELECT TO_VARCHAR(CURRENT_DATE,'YYYY') FROM DUMMY) AND ""ObjectCode"" = '" + ObjectCode + "'"
    End Function


    Public Function GetTransaksi(ByVal TipeJual As String, ByVal Date1 As String) As String Implements Queries.GetTransaksi
        'If TipeJual = "1" Then
        '    Return "SELECT  " _
        '     & "IFNULL(T0.""U_BASE_NO"",0) AS ""SPPB"" " _
        '     & ",T0.""DocNum"" AS ""SPT"" " _
        '     & ",IFNULL(T2.""NumAtCard"",'') AS ""PO"" " _
        '     & ",IFNULL(T1.""U_SO_NO"",0) AS ""SO"" " _
        '     & ",IFNULL(T0.""U_CARDCODE"",'') AS ""CardCode"" " _
        '     & ",IFNULL(T0.""U_CARDNAME"",'') AS ""CardName"" " _
        '     & ",'kapalxxx' AS ""Kapal"" " _
        '     & ",IFNULL(t0.""U_TRUCK"",'') AS ""Kendaraan"" " _
        '     & ",IFNULL(T0.""U_NOPOL"",'') AS ""Nopol"" " _
        '     & ",IFNULL(T0.""U_VENDORNAME"",'') AS ""Transporter"" " _
        '     & ",IFNULL(T0.""U_DRIVER"",'') AS ""Supir"" " _
        '     & ",IFNULL(T0.""U_ITEMCODE"",'') AS ""ItemCode"" " _
        '     & ",IFNULL(T0.""U_ITEMNAME"",'') AS ""ItemName"" " _
        '     & ", T0.""U_QTY""  AS ""QtySPM""  " _
        '     & ",'-' AS ""NoSystem"" " _
        '     & ",'' AS ""NoSpm"" " _
        '     & ",'' AS ""NoSJ"" " _
        '     & ",0 AS ""QtySJ"" FROM" _
        '     & " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_SPT_H"" T0 " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_SPPB_H"" T1 ON T0.""U_BASE_ENTRY"" = T1.""DocEntry"" " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".ORDR T2 ON T1.""U_SO_ENTRY"" = T2.""DocEntry"" " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_JENISKENDARAAN"" T3 ON T0.""U_TRUCK"" = T3.""Code"" " _
        '     & "WHERE T0.""U_BASE_TYPE"" = 'SPPB' AND T0.""U_DATE"" = '" & Date1 & "' AND T0.""U_DOCSTATUS"" = 'O' AND T2.""U_SOL_TIPEKIRIM"" = '1' AND T0.""DocEntry"" not in (SELECT ""U_SOL_SPT"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_WB"") "
        'ElseIf TipeJual = "2" Then
        '    Return "SELECT  " _
        '     & "IFNULL(T0.""U_BASE_NO"",0) AS ""SPPB"" " _
        '     & ",T0.""DocNum"" AS ""SPT"" " _
        '     & ",IFNULL(T2.""NumAtCard"",'') AS ""PO"" " _
        '     & ",IFNULL(T1.""U_SO_NO"",0) AS ""SO"" " _
        '     & ",IFNULL(T0.""U_CARDCODE"",'') AS ""CardCode"" " _
        '     & ",IFNULL(T0.""U_CARDNAME"",'') AS ""CardName"" " _
        '     & ",'kapalxxx' AS ""Kapal"" " _
        '     & ",IFNULL(t0.""U_TRUCK"",'') AS ""Kendaraan"" " _
        '     & ",IFNULL(T0.""U_NOPOL"",'') AS ""Nopol"" " _
        '     & ",IFNULL(T0.""U_VENDORNAME"",'') AS ""Transporter"" " _
        '     & ",IFNULL(T0.""U_DRIVER"",'') AS ""Supir"" " _
        '     & ",IFNULL(T0.""U_ITEMCODE"",'') AS ""ItemCode"" " _
        '     & ",IFNULL(T0.""U_ITEMNAME"",'') AS ""ItemName"" " _
        '     & ", T0.""U_QTY""  AS ""QtySPM""  " _
        '     & ",'-' AS ""NoSystem"" " _
        '     & ",'' AS ""NoSpm"" " _
        '     & ",'' AS ""NoSJ"" " _
        '     & ",0 AS ""QtySJ"" FROM" _
        '     & " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_SPT_H"" T0 " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_SPPB_H"" T1 ON T0.""U_BASE_ENTRY"" = T1.""DocEntry"" " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".ORDR T2 ON T1.""U_SO_ENTRY"" = T2.""DocEntry"" " _
        '     & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_JENISKENDARAAN"" T3 ON T0.""U_TRUCK"" = T3.""Code"" " _
        '     & "WHERE T0.""U_BASE_TYPE"" = 'SPPB' AND T0.""U_DATE"" = '" & Date1 & "' AND T0.""U_DOCSTATUS"" = 'O' AND T2.""U_SOL_TIPEKIRIM"" = '2' AND T0.""DocEntry"" not in (SELECT ""U_SOL_SPT"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_WB"") "
        'ElseIf TipeJual = "3" Then
        '    Return "SELECT  " _
        '  & "T0.""DocNum"" AS ""SPT"" " _
        '    & ",IFNULL(T0.""U_BASE_NO"",0) AS ""PO"" " _
        '    & ",IFNULL(T0.""U_CARDCODE"",'') AS ""CardCode"" " _
        '    & ",IFNULL(T0.""U_CARDNAME"",'') AS ""CardName"" " _
        '    & ",'kapalxxx' AS ""Kapal"" " _
        '    & ",'' AS ""NoSJ"" " _
        '    & ",IFNULL(t0.""U_TRUCK"",'') AS ""Kendaraan"" " _
        '    & ",IFNULL(t2.""Name"",'') AS ""KendaraanName"" " _
        '    & ",IFNULL(T0.""U_NOPOL"",'') AS ""Nopol"" " _
        '    & ",IFNULL(T0.""U_VENDORNAME"",'') AS ""Transporter"" " _
        '    & ",IFNULL(T0.""U_DRIVER"",'') AS ""Supir"" " _
        '    & ",IFNULL(T0.""U_ITEMCODE"",'') AS ""ItemCode"" " _
        '    & ",IFNULL(T0.""U_ITEMNAME"",'') AS ""ItemName"" " _
        '    & ",T0.""U_QTY"" AS ""QtySJ"" " _
        '     & ",'-' AS ""SO"" " _
        '    & ",'-' AS ""NoSystem"" " _
        '    & ",'' AS ""NoSpm"" " _
        '    & ",0 AS ""SPPB"" " _
        '    & ", 0  AS ""QtySPM"" FROM " _
        '    & " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_SPT_H"" T0 " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".POR1 T1 ON T0.""U_BASE_ENTRY"" = T1.""DocEntry"" AND T0.""U_BASE_LINENUM"" = T1.""LineNum"" AND T0.""U_BASE_TYPE"" = 'PO' " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_JENISKENDARAAN"" T2 ON T0.""U_TRUCK"" = T2.""Code"" " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".OPOR T3 ON T3.""DocEntry"" = T1.""DocEntry"" " _
        '    & "WHERE  T0.""U_DOCSTATUS"" = 'O' AND T0.""U_DATE"" = '" & Date1 & "' AND T0.""DocEntry"" not in (SELECT ""U_SOL_SPT"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_WB"")  "
        'ElseIf TipeJual = "4" Then
        '    Return "SELECT  " _
        '    & "cast(T0.""DocNum"" as nvarchar(25)) AS ""PO"" " _
        '    & ",IFNULL(T0.""CardCode"",'') AS ""CardCode"" " _
        '    & ",IFNULL(T2.""CardName"",'') AS ""CardName"" " _
        '    & ",IFNULL(T1.""ItemCode"",'') AS ""ItemCode"" " _
        '    & ",IFNULL(T1.""Dscription"",'') AS ""ItemName"" " _
        '    & ",IFNULL(T1.""Quantity"",0) AS ""QtySJ"" " _
        '    & ",'-' AS ""SPPB"" " _
        '    & ",'-' AS ""SPt"" " _
        '    & ",'-' AS ""SO"" " _
        '    & ",'' AS ""Kapal"" " _
        '    & ",'' AS ""NoSJ"" " _
        '    & ",1 AS ""Kendaraan"" " _
        '    & ",'' AS ""Nopol"" " _
        '    & ",'' AS ""Transporter"" " _
        '    & ",'' AS ""Supir"" " _
        '     & ",'-' AS ""NoSystem"" " _
        '    & ",'' AS ""NoSpm"" " _
        '    & ", 0  AS ""QtySPM"" FROM " _
        '    & " " + oSBOConnection.DatabaseName.ToString + ".OPOR T0 " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ". POR1 T1 ON T0.""DocEntry"" = T1.""DocEntry"" " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".OCRD T2 ON T0.""CardCode"" = T2.""CardCode"" " _
        '    & "INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_ITEMTIMB"" T3 ON T1.""ItemCode"" = T3.""U_SOL_ITEMCODE"" " _
        '    & "WHERE T0.""U_SOL_TIPEPR"" = '1' AND T1.""OpenQty"" > 0 "
        'Else
        '    Return "select 1 from dummy" '
        'End If
        Return " CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_GET_TRANSAKSI('" & TipeJual & "','" & Date1 & "')"
    End Function

    Public Function GetListSPMSPB(ByVal DateFrom As String, ByVal DateTo As String, ByVal Status As String, ByVal flag As String) As String Implements Queries.GetListSPMSPB
        Return " CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_GetListSPMSPB(' " & DateFrom & "','" & DateTo & "','" & Status & "','" & flag & "')"

    End Function


    Public Function GetCurrent() As String Implements Queries.GetCurrent
        Return "SELECT TO_NVARCHAR(CURRENT_DATE,'YYYYMMDD') AS ""CDate"",TO_NVARCHAR(CURRENT_TIME,'HH24MI') as ""Ctime"",current_date as ""CDate2"" FROM DUMMY"
    End Function


    Public Function GetToleransi() As String Implements Queries.GetToleransi
        Return "SELECT TOP 1 ""U_SOL_PERCENTAGE"" as ""Percentage"",""U_SOL_MIN"" AS ""Minimum"" , ""U_SOL_MAX"" as ""Maximum"",""U_SOL_PERCENTAGE_WB"" as ""Percentage2"", ""U_SOL_VALIDASI"" AS ""Validasi"" FROM ""@SOL_TOLERANSI"" "
    End Function

    Public Function GetForm(ByVal Docentry As Integer, ByVal type As String) As String Implements Queries.GetForm
        Return "CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_FORM_WB(" + Docentry.ToString + ",'" & type & "') "
    End Function


    Public Function GetFormTicket(ByVal Docentry As Integer, ByVal type As String) As String Implements Queries.GetFormTicket
        Return "CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_FORM_TICKET(" + Docentry.ToString + ",'" & type & "') "
    End Function


    Public Function GetLastScale(ByVal Timbangan As String, ByVal shift As String, ByVal tanggal As String) As String Implements Queries.GetLastScale
        Return "SELECT IFNULL(MAX(T0.""U_SOL_END""),0) as ""MAXID"" FROM ""@SOL_RAW"" T0 where T0.""U_SOL_MESIN"" = '" + Timbangan + "' and t0.""U_SOL_DATE"" = '" & tanggal & "' and t0.""U_SOL_SHIFT"" = '" & shift & "' "
    End Function


    Public Function GetBagging(ByVal Mesin As String, ByVal shift As String, ByRef date1 As String) As String Implements Queries.GetBagging
        Return "SELECT T0.""DocEntry"" as ""DocEntry"" , T0.""U_SOL_WEIGHT"" AS ""Weight"" , T0.""U_SOL_SUM"" AS ""Bag"" , T0.""U_SOL_FAIL"" as ""Failed"" FROM ""@SOL_BAGGING_H"" T0 where T0.""U_SOL_SHIFT"" = '" & shift & "' and T0.""U_SOL_MESIN"" = '" & Mesin & "' AND T0.""U_SOL_DATE"" = '" & date1 & "'"
    End Function

    Public Function GetRawSugarList(ByVal stat As String, ByVal date1 As String, ByVal date2 As String) As String Implements Queries.GetRawSugarList
        Return "CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_GET_LISTRAWSUGARHEADER('" & stat & "','" & date1 & "','" & date2 & "')"
    End Function


    Public Function GetMesin(ByVal Type As String) As String Implements Queries.GetMesin
        Return "SELECT * FROM " & oSBOConnection.DatabaseName.ToString & ".""@SOL_MESIN"" where ""U_SOL_TYPE"" = '" & Type & "'"
    End Function

    Public Function GetShift() As String Implements Queries.GetShift
        Return "SELECT ""U_SOL_SHIFT"" AS ""Shift"",""U_SOL_START"" AS ""Start"" ,""U_SOL_END"" as ""End"", ""U_SOL_TYPE"" as ""Type"" FROM " & oSBOConnection.DatabaseName.ToString & ".""@SOL_SHIFT"" "
    End Function

    Public Function GetSumRawSugar(ByVal mesin As String, ByVal date1 As String, ByVal shift As String) As String Implements Queries.GetSumRawSugar
        Return "SELECT SUM(""U_SOL_QTY2"") AS ""TOTALWEIGHT"" FROM ""@SOL_RAW"" WHERE ""U_SOL_MESIN"" = '" & mesin & "' AND ""U_SOL_DATE"" = '" & date1 & "'"
    End Function



    Public Function GetRawSugarDetail(ByVal shift As String, ByVal date1 As String, ByVal docentry As String) As String Implements Queries.GetRawSugarDetail
        Dim query As String = ""
        query = "CALL " & oSBOConnection.DatabaseName.ToString & ".SOL_GET_LISTRAWSUGARDETAIL_DIAPI('" & shift & "','" & date1 & "'," & docentry & ")"
        'If docentry = "" Then
        '    query = query + "SELECT"
        '    query = query + " CASE WHEN LENGTH(""U_SOL_TIME"") = 3 THEN '0' || SUBSTRING(""U_SOL_TIME"",1,1) || ':' || SUBSTRING(""U_SOL_TIME"",2,2) ELSE SUBSTRING(""U_SOL_TIME"",1,2) || ':' || SUBSTRING(""U_SOL_TIME"",3,2) END  AS ""Jam"""
        '    query = query + " ,T1.""Name"" as ""Timbangan"""
        '    query = query + " ,ifnull(T0.""U_SOL_COUNTER1"",0) as ""Counter Awal"""
        '    query = query + " ,ifnull(T0.""U_SOL_COUNTER2"",0) as ""Counter Akhir"""
        '    query = query + " ,t0.""U_SOL_QTY1"" AS ""Berat Raw Sugar"""
        '    query = query + " ,t0.""U_SOL_QTY2"" AS ""Berat Raw Sugar Confirm"""
        '    query = query + " ,T2.""Name"" as ""Operator"""
        '    query = query + " FROM"
        '    query = query + " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_RAW"" T0"
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T1 ON T0.""U_SOL_MESIN"" = T1.""Code"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" T2 ON T0.""U_SOL_OPERATOR"" = T2.""Code"""
        '    query = query + " WHERE"
        '    query = query + " T0.""U_SOL_SHIFT"" = '" & shift & "' and T0.""U_SOL_DATE"" = '" & date1 & "' "
        'Else
        '    query = query + "SELECT"
        '    query = query + " CASE WHEN LENGTH(""U_SOL_TIME"") = 3 THEN '0' || SUBSTRING(""U_SOL_TIME"",1,1) || ':' || SUBSTRING(""U_SOL_TIME"",2,2) ELSE SUBSTRING(""U_SOL_TIME"",1,2) || ':' || SUBSTRING(""U_SOL_TIME"",3,2) END  AS ""Jam"""
        '    query = query + " ,T2.""Name"" as ""Timbangan"""
        '    query = query + " ,ifnull(T1.""U_SOL_COUNTER1"",0) as ""Counter Awal"""
        '    query = query + " ,ifnull(T1.""U_SOL_COUNTER2"",0) as ""Counter Akhir"""
        '    query = query + " ,t1.""U_SOL_QTY1"" AS ""Berat Raw Sugar"""
        '    query = query + " ,t1.""U_SOL_QTY2"" AS ""Berat Raw Sugar Confirm"""
        '    query = query + " ,T3.""Name"" as ""Operator"""
        '    query = query + " FROM"
        '    query = query + " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_RAW_D"" T0"
        '    query = query + " INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_RAW"" T1 ON T0.""U_SOL_BASEENTRY"" = T1.""DocEntry"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T2 ON T1.""U_SOL_MESIN"" = T2.""Code"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" T3 ON T1.""U_SOL_OPERATOR"" = T3.""Code"""
        '    query = query + " WHERE"
        '    query = query + " T0.""DocEntry"" = " & docentry
        'End If
        Return query
    End Function


    Public Function GetBaggingConfirmList(ByVal mesin As String, ByVal date1 As String, ByVal shift As String) As String Implements Queries.GetBaggingConfirmList
        If mesin = "All" Then
            mesin = "T0.""U_SOL_MESIN"""
        Else
            mesin = "'" & mesin & "'"
        End If

        Return "SELECT T0.""DocEntry"" AS ""DocEntry"" , T0.""U_SOL_SHIFT"" as ""Shift"" ,T1.""Name"" as ""Mesin Bagging"",RIGHT('00' ||T0.""U_SOL_TIME"",2) || ':' || IFNULL(T0.""U_SOL_MINUTE"",'00')   as ""Waktu"" , T0.""U_SOL_SUM"" AS ""Jumlah Karung"" , T0.""U_SOL_SUM"" AS ""Jumlah Karung Confirmed"",'0' AS ""Jumlah Karung Cleaning"",'' as ""Comments"", IFNULL(T0.""U_SOL_SUMOVER"",0) AS ""Jumlah Karung Over"", IFNULL(T0.""U_SOL_SUMUNDER"",0) AS ""Jumlah Karung Under"", ""U_SOL_MAXCOUNT"" AS ""Counter"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_BAGGING"" T0 LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T1 ON T0.""U_SOL_MESIN"" = T1.""Code"" AND T1.""U_SOL_TYPE"" = '2'  WHERE T0.""U_SOL_STAT"" = 'Open' and T0.""U_SOL_SHIFT"" = '" & shift & "' AND T0.""U_SOL_DATE"" = '" & date1 & "' AND T0.""U_SOL_MESIN"" = " & mesin & " ORDER BY T1.""Name"" asc, t0.""U_SOL_TIME"" ASC"
    End Function


    Public Function GetBaggingConfirmRecap(ByVal mesin As String, ByVal date1 As String, ByVal shift As String) As String Implements Queries.GetBaggingConfirmRecap
        If mesin = "All" Then
            mesin = "T0.""U_SOL_MESIN"""
        Else
            mesin = "'" & mesin & "'"
        End If

        Return "SELECT T1.""Name"",SUM(T0.""U_SOL_SUM"") AS ""Jumlah Karung""  FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_BAGGING"" T0 LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T1 ON T0.""U_SOL_MESIN"" = T1.""Code"" AND T1.""U_SOL_TYPE"" = '2' WHERE  T0.""U_SOL_STAT"" = 'Open' and  T0.""U_SOL_SHIFT"" = '" & shift & "' AND T0.""U_SOL_DATE"" = '" & date1 & "' AND T0.""U_SOL_MESIN"" = " & mesin & " GROUP BY  T1.""Name"" ORDER BY T1.""Name"" ASC"
    End Function

    Public Function GetAllBagging(ByVal stat As String, ByVal date1 As String, ByVal date2 As String) As String Implements Queries.GetAllBagging
        Return "CALL " + oSBOConnection.DatabaseName.ToString + ".SOL_GET_LISTBAGGINGHEADER('" & stat & "','" & date1 & "','" & date2 & "')"
    End Function



    'Public Function GetBaggingStatus(ByVal entry As String) As String Implements Queries.GetBaggingStatus
    '    Return "SELECT 1 FROM ""@SOL_BAGGING_H"" where ""DocEntry"" = " & entry & " and ""U_SOL_STAT"" = 'Confirm'"
    'End Function

    Public Function GetWB(ByVal date1 As String, ByVal date2 As String, ByVal status As String, ByVal type As String, ByVal NoPol As String) As String Implements Queries.GetWB
        Return "CALL " & oSBOConnection.DatabaseName.ToString & ".SOL_GetListWBNoPol('" & date1 & "','" & date2 & "','" & status & "','" & type & "','" & NoPol & "')"
    End Function

    Public Function GetBaggingList(ByVal shift As String, ByVal date1 As String, ByVal docentry As String) As String Implements Queries.GetBaggingList
        Dim query As String = ""
        query = "CALL " & oSBOConnection.DatabaseName.ToString & ".SOL_GET_LISTBAGGINGDETAIL_DIAPI('" & shift & "','" & date1 & "'," & docentry & ")"
        'If docentry = "" Then
        '    query = query + "SELECT"
        '    query = query + "  RIGHT('00' ||T0.""U_SOL_TIME"",2) || ':00' AS ""Jam"""
        '    query = query + " ,T1.""Name"" as ""Mesin Bagging"""
        '    query = query + " ,ifnull(T0.""U_SOL_SUM"",0) as ""Jumlah Karung"""
        '    query = query + " ,ifnull(T0.""U_SOL_SUM2"",0) as ""Jumlah Karung Confirm"""
        '    query = query + " ,ifnull(T0.""U_SOL_WEIGHT"",0) as ""Berat Karung"""
        '    query = query + " ,T2.""Name"" as ""Operator"""
        '    query = query + " ,T0.""U_SOL_MAXCOUNT"" as ""Counter"""
        '    query = query + " ,T0.""Remark"" as ""Comments"""
        '    query = query + " FROM"
        '    query = query + " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_BAGGING"" T0"
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T1 ON T0.""U_SOL_MESIN"" = T1.""Code"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" T2 ON T0.""U_SOL_OPERATOR"" = T2.""Code"""
        '    query = query + " WHERE"
        '    query = query + " T0.""U_SOL_SHIFT"" = '" & shift & "' and T0.""U_SOL_DATE"" = '" & date1 & "' "
        'Else
        '    query = query + "SELECT"
        '    query = query + "  RIGHT('00' ||T1.""U_SOL_TIME"",2) || ':00' AS ""Jam"""
        '    query = query + " ,T2.""Name"" as ""Mesin Bagging"""
        '    query = query + " ,ifnull(T1.""U_SOL_SUM"",0) as ""Jumlah Karung"""
        '    query = query + " ,ifnull(T1.""U_SOL_SUM2"",0) as ""Jumlah Karung Confirm"""
        '    query = query + " ,ifnull(T1.""U_SOL_WEIGHT"",0) as ""Berat Karung"""
        '    query = query + " ,T3.""Name"" as ""Operator"""
        '    query = query + " ,T1.""U_SOL_MAXCOUNT"" as ""Counter"""
        '    query = query + " ,T1.""Remark"" as ""Comments"""
        '    query = query + " FROM"
        '    query = query + " " + oSBOConnection.DatabaseName.ToString + ".""@SOL_BAGGING_D"" T0"
        '    query = query + " INNER JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_BAGGING"" T1 ON T0.""U_SOL_BASEENTRY"" = T1.""DocEntry"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_MESIN"" T2 ON T1.""U_SOL_MESIN"" = T2.""Code"""
        '    query = query + " LEFT JOIN " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" T3 ON T1.""U_SOL_OPERATOR"" = T3.""Code"""
        '    query = query + " WHERE"
        '    query = query + " T0.""DocEntry"" = " & docentry
        'End If
        Return query
    End Function


    Public Function GetTransbySPT(ByVal nospt As String, ByVal status As String, ByVal type As String) As String Implements Queries.GetTransbySPT
        Return "CALL SOL_GET_TRANSAKSIBYSPT('" & nospt & "','" & status & "','" & type & "')"
    End Function

    Public Function GetTransbyNopol(ByVal nopol As String, ByVal status As String, ByVal type As String) As String Implements Queries.GetTransbyNopol
        Return "CALL SOL_GET_TRANSAKSIBYNOPOL('" & nopol & "','" & status & "','" & type & "')"
    End Function

    Public Function GetTransbyNopo(ByVal nopo As String, ByVal status As String, ByVal type As String) As String Implements Queries.GetTransbyNopo
        Return "CALL SOL_GET_TRANSAKSIBYNOPO('" & nopo & "','" & status & "','" & type & "')"
    End Function


    Public Function GetTransDelivery(ByVal DocNum As String) As String Implements Queries.GetTransDelivery
        Return "SELECT 1 FROM ODLN T0 WHERE IFNULL(T0.""U_SOL_SPM"",'') = '" & DocNum & "' AND T0.""CANCELED"" = 'Y' "
    End Function

    Public Function GetTransDelivery2(ByVal DocNum As String) As String Implements Queries.GetTransDelivery2
        Return "SELECT 1 FROM ODLN T0 WHERE IFNULL(T0.""U_SOL_SPM"",'') = '" & DocNum & "' "
    End Function

    Public Function UpdateStatusSPM(ByVal docent As Integer, ByVal status As String) As String Implements Queries.UpdateStatusSPM
        Return "UPDATE ""@SOL_WB"" SET ""U_SOL_STATUS"" = '" & status & "', ""U_SOL_VOID"" = '" & ClassUser.UNAME & "' , ""U_SOL_DATEV"" = CURRENT_DATE WHERE ""DocEntry"" = " & docent
    End Function

End Class
