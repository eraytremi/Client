Imports System.IO
Imports System.Text.Json
Imports System.Windows.Forms

Class MainWindow
    Private ReadOnly _httpApiService As IHttpApiService
    Private imgSelectedImageFilePath As String
    Public Sub New(httpApiService As IHttpApiService)
        _httpApiService = httpApiService
        InitializeComponent()

    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(txtEmail.Text) AndAlso txtEmail.Text.Length > 0 Then
            textEmail.Visibility = Visibility.Collapsed
        Else
            textEmail.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textEmail_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtEmail.Focus()
    End Sub

    Private Sub textPassword_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtPassword.Focus()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(txtPassword.Text) AndAlso txtPassword.Text.Length > 0 Then
            textEmail.Visibility = Visibility.Collapsed
        Else
            textEmail.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub signUp_Click(sender As Object, e As RoutedEventArgs)

        LoginPage.Visibility = Visibility.Collapsed
        RegisterPage.Visibility = Visibility.Visible

    End Sub

    Private Async Sub logInButton_Click(sender As Object, e As RoutedEventArgs)
        If String.IsNullOrEmpty(txtEmail.Text) AndAlso String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("Email ve şifreyi eksiksiz giriniz", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If String.IsNullOrEmpty(txtEmail.Text) Then
            MessageBox.Show("Email boş bırakılamaz", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("Şifre boş bırakılamaz", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim apiUrl As String = "/User/login"
            Dim email As String = txtEmail.Text
            Dim password As String = txtPassword.Text

            Dim loginData As New With {
               .EMail = email,
               .Password = password
            }

            Dim jsonData As String = JsonSerializer.Serialize(loginData)
            Dim result = Await _httpApiService.PostDataAsync(Of ResponseBody(Of UserItem))(apiUrl, jsonData)

            If result.StatusCode = 200 Then
                Hide()
                Dim homeWindow As HomeWindow = New HomeWindow(result.Data, _httpApiService)
                homeWindow.Show()
            Else
                MessageBox.Show("Giriş bilgileriniz eksik veya hatalıdır. Lütfen tekrar deneyiniz.", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub forgotPassword_Click(sender As Object, e As RoutedEventArgs)
        LoginPage.Visibility = Visibility.Collapsed
        UpdatePassword.Visibility = Visibility.Visible
    End Sub

    Private Sub textFirstName_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtFirstName.Focus()
    End Sub

    Private Sub textFirstName_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textFirstName.Text) AndAlso textFirstName.Text.Length > 0 Then
            textFirstName.Visibility = Visibility.Collapsed
        Else
            textFirstName.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textLastName_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtLastName.Focus()
    End Sub

    Private Sub textLastName_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textLastName.Text) AndAlso textLastName.Text.Length > 0 Then
            textLastName.Visibility = Visibility.Collapsed
        Else
            textLastName.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textPhoneNumber_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtPhoneNumber.Focus()
    End Sub

    Private Sub textPhoneNumber_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textPhoneNumber.Text) AndAlso textPhoneNumber.Text.Length > 0 Then
            textPhoneNumber.Visibility = Visibility.Collapsed
        Else
            textPhoneNumber.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textIdNumber_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtIdNumber.Focus()
    End Sub

    Private Sub textIdNumber_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textIdNumber.Text) AndAlso textIdNumber.Text.Length > 0 Then
            textIdNumber.Visibility = Visibility.Collapsed
        Else
            textIdNumber.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textEmailReg_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtEmailReg.Focus()
    End Sub

    Private Sub textEmailReg_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textEmailReg.Text) AndAlso textEmailReg.Text.Length > 0 Then
            textEmailReg.Visibility = Visibility.Collapsed
        Else
            textEmailReg.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textPasswordReg_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtPasswordReg.Focus()
    End Sub

    Private Sub textPasswordReg_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textPasswordReg.Text) AndAlso textPasswordReg.Text.Length > 0 Then
            textPasswordReg.Visibility = Visibility.Collapsed
        Else
            textPasswordReg.Visibility = Visibility.Visible
        End If
    End Sub

    Private Async Sub btnAddUser_Click(sender As Object, e As RoutedEventArgs)
        Dim apiUrl = "/User"

        Try

            Dim name = txtFirstName.Text
            Dim lastName = txtLastName.Text
            Dim idNumber = txtIdNumber.Text
            Dim password = txtPasswordReg.Text
            Dim email = txtEmailReg.Text
            Dim phoneNumber = txtPhoneNumber.Text
            Dim imageFilePath = imgSelectedImageFilePath


            If String.IsNullOrEmpty(imageFilePath) Then
                MessageBox.Show("Lütfen bir resim seçiniz.", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim registerData As New With {
            .EMail = email,
            .Password = password,
            .Name = name,
            .LastName = lastName,
            .IdNumber = idNumber,
            .PhoneNumber = phoneNumber,
            .Image = imageFilePath
        }



            Dim jsonData As String = JsonSerializer.Serialize(registerData)
            Dim result = Await _httpApiService.PostDataAsync(Of ResponseBody(Of UserItem))(apiUrl, jsonData)

            If result.StatusCode = 201 Then

                MessageBox.Show("Kayıt başarılı", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoginPage.Visibility = Visibility.Visible
                RegisterPage.Visibility = Visibility.Collapsed
                txtFirstName.Text = "Ad"
                txtLastName.Text = "Soyad"
                txtIdNumber.Text = "TCNo"
                txtPasswordReg.Text = "Şifre"
                txtEmailReg.Text = "Email"
                txtPhoneNumber.Text = "Telefon Numarası"
            Else
                MessageBox.Show("Giriş bilgileriniz eksik veya hatalıdır. Lütfen tekrar deneyiniz.", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As RoutedEventArgs)
        LoginPage.Visibility = Visibility.Visible
        RegisterPage.Visibility = Visibility.Collapsed
    End Sub

    Private Sub txtEmailForget_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not String.IsNullOrEmpty(textEmailForget.Text) AndAlso textEmailForget.Text.Length > 0 Then
            textEmailForget.Visibility = Visibility.Collapsed
        Else
            textEmailForget.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub textEmailForget_MouseDown(sender As Object, e As MouseButtonEventArgs)
        txtEmailForget.Focus()
    End Sub

    Private Sub textPasswordLast_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If Not String.IsNullOrEmpty(textPasswordLast.Text) AndAlso textPasswordLast.Text.Length > 0 Then
            textPasswordLast.Visibility = Visibility.Collapsed
        Else
            textPasswordLast.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub txtPasswordLast_TextChanged(sender As Object, e As TextChangedEventArgs)
        txtPasswordLast.Focus()
    End Sub

    Private Sub textPasswordNew_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If Not String.IsNullOrEmpty(textPasswordNew.Text) AndAlso textPasswordNew.Text.Length > 0 Then
            textPasswordNew.Visibility = Visibility.Collapsed
        Else
            textPasswordNew.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub txtPasswordNew_TextChanged(sender As Object, e As TextChangedEventArgs)
        txtPasswordNew.Focus()
    End Sub

    Private Async Sub btnUpdatePassword_Click(sender As Object, e As RoutedEventArgs)
        Dim apiUrl = "/User/updatepassword"

        Try

            Dim password = txtPasswordNew.Text
            Dim email = txtEmailForget.Text


            Dim updateData As New With {
               .EMail = email,
               .Password = password
            }

            Dim control = txtPasswordNew.Text.Equals(txtPasswordLast.Text)
            If control = False Then
                MessageBox.Show("Şifreler aynı olmalıdır!", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim jsonData As String = JsonSerializer.Serialize(updateData)
            Dim result = Await _httpApiService.PostDataAsync(Of ResponseBody(Of UserItem))(apiUrl, jsonData)


            If result.StatusCode = 200 Then
                MessageBox.Show("Kayıt başarılı", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoginPage.Visibility = Visibility.Visible
                UpdatePassword.Visibility = Visibility.Collapsed
                txtPasswordNew.Text = "Şifreyi tekrar gir"
                txtPasswordLast.Text = "Şifre"
                txtEmailForget.Text = "Email"
            Else
                MessageBox.Show("Giriş bilgileriniz eksik veya hatalıdır. Lütfen tekrar deneyiniz.", "Uyarı Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.", "Hata Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBackLogin_Click(sender As Object, e As RoutedEventArgs)
        LoginPage.Visibility = Visibility.Visible
        UpdatePassword.Visibility = Visibility.Collapsed
    End Sub

    Private Sub btnSelectImage_Click(sender As Object, e As RoutedEventArgs)
        ' OpenFileDialog oluştur
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Filter = "Resim Dosyaları (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp"

        ' Kullanıcı dosyayı seçti mi?


        ' Seçilen dosya yolunu al
        Dim filePath As String = openFileDialog.FileName

            ' Seçilen dosyayı Photos klasörüne kaydet
            Dim saveFileDialog As New SaveFileDialog
            saveFileDialog.FileName = filePath
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            saveFileDialog.Filter = "Resim Dosyaları (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp"
            If saveFileDialog.ShowDialog() = True Then
                filePath = saveFileDialog.FileName
            End If

            ' Seçilen resmin dosya yolunu kaydet
            Dim imgSelectedImageFilePath = filePath

            ' Seçilen dosyadan BitmapImage nesnesi oluştur
            Dim bitmapImage As New BitmapImage
            bitmapImage.BeginInit()
            bitmapImage.UriSource = New Uri(filePath)
            bitmapImage.EndInit()

            ' Image denetiminin Source özelliğini ayarlayarak seçilen resmi göster
            imgSelectedImage.Source = bitmapImage


    End Sub
End Class
