Public Class ContactsList

    Dim lstContactsList As New List(Of String())

    Private Sub ContactsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblWelcome.Text = "welcome " + Module1.GetUserName()

        'Get contacts
        'GetContacts()
    End Sub

    Private Sub ContactsList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        GetContacts()
    End Sub

    'Close hiden login form on exit
    Private Sub ContactsList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Module1.ClearUser()
        Login_Form.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim addContact As New AddContact
        addContact.Show()

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click

        Module1.ClearUser()
        Login_Form.Show()
        Me.Close()

    End Sub

    Private Sub lstContacts_Click(sender As Object, e As EventArgs) Handles lstContacts.Click
        Dim viewContact As New ViewContact

        Dim index As Integer
        index = lstContacts.SelectedIndex()

        If index <> -1 Then

            Module1.SaveIndex(index)
            viewContact.Show()
        End If

    End Sub

    Sub GetContacts()
        'Get contacts
        lstContacts.Items.Clear()
        lstContactsList.Clear()
        lstContactsList = Module1.GetContacts()

        For Each item In lstContactsList
            lstContacts.Items.Add(item(0))
        Next
    End Sub

End Class