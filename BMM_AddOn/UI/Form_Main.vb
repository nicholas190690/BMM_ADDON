Imports System.IO
Public Class Form_Main
    Dim mdiChildForm As New Form
    Dim oCompany As SAPbobsCOM.Company
    Dim oGeneralService As SAPbobsCOM.GeneralService
    Dim oGeneralData As SAPbobsCOM.GeneralData
    Dim oCompService As SAPbobsCOM.CompanyService
    Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
    Dim oRec As SAPbobsCOM.Recordset



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





    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not oSBOConnection Is Nothing And oSBOConnection.Connected Then
                oSBOConnection.Disconnect()
            End If

            If Not oDBConnection Is Nothing And oDBConnection.Connected Then
                oDBConnection.Disconnect()
            End If
            Application.Exit()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub NumericSeparators()


       
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CurrentMdiParent = Me

        'oConfigXMLParser.read()
        'isInit = True
        'If oConfigXMLParser.isConfigFileExists Then
        '    If oConfigXMLParser.read Then
        '        isInit = False
        '    End If
        'End If
        'ConfigurationToolStripMenuItem.Visible = True
        'WeighBridgeToolStripMenuItem.Visible = True
        If ClassUser.UVBAG = 1 Then
            BaggingToolStripMenuItem.Visible = True
        End If

        If ClassUser.UVRAW = 1 Then
            RawSugarToolStripMenuItem.Visible = True
        End If
        If ClassUser.UVWB = 1 Then
            WeighBridgeToolStripMenuItem.Visible = True
        End If

        If ClassUser.UVREPORT = 1 Then
            ReportToolStripMenuItem.Visible = True
        End If

        If ClassUser.UVCONFIG = 1 Then
            ConfigurationToolStripMenuItem.Visible = True
        End If


        Try
            connect()




            SQLHandler = oSBOConnection.SQLHandler
            oCompany = oSBOConnection.Company

            'Dim arrO As String() = {"Y", "Yes"}
            'Dim arrL As String() = {"N", "No"}
            'Dim liststring As New List(Of String())
            'liststring.Add(arrO)
            'liststring.Add(arrL)

            'Utils.CreateUDT(oSBOConnection.Company, "SOL_TOLERANSI", "Toleransi", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_TOLERANSI", "SOL_PERCENTAGE", "Percentage", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_TOLERANSI", "SOL_PERCENTAGE_WB", "Percentage Weigh Bridge", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_TOLERANSI", "SOL_MIN", "Minimum", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_TOLERANSI", "SOL_MAX", "Maximum", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Measurement)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_TOLERANSI", "SOL_VALIDASI", "Validasi", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, liststring, "N")
            'Utils.CreateUDT(oSBOConnection.Company, "SOL_OPERATOR", "Operator", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_OPERATOR", "SOL_TYPE", "TYPE", SAPbobsCOM.BoFieldTypes.db_Numeric)
            ' Utils.CreateUDF(oSBOConnection.Company, "@SOL_OPERATOR", "SOL_KD_USER", "Kode Login", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50)
            'Utils.CreateUDT(oSBOConnection.Company, "SOL_SHIFT", "Shift", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_SHIFT", "SOL_START", "Start Shift(Format 24H)", SAPbobsCOM.BoFieldTypes.db_Numeric)
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_SHIFT", "SOL_END", "End Shift(Format 24H)", SAPbobsCOM.BoFieldTypes.db_Numeric)
            ''type = 1 shift di timbangan, 2 shift di bagging
            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_SHIFT", "SOL_TYPE", "TYPE", SAPbobsCOM.BoFieldTypes.db_Numeric)

            'Utils.CreateUDF(oSBOConnection.Company, "@SOL_SHIFT", "SOL_SHIFT", "Nomor Shift", SAPbobsCOM.BoFieldTypes.db_Numeric)



            ''create table user dan hak akses
            'Utils.CreateUDT(oSBOConnection.Company, "BMM_USER", "User Login", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_USER", "User Name", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_PASS", "Password", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ACTIVE", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_VCONFIG", "View Config", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ACONFIG", "Add Config", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ADMCONFIG", "Admin Config", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)

            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_VWB", "View WB", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_AWB", "Add WB", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ADMWB", "Admin WB", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)

            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_VRAW", "View RAW", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ARAW", "Add RAW", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ADMRAW", "Admin RAW", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)


            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_VBAG", "View BAG", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ABAG", "Add BAG", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ADMBAG", "Admin BAG", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)


            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_VRPT", "View RPT", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ARPT", "Add RPT", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            'Utils.CreateUDF(oSBOConnection.Company, "@BMM_USER", "BMM_ADMRPT", "Admin RPT", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_WB", "BMM_SHIFT", "Shift", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            Utils.CreateUDF(oSBOConnection.Company, "@SOL_WB", "BMM_POSTING", "Posting Date", SAPbobsCOM.BoFieldTypes.db_Date, 0)

            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec.DoQuery(SQLHandler.GetToleransi())



            If oRec.RecordCount > 0 Then
                toleransi = oRec.Fields.Item(0).Value
                min = oRec.Fields.Item(1).Value
                max = oRec.Fields.Item(2).Value
                toleransiWB = oRec.Fields.Item(3).Value

            Else
                toleransi = 0
                min = 0
                max = 0
                toleransi = 0
            End If
            Utils.releaseobject(oRec)




            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec.DoQuery("SELECT ""DecSep"", ""ThousSep"" FROM ""OADM""")

            WinDecSep = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            SBODecSep = oRec.Fields.Item("DecSep").Value.ToString
            SBOThousSep = oRec.Fields.Item("ThousSep").Value.ToString

            Utils.releaseobject(oRec)

        Catch ex As Exception
            MessageBox.Show("Koneksi Ke SAP tidak berhasil,mohon untuk di konfigurasi terlebih dahulu!")
            ConfigurationForm.Show()
            ConfigurationForm.MdiParent = Me
            'MessageBox.Show("test")
        End Try
    End Sub





  

    Private Sub LoadingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LoadingToolStripMenuItem.Click
        load_stat = "Start"
        Form_Loading.Show()
        Form_Loading.MdiParent = Me
    End Sub

    Private Sub StopLoadingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StopLoadingToolStripMenuItem.Click
        load_stat = "Stop"
        Form_Loading.Show()
        Form_Loading.MdiParent = Me

    End Sub





    Private Sub ListTransaksiRawSugarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ListTransaksiRawSugarToolStripMenuItem.Click
       
        Form_ListRaw.Show()
        Form_ListRaw.MdiParent = Me
    End Sub

    Private Sub ConfirmRawSugarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ConfirmRawSugarToolStripMenuItem.Click
        'If File.Exists(oApplicationConfig.PathMDB.ToString & "BeltScale.mdb") = False Then
        '    MessageBox.Show("File MDB tidak di temukan - " & oApplicationConfig.PathMDB.ToString & "BeltScale.mdb , mohon di check menu configuration !")
        '    Return
        'End If
        Form_RawSugar.Show()
        Form_RawSugar.MdiParent = Me
    End Sub

  

    Private Sub ConfirmBaggingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmBaggingToolStripMenuItem.Click
        Form_BaggingToSAP.Show()
        Form_BaggingToSAP.MdiParent = Me
    End Sub

    Private Sub ListTransaksiBaggingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListTransaksiBaggingToolStripMenuItem.Click

        Form_ListBagging.Show()
        Form_ListBagging.MdiParent = Me
    End Sub

    Private Sub RekapTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RekapTransaksiToolStripMenuItem.Click
        Form_ListWB.Show()
        Form_ListWB.MdiParent = Me
    End Sub

    Private Sub TimbanganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form_WeighBridge.Show()
        Form_WeighBridge.MdiParent = Me
    End Sub

 

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ConfigurationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurationToolStripMenuItem1.Click
        ConfigurationForm.Show()
        ConfigurationForm.MdiParent = Me
    End Sub

    Private Sub ReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportToolStripMenuItem.Click
        Form_Reporting.Show()
        Form_Reporting.MdiParent = Me
    End Sub

    Private Sub TimbanganToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimbanganToolStripMenuItem.Click
        isView = False
        Form_WeighBridge.Show()
        Form_WeighBridge.MdiParent = Me
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        Form_Password.Show()
        Form_Password.MdiParent = Me
    End Sub

    Private Sub UserToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem1.Click
        Form_MasterUser.Show()
        Form_MasterUser.MdiParent = Me
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Close()
        Form_Login.Show()

    End Sub
End Class