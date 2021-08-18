using Home3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public MainPage()
        {
            InitializeComponent();
            server = DependencyService.Get<QServer>();
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

        async void Button_PC_toggle(object sender, EventArgs e)
        {
            string result = await server.GET("/api/pc?code=toggle");

        }
    }
}