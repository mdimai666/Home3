using Home3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Home3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        QServer server; 

        public Command cmdPlayPause { get; set; }
        public Command cmdCallApi { get; set; }

        HttpClient httpClientPC = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            server = DependencyService.Get<QServer>();

            httpClientPC.BaseAddress = new Uri("http://192.168.3.2:1880");

            cmdPlayPause = new Command(async () => await SendApi2("/api/playpause"));
            //cmdPlayPause = new Command( () => ButtonToggle_Clicked(null,null));
            
            //cmdCallApi = new Command(sender => SendApi((sender as View).com))
        }

        private void ButtonToggle_Clicked(object sender, EventArgs e)
        {
            SendCommand("toggle");
        }

        private void ButtonOn_Clicked(object sender, EventArgs e)
        {
            SendCommand("on");
        }

        private void ButtonOff_Clicked(object sender, EventArgs e)
        {
            SendCommand("off");
        }

        async void SendCommand(string cmd)
        {
            string result = await server.GET("/api/ac?code=" + cmd);
            
        }

        async void SendApi(string urlpath)
        {
            string result = await server.GET(urlpath);
        }
        
        async Task SendApi2(string urlpath)
        {
            var result = await httpClientPC.GetAsync(urlpath);
        }

        async void Button_PC_toggle(object sender, EventArgs e)
        {
            //string result = await server.GET("/api/pc?code=toggle");
            string result = await server.GET("http://192.168.3.13/cmd?code=start");

        }

        async void Button_PC_playpause(object sender, EventArgs e)
        {
            await SendApi2("/api/playpause");

        }

        private async void ButtonCommand_Clicked(object sender, EventArgs e)
        {
            string cmd = ((View)sender).BindingContext as string;
            string result = await server.GET($"/api/mobile1?code={cmd}");
        }
    }
}