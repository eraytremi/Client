Imports System.Text.Json
Imports System.Windows.Forms

Class BookListPage
    Private ReadOnly _currentUserItem As UserItem
    Private ReadOnly _httpApiService As IHttpApiService

    Public Sub New(currentUserItem As UserItem, httpApiService As IHttpApiService)
        InitializeComponent()
        _currentUserItem = currentUserItem
        _httpApiService = httpApiService
    End Sub

    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)


        Dim searchText As String = searchTextBox.Text

        Dim jsonData As String = JsonSerializer.Serialize(New With {Key .SearchText = searchText})

        Dim result = Await _httpApiService.PostDataAsync(Of ResponseBody(Of List(Of BookItem)))("/Book/getall", jsonData, _currentUserItem.Token)
        If result.StatusCode = 200 Then
            Dim allData As List(Of BookItem) = result.Data

            Dim filteredData = (From item In allData
                                Where (item.Name IsNot Nothing AndAlso item.Name.ToLower().Contains(searchText))
                                Select item).ToList()
            If TypeOf resultDataGrid.ItemsSource Is List(Of BookItem) Then
                Dim currentData As List(Of BookItem) = DirectCast(resultDataGrid.ItemsSource, List(Of BookItem))
                currentData.Clear()
                currentData.AddRange(filteredData)
            Else
                resultDataGrid.ItemsSource = filteredData
            End If


            If filteredData.Count > 0 Then
                resultDataGrid.Visibility = Visibility.Visible
            Else
                resultDataGrid.Visibility = Visibility.Collapsed
            End If
        End If

    End Sub

    Private Sub searchTextBox_TextChanged(sender As Object, e As TextChangedEventArgs)
    End Sub

    Private Async Sub loanBookButton_Click_(sender As Object, e As RoutedEventArgs)
        Dim apiUrl = "/UserBook"

        Try
            Dim selectedBook As BookItem = TryCast(resultDataGrid.SelectedItem, BookItem)
            Dim bookId = selectedBook.Id
            Dim userId = _currentUserItem.Id

            Dim data As New With {
                .BookId = bookId,
                .UserId = userId
            }



            Dim jsonData As String = JsonSerializer.Serialize(data)
            Dim result = Await _httpApiService.PostDataAsync(Of ResponseBody(Of UserBookItem))(apiUrl, jsonData, _currentUserItem.Token)

            If result.StatusCode = 201 Then

                MessageBox.Show("Kitap ödüç alındı", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                MessageBox.Show("Giriş bilgileriniz eksik veya hatalıdır. Lütfen tekrar deneyiniz.", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class
