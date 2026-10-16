Imports System.Xml

Public Class ApplicationConfig

    Private _Password As String
    Private _PortName As String
    Private _BaudRate As Integer
    Private _DataBits As Integer
    Private _ReadTimeout As Integer
    Private _Separator As Integer
    Private _PathMDB As String
    Private _ItemIssueCode As String
    Private _ItemIssueName As String
    Private _ItemReceiptCode As String
    Private _ItemReceiptName As String
    Private _TipeIssue As String
    Private _TipeReceipt As String
    Private _StartID As String
    Private _Timbangan As String
    Private _PathBag As String
    Private _Menu As String
    Private _ReportPath As String
    Private _PrinterDotMatrix As String
    Private _PrinterThermal As String
    Private _PathTimbanganL As String
    Private _PathTimbanganR As String
#Region "Properties"

    Public Property PathTimbanganL As String
        Get
            Return _PathTimbanganL
        End Get
        Set(ByVal value As String)
            _PathTimbanganL = value
        End Set
    End Property


    Public Property PathTimbanganR As String
        Get
            Return _PathTimbanganR
        End Get
        Set(ByVal value As String)
            _PathTimbanganR = value
        End Set
    End Property


    Public Property PortName() As String
        Get
            Return _PortName
        End Get
        Set(ByVal value As String)
            _PortName = value
        End Set
    End Property

    Public Property BaudRate() As Integer
        Get
            Return _BaudRate
        End Get
        Set(ByVal value As Integer)
            _BaudRate = value
        End Set
    End Property

    Public Property DataBits() As Integer
        Get
            Return _DataBits
        End Get
        Set(ByVal value As Integer)
            _DataBits = value
        End Set
    End Property

    Public Property ReadTimeout() As Integer
        Get
            Return _ReadTimeout
        End Get
        Set(ByVal value As Integer)
            _ReadTimeout = value
        End Set
    End Property

    Public Property Separator() As Integer
        Get
            Return _Separator
        End Get
        Set(ByVal value As Integer)
            _Separator = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property


    Public Property PathMDB As String
        Get
            Return _PathMDB
        End Get
        Set(ByVal value As String)
            _PathMDB = value
        End Set
    End Property


    Public Property ItemIssueCode As String
        Get
            Return _ItemIssueCode
        End Get
        Set(ByVal value As String)
            _ItemIssueCode = value
        End Set
    End Property

    Public Property ItemIssueName As String
        Get
            Return _ItemIssueName
        End Get
        Set(ByVal value As String)
            _ItemIssueName = value
        End Set
    End Property



    Public Property ItemReceiptCode As String
        Get
            Return _ItemReceiptCode
        End Get
        Set(ByVal value As String)
            _ItemReceiptCode = value
        End Set
    End Property

    Public Property ItemReceiptName As String
        Get
            Return _ItemReceiptName
        End Get
        Set(ByVal value As String)
            _ItemReceiptName = value
        End Set
    End Property

    Public Property TipeIssue As String
        Get
            Return _TipeIssue
        End Get
        Set(ByVal value As String)
            _TipeIssue = value
        End Set
    End Property

 Public Property TipeReceipt As String
        Get
            Return _TipeReceipt
        End Get
        Set(ByVal value As String)
            _TipeReceipt = value
        End Set
    End Property


    Public Property StartID As String
        Get
            Return _StartID
        End Get
        Set(ByVal value As String)
            _StartID = value
        End Set
    End Property

    Public Property Timbangan As String
        Get
            Return _Timbangan
        End Get
        Set(ByVal value As String)
            _Timbangan = value
        End Set
    End Property


    Public Property PathBag As String
        Get
            Return _PathBag
        End Get
        Set(ByVal value As String)
            _PathBag = value
        End Set
    End Property


    Public Property Menu As String
        Get
            Return _Menu
        End Get
        Set(ByVal value As String)
            _Menu = value
        End Set
    End Property


    Public Property ReportPath As String
        Get
            Return _ReportPath
        End Get
        Set(ByVal value As String)
            _ReportPath = value
        End Set
    End Property


    Public Property PrinterDotMatrix As String
        Get
            Return _PrinterDotMatrix
        End Get
        Set(ByVal value As String)
            _PrinterDotMatrix = value
        End Set
    End Property

    Public Property PrinterThermal As String
        Get
            Return _PrinterThermal
        End Get
        Set(ByVal value As String)
            _PrinterThermal = value
        End Set
    End Property
