Imports MySql.Data.MySqlClient

Public Class Form1

    Public conn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public adapter As New MySqlDataAdapter
    Public ds As New DataSet
    Dim itemColl(999) As String
    Dim genderBind As String

    Dim gender As String

    Sub openConn()
        conn.ConnectionString = "server=localhost; username=root; password=; database=dbsample; port=3308"
        conn.Open()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If txtFullname.Text = "" Or txtContact.Text = "" Or txtEmail.Text = "" Or txtAddress.Text = "" Or gender = "" Then
            MsgBox("Please specify the blank inputs.")
        Else
            Try
                openConn()
                cmd.Connection = conn
                cmd.CommandText = "insert into tblsampletwo (`Fullname`,`Gender`,`ContactNo`, `Email`, `DepartmentSubject`, `Address`) values ('" & txtFullname.Text & "','" & gender & "', '" & txtContact.Text & "', '" & txtEmail.Text & "', '" & cbDepartmentSub.Text & "', '" & txtAddress.Text & "' ) "
                cmd.ExecuteNonQuery()
                MsgBox("Successfuly saved")
                clearInputs()
                conn.Close()
                loadListView()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        gender = "Male"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        gender = "Female"
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If txtFullname.Text = "" Or txtContact.Text = "" Or txtEmail.Text = "" Or txtAddress.Text = "" Or gender = "" Then
            MsgBox("Please specify the blank inputs.")
        Else
            Try
                openConn()
                cmd.Connection = conn
                cmd.CommandText = "update tblsampletwo set Fullname='" & txtFullname.Text & "', Gender ='" & gender & "', ContactNo='" & txtContact.Text & "', Email='" & txtEmail.Text & "', DepartmentSubject='" & cbDepartmentSub.Text & "', Address='" & txtAddress.Text & "' where TeacherID='" & txtID.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Successfuly updated")
                clearInputs()
                conn.Close()
                loadListView()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub loadListView()
        Try
            ListView1.Items.Clear()
            openConn()
            ds.Clear()
            cmd.Connection = conn
            cmd.CommandText = "Select * from tblsampletwo"
            adapter.SelectCommand = cmd
            adapter.Fill(ds, "table")
            For r = 0 To ds.Tables(0).Rows.Count - 1
                For c = 0 To ds.Tables(0).Columns.Count - 1
                    itemColl(c) = ds.Tables(0).Rows(r)(c).ToString
                Next
                Dim listItems As New ListViewItem(itemColl)
                ListView1.Items.Add(listItems)
            Next
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadListView()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            txtID.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(0).Text
            txtFullname.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(1).Text
            gender = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(2).Text
            genderBind = gender
            If genderBind = "Male" Then
                RadioButton1.Checked = True
            ElseIf genderBind = "Female" Then
                RadioButton2.Checked = True
            End If
            txtContact.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(3).Text
            txtEmail.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(4).Text
            cbDepartmentSub.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(5).Text
            txtAddress.Text = ListView1.Items(ListView1.SelectedIndices(0)).SubItems(6).Text
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If txtFullname.Text = "" Or txtContact.Text = "" Or txtEmail.Text = "" Or txtAddress.Text = "" Or gender = "" Then
            MsgBox("Please specify the blank inputs.")
        Else
            Try
                openConn()
                cmd.Connection = conn
                cmd.CommandText = "delete from tblsampletwo where TeacherID='" & txtID.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Successfuly deleted")
                conn.Close()
                clearInputs()
                loadListView()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

    End Sub

    Sub clearInputs()

        txtFullname.Clear()
        txtAddress.Clear()
        txtContact.Clear()
        txtEmail.Clear()
        txtID.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        cbDepartmentSub.Text = ""

    End Sub
End Class
