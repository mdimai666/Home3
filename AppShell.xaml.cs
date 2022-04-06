namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        

        public AppShell()
        {
            InitializeComponent();

            BindingContext = this;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            _ = Shell.Current.GoToAsync("///settings");
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return false;
        }

    }
}
