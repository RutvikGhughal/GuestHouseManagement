﻿Public Class Dashboard

    Private Sub max()
        Dim CW As Integer = Me.Width ' Current Width
        Dim CH As Integer = Me.Height ' Current Height
        Me.WindowState = FormWindowState.Maximized
        Dim RW As Double = (Me.Width - CW) / CW ' Ratio change of width
        Dim RH As Double = (Me.Height - CH) / CH ' Ratio change of height

        For Each Ctrl As Control In Controls
            Ctrl.Width += CInt(Ctrl.Width * RW)
            Ctrl.Height += CInt(Ctrl.Height * RH)
            Ctrl.Left += CInt(Ctrl.Left * RW)
            Ctrl.Top += CInt(Ctrl.Top * RH)
        Next
        CW = Me.Width
        CH = Me.Height
    End Sub

    Private Sub Dashboard_closing(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Form1.Close()
    End Sub

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        max()
    End Sub
End Class