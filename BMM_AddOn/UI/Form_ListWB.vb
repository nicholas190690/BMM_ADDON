Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Windows.Forms
Imports System.Management
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security



Imports System.Web
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Net.Http.HttpClient

Imports WinSCP
Imports System.Threading

Public Class Form_ListWB
    Dim oRec As SAPbobsCOM.Recordset
    Dim oRec2 As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim dt_transaksi As DataTable
    Dim dt_detail As DataTable
    Dim index As Integer
    Dim shift As String
    Dim date1 As String
    Dim date2 As Date
    Dim docentry As String
    Dim excludelist As New List(Of String)
    Dim docent As String
    Dim tipe As String
    Dim result As String
    Dim cFunction As New oFunctional
    Dim crtableLogoninfos As New TableLogOnInfos
    Dim crtableLogoninfo As New TableLogOnInfo
    Dim crConnectionInfo As New ConnectionInfo
    Dim CrTables As Tables
    Dim CrTable As CrystalDecisions.CrystalReports.Engine.Table



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


    Private Sub btn_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_search.Click
        dgv_list.DataSource = Nothing
        dgv_list.Rows.Clear()
        dgv_list.Columns.Clear()

        Dim stat As String
        Dim tipe As String
        Try

            connect()
            oCompany = oSBOConnection.Company
            SQLHandler = oSBOConnection.SQLHandler
            stat = Mid(cmb_stat.Text.ToString, 1, 1)
            tipe = Mid(cmb_type.Text.ToString, 1, 1)
            If stat = "" Then
                stat = "A"
            End If

            If tipe = "" Then
                tipe = "A"
            End If
            dt_transaksi = oDBConnection.executeQuery(SQLHandler.GetWB(dtp1.Value.ToString("yyyyMMdd"), dtp2.Value.ToString("yyyyMMdd"), stat, tipe, txt_plat.Text))
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

            Dim btn As New DataGridViewButtonColumn
            btn.HeaderText = ""
            btn.Text = "Select"
            btn.Name = "btn"
            btn.UseColumnTextForButtonValue = True
            dgv_list.Columns.Insert(0, btn)
            dgv_list.AllowUserToAddRows = False
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Sub print(ByVal tipe As String, ByVal tipedokumen As String, ByVal docent As Integer)
        Dim crReport As New ReportDocument
        Dim dialog As New PrintDialog
        Dim filename As String = ""
        Dim printername As String = oApplicationConfig.PrinterDotMatrix
        Dim copies As Integer = 1
        Try
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
                'If tipedokumen = "1" Or tipedokumen = "2" Then
                '    filename = "Form Ticket.rpt"

                'Else
                filename = "Form Ticket V2.rpt"
                printername = oApplicationConfig.PrinterThermal
                copies = 2
                'End If

                '  Else
                ' filename = "Form Ticket.rpt"
                ' End If

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
                                End If
                                Exit For
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
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try


    End Sub



    Sub printtofile(ByVal docent As Integer, ByVal docnum As Integer, ByVal year As String, ByVal month As String, ByVal datesj As String, ByVal typefile As String)
        Dim crReport As New ReportDocument
        Dim dialog As New PrintDialog
        Dim filename As String = ""
        Dim printername As String = oApplicationConfig.PrinterDotMatrix
        Dim copies As Integer = 1
        Dim path As String
        Try
            If typefile = "E-SJ" Then
                filename = "Surat Jalan Full.rpt"
            Else
                filename = "Surat Penugasan Transportir (SPT).rpt"
            End If


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
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/" + If(typefile = "E-SJ", "sjupload", "sptupload") + "/" + year + "/"
                    If (session.FileExists(path) = False) Then
                        session.CreateDirectory(path)
                    End If
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/" + If(typefile = "E-SJ", "sjupload", "sptupload") + "/" + year + "/" + month + "/"
                    If (session.FileExists(path) = False) Then
                        session.CreateDirectory(path)
                    End If
                    path = "/var/www/html/com_berkahmm_onlineinvoice/public/files/" + If(typefile = "E-SJ", "sjupload", "sptupload") + "/" + year + "/" + month + "/" + datesj + "/"
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


    Sub printtofilelocal(ByVal docent As Integer, ByVal docnum As Integer, ByVal year As String, ByVal month As String, ByVal datesj As String)
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


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try


    End Sub


    'Sub print(ByVal docentry As String, ByVal typeprint As String, ByVal typewb As String)
    '    Dim crReport As New ReportDocument
    '    Dim printdialog As New PrintDialog
    '    Dim dt_print As New DataTable
    '    Dim filename As String = ""
    '    Try
    '        If typeprint = "1" Then
    '            If typewb = "1" Or typewb = "2" Then
    '                filename = "Form SPM.rpt"
    '            ElseIf typewb = "3" Or typewb = "4" Or typewb = "6" Then
    '                filename = "Form SPB.rpt"
    '            ElseIf typewb = "5" Then
    '                filename = "Form Limbah.rpt"
    '            End If
    '            dt_print = oDBConnection.executeQuery(SQLHandler.GetForm(docentry, typewb))
    '        Else
    '            If typewb = "1" Or typewb = "2" Then
    '                filename = "Form Ticket.rpt"
    '            ElseIf typewb = "3" Or typewb = "4" Or typewb = "6" Then
    '                filename = "Form Ticket.rpt"
    '            ElseIf typewb = "5" Then
    '                filename = "Form Ticket.rpt"
    '            End If

    '            dt_print = oDBConnection.executeQuery(SQLHandler.GetFormTicket(docentry, typewb))
    '        End If

    '        If File.Exists("\\192.168.1.5\b1_shf\Report\" & filename) Then
    '            filename = "\\192.168.1.5\b1_shf\Report\" & filename
    '        End If


    '        If (File.Exists(filename)) And dt_print.Rows.Count > 0 Then
    '            crReport.Load(filename)
    '            crReport.Refresh()
    '            crReport.SetParameterValue(0, dt_print.Rows(0).Item(0).ToString().Trim())
    '            crReport.PrintToPrinter(1, True, 1, 1)
    '        Else
    '            MessageBox.Show("Tidak Dapat Print Dokumen")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message.ToString())
    '    End Try

    'End Sub

    Private Sub btn_printspm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_printspm.Click

        docent = Replace(Replace(dgv_list.Item("NoSystem", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
        tipe = dgv_list.Item("Tipe", dgv_list.CurrentRow.Index).Value.ToString
        If Trim(docent) <> "" Then
            result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk cetak SPM/SPB ?", "Konfirmasi Print", 2)
            If result = "Confirm" Then
                print("In", tipe, docent)
            End If
        End If

    End Sub

    Private Sub btn_printticket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_printticket.Click

        docent = Replace(Replace(dgv_list.Item("NoSystem", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
        tipe = dgv_list.Item("Tipe", dgv_list.CurrentRow.Index).Value.ToString

        If Trim(docent) <> "" Then
            result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk cetak Ticket ?", "Konfirmasi Print", 2)
            If result = "Confirm" Then
                print("Out", tipe, docent)
            End If

        End If
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            Dim docnum As String
            docent = Replace(Replace(dgv_list.Item("NoSystem", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
            docnum = Replace(Replace(dgv_list.Item("Nomor SPM/SPB", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
            If Trim(docent) <> "" Then
                result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk cancel SPM/SPB " & docnum & " ?", "Konfirmasi Cancel", 2)
                If result = "Confirm" Then
                    oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRec2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRec2.DoQuery(SQLHandler.GetTransDelivery2(docnum))
                    If oRec2.RecordCount > 0 Then
                        Utils.releaseobject(oRec2)
                        oRec2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        oRec2.DoQuery(SQLHandler.GetTransDelivery(docnum))


                        If oRec2.RecordCount > 0 Then
                            oRec.DoQuery(SQLHandler.UpdateStatusSPM(docent, "V"))
                            MessageBox.Show("Success Update Status SPM/SPB No " & docnum)
                        Else
                            MessageBox.Show("Delivery harus di cancel terlebih dahulu")
                        End If

                        'oRec.DoQuery(SQLHandler.UpdateStatusSPM(docent, "V"))
                        'MessageBox.Show("Success Update Status SPM/SPB No " & docnum)

                    Else
                        oRec.DoQuery(SQLHandler.UpdateStatusSPM(docent, "V"))
                        MessageBox.Show("Success Update Status SPM/SPB No " & docnum)
                    End If

                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally
            Utils.releaseobject(oRec)
            Utils.releaseobject(oRec2)
        End Try

    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filepath As String
        Try
            filepath = System.IO.Path.Combine(
                  My.Computer.FileSystem.SpecialDirectories.MyDocuments, "BMM-ListWB-" & Now.ToString("yyyyMMddHHmmss") & ".xlsx")
            cFunction.ExportToExcel(dt_transaksi, filepath)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Sub choose()
        ClassWeighbridge = New oWeighBridge
        index = dgv_list.CurrentRow.Index



        ClassWeighbridge.UNOPOL = If(dgv_list.Columns.Contains("Nopol") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nopol").Value), "", dgv_list.Rows(index).Cells("Nopol").Value), "")
        ClassWeighbridge.UNOSPPB = If(dgv_list.Columns.Contains("Nomor SPPB") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor SPPB").Value), "-", dgv_list.Rows(index).Cells("Nomor SPPB").Value), "-")
        ClassWeighbridge.UNOSPT = If(dgv_list.Columns.Contains("Nomor SPT") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor SPT").Value), "-", dgv_list.Rows(index).Cells("Nomor SPT").Value), "-")
        ClassWeighbridge.UNOPO = If(dgv_list.Columns.Contains("Nomor PO") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor PO").Value), "-", dgv_list.Rows(index).Cells("Nomor PO").Value), "-")
        ClassWeighbridge.UNOSO = If(dgv_list.Columns.Contains("Nomor SO") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor SO").Value), "-", dgv_list.Rows(index).Cells("Nomor SO").Value), "-")
        ClassWeighbridge.UKENDARAAN = If(dgv_list.Columns.Contains("Kendaraan") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Kendaraan").Value), "1", dgv_list.Rows(index).Cells("Kendaraan").Value), "-")
        ClassWeighbridge.USUPIR = If(dgv_list.Columns.Contains("Supir") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Supir").Value), "", dgv_list.Rows(index).Cells("Supir").Value), "")
        ClassWeighbridge.UBATCH = If(dgv_list.Columns.Contains("Batch") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Batch").Value), "", dgv_list.Rows(index).Cells("Batch").Value), "")

        ClassWeighbridge.UTRANSPORTIR = If(dgv_list.Columns.Contains("Transportir") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Transportir").Value), "", dgv_list.Rows(index).Cells("Transportir").Value), "")


        ClassWeighbridge.DocEntry = If(dgv_list.Columns.Contains("NoSystem") = True, If(IsDBNull(dgv_list.Rows(index).Cells("NoSystem").Value), 0, dgv_list.Rows(index).Cells("NoSystem").Value), 0)
        ClassWeighbridge.DocNum = If(dgv_list.Columns.Contains("Nomor SPM/SPB") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor SPM/SPB").Value), 0, dgv_list.Rows(index).Cells("Nomor SPM/SPB").Value), 0)
        ClassWeighbridge.USURATJALAN = If(dgv_list.Columns.Contains("Nomor Surat Jalan") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Nomor Surat Jalan").Value), "", dgv_list.Rows(index).Cells("Nomor Surat Jalan").Value), "")
        ClassWeighbridge.USUPIR = If(dgv_list.Columns.Contains("Supir") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Supir").Value), "", dgv_list.Rows(index).Cells("Supir").Value), "")
        ClassWeighbridge.UQTYSPM = If(dgv_list.Columns.Contains("Qty SPM/SPB") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Qty SPM/SPB").Value), 0, dgv_list.Rows(index).Cells("Qty SPM/SPB").Value), 0)
        ClassWeighbridge.UQTYSJ = If(dgv_list.Columns.Contains("Qty SJ") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Qty SJ").Value), 0, dgv_list.Rows(index).Cells("Qty SJ").Value), 0)
        ClassWeighbridge.UQTY1 = If(dgv_list.Columns.Contains("Berat Masuk") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Berat Masuk").Value), 0, dgv_list.Rows(index).Cells("Berat Masuk").Value), 0)
        ClassWeighbridge.UDTWEIGH1 = If(dgv_list.Columns.Contains("Tanggal Masuk") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Tanggal Masuk").Value), "19000101", dgv_list.Rows(index).Cells("Tanggal Masuk").Value.ToString().Substring(6, 4) & dgv_list.Rows(index).Cells("Tanggal Masuk").Value.ToString().Substring(3, 2) & dgv_list.Rows(index).Cells("Tanggal Masuk").Value.ToString().Substring(0, 2)), "19000101")
        ClassWeighbridge.UQTY2 = If(dgv_list.Columns.Contains("Berat Keluar") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Berat Keluar").Value), 0, dgv_list.Rows(index).Cells("Berat Keluar").Value), 0)
        ClassWeighbridge.UDTWEIGH2 = If(dgv_list.Columns.Contains("Tanggal Keluar") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Tanggal Keluar").Value), "19000101", dgv_list.Rows(index).Cells("Tanggal Keluar").Value.ToString().Substring(6, 4) & dgv_list.Rows(index).Cells("Tanggal Keluar").Value.ToString().Substring(3, 2) & dgv_list.Rows(index).Cells("Tanggal Keluar").Value.ToString().Substring(0, 2)), "19000101")
        ClassWeighbridge.UREMARKS = If(dgv_list.Columns.Contains("Remarks") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Remarks").Value), "", dgv_list.Rows(index).Cells("Remarks").Value), "")
        ClassWeighbridge.Remarks = If(dgv_list.Columns.Contains("Remarks") = True, If(IsDBNull(dgv_list.Rows(index).Cells("Remarks").Value), "", dgv_list.Rows(index).Cells("Remarks").Value), "")
        ClassWeighbridge.UOP1 = If(dgv_list.Columns.Contains("U_SOL_OPERATORW1") = True, If(IsDBNull(dgv_list.Rows(index).Cells("U_SOL_OPERATORW1").Value), "", dgv_list.Rows(index).Cells("U_SOL_OPERATORW1").Value), "")
        ClassWeighbridge.UOP2 = If(dgv_list.Columns.Contains("U_SOL_OPERATORW2") = True, If(IsDBNull(dgv_list.Rows(index).Cells("U_SOL_OPERATORW2").Value), "", dgv_list.Rows(index).Cells("U_SOL_OPERATORW2").Value), "")
        'ClassWeighbridge.UPERCENTAGE = If(IsDBNull(dgv_list.Rows(index).Cells("Summary").Value), 0, dgv_list.Rows(index).Cells("Summary").Value)
        ClassWeighbridge.UTIPE = If(dgv_list.Columns.Contains("Tipe") = True, dgv_list.Rows(index).Cells("Tipe").Value, "")
        ClassWeighbridge.UCARDCODE = If(dgv_list.Columns.Contains("CardCode") = True, dgv_list.Rows(index).Cells("CardCode").Value, "")
        ClassWeighbridge.UCARDNAME = If(dgv_list.Columns.Contains("CardName") = True, dgv_list.Rows(index).Cells("CardName").Value, "")
        ClassWeighbridge.UITEMCODE = If(dgv_list.Columns.Contains("ItemCode") = True, dgv_list.Rows(index).Cells("ItemCode").Value, "")
        ClassWeighbridge.UITEMNAME = If(dgv_list.Columns.Contains("ItemName") = True, dgv_list.Rows(index).Cells("ItemName").Value, "")

        isView = True
        ClassWeighbridge.checkNopolOutstanding()
        Form_WeighBridge.Show()

    End Sub


    Private Sub dgv_list_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_list.CellClick
        Try
            If e.ColumnIndex = 0 Then
                choose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub dgv_list_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_list.ColumnHeaderMouseClick
        If (dgv_list.SortOrder.ToString() = "Descending") Then
            dt_transaksi.DefaultView.Sort = dgv_list.SortedColumn.Name + " DESC"
        Else
            dt_transaksi.DefaultView.Sort = dgv_list.SortedColumn.Name + " ASC"
        End If
        dt_transaksi = dt_transaksi.DefaultView.ToTable()
    End Sub




    Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim idin As String = ""
        Dim typeWA As String
        Dim fileName As String

        Dim filePath As String
        typeWA = ""
        typeWA = CustomMessageBox.Show({"E-SJ", "E-SPT"}, "Konfirmasi Untuk Send WA ? ", "Konfirmasi Resend", 2)

        If typeWA = "" Then
            MessageBox.Show("Type WA harus dipilih")
            Return
        Else
            Try
                Dim nohp As String = ""
                Dim nofax As String = ""
                result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk resend WA ?", "Konfirmasi Resend", 2)
                If result = "Confirm" Then
                    nohp = dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString()
                    nofax = dgv_list.Item("NOFAX", dgv_list.CurrentRow.Index).Value.ToString()
                    result = CustomMessageBox.Show({"Sama", "Berbeda"}, "Apakah nomor HP driver SPT sama dengan nomor WA ?", "Konfirmasi Resend", 2)
                    If result = "Sama" Then
                        nohp = dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString()
                    Else
                        nohp = InputBox("Masukan nomor WA supir tanpa tanda - atau spasi , contoh : 08131234821", "Send WA").Trim().Replace(" ", "")
                        If nohp.Length < 9 Then
                            Throw New Exception("Error! Nomor WA tidak sesuai")
                        End If
                        If Regex.IsMatch(nohp, "^[0-9 ]+$") = False Then
                            Throw New Exception("Error! Nomor WA tidak boleh ada huruf")
                        End If

                        If nohp.Substring(0, 1) = "0" Then
                            nohp = "62" + nohp.Substring(1)
                        End If

                        If nohp.Substring(0, 2) <> "62" Then
                            Throw New Exception("Error! Nomor WA tidak sesaui")
                        End If
                    End If
                    'nohp = "6281806875210"
                    'nofax = "6281806875210"
                    If typeWA = "E-SJ" Then
                        If ((dgv_list.Item("Status", dgv_list.CurrentRow.Index).Value.ToString() = "Timbang Keluar & Confirm" And (dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FG" Or dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM") And (dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJENTRY", dgv_list.CurrentRow.Index).Value <> 0 And dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString() <> "-"))) = False Then
                            MessageBox.Show("E-SJ Belum dapat di kirim")
                            Return
                        End If

                    Else
                        If (((dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FG" Or dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM") And (dgv_list.Item("DATESPT", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("MONTHSPT", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("YEARSPT", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("Nomor SPT", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SPTENTRY", dgv_list.CurrentRow.Index).Value <> 0 And dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString() <> "-"))) = False Then
                            MessageBox.Show("E-SPT Belum dapat di kirim")
                        End If
                    End If
                End If

                printtofile(dgv_list.Item(If(typeWA = "E-SJ", "SJENTRY", "SPTENTRY"), dgv_list.CurrentRow.Index).Value, dgv_list.Item(If(typeWA = "E-SJ", "SJNO", "Nomor SPT"), dgv_list.CurrentRow.Index).Value, dgv_list.Item(If(typeWA = "E-SJ", "YEARSJ", "YEARSPT"), dgv_list.CurrentRow.Index).Value, dgv_list.Item(If(typeWA = "E-SJ", "MONTHSJ", "MONTHSPT"), dgv_list.CurrentRow.Index).Value, dgv_list.Item(If(typeWA = "E-SJ", "DATESJ", "DATESPT"), dgv_list.CurrentRow.Index).Value, typeWA)

                fileName = dgv_list.Item(If(typeWA = "E-SJ", "SJNO", "Nomor SPT"), dgv_list.CurrentRow.Index).Value.ToString() & ".pdf"

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


                If idin = "" Then
                    MessageBox.Show("File SJ/SPT gagal upload , Mohon hubungi IT")
                    Return
                End If

                If File.Exists(filePath) Then
                    File.Delete(filePath)
                End If

                Dim recipientNumbers As New List(Of String) From {nohp}

                If dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM" And nofax <> "-" And typeWA = "E-SJ" Then
                    recipientNumbers.Add(nofax)
                End If
                Dim messages As New List(Of WhatsAppMessage)


                For Each number In recipientNumbers
                    Dim message As New WhatsAppMessage() With {
                        .messaging_product = "whatsapp",
                        .recipient_type = "individual",
                        .to = number,
                        .type = "template",
                        .template = New Template() With {
                            .name = If(typeWA = "E-SJ", "bmm_esj_utility_template", "bmm_espt_utility_template"),
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
                                                .filename = dgv_list.Item(If(typeWA = "E-SJ", "SJNO", "Nomor SPT"), dgv_list.CurrentRow.Index).Value.ToString() + ".pdf"
                                            }
                                        }
                                    }
                                },
                                New Component() With {
                                    .type = "body",
                                    .parameters = New List(Of Parameter) From {
                                        New Parameter() With {.type = "text", .text = dgv_list.Item("CardName", dgv_list.CurrentRow.Index).Value.ToString()},
                                        New Parameter() With {.type = "text", .text = dgv_list.Item(If(typeWA = "E-SJ", "SJNO", "Nomor SPT"), dgv_list.CurrentRow.Index).Value.ToString()}
                                    }
                                }
                            }
                        }
                    }
                    messages.Add(message)
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
                        ' MessageBox.Show(PostString.Replace(",""document"":null", "").Replace(",""text"":null", ""))

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
                        ' MessageBox.Show(Response)
                        loResponseStream.Close()

                    Next
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
            End Try

        End If




        ' Try
        '    Dim nohp As String = ""
        '    Dim nofax As String = ""
        '    result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk resend WA ?", "Konfirmasi Resend", 2)
        '    If result = "Confirm" Then
        '        nohp = dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString()
        '        nofax = dgv_list.Item("NOFAX", dgv_list.CurrentRow.Index).Value.ToString()
        '        result = CustomMessageBox.Show({"Sama", "Berbeda"}, "Apakah nomor HP driver SPT sama dengan nomor WA ?", "Konfirmasi Resend", 2)
        '        If result = "Sama" Then
        '            nohp = dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString()
        '        Else
        '            nohp = InputBox("Masukan nomor WA supir tanpa tanda - atau spasi , contoh : 08131234821", "Send WA").Trim().Replace(" ", "")
        '            If nohp.Length < 9 Then
        '                Throw New Exception("Error! Nomor WA tidak sesuai")
        '            End If
        '            If Regex.IsMatch(nohp, "^[0-9 ]+$") = False Then
        '                Throw New Exception("Error! Nomor WA tidak boleh ada huruf")
        '            End If

        '            If nohp.Substring(0, 1) = "0" Then
        '                nohp = "62" + nohp.Substring(1)
        '            End If

        '            If nohp.Substring(0, 2) <> "62" Then
        '                Throw New Exception("Error! Nomor WA tidak sesaui")
        '            End If
        '        End If
        '        'nohp = "6281806875210"
        '        'nofax = "6281806875210"
        '        If (dgv_list.Item("Status", dgv_list.CurrentRow.Index).Value.ToString() = "Timbang Keluar & Confirm" And (dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FG" Or dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM") And (dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJENTRY", dgv_list.CurrentRow.Index).Value <> 0 And dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString() <> "-")) Then
        '            printtofile(dgv_list.Item("SJENTRY", dgv_list.CurrentRow.Index).Value, dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value, dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value, dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value, dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value)






        '            Dim recipientNumbers As New List(Of String) From {nohp}
        '            If dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM" And nofax <> "-" Then
        '                recipientNumbers.Add(nofax)
        '            End If


        '            ' Create WhatsAppMessage for each recipient
        '            Dim messages As New List(Of WhatsAppMessage)

        '            For Each number In recipientNumbers
        '                Dim message As New WhatsAppMessage() With {
        '                    .messaging_product = "whatsapp",
        '                    .recipient_type = "individual",
        '                    .to = number,
        '                    .type = "template",
        '                    .template = New Template() With {
        '                        .name = "bmm_esj_utility_template",
        '                        .language = New Language() With {
        '                            .code = "ID"
        '                        },
        '                        .components = New List(Of Component) From {
        '                            New Component() With {
        '                                .type = "header",
        '                                .parameters = New List(Of Parameter) From {
        '                                    New Parameter() With {
        '                                        .type = "document",
        '                                        .document = New Document() With {
        '                                            .link = "https://app.berkahmm.com/files/sjupload/" + dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value.ToString() + "/" + dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value.ToString() + "/" + dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value.ToString() + "/" + dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString() + ".pdf",
        '                                            .filename = dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString() + ".pdf"
        '                                        }
        '                                    }
        '                                }
        '                            },
        '                            New Component() With {
        '                                .type = "body",
        '                                .parameters = New List(Of Parameter) From {
        '                                    New Parameter() With {.type = "text", .text = dgv_list.Item("CardName", dgv_list.CurrentRow.Index).Value.ToString()},
        '                                    New Parameter() With {.type = "text", .text = dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString()}
        '                                }
        '                            }
        '                        }
        '                    }
        '                }
        '                messages.Add(message)
        '            Next

        '            For Each msg In messages



        '                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        '                ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        '                ServicePointManager.Expect100Continue = True

        '                Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://graph.facebook.com/v16.0/105582345757797/messages")






        '                ''bmm di remarks
        '                Dim starcraft As Authorization = New Authorization With {
        '               .SystemUserAccessToken = "Bearer EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
        '               }
        '                ' Dim json As String = JsonConvert.SerializeObject(starcraft, Formatting.Indented)\
        '                Dim json As String = JsonConvert.SerializeObject(starcraft)
        '                Dim PostString As String = JsonConvert.SerializeObject(msg)
        '                myReq.Headers.Add("Authorization", "Bearer EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8")
        '                Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString.Replace(",""document"":null", "").Replace(",""text"":null", ""))
        '                myReq.ContentLength = byteArray.Length
        '                myReq.Method = "POST"
        '                myReq.ContentType = "application/json"
        '                MessageBox.Show(PostString.Replace(",""document"":null", "").Replace(",""text"":null", ""))

        '                Dim dataStream As Stream = myReq.GetRequestStream()
        '                dataStream.Write(byteArray, 0, byteArray.Length)
        '                dataStream.Close() 'sends request

        '                Dim myResp As HttpWebResponse
        '                Try
        '                    myResp = myReq.GetResponse()

        '                Catch ex As Exception
        '                    MessageBox.Show(ex.Message.ToString)
        '                End Try


        '                Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
        '                Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
        '                Dim Response As String = loResponseStream.ReadToEnd()
        '                MessageBox.Show(Response)
        '                loResponseStream.Close()

        '            Next
        '            MessageBox.Show("Resend WA Berhasil")



        '        End If
        '    End If


        'Catch ex As Exception
        '    MessageBox.Show(ex.Message.ToString)
        'End Try






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


    Private Sub Form_ListWB_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If (dgv_list.Item("Status", dgv_list.CurrentRow.Index).Value.ToString() = "Timbang Keluar & Confirm" And (dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FG" Or dgv_list.Item("ItemCode", dgv_list.CurrentRow.Index).Value.ToString().Substring(0, 2) = "FM") And (dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value.ToString() <> "-" And dgv_list.Item("SJENTRY", dgv_list.CurrentRow.Index).Value <> 0 And dgv_list.Item("NOHP", dgv_list.CurrentRow.Index).Value.ToString() <> "-")) Then
            printtofilelocal(dgv_list.Item("SJENTRY", dgv_list.CurrentRow.Index).Value, dgv_list.Item("SJNO", dgv_list.CurrentRow.Index).Value, dgv_list.Item("YEARSJ", dgv_list.CurrentRow.Index).Value, dgv_list.Item("MONTHSJ", dgv_list.CurrentRow.Index).Value, dgv_list.Item("DATESJ", dgv_list.CurrentRow.Index).Value)

            MessageBox.Show("Generate SJ Selesai, check folder ")
        End If

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        docent = Replace(Replace(dgv_list.Item("CLEARFORM", dgv_list.CurrentRow.Index).Value.ToString, ",", ""), ".", "")
        If docent = "0" Then
            MessageBox.Show("Tidak ada Data")
            Return
        End If
        If Trim(docent) <> "" Then
            result = CustomMessageBox.Show({"Confirm", "Cancel"}, "Apakah yakin untuk cetak clearance Form?", "Konfirmasi Print", 2)
            If result = "Confirm" Then
                printclearance(docent)
            End If

        End If

    End Sub
End Class