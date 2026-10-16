Imports System.Xml

Public Class ApplicationXMLHandler
    Inherits XMLHandler

    Public Overrides Function handle(ByVal xmlNode As XmlNode) As Object
        If Not String.Equals("APPLICATION", xmlNode.Name) Then
            Return Nothing
        End If

        Dim Application As ApplicationConfig
        Application = New ApplicationConfig()

        Dim xmlNodeEnumerator As IEnumerator = xmlNode.ChildNodes.GetEnumerator
        While (xmlNodeEnumerator.MoveNext)
            Dim xmlElement As XmlElement
            xmlElement = CType(xmlNodeEnumerator.Current, XmlElement)

            Dim name As String = xmlElement.Name
            Dim value As String = xmlElement.InnerText

            If String.Equals("PORT_NAME", name) Then
                If Not Application Is Nothing Then
                    Application.PortName = value
                End If
            ElseIf String.Equals("BAUD_RATE", name) Then
                If Not Application Is Nothing Then
                    Application.BaudRate = value
                End If
            ElseIf String.Equals("DATA_BITS", name) Then
                If Not Application Is Nothing Then
                    Application.DataBits = value
                End If
            ElseIf String.Equals("READ_TIMEOUT", name) Then
                If Not Application Is Nothing Then
                    Application.ReadTimeout = value
                End If
            ElseIf String.Equals("SEPARATOR", name) Then
                If Not Application Is Nothing Then
                    Application.Separator = value
                End If
            ElseIf String.Equals("PathMDB", name) Then
                If Not Application Is Nothing Then
                    Application.PathMDB = value
                End If

            ElseIf String.Equals("ItemIssueCode", name) Then
                If Not Application Is Nothing Then
                    Application.ItemIssueCode = value
                End If


            ElseIf String.Equals("ItemIssueName", name) Then
                If Not Application Is Nothing Then
                    Application.ItemIssueName = value
                End If


            ElseIf String.Equals("ItemReceiptCode", name) Then
                If Not Application Is Nothing Then
                    Application.ItemReceiptCode = value
                End If


            ElseIf String.Equals("ItemReceiptName", name) Then
                If Not Application Is Nothing Then
                    Application.ItemReceiptName = value
                End If

            ElseIf String.Equals("TipeIssue", name) Then
                If Not Application Is Nothing Then
                    Application.TipeIssue = value
                End If


            ElseIf String.Equals("TipeReceipt", name) Then
                If Not Application Is Nothing Then
                    Application.TipeReceipt = value
                End If


            ElseIf String.Equals("StartID", name) Then
                If Not Application Is Nothing Then
                    Application.StartID = value
                End If

            ElseIf String.Equals("Timbangan", name) Then
                If Not Application Is Nothing Then
                    Application.Timbangan = value
                End If


            ElseIf String.Equals("PathBag", name) Then
                If Not Application Is Nothing Then
                    Application.PathBag = value
                End If

            ElseIf String.Equals("Menu", name) Then
                If Not Application Is Nothing Then
                    Application.Menu = value
                End If

            ElseIf String.Equals("ReportPath", name) Then
                If Not Application Is Nothing Then
                    Application.ReportPath = value
                End If

            ElseIf String.Equals("PrinterDotMatrix", name) Then
                If Not Application Is Nothing Then
                    Application.PrinterDotMatrix = value
                End If

            ElseIf String.Equals("PrinterThermal", name) Then
                If Not Application Is Nothing Then
                    Application.PrinterThermal = value
                End If

            ElseIf String.Equals("PathTimbanganL", name) Then
                If Not Application Is Nothing Then
                    Application.PathTimbanganL = value
                End If

            ElseIf String.Equals("PathTimbanganR", name) Then
                If Not Application Is Nothing Then
                    Application.PathTimbanganR = value
                End If


            End If



        End While

        Return Application
    End Function

End Class
