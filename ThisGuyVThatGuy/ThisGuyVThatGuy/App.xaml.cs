using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ThisGuyVThatGuy
{
	public partial class App : PrismApplication
	{
		public App (IPlatformInitializer initializer = null) : base(initializer)
		{
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new System.Uri("http://www.ThisGuyVThatGuy/MainPage", System.UriKind.Absolute));
        }
    }
}
