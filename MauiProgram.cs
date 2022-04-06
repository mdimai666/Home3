using Microsoft.Maui.Controls.Compatibility;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    //handlers.AddCompatibilityRenderer(typeof(Components.MyButton2), typeof(Platforms.Android.MyButtonRenderer));
                    
                    // Register ALL handlers in the Xamarin Community Toolkit assembly
                    //handlers.AddCompatibilityRenderers(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer).Assembly);
#endif

                    // Register just one handler for the control you need
                    //handlers.AddCompatibilityRenderer(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElement), typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer));

                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesome");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");

                });

            return builder.Build();
        }
    }
}
