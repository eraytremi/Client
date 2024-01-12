Public Class HomeWindow
    Private ReadOnly _currentUserItem As UserItem
    Private ReadOnly _httpApiService As IHttpApiService

    Public Sub New(currentUserItem As UserItem, httpApiService As IHttpApiService)
        InitializeComponent()
        _currentUserItem = currentUserItem
        _httpApiService = httpApiService
    End Sub

    Private Sub btnHome_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btnBookList_Click(sender As Object, e As RoutedEventArgs)
        Dim bookListPage = New BookListPage(_currentUserItem, _httpApiService)
        If framePanel.NavigationService.CanGoBack Then
            framePanel.NavigationService.RemoveBackEntry()
        End If
        framePanel.Content = bookListPage
    End Sub

    Private Sub btnProfile_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
