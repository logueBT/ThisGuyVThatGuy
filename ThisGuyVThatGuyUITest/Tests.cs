using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ThisGuyVThatGuyUITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests : BaseUITests
    {

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        /// <summary>
        /// Test to open the REPL window
        /// </summary>
        [Test]
        public void RunRepl()
        {
            this.app.Repl();
        }
    }
}

