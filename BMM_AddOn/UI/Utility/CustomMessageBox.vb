Imports System.Windows.Forms


Module CustomMessageBox
    Private result As String
    Public Function Show(ByVal options As IEnumerable(Of String), Optional ByVal message As String = "", Optional ByVal title As String = "", Optional ByVal total As Integer = 0) As String
        result = "Cancel"
        Dim myForm As New Form With {.Text = title}
        Dim tlp As New TableLayoutPanel With {.ColumnCount = 1, .RowCount = total}
        Dim flp As New FlowLayoutPanel()
        Dim l As New Label With {.Text = message}
        myForm.Controls.Add(tlp)
        tlp.Dock = DockStyle.Fill
        tlp.Controls.Add(l)
        l.Dock = DockStyle.Fill
        tlp.Controls.Add(flp)
        flp.Dock = DockStyle.Fill
        For Each o In options
            Dim b As New Button With {.Text = o}
            flp.Controls.Add(b)
            AddHandler b.Click,
                Sub(sender As Object, e As EventArgs)
                    result = DirectCast(sender, Button).Text
                    myForm.Close()
                End Sub
        Next
        myForm.FormBorderStyle = FormBorderStyle.FixedDialog
        myForm.Height = 100
        myForm.StartPosition = FormStartPosition.CenterParent
        myForm.ShowDialog()
        Return result
    End Function
End Module
