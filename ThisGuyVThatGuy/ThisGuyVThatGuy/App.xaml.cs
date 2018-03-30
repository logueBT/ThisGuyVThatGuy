// <copyright file="App.xaml.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy
{
    using Prism;
    using Prism.Ioc;
    using Prism.Unity;

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        protected override void OnInitialized()
        {
            this.InitializeComponent();
            this.NavigationService.NavigateAsync(new System.Uri("http://www.ThisGuyVThatGuy/MainPage", System.UriKind.Absolute));
        }
    }
}
