Imports System.Data.SqlClient
Imports System.Data

Class MainWindow

    'connectionString=";Server=CPCVNC10\SQLEXPRESS;Initial Catalog=Quanlydiem;Integrated Security=True"
    Dim cs As New SqlConnection("Data Source=CPCVNC10\SQLEXPRESS;Initial Catalog=Quanlydiem;Integrated Security=True;MultipleActiveResultSets=True")
    Dim da As New SqlDataAdapter()
    Dim ds As New DataSet
    Dim cmb As New SqlCommandBuilder()

    Public Sub ShowData()

        Try
            da = New SqlDataAdapter("SELECT * FROM SINHVIEN", cs)
            ds = New DataSet
            cmb = New SqlCommandBuilder(da)
            da.Fill(ds, "SINHVIEN")
            dataGridView.ItemsSource = ds.Tables("SINHVIEN").DefaultView
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub LoadLOP()

        Try
            Dim da_LOP = New SqlDataAdapter("SELECT * FROM LOP", cs)
            Dim ds_LOP = New DataSet
            Dim cmb_LOP = New SqlCommandBuilder(da)
            da_LOP.Fill(ds_LOP, "LOP")
            cmbLOP.ItemsSource = ds_LOP.Tables("LOP").DefaultView
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs)
        LoadLOP()
        ShowData()
    End Sub


    Private Sub dataGridView_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs)

        If IsDBNull(e.AddedItems) = False Then
            Dim selectedRow = DirectCast(dataGridView.SelectedItem, DataRowView).Row
            txt.Text = selectedRow.Item(1)
            cmbLOP.SelectedValue = Integer.Parse(selectedRow.Item(9))
        End If

    End Sub

    Private Sub btn_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Delete(10)
    End Sub

    Private Sub btn_New_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        If IsDBNull(dataGridView.SelectedItem) = False Then
            Dim selectedRow = DirectCast(dataGridView.SelectedItem, DataRowView).Row
            Dim id As Integer = Integer.Parse(selectedRow.Item(0))
            selectedRow.Item(1) = txt.Text
            selectedRow.Item(9) = Integer.Parse(cmbLOP.SelectedValue)
            Update(id)
        End If
    End Sub

    Private Sub btn_Save_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Insert()
    End Sub

    Private Sub Insert()

        Try
            Dim sinhvien As String = "Pham Ninh Thai"
            Dim gt As String = "Nam"
            Dim day As Date = DateAndTime.Now
            Dim dc As String = "HCM"
            Dim sdt As String = "0979479646"
            Dim que As String = "Tay Ninh"
            Dim user As String = "Tay Ninh"
            Dim ps As String = "Tay Ninh"
            Dim lop As Integer = 4
            Dim sqlInsert As String = String.Format("INSERT INTO SINHVIEN(HoTen,Gioitinh,Ngaysinh,Diachi,Sodienthoai,Quequan,Tentruycapsv,Matkhausv,Malop)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8})", sinhvien, gt, day, dc, sdt, que, user, ps, lop)
            Dim cmd = New SqlCommand(sqlInsert, cs)
            If cs.State = ConnectionState.Closed Then cs.Open()
            cmd.ExecuteNonQuery()
            ShowData() 'Rebinding to DataGridView and view result
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Update(ID As Integer)

        Try
            Dim sqlDelete As String = String.Format("update SINHVIEN set HoTen ='{0}' ,Malop ={1} where Masv={2}", txt.Text, Integer.Parse(cmbLOP.SelectedValue), ID)
            Dim cmd = New SqlCommand(sqlDelete, cs)
            If cs.State = ConnectionState.Closed Then cs.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Delete(ID As Integer)
        Try
            Dim sqlDelete As String = String.Format("delete from SINHVIEN where Masv={0}", ID)
            Dim cmd = New SqlCommand(sqlDelete, cs)
            If cs.State = ConnectionState.Closed Then cs.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
