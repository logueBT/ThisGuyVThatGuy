namespace ThisGuyVThatGuyUnitTest.MockServices
{
    using System;
    using System.Threading.Tasks;
    using Prism.Navigation;

    public class MockNavigationService : INavigationService
    {
        public Task<bool> GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GoBackAsync(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(Uri uri, NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(string name, NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
