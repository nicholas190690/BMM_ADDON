Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Windows.Forms
Imports System.Management
Imports SAPbobsCOM
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Threading

Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Imports System.Reflection


Imports System.Web
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Net.Http.HttpClient

Imports WinSCP


Public Class Form_WeighBridge


#Region "Declare Variable"

    Dim oCompany As SAPbobsCOM.Company
    Dim oGeneralService As SAPbobsCOM.GeneralService
    Dim oGeneralData As SAPbobsCOM.GeneralData
    Dim oCompService As SAPbobsCOM.CompanyService
    Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
    Dim oRec As SAPbobsCOM.Recordset
    Dim dt_operator As New DataTable
    Dim dt_kendaraan As New DataTable
    Dim tipetimbang As String
    Dim berattimbang As Double
    Dim InputData As String

    Dim dt_setting As New DataTable


    Dim insentifmesin As Double
    Dim insentifjarak As Double
    Dim insentifberat As Double
    Dim beratrunning As Double
    Dim berat As Double
    Dim barcode As String
    Dim typebarcode As String
    Dim querystring As String
    Dim crtableLogoninfos As New TableLogOnInfos
    Dim crtableLogoninfo As New TableLogOnInfo
    Dim crConnectionInfo As New ConnectionInfo


    Dim CrTables As Tables
    Dim CrTable As CrystalDecisions.CrystalReports.Engine.Table


    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
#End Region


