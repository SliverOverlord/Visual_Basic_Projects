Public Class Login_Form

    Private Sub Login_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.InitFile()
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'Check for empty input
        If txtUsername.Text = String.Empty Then
            MessageBox.Show("Please enter a username")
        ElseIf txtPassword.Text = String.Empty Then
            MessageBox.Show("Please enter a password")
        Else
            'Get info
            Dim userName As String = txtUsername.Text
            Dim password As String = txtPassword.Text

            'LoadusersList and check if the user is valid
            Module1.GetUsers()
            If Module1.ValidateUser(userName, password) Then

                'Open contactList
                Dim contactList As New ContactsList
                Reset()
                Me.Hide()
                contactList.Show()

            Else
                MessageBox.Show("Login failed, invalid login")
                reset()
            End If
            'If valid login and direct to ContactsList form.
        End If

    End Sub

    Private Sub btnNewUser_Click(sender As Object, e As EventArgs) Handles btnNewUser.Click
        ' direct user to newUser form
        Dim newUser As New NewUser
        newUser.Show()

    End Sub

    Sub Reset()
        txtUsername.Clear()
        txtPassword.Clear()

        txtUsername.Select()
    End Sub

End Class