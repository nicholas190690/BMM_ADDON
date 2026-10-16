Public Class oReport

    Private _NAME As String
    Private _DESCRIPTION As String
    Private _PATH As String
    Private _ACTIVE As Integer

    Public Property NAME As String
        Get
            Return _NAME
        End Get
        Set(ByVal value As String)
            _NAME = value
        End Set
    End Property

    Public Property DESCRIPTION As String
        Get
            Return _DESCRIPTION
        End Get
        Set(ByVal value As String)
            _DESCRIPTION = value
        End Set
    End Property

    Public Property PATH As String
        Get
            Return _PATH
        End Get
        Set(ByVal value As String)
            _PATH = value
        End Set
    End Property


    Public Property ACTIVE As String
        Get
            Return _ACTIVE
        End Get
        Set(ByVal value As String)
            _ACTIVE = value
        End Set
    End Property


    Public Sub New()
        _NAME = ""
        _DESCRIPTION = ""
        _PATH = ""
        _ACTIVE = 0
    End Sub



    Public Function getByKey(ByVal code As String) As Integer
        Dim retval As Integer = -1


        Dim oDT As New DataTable

        oDT = oDBConnection.executeQuery("SELECT * FROM " & oSBOConnection.DatabaseName.ToString & ".""@BMM_REPORT"" WHERE ""Code"" = '" & code & "'")


        If oDT.Rows.Count > 0 Then
            If Not IsDBNull(oDT.Rows(0).Item("Name")) Then
                _NAME = oDT.Rows(0).Item("Name")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_DESC")) Then
                _DESCRIPTION = oDT.Rows(0).Item("U_DESC")
            End If


            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_PATH")) Then
                _PATH = oDT.Rows(0).Item("U_BMM_PATH")
            End If

            If Not IsDBNull(oDT.Rows(0).Item("U_BMM_ACTIVE")) Then
                _ACTIVE = oDT.Rows(0).Item("U_BMM_ACTIVE")
            End If


          
            retval = _ACTIVE
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
            oUserTable = oCompany.UserTables.Item("BMM_REPORT")
            oUserTable.Name = NAME
            oUserTable.UserFields.Fields.Item("U_BMM_DESC").Value = DESCRIPTION
            oUserTable.UserFields.Fields.Item("U_BMM_PATH").Value = PATH
            oUserTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = ACTIVE

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



    Public Function update(ByVal code As String) As Integer
        Try
            Dim oCompany As SAPbobsCOM.Company

            Dim LastEntry As Int64 = -1
            Dim sErrMsg As String = ""
            oCompany = oSBOConnection.Company
            oCompany.StartTransaction()

            Dim oUserTable As SAPbobsCOM.UserTable
            oUserTable = oCompany.UserTables.Item("BMM_USER")
            If oUserTable.GetByKey(code) Then
                oUserTable.UserFields.Fields.Item("U_BMM_DESC").Value = DESCRIPTION
                oUserTable.UserFields.Fields.Item("U_BMM_PATH").Value = PATH
                oUserTable.UserFields.Fields.Item("U_BMM_ACTIVE").Value = ACTIVE

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
