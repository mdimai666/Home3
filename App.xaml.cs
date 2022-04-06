namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<DToast>();

            MainPage = new AppShell();

            //if (Device.Idiom == TargetIdiom.Phone)
            //    Shell.Current.CurrentItem = PhoneTabs;

            Routing.RegisterRoute("settings", typeof(SettingsPage));
            //Preferences.Set("IsEnabled", true);


            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://192.168.3.6");
                httpClient.DefaultRequestHeaders.Add("ClientType", "app.home3");
                QServer server = new QServer(httpClient);
                server.EnsureSuccessStatusCode = false;

                DependencyService.RegisterSingleton<HttpClient>(httpClient);
                DependencyService.RegisterSingleton<QServer>(server);

            }

        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            // Workaround for: 'Either set MainPage or override CreateWindow.'??
            if (this.MainPage == null)
            {
                this.MainPage = new AppShell();
            }

            return base.CreateWindow(activationState);
        }

        public async Task<string> LoadResource(string resourceFilename)
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync($"Resources/{resourceFilename}"))
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        public bool IsSimulator => DeviceInfo.DeviceType == DeviceType.Virtual;

        

    }
}
