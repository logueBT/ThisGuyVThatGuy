using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace ThisGuyVThatGuyUITest
{
    public class BaseUITests
    {
        /// <summary>
        /// The app reference
        /// </summary>
        protected IApp app;

        /// <summary>
        /// The platform type
        /// </summary>
        protected Platform platform;

        /// <summary>
        /// Base implementation, override in each class if different behaviour required.
        /// </summary>
        [SetUp]
        public void BeforeEachTest()
        {
            if (this.platform == Platform.Android)
            {
                this.app = ConfigureApp.Android.InstalledApp("com.josh.ThisGuyVThatGuy").EnableLocalScreenshots().StartApp();
            }

            //if (this.platform == Platform.iOS)
            //{
            //    this.app = ConfigureApp.iOS.InstalledApp("com.josh.ThisGuyVThatGuy").EnableLocalScreenshots().StartApp();
            //}
        }
    }
}
