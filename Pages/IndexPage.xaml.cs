using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MauiApp1.Resources;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace MauiApp1.Pages
{
    public partial class IndexPage : ContentPage
    {

        public string _text1 = "text1";
        public string Text1 { get => _text1; set { _text1 = value; OnPropertyChanged(); } }

        //public ObservableCollection<Post> Items = null;

        HttpClient httpClientPC = new HttpClient();
        QServer server;
        public Command cmdPlayPause { get; set; }


        public IndexPage()
        {
            InitializeComponent();

            server = DependencyService.Get<QServer>();
            httpClientPC.BaseAddress = new Uri("http://192.168.3.2:1880");
            cmdPlayPause = new Command(async () => await SendApi2("/api/playpause"));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ////Items = new(DataService.GetPosts());
            ////OnPropertyChanged(nameof(Items));

            //if (vm.Items.Count == 0)
            //    vm.LoadItemsCommand.Execute(null);
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
            try
            {
                string result = await server.GET("/api/ac?code=" + cmd);
            }
            catch (Exception ex)
            {
                Toast.ShowToastError(ex.Message);
            }

        }

        async void SendApi(string urlpath)
        {
            string result = await server.GET(urlpath);
        }

        async Task SendApi2(string urlpath)
        {
            try
            {
                var result = await httpClientPC.GetAsync(urlpath);
            }
            catch (Exception ex)
            {
                Toast.ShowToastError(ex.Message);
            }
        }

        async void Button_PC_toggle(object sender, EventArgs e)
        {
            //string result = await server.GET("/api/pc?code=toggle");
            try
            {
                string result = await server.GET("http://192.168.3.13/cmd?code=start");
            }
            catch (Exception ex)
            {
                Toast.ShowToastError(ex.Message);
            }

        }

        async void Button_PC_playpause(object sender, EventArgs e)
        {
            await SendApi2("/api/playpause");

        }

        private async void ButtonCommand_Clicked(object sender, EventArgs e)
        {
            string cmd = ((View)sender).BindingContext as string;
            try
            {
                string result = await server.GET($"/api/mobile1?code={cmd}");
            }
            catch (Exception ex)
            {
                Toast.ShowToastError(ex.Message);
            }
        }
    }

    public class IndexPageVM : BaseViewModel
    {

    }
}
