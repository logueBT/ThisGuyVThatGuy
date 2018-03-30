// <copyright file="MainPageViewModel.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy
{
    using Prism.Navigation;

    public class MainPageViewModel : INavigationAware
    {
        private readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService)
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
