Public Class EditContact
    Dim selection As Integer
    Dim contact(5) As String
    Private Sub EditContact_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selection = Module1.GetIndex()
        FillForm()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'Get values
        Dim name As String = txtName.Text
        Dim phone As String = txtPhone.Text
        Dim email As String = txtEmail.Text
        Dim birthday As String = txtBirthday.Text
        Dim address As String = txtAddress.Text

        Dim result As Boolean = Module1.EditContact(name, phone, email, birthday, address)

        If result = True Then

            Me.Close()
        Else
            MessageBox.Show("Error editing contact")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim deleted As Boolean = Module1.DeleteContact(selection)

        If deleted = True Then
            MessageBox.Show("The selected contact has been removed")
            Me.Close()
        Else
            MessageBox.Show("Failure to delete selected contact")
        End If
    End Sub

    Sub FillForm()

        contact = Module1.GetContactData(selection)
        txtName.Text = contact(0)
        txtPhone.Text = contact(1)
        txtEmail.Text = contact(2)
        txtBirthday.Text = contact(3)
        txtAddress.Text = contact(4)

    End Sub


End Class