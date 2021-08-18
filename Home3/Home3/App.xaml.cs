using Home3.Services;
using Home3.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Home3
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.3.6");
            QServer server = new QServer(httpClient);
            server.EnsureSuccessStatusCode = false;

            DependencyService.Register<DToast>();
            DependencyService.RegisterSingleton<HttpClient>(httpClient);
            DependencyService.RegisterSingleton<QServer>(server);


            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
