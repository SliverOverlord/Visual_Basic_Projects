Public Class AddContact

    Dim contactName As String
    Dim phone As String = ""
    Dim email As String = ""
    Dim birthday As String = ""
    Dim address As String = ""
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = String.Empty Then
            MessageBox.Show("No input for contact Name")
        Else
            contactName = txtName.Text

            phone = txtPhone.Text
            email = txtEmail.Text
            birthday = txtBirthday.Text
            address = txtAddress.Text

            Module1.AddContact(contactName, phone, email, birthday, address)
            Me.Close()
        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Sub reset()

        'Clear textboxes
        txtName.Clear()
        txtPhone.Clear()
        txtEmail.Clear()
        txtBirthday.Clear()
        txtAddress.Clear()

        txtName.Select()
    End Sub
End Class