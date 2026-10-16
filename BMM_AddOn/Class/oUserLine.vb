Public Class oUserLine
    Private _docEntry As String
    Private _rptCode As String
    Private _active As Integer
    Private _LineNum As String
    Private _header As oUser


    Public Property docEntry As String
        Get
            Return _docEntry
        End Get
        Set(ByVal value As String)
            _docEntry = value
        End Set
    End Property

    Public Property rptCode As String
        Get
            Return _rptCode
        End Get
        Set(ByVal value As String)
            _rptCode = value
        End Set
    End Property

    Public Property active As Integer
        Get
            Return _active
        End Get
        Set(ByVal value As Integer)
            _active = value
        End Set
    End Property

    Public Property LineNum As Integer
        Get
            Return _LineNum
        End Get
        Set(ByVal value As Integer)
            _LineNum = value
        End Set
    End Property



    Public Sub New(ByVal pHeader As oUser)
        _header = pHeader
    End Sub

    Private _Lines As New List(Of oUserLine)


    Public ReadOnly Property Count As Integer
        Get
            Return _Lines.Count
        End Get
    End Property


    

    Public Sub New()
        _docEntry = ""
        _rptCode = ""
        _active = 1
        _LineNum = 0
    End Sub

    Public Sub ClearAllLines()
        For i As Integer = _Lines.Count - 1 To 0 Step -1
            _Lines.RemoveAt(i)
        Next
    End Sub


    Public Sub Add()
        'If _ItemCode = "" Then

        _Lines.Add(New oUserLine(
                          _docEntry _
                        , _rptCode _
                        , _active _
                        , _LineNum))
        SortLineNum(_Lines)

    End Sub

    Public Sub New(
             ByVal pdocEntry As String _
            , ByVal prptCode As String _
            , ByVal pActive As Integer _
             , ByVal pLineNum As String _
            )
        _docEntry = pdocEntry
        _rptCode = prptCode
        _active = pActive
        _LineNum = pLineNum
    End Sub


    Public Function setLineData(ByVal LineNumber As Int64) As Integer
        Dim retval As Integer = -1
        Try
            _LineNum = LineNumber
            _docEntry = _Lines.Item(LineNumber - 1).docEntry
            _rptCode = _Lines.Item(LineNumber - 1).rptCode
            _active = _Lines.Item(LineNumber - 1).active
            retval = 0
        Catch ex As Exception
            retval = -1
        End Try
        Return retval
    End Function



    Private Sub SortLineNum(ByRef pLines As List(Of oUserLine))
        For i As Integer = 1 To pLines.Count Step 1
            pLines.Item(i - 1).LineNum = i
        Next
    End Sub

    Public Sub RemoveLine(ByVal LineNumber As Int64)
        _Lines.RemoveAt(LineNumber - 1)
        SortLineNum(_Lines)
    End Sub




End Class
