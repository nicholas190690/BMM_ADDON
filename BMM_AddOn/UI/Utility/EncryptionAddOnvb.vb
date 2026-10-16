Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Public Class EncryptionAddOnvb
    Public Shared Function Encrypt(ByVal sText As String, ByVal sKey As String) As String
        Dim RetVal As String = ""
        Dim EncryptionKey As String = sKey
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(sText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                RetVal = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using


        Return RetVal
    End Function

    Public Shared Function Decrypt(ByVal sText As String, ByVal sKey As String) As String
        Dim RetVal
        Dim EncryptionKey As String = sKey
        Dim cipherBytes As Byte() = Convert.FromBase64String(sText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                RetVal = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return RetVal
    End Function
End Class
