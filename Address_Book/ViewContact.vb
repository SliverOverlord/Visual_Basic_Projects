Public Class ViewContact

    Dim selection As Integer
    Dim contact(5) As String

    Private Sub ViewContact_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selection = Module1.GetIndex()

    End Sub

    Private Sub ViewContact_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        FillForm()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim editContact As New EditContact
        editContact.Show()
        Me.Close()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Sub FillForm()
        contact = Module1.GetContactData(selection)
        lblName.Text = contact(0)
        lblPhone.Text = contact(1)
        lblEmail.Text = contact(2)
        lblBirthday.Text = contact(3)
        lblAddress.Text = contact(4)

    End Sub

End Class