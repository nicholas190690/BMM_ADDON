Public Class oUser
    Private _UNAME As String
    Private _UPASS As String
    Private _UAWB As Integer
    Private _UVWB As Integer
    Private _UADMWB As Integer
    Private _UARAW As Integer
    Private _UVRAW As Integer
    Private _UADMRAW As Integer
    Private _UABAG As Integer
    Private _UVBAG As Integer
    Private _UADMBAG As Integer
    Private _UACONFIG As Integer
    Private _UVCONFIG As Integer
    Private _UADMCONFIG As Integer
    Private _UAREPORT As Integer
    Private _UVREPORT As Integer
    Private _UADMREPORT As Integer
    Private _UACTIVE As Integer


    Public Property UACTIVE As String
        Get
            Return _UACTIVE
        End Get
        Set(ByVal value As String)
            _UACTIVE = value
        End Set
    End Property




    Public Property UNAME As String
        Get
            Return _UNAME
        End Get
        Set(ByVal value As String)
            _UNAME = value
        End Set
    End Property



    Public Property UPASS As String
        Get
            Return _UPASS
        End Get
        Set(ByVal value As String)
            _UPASS = value
        End Set
    End Property




   

    Public Property UAWB As Integer
        Get
            Return _UAWB
        End Get
        Set(ByVal value As Integer)
            _UAWB = value
        End Set
    End Property

    Public Property UVWB As Integer
        Get
            Return _UVWB
        End Get
        Set(ByVal value As Integer)
            _UVWB = value
        End Set
    End Property


    Public Property UADMWB As Integer
        Get
            Return _UADMWB
        End Get
        Set(ByVal value As Integer)
            _UADMWB = value
        End Set
    End Property

    Public Property UARAW As Integer
        Get
            Return _UARAW
        End Get
        Set(ByVal value As Integer)
            _UARAW = value
        End Set
    End Property


    Public Property UVRAW As Integer
        Get
            Return _UVRAW
        End Get
        Set(ByVal value As Integer)
            _UVRAW = value
        End Set
    End Property


    Public Property UADMRAW As Integer
        Get
            Return _UADMRAW
        End Get
        Set(ByVal value As Integer)
            _UADMRAW = value
        End Set
    End Property


    Public Property UABAG As Integer
        Get
            Return _UABAG
        End Get
        Set(ByVal value As Integer)
            _UABAG = value
        End Set
    End Property


    Public Property UVBAG As Integer
        Get
            Return _UVBAG
        End Get
        Set(ByVal value As Integer)
            _UVBAG = value
        End Set
    End Property

    Public Property UADMBAG As Integer
        Get
            Return _UADMBAG
        End Get
        Set(ByVal value As Integer)
            _UADMBAG = value
        End Set
    End Property

    Public Property UACONFIG As Integer
        Get
            Return _UACONFIG
        End Get
        Set(ByVal value As Integer)
            _UACONFIG = value
        End Set
    End Property



    Public Property UVCONFIG As Integer
        Get
            Return _UVCONFIG
        End Get
        Set(ByVal value As Integer)
            _UVCONFIG = value
        End Set
    End Property


    Public Property UADMCONFIG As Integer
        Get
            Return _UADMCONFIG
        End Get
        Set(ByVal value As Integer)
            _UADMCONFIG = value
        End Set
    End Property


    Public Property UAREPORT As Integer
        Get
            Return _UAREPORT
        End Get
        Set(ByVal value As Integer)
            _UAREPORT = value
        End Set
    End Property



    Public Property UVREPORT As Integer
        Get
            Return _UVREPORT
        End Get
        Set(ByVal value As Integer)
            _UVREPORT = value
        End Set
    End Property


    Public Property UADMREPORT As Integer
        Get
            Return _UADMREPORT
        End Get
        Set(ByVal value As Integer)
            _UADMREPORT = value
        End Set
    End Property

    Public Sub New()
        _UNAME = ""
        _UPASS = ""
        _UAWB = 0
        _UVWB = 0
        _UADMWB = 0
        _UARAW = 0
        _UVRAW = 0
        _UADMRAW = 0
        _UABAG = 0
        _UVBAG = 0
        _UADMBAG = 0
        _UACONFIG = 0
        _UVCONFIG = 0
        _UADMCONFIG = 0
        _UAREPORT = 0
        _UVREPORT = 0
        _UADMREPORT = 0
        _UACTIVE = -1
        _Lines.ClearAllLines()
    End Sub

    Private _Lines As New oUserLine(Me)

    Public ReadOnly Property Lines As oUserLine
        Get
            Return _Lines
        End Get
    End Property

    Public Function checkPassword(ByVal username As String, ByVal password As String) As Integer
        'Dim sQuery As String = Queries.StockTransferList(DocNum)

        Dim oDT As New DataTable

        oDT = oDBConnection.executeQuery("SELECT * FROM " & oSBOConnection.DatabaseName.ToString & ".""@BMM_USER"" WHERE ""U_BMM_USER"" = '" & username & "' AND ""U_BMM_PASS"" = '" & password & "'")
        If oDT.Rows.Count > 0 Then
            Return 1
        Else
            Return -1
        End If
    End Function

    Public Function getByKey(ByVal username As String, ByVal password As String) As Integer
        Dim retval As Integer = -1
        'Dim sQuery As String = Queries.StockTransferList(DocNum)

        Dim oDT As New DataTable

        oDT = oDBConnection.executeQuery("SELECT * FROM " & oSBOConnection.DatabaseName.ToString & ".""@BMM_USER"" WHERE ""U_BMM_USER"" = '" & username & "' AND ""U_BMM_PASS"" = '" & password & "'")


        If oDT.Rows.Count > 0 Then
            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_USER")) Then
                _UNAME = oDT.Rows(0).Item("U_BMM_USER")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_PASS")) Then
                _UPASS = oDT.Rows(0).Item("U_BMM_PASS")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_AWB")) Then
                _UAWB = oDT.Rows(0).Item("U_BMM_AWB")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_VWB")) Then
                _UVWB = oDT.Rows(0).Item("U_BMM_VWB")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ADMWB")) Then
                _UADMWB = oDT.Rows(0).Item("U_BMM_ADMWB")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ARAW")) Then
                _UARAW = oDT.Rows(0).Item("U_BMM_ARAW")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_VRAW")) Then
                _UVRAW = oDT.Rows(0).Item("U_BMM_VRAW")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ADMRAW")) Then
                _UADMRAW = oDT.Rows(0).Item("U_BMM_ADMRAW")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ABAG")) Then
                _UABAG = oDT.Rows(0).Item("U_BMM_ABAG")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_VBAG")) Then
                _UVBAG = oDT.Rows(0).Item("U_BMM_VBAG")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ADMBAG")) Then
                _UADMBAG = oDT.Rows(0).Item("U_BMM_ADMBAG")
            End If

           

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ACONFIG")) Then
                _UACONFIG = oDT.Rows(0).Item("U_BMM_ACONFIG")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_VCONFIG")) Then
                _UVCONFIG = oDT.Rows(0).Item("U_BMM_VCONFIG")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ADMCONFIG")) Then
                _UADMCONFIG = oDT.Rows(0).Item("U_BMM_ADMCONFIG")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ARPT")) Then
                _UAREPORT = oDT.Rows(0).Item("U_BMM_ARPT")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_VRPT")) Then
                _UVREPORT = oDT.Rows(0).Item("U_BMM_VRPT")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ADMRPT")) Then
                _UADMREPORT = oDT.Rows(0).Item("U_BMM_ADMRPT")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ACTIVE")) Then
                _UACTIVE = oDT.Rows(0).Item("U_BMM_ACTIVE")
            End If

            retval = _UACTIVE
        Else
            retval = -1

        End If
        Return retval

    End Function

    


    Public Function Add() As Integer
        Try
            Dim oCompany As SAPbobsCOM.Company
           
            Dim LastEntry As Int64 = -1
            Dim sErrMsg As String = ""
            oCompany = oSBOConnection.Company
            oCompany.StartTransaction()

            Dim oUserTable As SAPbobsCOM.UserTable
            oUserTable = oCompany.UserTables.Item("BMM_USER")


            oUserTable.Code = UNAME
            oUserTable.Name = UNAME
            'Set user field
            oUserTable.UserFields.Fields.Item("U_BMM_USER").Value = UNAME
            oUserTable.UserFields.Fields.Item("U_BMM_PASS").Value = UPASS

            oUserTable.UserFields.Fields.Item("U_BMM_VCONFIG").Value = UVCONFIG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ACONFIG").Value = UACONFIG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ADMCONFIG").Value = UADMCONFIG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_VRAW").Value = UVRAW.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ARAW").Value = UARAW.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ADMRAW").Value = UADMRAW.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_VBAG").Value = UVBAG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ABAG").Value = UABAG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ADMBAG").Value = UADMBAG.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_VRPT").Value = UVREPORT.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ARPT").Value = UAREPORT.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ADMRPT").Value = UADMREPORT.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_VWB").Value = UVWB.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_AWB").Value = UAWB.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ADMWB").Value = UADMWB.ToString
            oUserTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = UACTIVE.ToString



          

            For i As Integer = 1 To _Lines.Count
                Dim oUserMappingTable As SAPbobsCOM.UserTable
                oUserMappingTable = oCompany.UserTables.Item("BMM_RPT_MAP")
                _Lines.setLineData(i)
                If _Lines.active = 0 Then
                    Continue For
                End If
                oUserMappingTable.UserFields.Fields.Item("U_BMM_USER").Value = UNAME
                oUserMappingTable.UserFields.Fields.Item("U_BMM_RPT").Value = _Lines.rptCode
                oUserMappingTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = _Lines.active.ToString
                oUserMappingTable.Add()
            Next

            LastEntry = oUserTable.Add()
            If LastEntry = 0 Then
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            Else
                oCompany.GetLastError(LastEntry, sErrMsg)
                Throw New System.Exception("Error : " + sErrMsg.ToString)
            End If
            Return LastEntry
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            Return -1
        End Try





    End Function


    Public Function update()
        Try
            Dim oCompany As SAPbobsCOM.Company

            Dim LastEntry As Int64 = -1
            Dim sErrMsg As String = ""
            oCompany = oSBOConnection.Company
            oCompany.StartTransaction()

            Dim oUserTable As SAPbobsCOM.UserTable
            oUserTable = oCompany.UserTables.Item("BMM_USER")
            If oUserTable.GetByKey(UNAME) Then
                oUserTable.UserFields.Fields.Item("U_BMM_PASS").Value = UPASS

                oUserTable.UserFields.Fields.Item("U_BMM_VCONFIG").Value = UVCONFIG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ACONFIG").Value = UACONFIG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ADMCONFIG").Value = UADMCONFIG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_VRAW").Value = UVRAW.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ARAW").Value = UARAW.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ADMRAW").Value = UADMRAW.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_VBAG").Value = UVBAG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ABAG").Value = UABAG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ADMBAG").Value = UADMBAG.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_VRPT").Value = UVREPORT.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ARPT").Value = UAREPORT.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ADMRPT").Value = UADMREPORT.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_VWB").Value = UVWB.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_AWB").Value = UAWB.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ADMWB").Value = UADMWB.ToString
                oUserTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = UACTIVE.ToString

                For i As Integer = 1 To _Lines.Count
                    Dim oUserMappingTable As SAPbobsCOM.UserTable
                    oUserMappingTable = oCompany.UserTables.Item("BMM_RPT_MAP")
                    _Lines.setLineData(i)
                    If _Lines.active = 1 Then
                        If _Lines.docEntry <> "0" Then
                            oUserMappingTable.GetByKey(_Lines.docEntry)
                        End If
                    Else
                        If _Lines.docEntry <> "0" Then
                            oUserMappingTable.GetByKey(_Lines.docEntry)
                            'active lama
                        Else
                            Continue For
                        End If
                    End If
                    oUserMappingTable.UserFields.Fields.Item("U_BMM_USER").Value = UNAME
                    oUserMappingTable.UserFields.Fields.Item("U_BMM_RPT").Value = _Lines.rptCode
                    oUserMappingTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = _Lines.active.ToString
                    If _Lines.docEntry <> "0" Then
                        oUserMappingTable.Update()
                    Else
                        oUserMappingTable.Add()
                    End If

                Next

                LastEntry = oUserTable.Update()
                If LastEntry = 0 Then
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                Else
                    oCompany.GetLastError(LastEntry, sErrMsg)
                    Throw New System.Exception("Error : " + sErrMsg.ToString)
                End If
                Return LastEntry
            Else
                Throw New System.Exception("Error : Data tidak di temukan")
            End If
            Return 1
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            Return -1
        End Try
    End Function
End Class
