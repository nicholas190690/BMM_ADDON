Public Class ConfigurationForm

    Private Sub ConfigurationForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If oConfigXMLParser.isConfigFileExists Then
            If oConfigXMLParser.read Then
                isInit = False
            End If
        End If
        

        If Not IsNothing(oApplicationConfig) Then
            cmbPortName.Text = oApplicationConfig.PortName.ToString.Trim
            txtBaudRate.Text = oApplicationConfig.BaudRate.ToString.Trim
            txtDataBits.Text = oApplicationConfig.DataBits.ToString.Trim
            txtReadTimeout.Text = oApplicationConfig.ReadTimeout.ToString.Trim
            txt_path.Text = oApplicationConfig.PathMDB.ToString.Trim
            txt_issuecode.Text = oApplicationConfig.ItemIssueCode.ToString.Trim
            txt_issuename.Text = oApplicationConfig.ItemIssueName.ToString.Trim
            txt_receiptcode.Text = oApplicationConfig.ItemReceiptCode.ToString.Trim
            txt_receiptname.Text = oApplicationConfig.ItemReceiptName.ToString.Trim
            txt_tipeissue.Text = oApplicationConfig.TipeIssue.ToString.Trim
            txt_tipereceipt.Text = oApplicationConfig.TipeReceipt.ToString.Trim
            txt_start.Text = oApplicationConfig.StartID.ToString.Trim
            txt_timbangan.Text = oApplicationConfig.Timbangan.ToString.Trim
            txt_pathbag.Text = oApplicationConfig.PathBag.ToString.Trim
            cmb_type.Text = oApplicationConfig.Menu.ToString.Trim
            txt_reportpath.Text = oApplicationConfig.ReportPath.ToString.Trim
            txt_dotmatrix.Text = oApplicationConfig.PrinterDotMatrix.ToString.Trim
            txt_thermal.Text = oApplicationConfig.PrinterThermal.ToString.Trim
            txt_timbangan_l.Text = If(oApplicationConfig.PathTimbanganL = Nothing, "", oApplicationConfig.PathTimbanganL.ToString.Trim)
            txt_timbangan_r.Text = If(oApplicationConfig.PathTimbanganR = Nothing, "", oApplicationConfig.PathTimbanganR.ToString.Trim)

        End If

        If Not IsNothing(oSBOConnection) Then
            cboServerType.Text = oSBOConnection.DBServerType.ToString.Trim
            txtDBServer.Text = oSBOConnection.ServerName.ToString.Trim
            txtSAPDBPort.Text = oSBOConnection.Port.ToString.Trim
            txtDBUserName.Text = oSBOConnection.DBUserName.ToString.Trim
            txtDBPassword.Text = oSBOConnection.DBPassword.ToString.Trim
            cboDBType.Text = oSBOConnection.DBServerType.ToString.Trim
            txtSAPDatabase.Text = oSBOConnection.DatabaseName.ToString.Trim
            txtSAPUser.Text = oSBOConnection.SBOUserName.ToString.Trim
            txtSAPPass.Text = oSBOConnection.SBOPassword.ToString.Trim
            txtSAPLicenseServer.Text = oSBOConnection.SBOLicenseServer.ToString.Trim
            txtSAPAddonID.Text = oSBOConnection.SBOAddOnIdentifier.ToString.Trim

        End If





    End Sub


    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim message As String

        If cmbPortName.Text.Trim = "" Then
            message = "Port Name Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If txtBaudRate.Text.Trim = "" Then
            message = "Baud Rate Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If txtDataBits.Text.Trim = "" Then
            message = "Data Bits Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If txtReadTimeout.Text.Trim = "" Then
            message = "Read Timeout Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If




        If txtDBServer.Text.Trim = "" Then
            message = "Database Server Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If Trim(txtSAPDatabase.Text) = "" Then
            message = "Database Name Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If txtDBUserName.Text.Trim = "" Then
            message = "Database User Name Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If txtDBPassword.Text.Trim = "" Then
            message = "Database Password Cannot Be Empty"
            MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End If

        If oApplicationConfig Is Nothing Then
            oApplicationConfig = New ApplicationConfig
        End If

        oApplicationConfig.PortName = cmbPortName.Text.Trim
        oApplicationConfig.BaudRate = txtBaudRate.Text.Trim
        oApplicationConfig.DataBits = txtDataBits.Text.Trim
        oApplicationConfig.ReadTimeout = txtReadTimeout.Text.Trim
        oApplicationConfig.PathMDB = txt_path.Text.Trim
        oApplicationConfig.ItemIssueCode = txt_issuecode.Text.Trim
        oApplicationConfig.ItemIssueName = txt_issuename.Text.Trim
        oApplicationConfig.ItemReceiptCode = txt_receiptcode.Text.Trim
        oApplicationConfig.ItemReceiptName = txt_receiptname.Text.Trim
        oApplicationConfig.TipeIssue = txt_tipeissue.Text.Trim
        oApplicationConfig.TipeReceipt = txt_tipereceipt.Text.Trim
        oApplicationConfig.StartID = txt_start.Text.Trim
        oApplicationConfig.Timbangan = txt_timbangan.Text.Trim
        oApplicationConfig.PathBag = txt_pathbag.Text.Trim
        oApplicationConfig.Menu = cmb_type.Text.Trim
        oApplicationConfig.ReportPath = txt_reportpath.Text.Trim
        oApplicationConfig.PrinterDotMatrix = txt_dotmatrix.Text.Trim
        oApplicationConfig.PrinterThermal = txt_thermal.Text.Trim
        oApplicationConfig.PathTimbanganL = txt_timbangan_l.Text.Trim
        oApplicationConfig.PathTimbanganR = txt_timbangan_r.Text.Trim

        oSBOConnection = New SBOConnection("SBO", txtDBServer.Text, txtSAPDBPort.Text, txtDBUserName.Text, Encryption.Encrypt(txtDBPassword.Text), cboServerType.Text, txtSAPDatabase.Text, txtSAPUser.Text, Encryption.Encrypt(txtSAPPass.Text), txtSAPLicenseServer.Text, txtSAPAddonID.Text)


        If cboDBType.Text = "HANA" Then
            oDBConnection = New HANAConnection("HANA", txtDBServer.Text.Trim, txtSAPDBPort.Text.Trim, txtSAPDatabase.Text.Trim, txtDBUserName.Text.Trim, Encryption.Encrypt(txtDBPassword.Text.Trim))
        Else
            oDBConnection = New SQLConnection("SQL", txtDBServer.Text.Trim, txtDBUserName.Text.Trim, txtDBUserName.Text.Trim, Encryption.Encrypt(txtDBPassword.Text.Trim))
        End If



        oConfigXMLParser.write()

        message = "Konfigurasi telah di simpan,mohon untuk buka aplikasi kembali!"
        MessageBox.Show(Me, message, "BMM WeighBridge", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        oDBConnection.Disconnect()
        End
    End Sub

  
  
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        connect()
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

   
   
  
End Class