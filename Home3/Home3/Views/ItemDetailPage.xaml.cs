using Home3.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Home3.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}