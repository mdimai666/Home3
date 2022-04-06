using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace MauiApp1.Components
{

    //[ContentProperty(nameof(Content))]
    public partial class Card1 : ContentView
    {

        //---------
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(Card1), default(string));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        ////---------
        //public static readonly BindableProperty SideImageProperty =
        //    BindableProperty.Create(nameof(SideImage), typeof(string), typeof(Card1), default(string));

        //public string SideImage
        //{
        //    get => (string)GetValue(SideImageProperty);
        //    set => SetValue(SideImageProperty, value);
        //}

        ////---------
        //public static readonly BindableProperty SideLinkProperty =
        //    BindableProperty.Create(nameof(SideLink), typeof(string), typeof(Card1), default(string));

        //public string SideLink
        //{
        //    get => (string)GetValue(SideLinkProperty);
        //    set => SetValue(SideLinkProperty, value);
        //}

        ////---------
        public static readonly BindableProperty SideContentProperty =
            BindableProperty.Create(nameof(SideContent), typeof(View), typeof(Card1), default(View));

        public View SideContent
        {
            get => (View)GetValue(SideContentProperty);
            set => SetValue(SideContentProperty, value);
        }

        ////--------
        //public static readonly BindableProperty AvatarContentProperty =
        //BindableProperty.Create(nameof(AvatarContent), typeof(View), typeof(TitleView1), default(View));

        //public View AvatarContent
        //{
        //    get => (View)GetValue(AvatarContentProperty);
        //    set => SetValue(AvatarContentProperty, value);
        //}

        ////--------
        public static readonly BindableProperty AvatarImageSourceProperty =
            BindableProperty.Create(nameof(AvatarImageSource), typeof(string), typeof(Card1), default(string));

        public string AvatarImageSource
        {
            get => (string)GetValue(AvatarImageSourceProperty);
            set => SetValue(AvatarImageSourceProperty, value);
        }

        Thickness avatarSourceNone = new Thickness(0);
        Thickness avatarSourcePresent = new Thickness(16, 16, 0, 16);

        public Thickness AvatarMargin => AvatarImageSource is null ? avatarSourceNone : avatarSourcePresent;
        public bool AvatarSlotVisible => AvatarImageSource is not null;

        public Card1()
        {

            InitializeComponent();
        }
    }
}
