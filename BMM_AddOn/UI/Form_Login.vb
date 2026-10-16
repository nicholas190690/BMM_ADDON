
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

'Imports System.Security.Cryptography.X509Certificates
'Imports System.Net.Security



'Imports System.Web
'Imports System.Net.Http
'Imports System.Net.Http.Headers
'Imports System.Net.Http.HttpClient

'Imports WinSCP
'Imports System.Threading


Public Class Form_Login
    Dim isoke As Integer

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



    Private Sub Form_Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Form_Main.Show()
        lbl_version.Text = version

    End Sub


    Private Function Login() As Boolean
        Try
            oConfigXMLParser.read()
            isInit = True
            If oConfigXMLParser.isConfigFileExists Then
                If oConfigXMLParser.read Then
                    isInit = False
                End If
            End If
            connect()
        Catch ex As Exception
            MessageBox.Show("Koneksi Ke SAP tidak berhasil,mohon untuk di konfigurasi terlebih dahulu!")
            ConfigurationForm.Show()
        End Try
        Try
            ClassUser = New oUser
            isoke = ClassUser.getByKey(txt_user.Text, EncryptionAddOnvb.Encrypt(txt_pass.Text, "soltius"))
            If isoke = -1 Then
                Throw New System.Exception("Username & Password tidak sesuai")
            ElseIf isoke = 0 Then
                Throw New System.Exception("User sudah tidak aktif")
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Login Gagal : " + ex.Message.ToString)
            txt_user.Focus()
            Return False
        End Try
    End Function

    Private Sub btn_login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_login.Click
        Try
            If Login() = True Then
                txt_user.Text = ""
                txt_pass.Text = ""
                Dim MainForm As Form_Main = New Form_Main
                MainForm.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    Private Sub txt_user_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_user.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Or Asc(e.KeyChar) = Keys.Tab Then
            txt_pass.Focus()
        End If
    End Sub

    Private Sub txt_pass_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_pass.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            btn_login_Click(sender, e)
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dim newData As New oObjectWa
        'Dim attachmentData As New oObjectDocument
        'Dim numbers(0) As oObjectWa

        'Dim jsonResulttodict As Object

        ''     "template_name":"bmm_client_push_document_type1",
        ''"lang":"ID",
        ''"document":{
        ''    "link":"https://app.berkahmm.com/files/documents/2023_01_10/example.pdf",
        ''    "filename":"example.pdf"
        ''},
        ''"to":"6281806875210",
        ''"recipient_name":"*Amadeus*",
        ''"message":"Berikut adalah dokumen tes yang kami kirimkan melalui pesan Whatsapp."


        'attachmentData.link = "https://app.berkahmm.com/files/documents/2023_01_10/example.pdf"
        'attachmentData.filename = "example.pdf"
        'newData.template_name = "bmm_client_push_document_type1"
        'newData.lang = "ID"
        'newData.document = attachmentData
        'newData.to = "6281806875210"
        'newData.recipient_name = "*Amadeus*"
        'newData.message = "test"


        'Try

        '    numbers(0) = newData

        '    System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        '    ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
        '    ServicePointManager.Expect100Continue = True

        '    Dim myReq As HttpWebRequest = HttpWebRequest.Create("https://app.berkahmm.com/api/wabiz/senddocumenttemplate")






        '    ''bmm di remarks
        '    Dim starcraft As Authorization = New Authorization With {
        '   .SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
        '   }




        '    'Dim starcraft As Authorization = New Authorization With {
        '    '.SystemUserAccessToken = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8",
        '    '.Version = "v15.0",
        '    '.RecipientPhoneNumber = "6282264725838",
        '    ' .WABAID = "107614655551698",
        '    '.PINNumber = "199069",
        '    '.VerificationToken = "KaqVIMY1ljftp/6Ei/jNkDonVYV0XxZnhe4xarsTW1c="
        '    '}


        '    ' Dim json As String = JsonConvert.SerializeObject(starcraft, Formatting.Indented)\
        '    Dim json As String = JsonConvert.SerializeObject(starcraft)
        '    Dim PostString As String = JsonConvert.SerializeObject(numbers)
        '    myReq.Headers.Add("Authorization", json)


        '    ' Dim encoding As New ASCIIEncoding()
        '    ' Dim byteArray As Byte() = encoding.GetBytes(PostString + json + json + json)
        '    Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostString)
        '    myReq.ContentLength = byteArray.Length
        '    myReq.Method = "POST"
        '    myReq.ContentType = "application/json"


        '    Dim dataStream As Stream = myReq.GetRequestStream()
        '    dataStream.Write(byteArray, 0, byteArray.Length)
        '    dataStream.Close() 'sends request
        '    'Using Writer As New IO.BinaryWriter(myReq.GetRequestStream)
        '    '    Writer.Write(byteArray)
        '    'End Using



        '    Dim myResp As HttpWebResponse
        '    Try
        '        myResp = myReq.GetResponse()

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message.ToString)
        '    End Try


        '    Dim enc As Encoding = System.Text.Encoding.GetEncoding(1252)
        '    Dim loResponseStream As StreamReader = New StreamReader(myResp.GetResponseStream(), enc)
        '    Dim Response As String = loResponseStream.ReadToEnd()
        '    loResponseStream.Close()
        '    jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(Response)


        'Catch ex As Exception

        'End Try
        'Try
        '    Dim sessionOptions As New SessionOptions
        '    With sessionOptions
        '        .Protocol = Protocol.Sftp
        '        .HostName = "192.168.1.8"
        '        .UserName = "root"
        '        .Password = "Inv0ic3BMM"
        '        .SshHostKeyFingerprint = "ssh-ed25519 255 78:3b:b6:93:0b:09:9d:43:57:a5:8a:26:db:1f:38:c6"
        '    End With


        '    Using session As New Session
        '        connect()
        '        session.Open(sessionOptions)

        '        Upload(files)
        '        Dim transferOptions As New TransferOptions
        '        transferOptions.TransferMode = TransferMode.Binary

        '        Dim transferResult As TransferOperationResult
        '        If (session.FileExists("/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/2022/12/") = False) Then
        '            session.CreateDirectory("")
        '        End If

        '        Dim check As String = session.RemoteDirectoryInfo("/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/2022/12/09")
        '        transferResult = session.PutFiles(Environment.CurrentDirectory.ToString + "\220012051.pdf", "/var/www/html/com_berkahmm_onlineinvoice/public/files/sjupload/2022/12/09/220012056.pdf", False, transferOptions)

        '         Throw on any error
        '        transferResult.Check()

        '        Print(results)
        '        For Each transfer In transferResult.Transfers
        '            Console.WriteLine("Upload of {0} succeeded", transfer.FileName)
        '        Next
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message.ToString)
        'End Try


        'Try
        '    Dim capture As New oCaptureCCTV
        '    Dim tanggal As String = "20230317"
        '    capture.cctvname = "WB" + "1IN"
        '    capture.fullnumber = "992"
        '    capture.fullpath = capture.fullpath + (tanggal).ToString.Substring(0, 4) + "-" + tanggal.ToString.Substring(4, 2) + "-" + tanggal.ToString.Substring(6, 2) + "\\"
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



        Dim url As String = "https://graph.facebook.com/v18.0/105582345757797/media"
        Dim accessToken As String = "EAAIU6ejbZC38BACwRhOa6lkyPmff3KzFZBbsoPnl74azhNwtsEd8ohdYFvvPsKvSJSifT8HJVj5jkCX8J4qkBZADL69lS58R0jLgXW5pBZA53bKfkTo656IDIAuE5i48rfFgK3XD1ZAff8xDH89pXrH6r3V4MzcBVHzozZAbNmwRZCcGUbIboq8"
        Dim filePath As String = "C:\Users\nicho_o9gj1dt\Downloads\BA Penghapusan Laptop Pak Dior FAFU.00000360-signed.pdf"

        Dim boundary As String = "----WebKitFormBoundary" & DateTime.Now.Ticks.ToString("x")

        Dim request As HttpWebRequest =
            CType(WebRequest.Create(url), HttpWebRequest)

        request.Method = "POST"
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.Headers.Add("Authorization", "Bearer " & accessToken)
        request.KeepAlive = True

        Using requestStream As Stream = request.GetRequestStream()

            ' =========================
            ' messaging_product FIELD
            ' =========================
            Dim messagingPart As String =
                "--" & boundary & vbCrLf &
                "Content-Disposition: form-data; name=""messaging_product""" & vbCrLf & vbCrLf &
                "whatsapp" & vbCrLf

            Dim messagingBytes = Encoding.UTF8.GetBytes(messagingPart)
            requestStream.Write(messagingBytes, 0, messagingBytes.Length)

            ' =========================
            ' FILE FIELD
            ' =========================
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

        ' =========================
        ' RESPONSE
        ' =========================
        Try
            Using response As HttpWebResponse =
                CType(request.GetResponse(), HttpWebResponse)

                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    Console.WriteLine("SUCCESS:")
                    Console.WriteLine(responseText)
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





    End Sub




    'Public Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
    '    Return True
    'End Function

End Class