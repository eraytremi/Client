
Imports Microsoft.Extensions.DependencyInjection

Class Application
    Private ServiceProvider As IServiceProvider

    Protected Overrides Sub OnStartup(e As StartupEventArgs)

        Dim services = New ServiceCollection()
        ConfigureServices(services)

        ServiceProvider = services.BuildServiceProvider()

        Dim MainWindow = ServiceProvider.GetService(Of MainWindow)()
        MainWindow.Show()
    End Sub

    Private Sub ConfigureServices(services As IServiceCollection)
        services.AddSingleton(Of IHttpApiService, HttpApiService)
        services.AddSingleton(Of MainWindow)
    End Sub
End Class
