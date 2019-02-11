﻿Imports System.Net.Mail

Public Class forgotPass

    Private Function genToken() As String
        Dim AllLetters As String = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Dim Str As String = ""
        Dim R As New Random
        For i As Integer = 0 To 10
            Str = Str + AllLetters(R.Next(0, 60))
        Next
        Return Str
    End Function

    Private Function sendMail(token As String, email As String) As String
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("iitgguesthouse@gmail.com", "MMMM1234")

            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("iitgguesthouse@gmail.com")
            e_mail.To.Add(email)
            e_mail.Subject = "Password Recovery"
            e_mail.IsBodyHtml = False
            e_mail.Body = "Enter This Token into the App: " & token & " Do Not Share With Anyone!" 'Body Here' 
            Smtp_Server.Send(e_mail)
    End Function

    Private Sub btnForgot_Click(sender As Object, e As EventArgs) Handles btnForgot.Click
        Dim Rows As guestHouseDataSet.userTableDataTable
        Dim user As guestHouseDataSet.userTableRow


        Rows = UserTableTableAdapter1.GetUserData(txtEmail.Text)
        If Rows.Rows.Count = 0 Then
            MsgBox("Email Not Registered!")
        Else
            user = Rows.Rows(0)
            Dim token As String = genToken()
            If ForgotPassTableAdapter.getToken(txtEmail.Text) <> Nothing Then
                Dim question As Integer
                question = MessageBox.Show("Already Sent the Token! Do You Want to Resend the Token", "Want To Resend", MessageBoxButtons.YesNo)

                If question = vbNo Then
                    ' Do Something Maybe? '
                Else
                    ForgotPassTableAdapter.DeleteToken(txtEmail.Text)
                    btnForgot.PerformClick()
                End If

            Else
                Try
                    sendMail(token, txtEmail.Text) ' Send Mail ' 
                    ForgotPassTableAdapter.insertToken(txtEmail.Text, token)
                    MsgBox("Token Generated! Please Check Your Mail!")
                    txtOTP.Show()
                    lblOTP.Show()
                    btnVerify.Show()
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        End If

    End Sub

    Private Sub btnVerify_Click(sender As Object, e As EventArgs) Handles btnVerify.Click
        Dim Rows As guestHouseDataSet.userTableDataTable
        Dim user As guestHouseDataSet.userTableRow


        Rows = UserTableTableAdapter1.GetUserData(txtEmail.Text)
        If Rows.Rows.Count = 0 Then
            MsgBox("Email Not Registered!")
        Else
            user = Rows.Rows(0)
            If ForgotPassTableAdapter.getToken(user.username) = txtOTP.Text Then
                'Allow Him to Generate New Password!'
                formUpdatePassword.forgottenPassword = True
                formUpdatePassword.loggedUser = user.username
                Me.Close()
                formUpdatePassword.Show()
            Else
                MsgBox("Invalid Token!")
            End If
        End If
    End Sub
End Class