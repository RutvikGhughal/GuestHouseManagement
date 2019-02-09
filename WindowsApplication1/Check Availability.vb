﻿Public Class Check_Availability

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
            Ctrl.Font = New Font(Ctrl.Font.Name, CInt(Ctrl.Font.Size * (RW + 1)), Ctrl.Font.Style)
        Next
        CW = Me.Width
        CH = Me.Height
    End Sub

    Private Sub Booking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        max()
    End Sub

    Private Sub btnCheckAval_Click(sender As Object, e As EventArgs) Handles btnCheckAval.Click
        BookingTableAdapter1.BookRoom("A10", "sdfs", CInt(DateTimePickerFrom.Value.ToString("yyyyMMdd")), CInt(DateTimePickerTo.Value.ToString("yyyyMMdd")))
        Dim timeFrom As Integer = CInt(DateTimePickerFrom.Value.ToString("yyyyMMdd"))
        Dim timeTo As Integer = CInt(DateTimePickerTo.Value.ToString("yyyyMMdd"))
    End Sub
End Class