'Author: Joshua DeNio
'Date: 05/27/2021
'Description:
' This application is a simple address book. Multiple users can have seperate accounts within the application and their own address books.

Imports System.IO
Imports System.Security.Cryptography
Module Module1

    ' Variable declairations
    Const intMaxSUBSCRIPT As Integer = 2 ' used for user array 
    Const intMaxSUBSCRIPT_CONTACTS As Integer = 5 ' used for contacts array size

    Dim lstUserList As New List(Of String())
    Dim lstContactsList As New List(Of String())

    Dim user As String
    Dim selection As Integer
    Dim strContact(intMaxSUBSCRIPT_CONTACTS) As String
    Dim fileName As String = "Users.txt"


    Dim ContactsFile As StreamReader
    ' End of variable declairations

    ' Function deffinitions ---------------------------------------------

    'Name: FindUsername
    'Input: string username
    'Output: bool
    'Precondition: must have input and lstUserList must be populated
    'Postcondition: none
    Function FindUsername(ByVal username) As Boolean

        Dim found As Boolean = False

        For Each entry In lstUserList
            If entry(0) = username Then
                found = True
            End If
        Next

        Return found
    End Function

    Function ValidateUser(ByVal username, ByVal password) As Boolean

        Dim validity As Boolean = False

        For Each entry In lstUserList
            If entry(0) = username And entry(1) = password Then
                validity = True
            End If
        Next

        If validity = True Then
            'Set username
            user = username

            'Retrive contacts
            RetrieveContacts(username)
        End If

        Return validity
    End Function

    Function GetUserName() As String
        Return user
    End Function

    Function GetContacts() As List(Of String())

        'Update contacts List
        RetrieveContacts(user)

        Return lstContactsList
    End Function

    Function GetIndex()
        Return selection
    End Function

    Function GetContactData(ByVal index)

        Return lstContactsList.Item(index)
    End Function

    Function DeleteContact(ByVal index) As Boolean
        Dim sucess As Boolean = False

        Try
            'Remove and rewrite contacts file
            lstContactsList.RemoveAt(index)

            RewriteContactFile()

            sucess = True

        Catch ex As Exception

        End Try

        Return sucess
    End Function

    Function EditContact(ByVal name, ByVal phone, ByVal email, ByVal birthday, ByVal address) As Boolean
        Dim sucess As Boolean = False

        Try
            'Remove and rewrite contacts file

            'Remove the original version
            lstContactsList.RemoveAt(selection)

            'Add the edited contact
            LogContact(name, phone, email, birthday, address)
            lstContactsList.Add(strContact)

            'Resave the contact list
            RewriteContactFile()

            sucess = True

        Catch ex As Exception

        End Try

        Return sucess
    End Function

    ' End function definitions ------------------------------------------


    ' Procedure definitions ---------------------------------------------
    Sub GetUsers()

        Dim UsersFile As StreamReader
        UsersFile = File.OpenText(fileName)

        Do Until UsersFile.EndOfStream
            'use sha256 to decrypt files.********************************************

            Dim entry(intMaxSUBSCRIPT) As String

            Dim username As String
            Dim password As String

            ' Read in a users data
            username = UsersFile.ReadLine()
            password = UsersFile.ReadLine()

            'Decrypt the data***

            entry(0) = username
            entry(1) = password

            lstUserList.Add(entry)
        Loop

        UsersFile.Close()

    End Sub

    Sub AddUser(ByVal username, ByVal password)

        Dim userFile As StreamWriter

        userFile = File.AppendText(fileName)
        'userFile.WriteLine(entry)

        'Add username and password
        userFile.WriteLine(username)
        userFile.WriteLine(password)

        'Close the file
        userFile.Close()

        'Create contact file
        CreateContactlistFile(username)

    End Sub

    Sub CreateContactlistFile(ByVal username)
        Dim newFile As StreamWriter

        Dim fileName As String = username + "_contacts.txt"

        'Create contact file
        Try
            newFile = File.CreateText(fileName)
            newFile.Close()

        Catch ex As Exception

            MessageBox.Show("Error creating contact file")

        End Try
    End Sub

    Sub RewriteContactFile()
        Dim newFile As StreamWriter

        Dim fileName As String = user + "_contacts.txt"

        'Create contact file
        Try
            newFile = File.CreateText(fileName)

            For Each contact In lstContactsList
                newFile.WriteLine(contact(0)) 'Name
                newFile.WriteLine(contact(1)) 'Phone
                newFile.WriteLine(contact(2)) 'Email
                newFile.WriteLine(contact(3)) 'Birthday
                newFile.WriteLine(contact(4)) 'Address

            Next

            newFile.Close()

        Catch ex As Exception

            MessageBox.Show("Error overwriting contact file")

        End Try
    End Sub

    'Name: InitFile
    'Input: none
    'Output: none
    'Precondition: none
    'Postcondition: text file created or opened and getuser called
    Sub InitFile()
        If File.Exists(fileName) Then
            GetUsers()

        Else
            Dim usersFile As StreamWriter
            usersFile = File.CreateText(fileName)

            usersFile.Close()
            GetUsers()

        End If
    End Sub

    Sub InitFileName(ByVal fileName)
        Dim contactsFile As StreamWriter

        'Check if file exists and create it if needed
        If Not File.Exists(fileName) Then

            contactsFile = File.CreateText(fileName)
            contactsFile.Close()

        End If
    End Sub

    Sub RetrieveContacts(ByVal username)

        'Creeate file name
        Dim fileName As String = username + "_contacts.txt"
        Dim contactsFile As StreamReader

        'Create a contact file if it doesn't already exist
        'InitFileName(fileName)

        contactsFile = File.OpenText(fileName)

        'Clear anything that may be saved in list
        lstContactsList.Clear()

        Do Until contactsFile.EndOfStream
            'use sha256 to decrypt files.************************************************

            'Dim Entry As New List(Of String)
            Dim entry(intMaxSUBSCRIPT_CONTACTS) As String

            Dim name As String
            Dim number As String
            Dim email As String
            Dim birthday As String
            Dim address As String

            ' Read in a users data
            name = contactsFile.ReadLine()
            number = contactsFile.ReadLine()
            email = contactsFile.ReadLine()
            birthday = contactsFile.ReadLine()
            address = contactsFile.ReadLine()

            'Decrypt the data***

            entry(0) = name
            entry(1) = number
            entry(2) = email
            entry(3) = birthday
            entry(4) = address

            lstContactsList.Add(entry)


        Loop

        contactsFile.Close()
        SortContacts()

    End Sub

    Sub ClearUser()
        user = ""
        lstContactsList = Nothing
    End Sub

    Sub AddContact(ByRef contactName, ByRef phone, ByVal email, ByVal birthday, ByVal address)
        Dim fileName = user + "_contacts.txt"
        Dim contactsFile As StreamWriter

        contactsFile = File.AppendText(fileName)

        'Write data
        contactsFile.WriteLine(contactName)
        contactsFile.WriteLine(phone)
        contactsFile.WriteLine(email)
        contactsFile.WriteLine(birthday)
        contactsFile.WriteLine(address)

        'Close the file
        contactsFile.Close()

        RetrieveContacts(user)

    End Sub

    'Save contact in module
    Sub LogContact(ByRef contactName, ByRef phone, ByVal email, ByVal birthday, ByVal address)
        strContact(0) = contactName
        strContact(1) = phone
        strContact(2) = email
        strContact(3) = birthday
        strContact(4) = address
    End Sub

    Sub SortContacts()
        Dim index As Integer = 0
        lstContactsList.Sort(Function(x, y) x(index).CompareTo(y(index)))
    End Sub

    Sub SaveIndex(ByVal index)
        selection = index
    End Sub

    ' End procedure definitions --------------------------------------

End Module

'Name:
'Input:
'Output:
'Precondition:
'Postcondition: