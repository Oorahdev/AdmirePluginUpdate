﻿Imports adm
Imports adm.AdmUtilities
Imports AdmireUpdate.Utilities

Public Class frmupdate
    Dim util As New Utilities
    Public host As admAdmireCustomModuleInterface.AdmireHostParameters
    Public Plugin As AdmireUpdate
    Public Shared result As String


    Public Sub New(admireHost As admAdmireCustomModuleInterface.AdmireHostParameters)
        host = admireHost
    End Sub

    Public Sub New(ByVal AdmirePlugin As AdmireUpdate)
        Try
            InitializeComponent()
            Plugin = AdmirePlugin
            host = AdmirePlugin.Host
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub


    Private Sub update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkForUpdate()
    End Sub


    Public Sub checkForUpdate()
        Dim arrargs As Object() = {host.SqlMaster.UserId}
        host.SqlMaster.PullDataTable("UpdatedDlls", "AdmireUpdatedDLLs", arrargs, True)
        result = Nz(host.SqlMaster.DataTables("UpdatedDlls").Rows(1).Item("CUDLL_name"), 0)
        If host.SqlMaster.DataTables("UpdatedDlls").Rows.Count > 0 Then
            Button1.Text = "Update Required"
            Button1.ForeColor = Color.White
            Button1.BackColor = Color.Red
        Else
            Button1.Text = "Up To Date"
            Button1.ForeColor = Color.White
            Button1.BackColor = Color.MediumSeaGreen
        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim exe = "C:\Users\lfireman\Documents\GitHub\updateAdmire\AwsConsoleApp1\bin\Debug\AwsConsoleApp1.exe"
            Dim args = result
            Process.Start(exe, args)
            'process.Start("C:\Users\lfireman\Documents\GitHub\updateAdmire\AwsConsoleApp1\bin\Debug\AwsConsoleApp1.exe")
        Catch ex As Exception
            Print("Could not reach the updated files")
        End Try
        Dim arrargs As Object() = {host.SqlMaster.UserId}
        host.SqlMaster.RunSql("AdmireUpdateDlls_lastUpdate", arrargs)
        Button1.Text = "Update Successful"
        Button1.ForeColor = Color.White
        Button1.BackColor = Color.MediumSeaGreen
    End Sub


End Class