#End Region

   
 

    Public Function toXML(ByVal xmlDocument As XmlDocument) As XmlNode
        Dim xmlAppConfig As XmlNode = xmlDocument.CreateElement("APPLICATION")

        Dim xmlPassword As XmlNode = xmlDocument.CreateElement("PASSWORD")
        xmlPassword.InnerText = Trim(Password)
        xmlAppConfig.AppendChild(xmlPassword)

        Dim xmlPortName As XmlNode = xmlDocument.CreateElement("PORT_NAME")
        xmlPortName.InnerText = PortName.ToString.Trim
        xmlAppConfig.AppendChild(xmlPortName)

        Dim xmlBaudRate As XmlNode = xmlDocument.CreateElement("BAUD_RATE")
        xmlBaudRate.InnerText = BaudRate.ToString.Trim
        xmlAppConfig.AppendChild(xmlBaudRate)

        Dim xmlDataBits As XmlNode = xmlDocument.CreateElement("DATA_BITS")
        xmlDataBits.InnerText = DataBits.ToString.Trim
        xmlAppConfig.AppendChild(xmlDataBits)

        Dim xmlReadTimeout As XmlNode = xmlDocument.CreateElement("READ_TIMEOUT")
        xmlReadTimeout.InnerText = ReadTimeout.ToString.Trim
        xmlAppConfig.AppendChild(xmlReadTimeout)

        Dim xmlSeparator As XmlNode = xmlDocument.CreateElement("SEPARATOR")
        xmlSeparator.InnerText = Separator.ToString.Trim
        xmlAppConfig.AppendChild(xmlSeparator)

        Dim xmlPathMDB As XmlNode = xmlDocument.CreateElement("PathMDB")
        xmlPathMDB.InnerText = PathMDB.ToString.Trim
        xmlAppConfig.AppendChild(xmlPathMDB)


        Dim xmlItemIssueCode As XmlNode = xmlDocument.CreateElement("ItemIssueCode")
        xmlItemIssueCode.InnerText = ItemIssueCode.ToString.Trim
        xmlAppConfig.AppendChild(xmlItemIssueCode)



        Dim xmlItemIssueName As XmlNode = xmlDocument.CreateElement("ItemIssueName")
        xmlItemIssueName.InnerText = ItemIssueName.ToString.Trim
        xmlAppConfig.AppendChild(xmlItemIssueName)


        Dim xmlItemReceiptCode As XmlNode = xmlDocument.CreateElement("ItemReceiptCode")
        xmlItemReceiptCode.InnerText = ItemReceiptCode.ToString.Trim
        xmlAppConfig.AppendChild(xmlItemReceiptCode)



        Dim xmlItemReceiptName As XmlNode = xmlDocument.CreateElement("ItemReceiptName")
        xmlItemReceiptName.InnerText = ItemReceiptName.ToString.Trim
        xmlAppConfig.AppendChild(xmlItemReceiptName)



        Dim xmlTipeIssue As XmlNode = xmlDocument.CreateElement("TipeIssue")
        xmlTipeIssue.InnerText = TipeIssue.ToString.Trim
        xmlAppConfig.AppendChild(xmlTipeIssue)

        Dim xmlTipeReceipt As XmlNode = xmlDocument.CreateElement("TipeReceipt")
        xmlTipeReceipt.InnerText = TipeReceipt.ToString.Trim
        xmlAppConfig.AppendChild(xmlTipeReceipt)



        Dim xmlStartID As XmlNode = xmlDocument.CreateElement("StartID")
        xmlStartID.InnerText = StartID.ToString.Trim
        xmlAppConfig.AppendChild(xmlStartID)

        Dim xmlTimbangan As XmlNode = xmlDocument.CreateElement("Timbangan")
        xmlTimbangan.InnerText = Timbangan.ToString.Trim
        xmlAppConfig.AppendChild(xmlTimbangan)


        Dim xmlPathBag As XmlNode = xmlDocument.CreateElement("PathBag")
        xmlPathBag.InnerText = PathBag.ToString.Trim
        xmlAppConfig.AppendChild(xmlPathBag)

        Dim xmlMenu As XmlNode = xmlDocument.CreateElement("Menu")
        xmlMenu.InnerText = Menu.ToString.Trim
        xmlAppConfig.AppendChild(xmlMenu)


        Dim xmlPath As XmlNode = xmlDocument.CreateElement("ReportPath")
        xmlPath.InnerText = ReportPath.ToString.Trim
        xmlAppConfig.AppendChild(xmlPath)


        Dim xmlDot As XmlNode = xmlDocument.CreateElement("PrinterDotMatrix")
        xmlDot.InnerText = PrinterDotMatrix.ToString.Trim
        xmlAppConfig.AppendChild(xmlDot)


        Dim xmlThermal As XmlNode = xmlDocument.CreateElement("PrinterThermal")
        xmlThermal.InnerText = PrinterThermal.ToString.Trim
        xmlAppConfig.AppendChild(xmlThermal)


        Dim xmlPathTimbanganR As XmlNode = xmlDocument.CreateElement("PathTimbanganR")
        xmlPathTimbanganR.InnerText = PathTimbanganR.ToString.Trim
        xmlAppConfig.AppendChild(xmlPathTimbanganR)



        Dim xmlPathTimbanganL As XmlNode = xmlDocument.CreateElement("PathTimbanganL")
        xmlPathTimbanganL.InnerText = PathTimbanganL.ToString.Trim
        xmlAppConfig.AppendChild(xmlPathTimbanganL)


        Return xmlAppConfig

    End Function

End Class