#Region "Function"

    Private Sub FlushMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)

                Dim myprocesses As Process() = Process.GetProcessesByName("ApplicationName")
                Dim myprocess As Process
                For Each myprocess In myprocesses
                    SetProcessWorkingSetSize(myprocess.Handle, -1, -1)

                Next myprocess
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function CheckPrinter(ByVal printerName As String) As Boolean
        Try
            Dim printDocument As PrintDocument = New PrintDocument
            printDocument.PrinterSettings.PrinterName = printerName
            Return printDocument.PrinterSettings.IsValid
        Catch ex As System.Exception
            Return False
        End Try
    End Function

    Private Sub saveHistoryTransaksi()

        Dim FolderToHide As String = "C:\LogTimbangan\" & Now.ToString("yyyyMM") & "\" & Now.ToString("yyyyMMdd")


        If System.IO.Directory.Exists(FolderToHide) = False Then
            System.IO.Directory.CreateDirectory(FolderToHide)
            Dim ToHideDir As New System.IO.DirectoryInfo(FolderToHide)
            'Then Change the Attribute To Hidden Which will Hide the Folder
            ToHideDir.Attributes = IO.FileAttributes.Hidden
        End If

        'IF(expression,<true return>,<false return>)
        Using sw As New System.IO.StreamWriter(FolderToHide & "\Log_" & Now.ToString("yyyyMMdd") & "-" & Now.ToString("HH") & ".log", True)
            'login;nomorspt;nomorpo;nomorspm;timbangan;supir;nopol
            sw.WriteLine(ClassUser.UNAME & ";" & ClassWeighbridge.UNOSPT.ToString & ";" & ClassWeighbridge.UNOPO.ToString & ";" & ClassWeighbridge.DocNum.ToString & ";" & If(tipetimbang = "In", ClassWeighbridge.UQTY1.ToString, ClassWeighbridge.UQTY2.ToString) & ";" & ClassWeighbridge.USUPIR.ToString & ";" & ClassWeighbridge.UNOPOL.ToString)
            sw.Flush()
            sw.Close()
        End Using

    End Sub

    Private Sub loadcombo()
        Dim i As Integer
        Dim sql As String
        sql = "SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_JENISKENDARAAN"" ORDER BY ""Name"" ASC"
        dt_kendaraan = oDBConnection.executeQuery(sql)
        cmb_kendaraan.Items.Clear()
        cmb_kendaraan.DataSource = dt_kendaraan
        cmb_kendaraan.SelectedIndex() = 0
        cmb_kendaraan.DisplayMember = "Name"
        cmb_kendaraan.ValueMember = "Code"



        'sql = "SELECT  ""Code"" as ""Code"", ""Name"" as ""Name"" FROM " + oSBOConnection.DatabaseName.ToString + ".""@SOL_OPERATOR"" WHERE ""U_SOL_KD_USER"" = '" & ClassUser.UNAME & "'  ORDER BY ""Name"" ASC"
        'dt_operator = oDBConnection.executeQuery(sql)
        'cmb_operator.Items.Clear()
        'cmb_operator.DataSource = dt_operator
        'cmb_operator.SelectedIndex() = 0
        'cmb_operator.DisplayMember = "Name"
        'cmb_operator.ValueMember = "Code"
    End Sub

    Private Sub disabletext(ByVal visible As Boolean)
        If ClassWeighbridge.UTIPE = "7" Then
            txt_qty_suratjalan.Enabled = True
        Else
            txt_qty_suratjalan.Enabled = visible
        End If
        cmb_kendaraan.Enabled = visible
        txt_nopol.Enabled = visible
        txt_supir.Enabled = visible
        txt_nosj.Enabled = visible
        txt_transportir.Enabled = visible
        txt_qtykapal.Enabled = visible
        If ClassWeighbridge.UTIPE = "6" Or ClassWeighbridge.UTIPE = "5" Or ClassWeighbridge.UTIPE = "4" Then
            txt_qty_suratjalan.Enabled = visible
        Else
            txt_qty_suratjalan.Enabled = False
        End If

    End Sub

    Public Sub bindingtextbox()
        txt_batch.Text = ClassWeighbridge.UBATCH
        txt_cardcode.Text = ClassWeighbridge.UCARDCODE
        txt_cardname.Text = ClassWeighbridge.UCARDNAME
        txt_docnum.Text = ClassWeighbridge.DocNum
        txt_itemcode.Text = ClassWeighbridge.UITEMCODE
        txt_itemname.Text = ClassWeighbridge.UITEMNAME
        txt_kapal.Text = ClassWeighbridge.UKAPAL
        txt_nopol.Text = ClassWeighbridge.UNOPOL
        txt_nosj.Text = ClassWeighbridge.USURATJALAN
        txt_po.Text = ClassWeighbridge.UNOPO
        txt_qty_spt_kilo.Text = ClassWeighbridge.UQTYSPM
        txt_qty_suratjalan.Text = ClassWeighbridge.UQTYSJ
        txt_remarks.Text = ClassWeighbridge.UREMARKS
        txt_so.Text = ClassWeighbridge.UNOSO
        txt_sppb.Text = ClassWeighbridge.UNOSPPB
        txt_spt.Text = ClassWeighbridge.UNOSPT
        txt_supir.Text = ClassWeighbridge.USUPIR
        txt_transportir.Text = ClassWeighbridge.UTRANSPORTIR
        txt_qtykapal.Text = ClassWeighbridge.UQTYKAPAL
        txt_tukarguling.Text = ClassWeighbridge.UNOTG
        txt_qty_tg.Text = ClassWeighbridge.UQTYTG
        If ClassWeighbridge.UDTWEIGH1 = "19000101" Then
            lbl_datein.Text = "-"
        Else
            lbl_datein.Text = ClassWeighbridge.UDTWEIGH1
        End If

        If ClassWeighbridge.UDTWEIGH2 = "19000101" Then
            lbl_dateout.Text = "-"
        Else
            lbl_dateout.Text = ClassWeighbridge.UDTWEIGH2
        End If

        lbl_in.Text = ClassWeighbridge.UQTY1
        lbl_out.Text = ClassWeighbridge.UQTY2
        If ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2" Then
            If ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" Then
                lbl_qty_in_karung.Text = ((ClassWeighbridge.UQTYSPMSUM) / 50).ToString
                txt_qty_spt_karung.Text = ((ClassWeighbridge.UQTYSPM + ClassWeighbridge.UQTYSJ) / 50).ToString
            Else
                lbl_qty_in_karung.Text = 0
                txt_qty_spt_karung.Text = 0
            End If
        Else
            lbl_qty_in_karung.Text = 0
            txt_qty_spt_karung.Text = 0
        End If
        If isView = True Then
            cmb_kendaraan.Text = ClassWeighbridge.UKENDARAAN
        Else
            cmb_kendaraan.SelectedValue = ClassWeighbridge.UKENDARAAN
        End If

        If isView = False Then


            If ClassWeighbridge.UDTWEIGH1.ToString = "19000101" Then
                tipetimbang = "In"
                disabletext(False)
                If ClassWeighbridge.UTIPE = "3" Or ClassWeighbridge.UTIPE = "4" Or ClassWeighbridge.UTIPE = "5" Or ClassWeighbridge.UTIPE = "6" Then
                    disabletext(True)
                End If
                txt_operator1.Text = ClassUser.UNAME
                txt_operator2.Text = ""
            ElseIf ClassWeighbridge.UDTWEIGH1.ToString <> "19000101" And ClassWeighbridge.UDTWEIGH2.ToString = "19000101" Then
                tipetimbang = "Out"
                disabletext(False)
                txt_operator1.Text = ClassWeighbridge.UOP1
                txt_operator2.Text = ClassUser.UNAME
            End If
            If ClassWeighbridge.UITEMCODE = "" Then
                tipetimbang = "-"
                txt_operator1.Text = ""
                txt_operator2.Text = ""
            End If
        End If


        propertychanged()
    End Sub


    Sub print(ByVal tipe As String, ByVal tipedokumen As String, ByVal docent As Integer)
        Dim crReport As New ReportDocument
        Dim dialog As New PrintDialog
        Dim filename As String = ""
        Dim printername As String = oApplicationConfig.PrinterDotMatrix
        Dim copies As Integer = 1
        If tipe = "In" Then
            If tipedokumen = "1" Or tipedokumen = "2" Then
                filename = "Form SPM.rpt"
            ElseIf tipedokumen = "3" Or tipedokumen = "4" Or tipedokumen = "5" Or tipedokumen = "6" Or tipedokumen = "7" Then
                filename = "Form SPB.rpt"
                printername = oApplicationConfig.PrinterThermal
            Else
                Return
            End If
        Else
            ' If tipedokumen = "1" Or tipedokumen = "2" Then


            'If tipedokumen = "1" Or tipedokumen = "2" Then
            '    filename = "Form Ticket.rpt"
            '    printername = oApplicationConfig.PrinterDotMatrix

            'Else
            filename = "Form Ticket V2.rpt"
            printername = oApplicationConfig.PrinterThermal
            copies = 2
            'End If


            '   Else
            '  filename = "Form Ticket.rpt"
            '  End If

        End If


        If File.Exists(oApplicationConfig.ReportPath & "\" & filename) Then
            filename = oApplicationConfig.ReportPath & "\" & filename
        End If


        crReport.Load(filename)
        Dim query As String = String.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printername)
        Using searcher As New ManagementObjectSearcher(query)
            Dim coll As System.Management.ManagementObjectCollection = searcher.Get
            Try

                For Each printer As ManagementObject In coll

                    For Each asd As PropertyData In printer.Properties
                        If asd.Name = "ExtendedPrinterStatus" Then
                            If asd.Value.ToString = "7" Then
                                dialog.ShowDialog()
                                printername = dialog.PrinterSettings.PrinterName
                                Exit For
                            End If
                        End If
                    Next
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
            End Try

        End Using
        crReport.PrintOptions.NoPrinter = False
        crReport.PrintOptions.PrinterName = printername
        crReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        crReport.SetParameterValue(0, docent)
        crConnectionInfo.ServerName = oSBOConnection.ServerName & ":" & oSBOConnection.Port
        crConnectionInfo.UserID = oSBOConnection.DBUserName.ToString
        crConnectionInfo.Password = oSBOConnection.DBPassword.ToString
        crConnectionInfo.Type = ConnectionInfoType.Unknown
        CrTables = crReport.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        For i = 1 To copies
            crReport.PrintToPrinter(1, True, 0, 0)
        Next


    End Sub


    Private Function Validation() As String
        If c_manual.Checked = True Then
            If (CInt(txt_timbang_manual.Text) = 0 Or txt_timbang_manual.Text = "") Then
                Return " Warning : 02 - Berat timbangan belum di isi"
            End If
        ElseIf (CInt(lbl_timbang.Text) = 0 Or lbl_timbang.Text = "-" Or lbl_timbang.Text = "") Then
            Return " Warning : 02 - Berat timbangan belum di isi"
        End If
        If tipetimbang = "Out" Then
            If ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2" Or (ClassWeighbridge.UTIPE = "5" And ClassWeighbridge.UITEMCODE.Contains("ANGKUT.RAW") = False) Then
                If ClassWeighbridge.UQTY1 > ClassWeighbridge.UQTY2 Then
                    Return " Warning : 03- Timbang keluar harus lebih besar dari timbang masuk"
                End If
            ElseIf ClassWeighbridge.UTIPE = "4" Or ClassWeighbridge.UTIPE = "6" Or (ClassWeighbridge.UTIPE = "5" And ClassWeighbridge.UITEMCODE.Contains("ANGKUT.RAW") = True) Then

                If ClassWeighbridge.UQTY2 > ClassWeighbridge.UQTY1 Then
                    Return " Warning : 04- Timbang keluar harus lebih kecil dari timbang masuk"
                End If
            End If
        End If

        Return ""
    End Function

    Public Sub propertychanged()
        lbl_netto.Text = Math.Abs(ClassWeighbridge.UQTY1 - ClassWeighbridge.UQTY2)
        If isView = False Then
            If c_manual.Checked = True Then
                If tipetimbang = "In" Then
                    Double.TryParse(txt_timbang_manual.Text, ClassWeighbridge.UQTY1)
                    ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY1
                Else
                    Double.TryParse(txt_timbang_manual.Text, ClassWeighbridge.UQTY2)
                    ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY2
                End If
            Else

                If tipetimbang = "In" Then
                    Double.TryParse(lbl_timbang.Text, ClassWeighbridge.UQTY1)
                    ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY1

                Else
                    Double.TryParse(lbl_timbang.Text, ClassWeighbridge.UQTY2)
                    ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY2
                End If
            End If
            If tipetimbang = "-" Then
                lbl_in.Text = "0"
                lbl_out.Text = "0"
                lbl_qty_out_karung.Text = "0"
                lbl_qty_in_karung.Text = "0"
                lbl_qty_rata2.Text = "0"
                lbl_selisih.Text = "0"
                txt_timbang_manual.Text = "0"
            ElseIf tipetimbang = "In" Then
                lbl_qty_out_karung.Text = "0"
                lbl_qty_rata2.Text = "0"
                lbl_in.Text = ClassWeighbridge.UQTY1
            ElseIf tipetimbang = "Out" Then
                lbl_out.Text = ClassWeighbridge.UQTY2
                If (ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2") And ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" Then
                    lbl_qty_out_karung.Text = Math.Round(((lbl_netto.Text) / 50), 2)
                    lbl_qty_rata2.Text = Math.Round(lbl_netto.Text / lbl_qty_in_karung.Text, 2)
                End If
            End If
        Else
            If (ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2") And ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" Then
                lbl_out.Text = ClassWeighbridge.UQTY2
                lbl_qty_in_karung.Text = ClassWeighbridge.UQTYSPMSUM / 50
                lbl_qty_out_karung.Text = Math.Round(((lbl_netto.Text) / 50), 2)
                lbl_qty_rata2.Text = Math.Round(lbl_netto.Text / lbl_qty_in_karung.Text, 2)
            End If
        End If
    End Sub

    Public Sub GetQuantity()

        Dim angka As String = (Regex.Replace(InputData, "[^?\d\.]", ""))
        Dim StringBuild As New StringBuilder
        StringBuild.Append(CInt(Mid(angka, 1, 3)))
        StringBuild.Append(Mid(angka, 4, 4))
        If Len(StringBuild.ToString.Trim) > 2 Then
            lbl_timbang.Text = StringBuild.ToString.Trim.Substring(2)
        Else
            lbl_timbang.Text = StringBuild.ToString.Trim
        End If

        propertychanged()

    End Sub

    Public Sub ErrorDevice()
        lbl_timbang.Text = "0"
    End Sub




#End Region

#Region "Database"
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
#End Region

#Region "Form Event"
    Private Sub SerialPort_ErrorReceived(ByVal sender As Object, ByVal e As Ports.SerialErrorReceivedEventArgs) Handles SerialPort.ErrorReceived
        Try
            Me.BeginInvoke(New EventHandler(AddressOf ErrorDevice))
            FlushMemory()
        Catch ex As Exception
            Me.BeginInvoke(New EventHandler(AddressOf ErrorDevice))
        End Try
    End Sub


    Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Try
            If SerialPort.IsOpen Then
                InputData = SerialPort.ReadLine
                Me.BeginInvoke(New EventHandler(AddressOf GetQuantity))
            End If
            FlushMemory()
        Catch ex As Exception
            Me.BeginInvoke(New EventHandler(AddressOf ErrorDevice))
        End Try
    End Sub

    Private Sub ReloadPostingShift()
        PostingDate.Value = Today

        Dim index As String = "1"
        If Now.Hour >= 16 Then
            index = "3"
        ElseIf Now.Hour >= 8 Then
            index = "2"
        ElseIf Now.Hour >= 0 Then
            index = "1"
        End If

        cmb_shift.Text = index

    End Sub
    Private Sub Form_WeighBridge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReloadPostingShift()
        If ClassUser.UADMWB = 1 Then
            c_manual.Visible = True
            PostingDate.Enabled = True
            cmb_shift.Enabled = True
        Else
            c_manual.Visible = False
            PostingDate.Enabled = False
            cmb_shift.Enabled = False
        End If
        If isView = True Then
            Panel1.Enabled = False
            Panel2.Enabled = False
            Panel3.Enabled = False
            Panel4.Enabled = False
            cmb_kendaraan.DropDownStyle = ComboBoxStyle.Simple
            bindingtextbox()

        Else
            ClassWeighbridge = New oWeighBridge
            cmb_kendaraan.DropDownStyle = ComboBoxStyle.DropDownList
            Panel1.Enabled = True
            Panel2.Enabled = True
            Panel3.Enabled = True
            Panel4.Enabled = True
            Try
                SerialPort.NewLine = vbCr
                SerialPort.PortName = oApplicationConfig.PortName.ToString.Trim
                SerialPort.BaudRate = oApplicationConfig.BaudRate
                SerialPort.DataBits = oApplicationConfig.DataBits
                SerialPort.ReadTimeout = oApplicationConfig.ReadTimeout
                SerialPort.Parity = IO.Ports.Parity.None
                SerialPort.StopBits = IO.Ports.StopBits.One
                SerialPort.Handshake = IO.Ports.Handshake.None
                SerialPort.RtsEnable = True
                If Not SerialPort.IsOpen Then
                    SerialPort.Open()
                End If
            Catch ex As Exception
                MessageBox.Show(Me, "Timbangan Tidak Aktif", "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If ClassUser.UADMWB = 1 Then
                    c_manual.Visible = True
                    c_manual.Checked = True
                Else
                    c_manual.Visible = False
                    c_manual.Checked = False
                End If

            Finally

            End Try


            Try
                connect()
                SQLHandler = New HANAVersion(oSBOConnection.DatabaseName)
                SQLHandler = oSBOConnection.SQLHandler
                oCompany = oSBOConnection.Company


                loadcombo()
                disabletext(False)
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
            End Try
        End If
    End Sub


    Private Sub txt_timbang_manual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_timbang_manual.TextChanged
        If isView = False Then
            If tipetimbang = "In" Then
                Double.TryParse(txt_timbang_manual.Text, ClassWeighbridge.UQTY1)
                ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY1
            Else

                Double.TryParse(txt_timbang_manual.Text, ClassWeighbridge.UQTY2)
                ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY2
            End If

        End If

        propertychanged()

    End Sub

    Private Sub lbl_timbang_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_timbang.TextChanged
        If isView = False Then
            If tipetimbang = "In" Then
                Double.TryParse(lbl_timbang.Text, ClassWeighbridge.UQTY1)
                ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY1
            Else
                Double.TryParse(lbl_timbang.Text, ClassWeighbridge.UQTY2)
                ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY2
            End If

        End If

        propertychanged()

    End Sub

    Private Sub txt_timbang_manual_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_timbang_manual.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                If Asc(e.KeyChar) <> 46 Then
                    e.Handled = True
                End If

            End If
        End If
    End Sub


    Private Sub Form_WeighBridge_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If SerialPort.IsOpen Then
                SerialPort.DiscardInBuffer()
                SerialPort.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub c_manual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c_manual.CheckedChanged
        If c_manual.Checked = False Then
            lbl_timbang.Visible = True
            txt_timbang_manual.Visible = False
            If tipetimbang = "In" Then
                ClassWeighbridge.UTIPETIMBANG1 = "A"
            Else
                ClassWeighbridge.UTIPETIMBANG2 = "A"
            End If

            propertychanged()
        Else
            txt_timbang_manual.Visible = True
            lbl_timbang.Visible = False
            If tipetimbang = "In" Then
                ClassWeighbridge.UTIPETIMBANG1 = "M"
            Else
                ClassWeighbridge.UTIPETIMBANG2 = "M"
            End If
            propertychanged()
        End If
    End Sub

    Private Sub btn_po_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_po.Click
        If ClassUser.UADMWB = 0 Then
            ReloadPostingShift()
        End If
        ClassWeighbridge.UDTPOSTDATE = PostingDate.Value.ToString("yyyyMMdd")
        disabletext(True)
        Dim form As New Form_Find(Me.Name, Me, "PO", "")
        form.Show()
    End Sub

    Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Dim fileName As String = ""

        Dim filePath As String = ""
        Dim idin As String = ""
        Try
            propertychanged()
            Dim msgvalidasi As String
            Dim resultDB As Integer
            msgvalidasi = Validation()
            insentifmesin = 0
            If msgvalidasi <> "" Then
                MessageBox.Show(msgvalidasi.ToString)
                Return
            End If
            Dim result = CustomMessageBox.Show({"Confirm", "Cancel", "Not Cofirm"}, "Apakah yakin untuk konfirmasi timbangan ?", "Konfirmasi Timbangan", 3)
            If result = "Confirm" Then
                saveHistoryTransaksi()
                If tipetimbang = "In" Then
                    If ClassWeighbridge.UTIPETIMBANG1 = "A" Then
                        Double.TryParse(lbl_timbang.Text, ClassWeighbridge.UQTY1)
                    Else
                        Double.TryParse(txt_timbang_manual.Text, ClassWeighbridge.UQTY1)
                    End If
                    ClassWeighbridge.CreateDate = Today.ToString("yyyyMMdd")
                    ClassWeighbridge.CreateTime = Now.ToString("HHmm")
                    ClassWeighbridge.UDTWEIGH1 = Today.ToString("yyyyMMdd")
                    ClassWeighbridge.UTWEIGH1 = Now.ToString("HHmm")
                    ClassWeighbridge.UREMARKS = txt_remarks.Text
                    ClassWeighbridge.UOP1 = txt_operator1.Text

                    ClassWeighbridge.USUPIR = txt_supir.Text
                    ClassWeighbridge.UKENDARAAN = cmb_kendaraan.SelectedValue
                    ClassWeighbridge.UNOPOL = txt_nopol.Text
                    ClassWeighbridge.USURATJALAN = txt_nosj.Text
                    ClassWeighbridge.USTAT = "O"
                    If (ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2") And ClassWeighbridge.UITEMCODE.Substring(0, 2) <> "FG" Then
                        ClassWeighbridge.USTAT = "F"
                    End If
                    ClassWeighbridge.UQTYKAPAL = txt_qtykapal.Text
                    ClassWeighbridge.UTRANSPORTIR = txt_transportir.Text
                    If c_manual.Checked = True Then
                        ClassWeighbridge.UTIPETIMBANG1 = "M"
                    Else
                        ClassWeighbridge.UTIPETIMBANG1 = "A"
                    End If
                    ClassWeighbridge.USHIFT = Trim(cmb_shift.Text)
                    ClassWeighbridge.UDTPOSTDATE = PostingDate.Value.ToString("yyyyMMdd")
                    resultDB = ClassWeighbridge.Add()
                Else
                    If c_manual.Checked = False Then
                        ClassWeighbridge.UTIPETIMBANG2 = "A"
                    Else
                        ClassWeighbridge.UTIPETIMBANG2 = "M"
                    End If

                    If ClassWeighbridge.UTIPE = "1" Or ClassWeighbridge.UTIPE = "2" Then
                        If (ClassWeighbridge.UPERCENTAGE < 1) Then
                            ClassWeighbridge.UQTY0 = (ClassWeighbridge.UQTY2 - ClassWeighbridge.UQTY1) * ClassWeighbridge.UPERCENTAGE
                            ClassWeighbridge.UQTY0 = ClassWeighbridge.UQTY0 + ClassWeighbridge.UQTY1
                        End If
                    End If

                    ClassWeighbridge.UOP2 = txt_operator2.Text
                    ClassWeighbridge.UREMARKS = txt_remarks.Text
                    ClassWeighbridge.UQTYSJ = txt_qty_suratjalan.Text
                    ClassWeighbridge.USTAT = "R" 'If(ClassWeighbridge.ISCANCEL = "Y", "V", "R")
                    ClassWeighbridge.UDTWEIGH2 = Today.ToString("yyyyMMdd")
                    ClassWeighbridge.UTWEIGH2 = Now.ToString("HHmm")
                    'If ClassWeighbridge.UITEMCODE = "BB101.001.001" Then
                    '    oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    '    oRec.DoQuery("CALL BMM_GETDATABB('" & ClassWeighbridge.UNOPO & "')")
                    '    If oRec.RecordCount = 0 Then
                    '        'blok data tidak detumkan 
                    '    End If




                    '    oRec.MoveFirst()


                    '    ClassWeighbridge.BASEENTRY = CType(oRec.Fields.Item("DocEntry").Value, Int64)
                    '    ClassWeighbridge.BASELINE = CType(oRec.Fields.Item("LineNum").Value, Integer)
                    '    ClassWeighbridge.OPENQTY = CType(oRec.Fields.Item("OpenQty").Value, Double)
                    '    ClassWeighbridge.SERIES = CType(oRec.Fields.Item("SeriesGRPO").Value, Integer)
                    '    ClassWeighbridge.TIPEPR = CType(oRec.Fields.Item("U_SOL_TIPEPR").Value, Integer)
                    '    ClassWeighbridge.TIPEDOK = CType(oRec.Fields.Item("U_SOL_TIPEDOK").Value, Integer)
                    '    ClassWeighbridge.POREMARKS = CType(oRec.Fields.Item("U_SOL_REMARKS").Value, String)
                    '    ClassWeighbridge.TOLERANSI = CType(oRec.Fields.Item("TOLERANSI").Value, Double)

                    '    If ClassWeighbridge.UQTY0 > ClassWeighbridge.OPENQTY Then
                    '        'block over quantity
                    '    End If



                    'End If
                    resultDB = ClassWeighbridge.UpdateOut()

                End If
                'If(ClassWeighbridge.ISCANCEL = "Y", "V", "R")
                If resultDB = -1 Then
                    Throw New Exception("Proses Timbang gagal!")
                End If
                If (ClassWeighbridge.USTAT = "R" And ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" And ClassWeighbridge.ISCANCEL <> "Y") Then

                Else
                    result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk cetak SPM/SPB ?", "Konfirmasi Print", 2)
                    If result = "Confirm" Then
                        Try
                            print(tipetimbang, ClassWeighbridge.UTIPE, ClassWeighbridge.DocEntry)
                        Catch ex As Exception
                            MessageBox.Show("Print Gagal!, Mohon Untuk print ulang nomor " & ClassWeighbridge.DocNum)
                        End Try

                    End If
                End If

                If (ClassWeighbridge.USTAT = "R" And oDBConnection.DatabaseName = "BMM_LIVE" And ClassWeighbridge.UITEMCODE = "BB101.001.001" And ClassWeighbridge.ISCANCEL <> "Y") Then
                    oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRec.DoQuery("CALL BMM_GETDATABBFORWA('" & ClassWeighbridge.UNOPO & "')")
                    If oRec.RecordCount > 0 Then
                        oRec.MoveFirst()
                        Try
                            Dim newData As New oObjectWa
                            Dim attachmentData As New oObjectDocument
                            Dim numbers(1) As oObjectWa
                            Dim jsonResulttodict As Object
                            attachmentData.link = "https://app.berkahmm.com/files/documents/2023_01_10/example.pdf"
                            attachmentData.filename = "example.pdf"
                            newData.template_name = "bmm_client_push_document_type1"
                            newData.lang = "ID"
                            newData.document = attachmentData
                            newData.to = CType(oRec.Fields.Item("NOHP").Value, String)
                            newData.recipient_name = "*Team Purchasing*"
                            newData.message = CType(oRec.Fields.Item("REMARKS").Value, String).Replace("xxx", CType(oRec.Fields.Item("QTYPO").Value, Double).ToString("#,##0.00")).Replace("yyy", CType(oRec.Fields.Item("QTYTIMBANG").Value, Double).ToString("#,##0.00")).Replace("zzz", CType(oRec.Fields.Item("OPENTRUCK").Value, String))
                            numbers(0) = newData
                            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                            ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                            ServicePointManager.Expect100Continue = True
                            Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://app.berkahmm.com/api/wabiz/senddocumenttemplate")
                            ''bmm di remarks
                            Dim starcraft As Authorization = New Authorization With {
                           .SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
                           }
                            Dim json As String = JsonConvert.SerializeObject(starcraft)
                            Dim PostString As String = JsonConvert.SerializeObject(numbers)
                            myReq.Headers.Add("Authorization", json)
                            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString)
                            myReq.ContentLength = byteArray.Length
                            myReq.Method = "POST"
                            myReq.ContentType = "application/json"


                            Dim dataStream As Stream = myReq.GetRequestStream()
                            dataStream.Write(byteArray, 0, byteArray.Length)
                            dataStream.Close() 'sends request

                            Dim myResp As HttpWebResponse
                            Try
                                myResp = myReq.GetResponse()

                            Catch ex As Exception
                                MessageBox.Show(ex.Message.ToString)
                            End Try
                            Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
                            Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
                            Dim Response As String = loResponseStream.ReadToEnd()
                            loResponseStream.Close()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message.ToString)
                        End Try
                    End If
                    Utils.releaseobject(oRec)
                End If

                'If (ClassWeighbridge.USTAT = "R" And (ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FM") And (ClassWeighbridge.SJDATE <> "-" And ClassWeighbridge.SJMONTH <> "-" And ClassWeighbridge.SJYEAR <> "-" And ClassWeighbridge.SJDOCNUM <> "-" And ClassWeighbridge.SJENTRY <> 0 And ClassWeighbridge.NOHP <> "-")) Then
                If (ClassWeighbridge.USTAT = "R" And oDBConnection.DatabaseName = "BMM_LIVE" And ClassWeighbridge.ISCANCEL <> "Y" And (ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" Or ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FM") And (ClassWeighbridge.SJDATE <> "-" And ClassWeighbridge.SJMONTH <> "-" And ClassWeighbridge.SJYEAR <> "-" And ClassWeighbridge.SJDOCNUM <> "-" And ClassWeighbridge.SJENTRY <> 0 And ClassWeighbridge.NOHP <> "-")) Then


                    Try
                        printtofile(ClassWeighbridge.SJENTRY, ClassWeighbridge.SJDOCNUM, ClassWeighbridge.SJYEAR, ClassWeighbridge.SJMONTH, ClassWeighbridge.SJDATE)
                        fileName = ClassWeighbridge.SJDOCNUM & ".pdf"
                        filePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName)
                        If System.IO.File.Exists(filePath) Then
                            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                            ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                            ServicePointManager.Expect100Continue = True
                            Dim url As String = "https://graph.facebook.com/v18.0/105582345757797/media"
                            Dim accessToken As String = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
                            Dim boundary As String = "----WebKitFormBoundary" & DateTime.Now.Ticks.ToString("x")
                            Dim request As HttpWebRequest =
                                CType(WebRequest.Create(url), HttpWebRequest)
                            request.Method = "POST"
                            request.ContentType = "multipart/form-data; boundary=" & boundary
                            request.Headers.Add("Authorization", "Bearer " & accessToken)
                            request.KeepAlive = True
                            Using requestStream As Stream = request.GetRequestStream()
                                Dim messagingPart As String =
                                    "--" & boundary & vbCrLf &
                                    "Content-Disposition: form-data; name=""messaging_product""" & vbCrLf & vbCrLf &
                                    "whatsapp" & vbCrLf

                                Dim messagingBytes = Encoding.UTF8.GetBytes(messagingPart)
                                requestStream.Write(messagingBytes, 0, messagingBytes.Length)
                                Dim fileHeader As String =
                                    "--" & boundary & vbCrLf &
                                    "Content-Disposition: form-data; name=""file""; filename=""" & Path.GetFileName(filePath) & """" & vbCrLf &
                                    "Content-Type: application/pdf" & vbCrLf & vbCrLf
                                Dim headerBytes = Encoding.UTF8.GetBytes(fileHeader)
                                requestStream.Write(headerBytes, 0, headerBytes.Length)
                                Dim fileBytes = File.ReadAllBytes(filePath)
                                requestStream.Write(fileBytes, 0, fileBytes.Length)
                                Dim footer As String = vbCrLf & "--" & boundary & "--" & vbCrLf
                                Dim footerBytes = Encoding.UTF8.GetBytes(footer)
                                requestStream.Write(footerBytes, 0, footerBytes.Length)
                            End Using
                            Try
                                Using response As HttpWebResponse =
                                    CType(request.GetResponse(), HttpWebResponse)
                                    Using reader As New StreamReader(response.GetResponseStream())
                                        Dim responseText As String = reader.ReadToEnd()
                                        Dim json As JObject = JObject.Parse(responseText)
                                        idin = json("id").ToString()
                                    End Using
                                End Using

                            Catch ex As WebException
                                Using err = CType(ex.Response, HttpWebResponse)
                                    Using reader As New StreamReader(err.GetResponseStream())
                                        Console.WriteLine("ERROR:")
                                        Console.WriteLine(reader.ReadToEnd())
                                    End Using
                                End Using
                            End Try

                        End If


                        If File.Exists(filePath) Then
                            File.Delete(filePath)
                        End If


                        If idin = "" Then
                            MessageBox.Show("File SJ/SPT gagal upload , Mohon untuk di resend ulang melalui menu list WB")
                        Else
                            Dim recipientNumbers As New List(Of String) From {ClassWeighbridge.NOHP}
                            If ClassWeighbridge.NOFAX <> "-" Then
                                'ClassWeighbridge.NOFAX = "6281806875210"
                                recipientNumbers.Add(ClassWeighbridge.NOFAX)
                            End If


                            ' Create WhatsAppMessage for each recipient
                            Dim messages As New List(Of WhatsAppMessage)

                            For Each number In recipientNumbers
                                Dim message As New WhatsAppMessage() With {
                                    .messaging_product = "whatsapp",
                                    .recipient_type = "individual",
                                    .to = number,
                                    .type = "template",
                                    .template = New Template() With {
                                        .name = "bmm_esj_utility_template",
                                        .language = New Language() With {
                                            .code = "ID"
                                        },
                                        .components = New List(Of Component) From {
                                            New Component() With {
                                                .type = "header",
                                                .parameters = New List(Of Parameter) From {
                                                    New Parameter() With {
                                                        .type = "document",
                                                        .document = New Document() With {
                                                            .id = idin,
                                                            .filename = ClassWeighbridge.SJDOCNUM + ".pdf"
                                                        }
                                                    }
                                                }
                                            },
                                            New Component() With {
                                                .type = "body",
                                                .parameters = New List(Of Parameter) From {
                                                    New Parameter() With {.type = "text", .text = ClassWeighbridge.UCARDNAME.ToString()},
                                                    New Parameter() With {.type = "text", .text = ClassWeighbridge.SJDOCNUM}
                                                }
                                            }
                                        }
                                    }
                                }
                                messages.Add(message)
                            Next

                            For Each msg In messages
                                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                                ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                                ServicePointManager.Expect100Continue = True

                                Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://graph.facebook.com/v16.0/105582345757797/messages")
                                ''bmm di remarks
                                Dim starcraft As Authorization = New Authorization With {
                               .SystemUserAccessToken = "Bearer EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
                               }
                                ' Dim json As String = JsonConvert.SerializeObject(starcraft, Formatting.Indented)\
                                Dim json As String = JsonConvert.SerializeObject(starcraft)
                                Dim PostString As String = JsonConvert.SerializeObject(msg)
                                myReq.Headers.Add("Authorization", "Bearer EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8")
                                Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString.Replace(",""document"":null", "").Replace(",""text"":null", ""))
                                myReq.ContentLength = byteArray.Length
                                myReq.Method = "POST"
                                myReq.ContentType = "application/json"


                                Dim dataStream As Stream = myReq.GetRequestStream()
                                dataStream.Write(byteArray, 0, byteArray.Length)
                                dataStream.Close() 'sends request


                                Dim myResp As HttpWebResponse
                                Try
                                    myResp = myReq.GetResponse()

                                Catch ex As Exception
                                    MessageBox.Show(ex.Message.ToString)
                                End Try


                                Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
                                Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
                                Dim Response As String = loResponseStream.ReadToEnd()
                                loResponseStream.Close()
                            Next
                        End If


                      
                        ' Dim newData As New oObjectWa
                        ' Dim attachmentData As New oObjectDocument
                        ' Dim numbers(1) As oObjectWa
                        ' Dim jsonResulttodict As Object
                        ' attachmentData.link = "https://app.berkahmm.com/files/sjupload/" + ClassWeighbridge.SJYEAR + "/" + ClassWeighbridge.SJMONTH + "/" + ClassWeighbridge.SJDATE + "/" + ClassWeighbridge.SJDOCNUM + ".pdf"
                        ' attachmentData.filename = ClassWeighbridge.SJDOCNUM + ".pdf"
                        ' newData.template_name = "bmm_client_push_document_type1"
                        ' newData.lang = "ID"
                        ' newData.document = attachmentData
                        ' newData.to = ClassWeighbridge.NOHP
                        ' newData.recipient_name = "*" + ClassWeighbridge.UCARDNAME.ToString() + "*"
                        ' newData.message = "Berikut kami lampirkan Surat Jalan Nomor " + ClassWeighbridge.SJDOCNUM + ". Pesan ini adalah pesan otomatis, mohon untuk tidak reply ke whatsapp ini.Jika ada pertanyaan lebih lanjut dapat mengubungi kami pada nomor telepon (021)-29305611"
                        ' numbers(0) = newData

                        ' If ClassWeighbridge.NOFAX <> "-" Then
                        '     'If ClassWeighbridge.NOFAX <> "-" And ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FM" Then
                        '     newData = New oObjectWa
                        '     attachmentData.link = "https://app.berkahmm.com/files/sjupload/" + ClassWeighbridge.SJYEAR + "/" + ClassWeighbridge.SJMONTH + "/" + ClassWeighbridge.SJDATE + "/" + ClassWeighbridge.SJDOCNUM + ".pdf"
                        '     attachmentData.filename = ClassWeighbridge.SJDOCNUM + ".pdf"
                        '     newData.template_name = "bmm_client_push_document_type1"
                        '     newData.lang = "ID"
                        '     newData.document = attachmentData
                        '     newData.to = ClassWeighbridge.NOFAX
                        '     newData.recipient_name = "*" + ClassWeighbridge.UCARDNAME.ToString() + "*"
                        '     newData.message = "Berikut kami lampirkan Surat Jalan Nomor " + ClassWeighbridge.SJDOCNUM + ". Pesan ini adalah pesan otomatis, mohon untuk tidak reply ke whatsapp ini.Jika ada pertanyaan lebih lanjut dapat mengubungi kami pada nomor telepon (021)-29305611"
                        '     numbers(1) = newData
                        ' End If
                        ' System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                        ' ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                        ' ServicePointManager.Expect100Continue = True
                        ' Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://app.berkahmm.com/api/wabiz/senddocumenttemplate")
                        ' ''bmm di remarks
                        ' Dim starcraft As Authorization = New Authorization With {
                        '.SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
                        '}

                        ' 'Dim starcraft As Authorization = New Authorization With {
                        ' '.SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8",
                        ' '.Version = "v15.0",
                        ' '.RecipientPhoneNumber = "6282264725838",
                        ' ' .WABAID = "107614655551698",
                        ' '.PINNumber = "199069",
                        ' '.VerificationToken = "KaqVIMY1ljftp/6Ei/jNkDonVYV0XxZnhe4xarsTW1c="
                        ' '}


                        ' ' Dim json As String = JsonConvert.SerializeObject(starcraft, Formatting.Indented)\
                        ' Dim json As String = JsonConvert.SerializeObject(starcraft)
                        ' Dim PostString As String = JsonConvert.SerializeObject(numbers)
                        ' myReq.Headers.Add("Authorization", json)


                        ' ' Dim encoding As New ASCIIEncoding()
                        ' ' Dim byteArray As Byte() = encoding.GetBytes(PostString + json + json + json)
                        ' Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString)
                        ' myReq.ContentLength = byteArray.Length
                        ' myReq.Method = "POST"
                        ' myReq.ContentType = "application/json"


                        ' Dim dataStream As Stream = myReq.GetRequestStream()
                        ' dataStream.Write(byteArray, 0, byteArray.Length)
                        ' dataStream.Close() 'sends request
                        ' 'Using Writer As New IO.BinaryWriter(myReq.GetRequestStream)
                        ' '    Writer.Write(byteArray)
                        ' 'End Using



                        ' Dim myResp As HttpWebResponse
                        ' Try
                        '     myResp = myReq.GetResponse()

                        ' Catch ex As Exception
                        '     MessageBox.Show(ex.Message.ToString)
                        ' End Try


                        ' Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
                        ' Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
                        ' Dim Response As String = loResponseStream.ReadToEnd()
                        ' loResponseStream.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message.ToString)
                    End Try


                End If


                'Try
                '    Dim capture As New oCaptureCCTV

                '    capture.cctvname = "WB" + Strings.Right(oSBOConnection.SBOUserName, 1) + tipetimbang
                '    capture.fullnumber = ClassWeighbridge.DocEntry.ToString()
                '    capture.fullpath = capture.fullpath + (ClassWeighbridge.UDTWEIGH1).ToString.Substring(0, 4) + "-" + ClassWeighbridge.UDTWEIGH1.ToString.Substring(4, 2) + "-" + ClassWeighbridge.UDTWEIGH1.ToString.Substring(6, 2) + "\\"
                '    Dim myReq As Net.HttpWebRequest = Net.HttpWebRequest.Create("http://192.168.1.7:85/api/captureCCTV/captureCCTV")
                '    Dim PostString As String = JsonConvert.SerializeObject(capture)
                '    Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString)
                '    myReq.ContentLength = byteArray.Length
                '    myReq.Method = "POST"
                '    myReq.ContentType = "application/json"



                '    Using dataStream As Stream = myReq.GetRequestStream()
                '        '   Dim dataStream As Stream = myReq.GetRequestStream()
                '        dataStream.Write(byteArray, 0, byteArray.Length)
                '        dataStream.Close() 'sends request
                '    End Using




                '    Using myresp As Net.HttpWebResponse = myReq.GetResponse()

                '    End Using

                'Catch ex As Exception

                'Finally

                'End Try
                If ClassWeighbridge.ISCANCEL = "Y" Then
                    oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRec.DoQuery(SQLHandler.UpdateStatusSPM(ClassWeighbridge.DocEntry, "V"))
                    Utils.releaseobject(oRec)
                End If
                ClassWeighbridge = New oWeighBridge
                bindingtextbox()

                MessageBox.Show("Simpan data berhasil")

                txt_search.Text = ""
                txt_search.Focus()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
            If oCompany.InTransaction Then
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
            If ClassWeighbridge.UITEMCODE.Substring(0, 2) = "FG" Then
                Dim clearanceent As Integer
                Dim dtresult As DataTable
                Try
                    If ex.Message.ToString().Contains("toleransi") Then
                        Dim arrsplit() As String
                        arrsplit = ex.Message.ToString().Split("@")
                        Dim result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Truck diluar toleransi, Apakah ingin mencetak clearance form ?", "Konfirmasi", 2)
                        If result = "Confirm" Then
                            result = ""
                            dtresult = ClassWeighbridge.checkclearance()
                            If dtresult.Rows.Count > 0 Then
                                result = CustomMessageBox.Show({"Nomor Baru", "Nomor Lama"}, "Terdapat clearance form " & dtresult.Rows(0).Item("DOCNUM").ToString() & " untuk SPT  " & ClassWeighbridge.UNOSPT & ", apakah ingin cetak ulang  ?", "Konfirmasi", 2)
                                If result = "Confirm" Then

                                    clearanceent = ClassWeighbridge.saveClearanceForm(CDbl(arrsplit(1)))
                                    If clearanceent = -1 Then

                                    End If

                                End If
                            Else
                                clearanceent = ClassWeighbridge.saveClearanceForm(CDbl(arrsplit(1)))

                                If clearanceent = -1 Then

                                End If

                            End If
                            printclearance(clearanceent)
                        End If
                    End If
                Catch exs As Exception
                    MessageBox.Show("Create Clearance Form Error " & exs.Message.ToString())
                End Try

            End If
        Finally

        End Try
    End Sub




    Sub printclearance(ByVal docent As Integer)
        Dim crReport As New ReportDocument
        Dim dialog As New PrintDialog
        Dim filename As String = ""
        Dim param2 As String
        Dim param3 As String = ""
        Dim printername As String = oApplicationConfig.PrinterDotMatrix

        filename = "Clearance Form New BMM" & If(oDBConnection.DatabaseName = "BMM_LIVE", " Live", "") & ".rpt"


        If File.Exists(oApplicationConfig.ReportPath & "\" & filename) Then
            filename = oApplicationConfig.ReportPath & "\" & filename
        End If

        crReport.Load(filename)
        dialog.ShowDialog()
        crReport.PrintOptions.NoPrinter = False
        crReport.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName
        crReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        crReport.SetParameterValue(0, docent)

        crConnectionInfo.ServerName = oSBOConnection.ServerName & ":" & oSBOConnection.Port
        crConnectionInfo.UserID = oSBOConnection.DBUserName.ToString
        crConnectionInfo.Password = oSBOConnection.DBPassword.ToString
        CrTables = crReport.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        crReport.PrintToPrinter(1, True, 0, 0)
        crReport.Close()
        crReport.Dispose()
    End Sub

    Sub printtofile(ByVal docent As Integer, ByVal docnum As String, ByVal year As String, ByVal month As String, ByVal datesj As String)
        Dim crReport As New ReportDocument
        Dim dialog As New PrintDialog
        Dim filename As String = ""
        Dim printername As String = oApplicationConfig.PrinterDotMatrix
        Dim copies As Integer = 1
        Dim path As String
        Try
            filename = "Surat Jalan Full.rpt"

            If File.Exists(oApplicationConfig.ReportPath & "\" & filename) Then
                filename = oApplicationConfig.ReportPath & "\" & filename
            End If


            crReport.Load(filename)
            crReport.PrintOptions.NoPrinter = True
            crReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize

            crReport.SetParameterValue(0, docent)

            crConnectionInfo.ServerName = oSBOConnection.ServerName & ":" & oSBOConnection.Port
            crConnectionInfo.UserID = oSBOConnection.DBUserName.ToString
            crConnectionInfo.Password = oSBOConnection.DBPassword.ToString
            crConnectionInfo.Type = ConnectionInfoType.Unknown
            CrTables = crReport.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            'path = "\\192.168.1.15\Attachment WB\SJ\" + year + "\" + month + "\" + datesj + "\"
            'If IO.Directory.Exists(path) = False Then
            '    IO.Directory.CreateDirectory(path)
            'End If

            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            CrDiskFileDestinationOptions.DiskFileName = Environment.CurrentDirectory.ToString + "\" + docnum.ToString() + ".pdf"
            CrExportOptions = crReport.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions

            End With
            crReport.Export()

            crReport.Close()
            crReport.Dispose()



            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Sftp
                .HostName = "192.168.1.8"
                .UserName = "root"
                .Password = "Inv0ic3BMM"
                .SshHostKeyFingerprint = "ssh-ed25519 255 78:3b:b6:93:0b:09:9d:43:57:a5:8a:26:db:1f:38:c6"
            End With


            Using session As New Session
                Try
                    ' Connect
                    session.Open(sessionOptions)

                    ' Upload files
                    Dim transferOptions As New TransferOptions
                    transferOptions.TransferMode = TransferMode.Binary

                    Dim transferResult As TransferOperationResult
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/" + year + "/"
                    If (session.FileExists(path) = False) Then
                        session.CreateDirectory(path)
                    End If
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/" + year + "/" + month + "/"
                    If (session.FileExists(path) = False) Then
                        session.CreateDirectory(path)
                    End If
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/" + year + "/" + month + "/" + datesj + "/"
                    If (session.FileExists(path) = False) Then
                        session.CreateDirectory(path)
                    End If

                    'Dim check As String = session.RemoteDirectoryInfo("/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/2022/12/09")
                    transferResult = session.PutFiles(Environment.CurrentDirectory.ToString + "\" + docnum.ToString() + ".pdf", path + docnum.ToString() + ".pdf", False, transferOptions)

                    ' Throw on any error
                    transferResult.Check()

                    ' Print results

                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString)
                Finally
                    session.Close()
                    'If File.Exists(docnum.ToString() + ".pdf") Then
                    '    File.Delete(docnum.ToString() + ".pdf")
                    'End If
                End Try
            End Using




        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try


    End Sub

    Private Sub btn_spm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_spm.Click
        disabletext(True)
        Dim form As New Form_Find(Me.Name, Me, "DocNum", "")
        form.Show()
    End Sub


    Private Sub btn_spt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_spt.Click
        If ClassUser.UADMWB = 0 Then
            ReloadPostingShift()
        End If
        ClassWeighbridge.UDTPOSTDATE = PostingDate.Value.ToString("yyyyMMdd")
        disabletext(True)
        Dim form As New Form_Find(Me.Name, Me, "SPT", "")
        form.Show()
    End Sub



    Private Sub txt_search_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_search.KeyPress
        'prefix 1 = spt
        'prefix 2 = spta
        'prefix 3 = spm/spb
        Dim dateposting As String
        If Asc(e.KeyChar) = Keys.Enter Then
            Try

                ClassWeighbridge = New oWeighBridge
                If ClassUser.UADMWB = 0 Then
                    ReloadPostingShift()
                End If
                ClassWeighbridge.UDTPOSTDATE = PostingDate.Value.ToString("yyyyMMdd")
                barcode = txt_search.Text

                If barcode.Length < 4 Then
                    MessageBox.Show("Barcode tidak Sesuai")
                    Return
                End If
                If Mid(barcode, 2, 1) <> "-" Then
                    If IsNumeric(barcode.Substring(0, 1)) Then
                        If barcode.Length <> 9 Then
                            MessageBox.Show("Barcode tidak Sesuai")
                            Return
                        Else
                            txt_search.Text = barcode
                            typebarcode = "1"
                        End If
                    Else
                        txt_search.Text = barcode
                        typebarcode = "0"

                    End If

                Else
                    txt_search.Text = Mid(barcode, 3, barcode.Length - 2)
                    typebarcode = Mid(barcode, 1, 1)
                    barcode = txt_search.Text
                End If
                querystring = "CALL " & oSBOConnection.DatabaseName.ToString & ".BMM_GET_TRANSAKSIBYKEYFIRSTNEW('" & typebarcode & "','" & barcode & "')"

                If ClassWeighbridge.getByKey(querystring) = 0 Then
                    bindingtextbox()
                Else
                    If typebarcode = "3" Then
                        Throw New Exception("Data Tidak ditemukan")
                    Else
                        querystring = "CALL " & oSBOConnection.DatabaseName.ToString & ".BMM_GET_TRANSAKSIBYKEY('" & typebarcode & "','" & barcode & "')"
                    End If
                    If ClassWeighbridge.getByKey(querystring) = 0 Then
                        bindingtextbox()
                    Else
                        Throw New Exception("Data Tidak ditemukan")
                    End If
                End If


                propertychanged()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        End If



    End Sub



#End Region






    Private Sub txt_qtykapal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_qtykapal.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                If Asc(e.KeyChar) <> 46 Then
                    e.Handled = True
                End If

            End If
        End If
    End Sub





    Private Sub txt_qty_suratjalan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_qty_suratjalan.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                If Asc(e.KeyChar) <> 46 Then
                    e.Handled = True
                End If

            End If
        End If
    End Sub




    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        print("In", "4", 27042)
    End Sub

    Private Sub btn_tukarguling_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tukarguling.Click
        If ClassUser.UADMWB = 0 Then
            ReloadPostingShift()
        End If
        ClassWeighbridge.UDTPOSTDATE = PostingDate.Value.ToString("yyyyMMdd")
        disabletext(True)
        Dim form As New Form_Find(Me.Name, Me, "TG", "")
        form.Show()
    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        FlushMemory()
        MessageBox.Show("asd")
    End Sub


    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click

        oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("CALL BMM_GETDATABBFORWA('240100502')")
        If oRec.RecordCount > 0 Then
            oRec.MoveFirst()
            Try
                Dim newData As New oObjectWa
                Dim attachmentData As New oObjectDocument
                Dim numbers(1) As oObjectWa
                Dim jsonResulttodict As Object
                attachmentData.link = "https://app.berkahmm.com/files/documents/2023_01_10/example.pdf"
                attachmentData.filename = "example.pdf"
                newData.template_name = "bmm_client_push_document_type1"
                newData.lang = "ID"
                newData.document = attachmentData
                newData.to = CType(oRec.Fields.Item("NOHP").Value, String)
                newData.recipient_name = "*Team Purchasing*"
                newData.message = CType(oRec.Fields.Item("REMARKS").Value, String).Replace("xxx", CType(oRec.Fields.Item("QTYPO").Value, Double).ToString("#,##0.00")).Replace("yyy", CType(oRec.Fields.Item("QTYTIMBANG").Value, Double).ToString("#,##0.00")).Replace("zzz", CType(oRec.Fields.Item("OPENTRUCK").Value, String))
                numbers(0) = newData
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                ServicePointManager.Expect100Continue = True
                Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://app.berkahmm.com/api/wabiz/senddocumenttemplate")
                ''bmm di remarks
                Dim starcraft As Authorization = New Authorization With {
               .SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
               }
                Dim json As String = JsonConvert.SerializeObject(starcraft)
                Dim PostString As String = JsonConvert.SerializeObject(numbers)
                myReq.Headers.Add("Authorization", json)
                Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString)
                myReq.ContentLength = byteArray.Length
                myReq.Method = "POST"
                myReq.ContentType = "application/json"


                Dim dataStream As Stream = myReq.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
                dataStream.Close() 'sends request

                Dim myResp As HttpWebResponse
                Try
                    myResp = myReq.GetResponse()

                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString)
                End Try
                Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
                Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
                Dim Response As String = loResponseStream.ReadToEnd()
                loResponseStream.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
            End Try
        End If
        Utils.releaseobject(oRec)

    End Sub
End Class