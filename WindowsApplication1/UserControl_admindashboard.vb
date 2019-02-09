﻿Imports System.Runtime.InteropServices
Public Class UserControl_admindashboard

    <DllImport("user32.dll", EntryPoint:="SetProcessDPIAware")> _
    Private Shared Function SetProcessDPIAware() As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


    Private Sub max()
        Dim CW As Integer = Me.Width ' Current Width
        Dim CH As Integer = Me.Height ' Current Height
        Me.Size = New Size(CW * Form1.Width / 1920, CH * Form1.Height / 1080)
        Dim RW As Double = (Me.Width - CW) / CW ' Ratio change of width
        Dim RH As Double = (Me.Height - CH) / CH ' Ratio change of height

        For Each Ctrl As Control In Controls
            Ctrl.Width += CInt(Ctrl.Width * RW)
            Ctrl.Height += CInt(Ctrl.Height * RH)
            Ctrl.Left += CInt(Ctrl.Left * RW)
            Ctrl.Top += CInt(Ctrl.Top * RH)
            'Ctrl.Font = New Font(Ctrl.Font.Name, CInt(Ctrl.Font.Size * RW), Ctrl.Font.Style)
        Next
        CW = Me.Width
        CH = Me.Height
    End Sub

    Private Sub approveUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Hide()
        max()
        Try
            Me.UserTableTableAdapter.getNonApproved(Me.GuestHouseDataSet.userTable)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveReload_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        Dim ans As DialogResult
        ans = MessageBox.Show("Are You Sure?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ans = vbYes Then
            UserTableTableAdapter.Update(Me.GuestHouseDataSet.userTable)
            AdminDashboard.reload_data()
            Try
                Me.UserTableTableAdapter.getNonApproved(Me.GuestHouseDataSet.userTable)
            Catch ex As System.Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtRoomNo_TextChanged(sender As Object, e As EventArgs) Handles txtRoomNo.TextChanged

    End Sub

    Private Sub rbtnIsVIP_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnIsVIP.CheckedChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btnAddRemoveRoom_Click(sender As Object, e As EventArgs) Handles btnAddRemoveRoom.Click
        If rbtnAddARoomToService.Checked = True Then
            If rbtnIsVIP.Checked = True Then
                RoomTableAdapter1.AddRoom(txtRoomNo.Text, True)
            Else
                Try
                    RoomTableAdapter1.AddRoom(txtRoomNo.Text, False)
                Catch ex As Exception
                    MsgBox("Room Already Exists!")
                End Try
            End If
        Else
            RoomTableAdapter1.DeleteRoom(txtRoomNo.Text)
        End If
    End Sub
End Class
