'Author: Joshua DeNio
'Date: 11/10/2021
'Description:
' This class creats a new user if the username is not already in use.
Public Class NewUser

    'Load event
    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Retrive user list
        Module1.GetUsers()
    End Sub

    Private Sub btnCreateUser_Click(sender As Object, e As EventArgs) Handles btnCreateUser.Click
        'Add new user

        'Check if input is valid
        If txtUsername.Text = String.Empty Then
            MessageBox.Show("Please enter a username")
        ElseIf txtPassword.Text = String.Empty Then
            MessageBox.Show("Please enter a password")
        Else
            'Get info
            Dim userName As String = txtUsername.Text
            Dim password As String = txtPassword.Text

            'Check if the username exists
            'If the user exists output message else create a new user.
            If FindUsername(userName) Then

                MessageBox.Show("That Username already exists on this device")

                txtUsername.Clear()
                txtPassword.Clear()

                txtUsername.Select()

            Else
                'Create a new user
                Module1.AddUser(userName, password)
                Me.Close()
            End If

        End If

    End Sub

End Class