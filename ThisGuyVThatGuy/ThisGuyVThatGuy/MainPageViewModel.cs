using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThisGuyVThatGuy
{
    public class MainPageViewModel : INavigationAware
    {
        INavigationService navigationService;

        public MainPageViewModel (INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
