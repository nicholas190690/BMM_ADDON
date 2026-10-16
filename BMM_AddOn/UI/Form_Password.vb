Public Class Form_Password

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()

    End Sub

    Private Sub btn_confirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_confirm.Click
        Dim isoke As Integer
        '  isoke = ClassUser.checkPassword(txt_user.Text, EncryptionAddOnvb.Decrypt(txt_pass.Text, "soltius"))
        If ClassUser.UPASS <> EncryptionAddOnvb.Encrypt(txt_pass.Text, "soltius") Then
            MessageBox.Show("Password Lama tidak sesuai")
            Return
        End If
        If txt_new.Text <> txt_pass_conf.Text Then
            MessageBox.Show("Password baru tidak sesuai")
            Return
        End If
        If txt_new.Text.Length <= 4 Then
            MessageBox.Show("Password harus lebih dari 4 digit!")
            Return
        End If
        ClassUser.UPASS = EncryptionAddOnvb.Encrypt(txt_new.Text, "soltius")
        isoke = ClassUser.update()
        If isoke < 0 Then
            MessageBox.Show("Gagal Ganti password!")
        Else
            MessageBox.Show("Password Berhasil di ganti!")
            Me.Close()

        End If
    End Sub

    Private Sub Form_Password_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txt_user.Text = ClassUser.UNAME
        txt_pass.Focus()
    End Sub

    Private Sub txt_pass_conf_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_pass_conf.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            btn_confirm_Click(sender, e)
        End If
    End Sub

    Private Sub txt_pass_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_pass.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            txt_new.Focus()
        End If


    End Sub


    Private Sub txt_new_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_new.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            txt_pass_conf.Focus()
        End If

    End Sub
End Class