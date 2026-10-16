Public Class oWeighBridge
    Private _DocEntry As Int64
    Private _DocNum As Int64
    Private _CreateDate As String
    Private _CreateTime As Integer
    Private _Remarks As String
    Private _UNOSPPB As String
    Private _UNOSPT As String
    Private _UNOPO As String
    Private _UNOSO As String
    Private _USERIES As Integer
    Private _UCARDCODE As String
    Private _UCARDNAME As String
    Private _UKAPAL As String
    Private _USURATJALAN As String
    Private _UREMARKS As String
    Private _UITEMCODE As String
    Private _UITEMNAME As String
    Private _UQTYSPM As Double
    Private _UQTYSJ As Double
    Private _UOP1 As String
    Private _UOP2 As String
    Private _UQTY1 As Double
    Private _UDTWEIGH1 As String
    Private _UTWEIGH1 As Integer
    Private _UQTY2 As Double
    Private _UDTWEIGH2 As String
    Private _UTWEIGH2 As Integer
    Private _USTAT As String
    Private _UTIPE As String
    Private _UTIPETIMBANG1 As String
    Private _UTIPETIMBANG2 As String
    Private _UKENDARAAN As String
    Private _UNOPOL As String
    Private _UTRANSPORTIR As String
    Private _USUPIR As String
    Private _UQTY0 As Double
    Private _UPERCENTAGE As Double
    Private _UBATCH As String
    Private _USUMMARY As Double
    Private _UDTLOAD1 As String
    Private _UTLOAD1 As Integer
    Private _UDTLOAD2 As String
    Private _UTLOAD2 As Integer
    Private _UQTYKAPAL As Double
    Private _UQTYSPMSUM As Double
    Private _UQTYTG As Double
    Private _UNOTG As String
    Private _UDTPOSTDATE As String
    Private _USHIFT As String
    Private _SJDOCNUM As String
    Private _SJYEAR As String
    Private _SJMONTH As String
    Private _SJDATE As String
    Private _NOHP As String
    Private _NOFAX As String
    Private _SJENTRY As Int64

    Private _BASEENTRY As Int64
    Private _BASELINE As Integer
    Private _OPENQTY As Double
    Private _SERIES As Integer
    Private _TIPEPR As Integer
    Private _TIPEDOK As Integer
    Private _POREMARKS As String
    Private _TOLERANSI As Double
    Private _ISCANCEL As string

    Public Property TOLERANSI As Double
        Get
            Return _TOLERANSI
        End Get
        Set(ByVal value As Double)
            _TOLERANSI = value
        End Set
    End Property



    Public Property ISCANCEL As String
        Get
            Return _ISCANCEL
        End Get
        Set(ByVal value As String)
            _ISCANCEL = value
        End Set
    End Property

    Public Property POREMARKS As String
        Get
            Return _POREMARKS
        End Get
        Set(ByVal value As String)
            _POREMARKS = value
        End Set
    End Property

    Public Property TIPEDOK As Integer
        Get
            Return _TIPEDOK
        End Get
        Set(ByVal value As Integer)
            _TIPEDOK = value
        End Set
    End Property


    Public Property TIPEPR As Integer
        Get
            Return _TIPEPR
        End Get
        Set(ByVal value As Integer)
            _TIPEPR = value
        End Set
    End Property



    Public Property SERIES As Integer
        Get
            Return _SERIES
        End Get
        Set(ByVal value As Integer)
            _SERIES = value
        End Set
    End Property






    Public Property OPENQTY As Double
        Get
            Return _OPENQTY
        End Get
        Set(ByVal value As Double)
            _OPENQTY = value
        End Set
    End Property



    Public Property BASELINE As Integer
        Get
            Return _BASELINE
        End Get
        Set(ByVal value As Integer)
            _BASELINE = value
        End Set
    End Property



    Public Property BASEENTRY As Int64
        Get
            Return _BASEENTRY
        End Get
        Set(ByVal value As Int64)
            _BASEENTRY = value
        End Set
    End Property


    Public Property SJENTRY As Int64
        Get
            Return _SJENTRY
        End Get
        Set(ByVal value As Int64)
            _SJENTRY = value
        End Set
    End Property

    Public Property SJDOCNUM As String
        Get
            Return _SJDOCNUM
        End Get
        Set(ByVal value As String)
            _SJDOCNUM = value
        End Set
    End Property


    Public Property SJYEAR As String
        Get
            Return _SJYEAR
        End Get
        Set(ByVal value As String)
            _SJYEAR = value
        End Set
    End Property


    Public Property SJMONTH As String
        Get
            Return _SJMONTH
        End Get
        Set(ByVal value As String)
            _SJMONTH = value
        End Set
    End Property


    Public Property SJDATE As String
        Get
            Return _SJDATE
        End Get
        Set(ByVal value As String)
            _SJDATE = value
        End Set
    End Property


    Public Property NOHP As String
        Get
            Return _NOHP
        End Get
        Set(ByVal value As String)
            _NOHP = value
        End Set
    End Property



    Public Property NOFAX As String
        Get
            Return _NOFAX
        End Get
        Set(ByVal value As String)
            _NOFAX = value
        End Set
    End Property





    Public Property USHIFT As String
        Get
            Return _USHIFT
        End Get
        Set(ByVal value As String)
            _USHIFT = value
        End Set
    End Property


    Public Property UDTPOSTDATE As String
        Get
            Return _UDTPOSTDATE
        End Get
        Set(ByVal value As String)
            _UDTPOSTDATE = value
        End Set
    End Property

    Public Property UNOTG As String
        Get
            Return _UNOTG
        End Get
        Set(ByVal value As String)
            _UNOTG = value
        End Set
    End Property

    Public Property UQTYTG As Double
        Get
            Return _UQTYTG
        End Get
        Set(ByVal value As Double)
            _UQTYTG = value
        End Set
    End Property



    Public Property DocEntry As Int64
        Get
            Return _DocEntry
        End Get
        Set(ByVal value As Int64)
            _DocEntry = value
        End Set
    End Property

    Public Property DocNum As Int64
        Get
            Return _DocNum
        End Get
        Set(ByVal value As Int64)
            _DocNum = value
        End Set
    End Property

    Public Property CreateDate As String
        Get
            Return _CreateDate
        End Get
        Set(ByVal value As String)
            _CreateDate = value
        End Set
    End Property

    Public Property CreateTime As Integer
        Get
            Return _CreateTime
        End Get
        Set(ByVal value As Integer)
            _CreateTime = value
        End Set
    End Property


    Public Property Remarks As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property


    Public Property UNOSPPB As String
        Get
            Return _UNOSPPB
        End Get
        Set(ByVal value As String)
            _UNOSPPB = value
        End Set
    End Property

    Public Property UNOSPT As String
        Get
            Return _UNOSPT
        End Get
        Set(ByVal value As String)
            _UNOSPT = value
        End Set
    End Property

    Public Property UNOPO As String
        Get
            Return _UNOPO
        End Get
        Set(ByVal value As String)
            _UNOPO = value
        End Set
    End Property

    Public Property UNOSO As String
        Get
            Return _UNOSO
        End Get
        Set(ByVal value As String)
            _UNOSO = value
        End Set
    End Property

    Public Property USERIES As Integer
        Get
            Return _USERIES
        End Get
        Set(ByVal value As Integer)
            _USERIES = value
        End Set
    End Property

    Public Property UCARDCODE As String
        Get
            Return _UCARDCODE
        End Get
        Set(ByVal value As String)
            _UCARDCODE = value
        End Set
    End Property

    Public Property UCARDNAME As String
        Get
            Return _UCARDNAME
        End Get
        Set(ByVal value As String)
            _UCARDNAME = value
        End Set
    End Property

    Public Property UKAPAL As String
        Get
            Return _UKAPAL
        End Get
        Set(ByVal value As String)
            _UKAPAL = value
        End Set
    End Property

    Public Property USURATJALAN As String
        Get
            Return _USURATJALAN
        End Get
        Set(ByVal value As String)
            _USURATJALAN = value
        End Set
    End Property


    Public Property UREMARKS As String
        Get
            Return _UREMARKS
        End Get
        Set(ByVal value As String)
            _UREMARKS = value
        End Set
    End Property


    Public Property UITEMCODE As String
        Get
            Return _UITEMCODE
        End Get
        Set(ByVal value As String)
            _UITEMCODE = value
        End Set
    End Property

    Public Property UITEMNAME As String
        Get
            Return _UITEMNAME
        End Get
        Set(ByVal value As String)
            _UITEMNAME = value
        End Set
    End Property

    Public Property UQTYSPM As Double
        Get
            Return _UQTYSPM
        End Get
        Set(ByVal value As Double)
            _UQTYSPM = value
        End Set
    End Property

    Public Property UQTYSJ As Double
        Get
            Return _UQTYSJ
        End Get
        Set(ByVal value As Double)
            _UQTYSJ = value
        End Set
    End Property

    Public Property UOP1 As String
        Get
            Return _UOP1
        End Get
        Set(ByVal value As String)
            _UOP1 = value
        End Set
    End Property


    Public Property UOP2 As String
        Get
            Return _UOP2
        End Get
        Set(ByVal value As String)
            _UOP2 = value
        End Set
    End Property


    Public Property UQTY1 As Double
        Get
            Return _UQTY1
        End Get
        Set(ByVal value As Double)
            _UQTY1 = value
        End Set
    End Property


    Public Property UDTWEIGH1 As String
        Get
            Return _UDTWEIGH1
        End Get
        Set(ByVal value As String)
            _UDTWEIGH1 = value
        End Set
    End Property


    Public Property UTWEIGH1 As Integer
        Get
            Return _UTWEIGH1
        End Get
        Set(ByVal value As Integer)
            _UTWEIGH1 = value
        End Set
    End Property

    Public Property UQTY2 As Double
        Get
            Return _UQTY2
        End Get
        Set(ByVal value As Double)
            _UQTY2 = value
        End Set
    End Property


    Public Property UDTWEIGH2 As String
        Get
            Return _UDTWEIGH2
        End Get
        Set(ByVal value As String)
            _UDTWEIGH2 = value
        End Set
    End Property


    Public Property UTWEIGH2 As Integer
        Get
            Return _UTWEIGH2
        End Get
        Set(ByVal value As Integer)
            _UTWEIGH2 = value
        End Set
    End Property


    Public Property USTAT As String
        Get
            Return _USTAT
        End Get
        Set(ByVal value As String)
            _USTAT = value
        End Set
    End Property

    Public Property UTIPE As String
        Get
            Return _UTIPE
        End Get
        Set(ByVal value As String)
            _UTIPE = value
        End Set
    End Property

    Public Property UTIPETIMBANG1 As String
        Get
            Return _UTIPETIMBANG1
        End Get
        Set(ByVal value As String)
            _UTIPETIMBANG1 = value
        End Set
    End Property

    Public Property UTIPETIMBANG2 As String
        Get
            Return _UTIPETIMBANG2
        End Get
        Set(ByVal value As String)
            _UTIPETIMBANG2 = value
        End Set
    End Property

    Public Property UKENDARAAN As String
        Get
            Return _UKENDARAAN
        End Get
        Set(ByVal value As String)
            _UKENDARAAN = value
        End Set
    End Property

    Public Property UNOPOL As String
        Get
            Return _UNOPOL
        End Get
        Set(ByVal value As String)
            _UNOPOL = value
        End Set
    End Property

    Public Property UTRANSPORTIR As String
        Get
            Return _UTRANSPORTIR
        End Get
        Set(ByVal value As String)
            _UTRANSPORTIR = value
        End Set
    End Property

    Public Property USUPIR As String
        Get
            Return _USUPIR
        End Get
        Set(ByVal value As String)
            _USUPIR = value
        End Set
    End Property

    Public Property UQTY0 As Double
        Get
            Return _UQTY0
        End Get
        Set(ByVal value As Double)
            _UQTY0 = value
        End Set
    End Property

    Public Property UPERCENTAGE As Double
        Get
            Return _UPERCENTAGE
        End Get
        Set(ByVal value As Double)
            _UPERCENTAGE = value
        End Set
    End Property

    Public Property UBATCH As String
        Get
            Return _UBATCH
        End Get
        Set(ByVal value As String)
            _UBATCH = value
        End Set
    End Property

    Public Property USUMMARY As Double
        Get
            Return _USUMMARY
        End Get
        Set(ByVal value As Double)
            _USUMMARY = value
        End Set
    End Property

    Public Property UDTLOAD1 As String
        Get
            Return _UDTLOAD1
        End Get
        Set(ByVal value As String)
            _UDTLOAD1 = value
        End Set
    End Property


    Public Property UTLOAD1 As Integer
        Get
            Return _UTLOAD1
        End Get
        Set(ByVal value As Integer)
            _UTLOAD1 = value
        End Set
    End Property


    Public Property UDTLOAD2 As String
        Get
            Return _UDTLOAD2
        End Get
        Set(ByVal value As String)
            _UDTLOAD2 = value
        End Set
    End Property


    Public Property UTLOAD2 As Integer
        Get
            Return _UTLOAD2
        End Get
        Set(ByVal value As Integer)
            _UTLOAD2 = value
        End Set
    End Property

    Public Property UQTYKAPAL As Double
        Get
            Return _UQTYKAPAL
        End Get
        Set(ByVal value As Double)
            _UQTYKAPAL = value
        End Set
    End Property


    Public Property UQTYSPMSUM As Double
        Get
            Return _UQTYSPMSUM
        End Get
        Set(ByVal value As Double)
            _UQTYSPMSUM = value
        End Set
    End Property

    Public Sub New()
        _DocEntry = 0
        _DocNum = 0
        _CreateDate = "19000101"
        _CreateTime = 0
        _Remarks = ""
        _UCARDCODE = ""
        _UCARDNAME = ""
        _UDTWEIGH1 = "19000101"
        _UDTWEIGH2 = "19000101"
        _UITEMCODE = ""
        _UITEMNAME = ""
        _UKAPAL = ""
        _UKENDARAAN = "1"
        _UNOPO = "-"
        _UNOPOL = ""
        _UNOSO = "-"
        _UNOSPPB = "-"
        _UNOSPT = "-"
        _UOP1 = ""
        _UOP2 = ""
        _UPERCENTAGE = 1
        _UQTY0 = 0
        _UQTY1 = 0
        _UQTY2 = 0
        _UQTYSJ = 0
        _UQTYSPM = 0
        _UREMARKS = ""
        _USERIES = 0
        _USTAT = ""
        _USUPIR = ""
        _USURATJALAN = ""
        _UTIPE = ""
        _UTIPETIMBANG1 = ""
        _UTIPETIMBANG2 = ""
        _UTRANSPORTIR = ""
        _UTWEIGH1 = 0
        _UTWEIGH2 = 0
        _UBATCH = ""
        _USUMMARY = 0
        _UDTLOAD1 = "19000101"
        _UDTLOAD2 = "19000101"
        _UTLOAD1 = 0
        _UTLOAD2 = 0
        _UQTYKAPAL = 0
        _UQTYSPMSUM = 1
        _UQTYTG = 0
        _UNOTG = ""
        _UDTPOSTDATE = "19000101"
        _USHIFT = "1"

        _SJENTRY = 0
        _SJDOCNUM = "-"
        _SJYEAR = "-"
        _SJMONTH = "-"
        _SJDATE = "-"
        _NOHP = "-"
        _NOFAX = "-"

        _ISCANCEL = "N"
        _BASEENTRY = -1


    End Sub

    Public Function Add() As Integer
        ' Add Data
        Dim oCompany As SAPbobsCOM.Company
        Dim oGeneralService As SAPbobsCOM.GeneralService
        Dim oGeneralData As SAPbobsCOM.GeneralData
        Dim oCompService As SAPbobsCOM.CompanyService
        Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
        Dim oRec As SAPbobsCOM.Recordset
        Try

            Dim sErrMsg As String = ""
            Dim series As String = ""
            Dim serie As String = ""
            Dim result As String = ""
            oCompany = oSBOConnection.Company
            oCompService = oCompany.GetCompanyService()
            oGeneralService = oCompService.GetGeneralService("UDO_SOL_WB")
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            If UTIPE = "1" Or UTIPE = "2" Then
                series = "SPM"
            ElseIf UTIPE = "3" Then
                series = "SPB1"
            ElseIf UTIPE = "4" Then
                series = "SPB3"
            ElseIf UTIPE = "5" Then
                series = "SPM2"
            ElseIf UTIPE = "6" Then
                series = "SPB2"
            ElseIf UTIPE = "7" Then
                series = "SPM3"
            End If

            oRec.DoQuery(SQLHandler.GetSeries(series, "UDO_SOL_WB"))

            If oRec.RecordCount < 1 Then
                Throw New System.Exception("Series tidak di temukan")

            End If
            series = oRec.Fields.Item("Series").Value
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec.DoQuery(SQLHandler.GetCurrent)
            If oRec.RecordCount > 0 Then
                oRec.MoveFirst()
            End If
            oCompany.StartTransaction()
            oGeneralData.SetProperty("U_SOL_SPPB", UNOSPPB)
            oGeneralData.SetProperty("U_SOL_SPT", UNOSPT)
            oGeneralData.SetProperty("U_SOL_PO", UNOPO)
            oGeneralData.SetProperty("U_SOL_SO", UNOSO)
            oGeneralData.SetProperty("Series", series)
            oGeneralData.SetProperty("U_SOL_CARDCODE", UCARDCODE)
            oGeneralData.SetProperty("U_SOL_CARDNAME", UCARDNAME)
            oGeneralData.SetProperty("U_SOL_KAPAL", UKAPAL)
            oGeneralData.SetProperty("U_SOL_SURATJALAN", USURATJALAN)
            oGeneralData.SetProperty("U_SOL_REMARKS", UREMARKS)
            oGeneralData.SetProperty("U_SOL_ITEMCODE", UITEMCODE)
            oGeneralData.SetProperty("U_SOL_ITEMNAME", UITEMNAME)
            oGeneralData.SetProperty("U_SOL_QTY_SPM", UQTYSPM)
            oGeneralData.SetProperty("U_SOL_QTY_SJ", UQTYSJ)
            oGeneralData.SetProperty("U_SOL_OPERATORW1", UOP1)
            MessageBox.Show("OK")
            If USTAT = "O" Or USTAT = "F" Then
                oGeneralData.SetProperty("U_SOL_QTY_WEIGH1", UQTY1)
                oGeneralData.SetProperty("U_SOL_DT_WEIGH1", CType(oRec.Fields.Item("CDate2").Value, Date))
                oGeneralData.SetProperty("U_SOL_T_WEIGH1", CType(oRec.Fields.Item("Ctime").Value, Integer))
                oGeneralData.SetProperty("U_SOL_TIPE_TIMBANG", UTIPETIMBANG1)
            ElseIf USTAT = "L" Then
                oGeneralData.SetProperty("U_SOL_DT_LOAD1", CType(oRec.Fields.Item("CDate2").Value, Date))
                oGeneralData.SetProperty("U_SOL_T_LOAD1", CType(oRec.Fields.Item("Ctime").Value, Integer))
            ElseIf USTAT = "F" Then
                oGeneralData.SetProperty("U_SOL_DT_LOAD2", CType(oRec.Fields.Item("CDate2").Value, Date))
                oGeneralData.SetProperty("U_SOL_T_LOAD2", CType(oRec.Fields.Item("Ctime").Value, Integer))
            ElseIf USTAT = "C" Then
                oGeneralData.SetProperty("U_SOL_QTY_WEIGH2", UQTY2)
                oGeneralData.SetProperty("U_SOL_DT_WEIGH2", CType(oRec.Fields.Item("CDate2").Value, Date))
                oGeneralData.SetProperty("U_SOL_T_WEIGH2", CType(oRec.Fields.Item("Ctime").Value, Integer))
                oGeneralData.SetProperty("U_SOL_TIPE_TIMBANG", UTIPETIMBANG2)
            End If

            oGeneralData.SetProperty("U_SOL_STATUS", USTAT)
            oGeneralData.SetProperty("U_SOL_TIPE", UTIPE)

            oGeneralData.SetProperty("U_SOL_KENDARAAN", UKENDARAAN)
            oGeneralData.SetProperty("U_SOL_NOPOL", UNOPOL)
            oGeneralData.SetProperty("U_SOL_TRANSPORTIR", UTRANSPORTIR)
            oGeneralData.SetProperty("U_SOL_SUPIR", USUPIR)
            oGeneralData.SetProperty("U_SOL_QTY_KAPAL", UQTYKAPAL)

            oGeneralData.SetProperty("U_BMM_TG", UNOTG)
            oGeneralData.SetProperty("U_BMM_QTY_TG", UQTYTG)

            oGeneralData.SetProperty("U_BMM_POSTING", Date.ParseExact(UDTPOSTDATE, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo))
            oGeneralData.SetProperty("U_BMM_SHIFT", USHIFT)

            If USTAT = "O" Or USTAT = "F" Then
                DocEntry = oGeneralService.Add(oGeneralData).GetProperty("DocEntry").ToString

            End If

            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)




        Catch ex As Exception
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            MessageBox.Show(ex.Message.ToString)
            Return -1

        Finally
            Utils.releaseobject(oGeneralData)
            Utils.releaseobject(oGeneralParams)
            Utils.releaseobject(oRec)

        End Try
        Return 1

    End Function



    Public Function UpdateOut() As Integer
        Dim oCompany As SAPbobsCOM.Company
        Dim oGeneralService As SAPbobsCOM.GeneralService
        Dim oGeneralData As SAPbobsCOM.GeneralData
        Dim oCompService As SAPbobsCOM.CompanyService
        Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
        Dim oRec As SAPbobsCOM.Recordset
        Dim vDocument As SAPbobsCOM.Documents
        Try
            oCompany = oSBOConnection.Company
            
            oCompService = oCompany.GetCompanyService()
            oGeneralService = oCompService.GetGeneralService("UDO_SOL_WB")

            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            oGeneralParams.SetProperty("DocEntry", CInt(DocEntry))
            oGeneralData = oGeneralService.GetByParams(oGeneralParams)
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oRec.DoQuery(SQLHandler.GetCurrent)
            If oRec.RecordCount > 0 Then
                oRec.MoveFirst()
            End If
            oCompany.StartTransaction()
            If oRec.RecordCount > 0 Then
                oGeneralData.SetProperty("U_SOL_TIPE_TIMBANG2", UTIPETIMBANG2)
                oGeneralData.SetProperty("U_SOL_QTY_WEIGH2", UQTY2)
                oGeneralData.SetProperty("U_SOL_PERCENTAGE", UPERCENTAGE)
                oGeneralData.SetProperty("U_SOL_DT_WEIGH2", CType(oRec.Fields.Item("CDate2").Value, Date))
                oGeneralData.SetProperty("U_SOL_T_WEIGH2", CType(oRec.Fields.Item("Ctime").Value, Integer))
                oGeneralData.SetProperty("U_SOL_STATUS", USTAT)
                oGeneralData.SetProperty("U_SOL_OPERATORW2", UOP2)
                oGeneralData.SetProperty("U_SOL_SURATJALAN", USURATJALAN)
                If (ISCANCEL = "Y") Then
                    oGeneralData.SetProperty("U_SOL_REMARKS", UREMARKS)
                End If
                oGeneralData.SetProperty("Remark", UREMARKS)
                oGeneralData.SetProperty("U_SOL_QTY_WEIGHO", UQTY0)
                oGeneralService.Update(oGeneralData)
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)

            Else
                Throw New Exception("Data not found")
            End If

            'If UITEMCODE = "BB101.001.001" Then
            '    oCompany = oSBOConnection.Company
            '    If oCompany.InTransaction = False Then
            '        oCompany.StartTransaction()
            '    End If
            '    vDocument = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes)
            '    vDocument.Series = SERIES
            '    vDocument.CardCode = UCARDCODE
            '    vDocument.CardName = UCARDNAME
            '    vDocument.UserFields.Fields.Item("U_SOL_TIPEPR").Value = TIPEPR.ToString
            '    vDocument.UserFields.Fields.Item("U_SOL_TIPEDOK").Value = TIPEDOK.ToString
            '    vDocument.UserFields.Fields.Item("U_SOL_SHIFT").Value = USHIFT.ToString
            '    vDocument.UserFields.Fields.Item("U_SOL_REMARKS").Value = UREMARKS
            '    vDocument.DocDate = Date.ParseExact(UDTWEIGH2, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '    vDocument.DocDueDate = Date.ParseExact(UDTWEIGH2, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '    vDocument.NumAtCard = USURATJALAN

            '    vDocument.UserFields.Fields.Item("U_SOL_SPM").Value = DocNum.ToString
            '    vDocument.UserFields.Fields.Item("U_SOL_NOPOL").Value = UNOPOL
            '    vDocument.UserFields.Fields.Item("U_SOL_DRIVER").Value = USUPIR

            '    vDocument.Lines.BaseType = 22
            '    vDocument.Lines.BaseLine = BASELINE
            '    vDocument.Lines.BaseEntry = BASEENTRY
            '    vDocument.Lines.Quantity = UQTY0
            '    vDocument.Lines.Add()
            '    Dim iErr As Integer = vDocument.Add()

            '    If iErr <> 0 Then
            '        Throw New Exception("Failed Create BP -  " & oCompany.GetLastErrorDescription())
            '    End If

            '    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            'End If


            Return 1
        Catch ex As Exception
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            MessageBox.Show(ex.Message.ToString)
            Return -1
        Finally
            Utils.releaseobject(oGeneralData)
            Utils.releaseobject(oGeneralParams)
            Utils.releaseobject(oRec)
        End Try

    End Function
    Public Function checkclearance()
        Dim dtresult As New DataTable
        Try
            dtresult = oDBConnection.executeQuery("select docentry,docnum,quantity from  " & If(oDBConnection.DatabaseName = "BMM_LIVE", "BMM_ADDON_MASTER", "BMM_ADDON_HUAWEI") & ".BMM_CLEARANCE where SPTNUM = " & _UNOSPT & " and  to_nvarchar(createdate) = '" & _UDTWEIGH2 & "' order by docentry desc")
            Return dtresult

        Catch ex As Exception
            Return dtresult

        End Try

    End Function

    Public Sub checkNopolOutstanding()
        Dim oDT As New DataTable
        Dim querystring As String = ""
        Dim formulatanggal As String = ""
        formulatanggal = _CreateDate & Right(("0000" & _CreateTime.ToString), 4)
        If (_UTIPE = "1" Or _UTIPE = "2") And _UQTY1 > 0 Then
            'querystring = "select SUM(""U_SOL_QTY_SPM"") as ""QTYSPM"" , SUM((case when ""U_SOL_QTY_WEIGH2"" = 0 then ""U_SOL_QTY_WEIGH1"" else ""U_SOL_QTY_WEIGH2"" end)  - ""U_SOL_QTY_WEIGH1"") AS ""NETWEIGH"" from " & oSBOConnection.DatabaseName & ".""@SOL_WB"" WHERE  ""U_SOL_NOPOL"" = '" & _UNOPOL & "' AND TO_TIMESTAMP(to_char(""CreateDate"",'yyyymmdd') || RIGHT('0000' || TO_NVARCHAR(""CreateTime""),4),'yyyymmddHH24MI') between add_seconds(to_timestamp('" & formulatanggal & "','yyyymmddHH24MI'),-1500) and add_seconds(to_timestamp('" & formulatanggal & "','yyyymmddHH24MI'),1500) and ""U_SOL_STATUS"" <> 'V' and ""U_SOL_TIPE"" IN ('1','2') AND SUBSTRING(""U_SOL_ITEMCODE"" ,1,2) = 'FG' "
            querystring = "CALL " & oSBOConnection.DatabaseName & ".BMM_GETVALUE ('" & _UNOPOL & "','" & formulatanggal & "') "
            '""DocEntry"" <> " & _DocEntry & " AND
            oDT = oDBConnection.executeQuery(querystring)
            If oDT.Columns.Contains("QTYSPM") = True And oDT.Columns.Contains("NETWEIGH") = True Then
                If Not IsDBNull(oDT.Rows(0).Item("QTYSPM")) And Not IsDBNull(oDT.Rows(0).Item("NETWEIGH")) Then
                    _UPERCENTAGE = _UQTYSPM / oDT.Rows(0).Item("QTYSPM") * 100
                    _UQTYSPMSUM = oDT.Rows(0).Item("QTYSPM")
                Else
                    _UPERCENTAGE = 100
                    _UQTYSPMSUM = _UQTYSPM
                End If
                _UPERCENTAGE = _UPERCENTAGE / 100
            End If
        End If
    End Sub


    Public Function saveClearanceForm(ByVal toleransi As Double)
        Dim Result As String
        Dim docNum As Int64
        Dim docent As Integer
        Dim success As Integer
        Dim dtresult As New DataTable
        Dim datetimenow As String = Date.Now().ToString("yyyyMMddHHmmss")
        Try
            oDBConnection.BeginTransaction()
            Result = oDBConnection.executeScalar("SELECT " & If(oDBConnection.DatabaseName = "BMM_LIVE", "BMM_ADDON_MASTER", "BMM_ADDON_HUAWEI") & ".BMM_CLEARANCE_S.NEXTVAL  from dummy")
            If Result Is Nothing Or Result = "" Then
                Throw New Exception("DocEntry Not Found")
            End If

            docent = Convert.ToInt32(Result)


            Result = oDBConnection.executeScalar("SELECT TOP 1 DocNum FROM " & If(oDBConnection.DatabaseName = "BMM_LIVE", "BMM_ADDON_MASTER", "BMM_ADDON_HUAWEI") & ".BMM_CLEARANCE WHERE TO_NVARCHAR(CreateDate, 'YYYY') = '" & Now.Year.ToString() & "'  ORDER BY DocEntry DESC")
            If Result Is Nothing Or Result = "" Then
                Result = Right(Now.Year.ToString, 2) & "0000"
            End If

            docNum = Convert.ToInt64(Result) + 1

            dtresult = oDBConnection.executeQuery("call " & If(oDBConnection.DatabaseName = "BMM_LIVE", "BMM_ADDON_MASTER", "BMM_ADDON_HUAWEI") & ".BMM_checkallnopol(" & _DocEntry & ",'" & _UNOPOL & "')")

            For a As Integer = 0 To dtresult.Rows.Count - 1
                success = oDBConnection.executeNonQuery("insert into " & If(oDBConnection.DatabaseName = "BMM_LIVE", "BMM_ADDON_MASTER", "BMM_ADDON_HUAWEI") & ".BMM_CLEARANCE(docentry,docnum,sptnum,spmentry,quantity,weigh2,createdate) VALUES (" & docent & "," & docNum & "," & dtresult.Rows(a).Item("U_SOL_SPT") & "," & dtresult.Rows(a).Item("DocEntry") & ", " & toleransi & ", " & _UQTY2 & ",'" & datetimenow & "')")
            Next


            If success > 0 Then
                oDBConnection.Commit()
                Return docent
            End If
        Catch ex As Exception
            oDBConnection.Rollback()
            Throw New Exception("Error : " & ex.Message.ToString)
            Return -1
        End Try
    End Function

    Public Function getByKey(ByVal querystring As String) As Integer
        Dim retval As Integer = -1
        'Dim sQuery As String = Queries.StockTransferList(DocNum)
        Dim temp As Date
        Dim oDT As New DataTable

       
        Try
            oDT = oDBConnection.executeQuery(querystring)
            If oDT.Rows.Count > 0 Then
                If oDT.Columns.Contains("DocEntry") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("DocEntry")) Then
                        _DocEntry = oDT.Rows(0).Item("DocEntry")
                    End If
                End If

                If oDT.Columns.Contains("DocNum") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("DocNum")) Then
                        _DocNum = oDT.Rows(0).Item("DocNum")
                    End If
                End If


                If oDT.Columns.Contains("CreateDate") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("CreateDate")) Then
                        temp = oDT.Rows(0).Item("CreateDate")
                        _CreateDate = temp.ToString("yyyyMMdd")
                    End If
                End If


                If oDT.Columns.Contains("CreateTime") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("CreateTime")) Then
                        _CreateTime = oDT.Rows(0).Item("CreateTime")
                    End If
                End If


                If oDT.Columns.Contains("Remark") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("Remark")) Then
                        _Remarks = oDT.Rows(0).Item("Remark")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_SPPB") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_SPPB")) Then
                        _UNOSPPB = oDT.Rows(0).Item("U_SOL_SPPB")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_SPT") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_SPT")) Then
                        _UNOSPT = oDT.Rows(0).Item("U_SOL_SPT")
                    End If
                End If





                If oDT.Columns.Contains("U_SOL_SO") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_SO")) Then
                        _UNOSO = oDT.Rows(0).Item("U_SOL_SO")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_PO") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_PO")) Then
                        _UNOPO = oDT.Rows(0).Item("U_SOL_PO")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_CARDCODE") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_CARDCODE")) Then
                        _UCARDCODE = oDT.Rows(0).Item("U_SOL_CARDCODE")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_CARDNAME") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_CARDNAME")) Then
                        _UCARDNAME = oDT.Rows(0).Item("U_SOL_CARDNAME")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_SURATJALAN") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_SURATJALAN")) Then
                        _USURATJALAN = oDT.Rows(0).Item("U_SOL_SURATJALAN")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_ITEMCODE") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_ITEMCODE")) Then
                        _UITEMCODE = oDT.Rows(0).Item("U_SOL_ITEMCODE")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_ITEMNAME") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_ITEMNAME")) Then
                        _UITEMNAME = oDT.Rows(0).Item("U_SOL_ITEMNAME")
                    End If
                End If

                If oDT.Columns.Contains("U_SOL_QTY_SPM") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_SPM")) Then
                        _UQTYSPM = oDT.Rows(0).Item("U_SOL_QTY_SPM")
                    End If
                End If

                If oDT.Columns.Contains("U_SOL_QTY_SJ") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_SJ")) Then
                        _UQTYSJ = oDT.Rows(0).Item("U_SOL_QTY_SJ")
                    End If
                End If






                If oDT.Columns.Contains("U_SOL_QTY_WEIGH1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_WEIGH1")) Then
                        _UQTY1 = oDT.Rows(0).Item("U_SOL_QTY_WEIGH1")
                    End If
                End If




                If oDT.Columns.Contains("U_SOL_QTY_WEIGH2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_WEIGH2")) Then
                        _UQTY2 = oDT.Rows(0).Item("U_SOL_QTY_WEIGH2")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_DT_WEIGH1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_DT_WEIGH1")) Then
                        temp = oDT.Rows(0).Item("U_SOL_DT_WEIGH1")
                        _UDTWEIGH1 = temp.ToString("yyyyMMdd")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_T_WEIGH1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_T_WEIGH1")) Then
                        _UTWEIGH1 = oDT.Rows(0).Item("U_SOL_T_WEIGH1")
                    End If
                End If

                If oDT.Columns.Contains("U_SOL_DT_WEIGH2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_DT_WEIGH2")) Then
                        temp = oDT.Rows(0).Item("U_SOL_DT_WEIGH2")
                        _UDTWEIGH2 = temp.ToString("yyyyMMdd")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_T_WEIGH2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_T_WEIGH2")) Then
                        _UTWEIGH2 = oDT.Rows(0).Item("U_SOL_T_WEIGH2")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_DT_LOAD1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_DT_LOAD1")) Then
                        temp = oDT.Rows(0).Item("U_SOL_DT_LOAD1")
                        _UDTLOAD1 = temp.ToString("yyyyMMdd")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_T_LOAD1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_T_LOAD1")) Then
                        _UTLOAD1 = oDT.Rows(0).Item("U_SOL_T_LOAD1")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_DT_LOAD2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_DT_LOAD2")) Then
                        temp = oDT.Rows(0).Item("U_SOL_DT_LOAD2")
                        _UDTLOAD2 = temp.ToString("yyyyMMdd")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_T_LOAD2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_T_LOAD2")) Then
                        _UTLOAD2 = oDT.Rows(0).Item("U_SOL_T_LOAD2")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_STATUS") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_STATUS")) Then
                        _USTAT = oDT.Rows(0).Item("U_SOL_STATUS")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_TIPE") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_TIPE")) Then
                        _UTIPE = oDT.Rows(0).Item("U_SOL_TIPE")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_TIPE_TIMBANG") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_TIPE_TIMBANG")) Then
                        _UTIPETIMBANG1 = oDT.Rows(0).Item("U_SOL_TIPE_TIMBANG")
                    End If
                End If

                If oDT.Columns.Contains("U_SOL_TIPE_TIMBANG2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_TIPE_TIMBANG2")) Then
                        _UTIPETIMBANG2 = oDT.Rows(0).Item("U_SOL_TIPE_TIMBANG2")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_KENDARAAN") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_KENDARAAN")) Then
                        _UKENDARAAN = oDT.Rows(0).Item("U_SOL_KENDARAAN")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_NOPOL") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_NOPOL")) Then
                        _UNOPOL = oDT.Rows(0).Item("U_SOL_NOPOL")
                    End If
                End If

                If oDT.Columns.Contains("U_SOL_TRANSPORTIR") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_TRANSPORTIR")) Then
                        _UTRANSPORTIR = oDT.Rows(0).Item("U_SOL_TRANSPORTIR")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_SUPIR") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_SUPIR")) Then
                        _USUPIR = oDT.Rows(0).Item("U_SOL_SUPIR")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_OPERATORW1") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_OPERATORW1")) Then
                        _UOP1 = oDT.Rows(0).Item("U_SOL_OPERATORW1")
                    End If
                End If



                If oDT.Columns.Contains("U_SOL_OPERATORW2") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_OPERATORW2")) Then
                        _UOP2 = oDT.Rows(0).Item("U_SOL_OPERATORW2")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_QTY_WEIGHO") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_WEIGHO")) Then
                        _UQTY0 = oDT.Rows(0).Item("U_SOL_QTY_WEIGHO")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_PERCENTAGE") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_PERCENTAGE")) Then
                        _UPERCENTAGE = oDT.Rows(0).Item("U_SOL_PERCENTAGE")
                    End If
                End If





                If oDT.Columns.Contains("U_SOL_KAPAL") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_KAPAL")) Then
                        _UKAPAL = oDT.Rows(0).Item("U_SOL_KAPAL")
                    End If
                End If





                If oDT.Columns.Contains("U_SOL_BATCH") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_BATCH")) Then
                        _UBATCH = oDT.Rows(0).Item("U_SOL_BATCH")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_QTY_KAPAL") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_QTY_KAPAL")) Then
                        _UQTYKAPAL = oDT.Rows(0).Item("U_SOL_QTY_KAPAL")
                    End If
                End If


                If oDT.Columns.Contains("U_SOL_REMARKS") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_SOL_REMARKS")) Then
                        _UREMARKS = oDT.Rows(0).Item("U_SOL_REMARKS")
                    End If
                End If

                If oDT.Columns.Contains("U_BMM_TG") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_BMM_TG")) Then
                        _UNOTG = oDT.Rows(0).Item("U_BMM_TG")
                    End If
                End If

                If oDT.Columns.Contains("U_BMM_QTY_TG") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("U_BMM_QTY_TG")) Then
                        _UQTYTG = oDT.Rows(0).Item("U_BMM_QTY_TG")
                    End If
                End If

                If oDT.Columns.Contains("SJNO") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("SJNO")) Then
                        _SJDOCNUM = oDT.Rows(0).Item("SJNO")
                    End If
                End If

                If oDT.Columns.Contains("YEARSJ") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("YEARSJ")) Then
                        _SJYEAR = oDT.Rows(0).Item("YEARSJ")
                    End If
                End If

                If oDT.Columns.Contains("MONTHSJ") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("MONTHSJ")) Then
                        _SJMONTH = oDT.Rows(0).Item("MONTHSJ")
                    End If
                End If

                If oDT.Columns.Contains("DATESJ") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("DATESJ")) Then
                        _SJDATE = oDT.Rows(0).Item("DATESJ")
                    End If
                End If

                If oDT.Columns.Contains("NOHP") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("NOHP")) Then
                        _NOHP = oDT.Rows(0).Item("NOHP")
                    End If
                End If

                If oDT.Columns.Contains("NOFAX") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("NOFAX")) Then
                        _NOFAX = oDT.Rows(0).Item("NOFAX")
                    End If
                End If

                If oDT.Columns.Contains("SJENTRY") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("SJENTRY")) Then
                        _SJENTRY = oDT.Rows(0).Item("SJENTRY")
                    End If
                End If

                If oDT.Columns.Contains("ISCANCEL") = True Then
                    If Not IsDBNull(oDT.Rows(0).Item("ISCANCEL")) Then
                        _ISCANCEL = oDT.Rows(0).Item("ISCANCEL")
                    End If
                End If


                _BASEENTRY = -1

                If ClassWeighbridge.UQTY1 > 0 And (ClassWeighbridge.UTIPE = 1 Or ClassWeighbridge.UTIPE = 2) Then
                    checkNopolOutstanding()
                ElseIf ClassWeighbridge.UQTY1 = 0 And (ClassWeighbridge.UTIPE = 1 Or ClassWeighbridge.UTIPE = 2) Then
                    _UQTYSPMSUM = _UQTYSPM
                End If
                retval = 0
            Else
                retval = -1
                'MessageBox.Show("Data tida")
            End If
            Return retval
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            Return -1
        End Try
    End Function


End Class