using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThisGuyVThatGuy;
using ThisGuyVThatGuyUnitTest.MockServices;

namespace ThisGuyVThatGuyUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private MainPageViewModel viewModel;

        public UnitTest1()
        {
            this.viewModel = new MainPageViewModel( new MockNavigationService(), new MockGetJsonService());
        }

        [TestMethod]
        public void TestOnNavigatedTo()
        {
            this.viewModel.OnNavigatedTo(null);
            Assert.IsTrue(this.viewModel.Enabled);
            Assert.IsTrue(this.viewModel.ButtonEnabled);
            Assert.IsFalse(this.viewModel.ShowList);
            Assert.IsFalse(this.viewModel.ShowScore);
            Assert.AreEqual(this.viewModel.SuccessMessage, "Pick who has the highest FPPG");
            Assert.AreEqual(this.viewModel.NumberPlayers, "2");
            Assert.AreEqual(this.viewModel.ButtonMessage, "Go!");
            Assert.AreEqual(this.viewModel.Count, 0);
        }

        [TestMethod]
        public void TestParseData()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();

            Assert.AreNotEqual(this.viewModel.PlayersList.Count, 0);
            Assert.AreNotEqual(this.viewModel.PlayersPick.Count, 0);
            Assert.IsTrue(this.viewModel.ButtonEnabled);
            Assert.IsTrue(this.viewModel.ShowList);

        }

        [TestMethod]
        public void TestGetRandomPlayers()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();
            Assert.AreEqual(this.viewModel.PlayersPick.Count, 2);

            this.viewModel.GetRandomPlayers(3);

            Assert.AreEqual(this.viewModel.PlayersPick.Count, 3);

        }

        [TestMethod]
        public void TestRightAnswerCorrect()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();
            double one = Convert.ToDouble(this.viewModel.PlayersPick[0].FPPG);
            double two = Convert.ToDouble(this.viewModel.PlayersPick[1].FPPG);

            if (one > two)
            {
                this.viewModel.SelectedPlayer = this.viewModel.PlayersPick[0];
            }
            else
            {
                this.viewModel.SelectedPlayer = this.viewModel.PlayersPick[1];
            }
            

            Assert.AreEqual(this.viewModel.SuccessMessage, "Correct, keep going");
            Assert.AreEqual(this.viewModel.ButtonMessage, "Go again");
            Assert.AreEqual(this.viewModel.Count, 1);
        }

        [TestMethod]
        public void TestRightAnswerWrong()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();
            double one = Convert.ToDouble(this.viewModel.PlayersPick[0].FPPG);
            double two = Convert.ToDouble(this.viewModel.PlayersPick[1].FPPG);

            if (one < two)
            {
                this.viewModel.SelectedPlayer = this.viewModel.PlayersPick[0];
            }
            else
            {
                this.viewModel.SelectedPlayer = this.viewModel.PlayersPick[1];
            }

            Assert.AreEqual(this.viewModel.SuccessMessage, "Wrong, try again");
            Assert.AreEqual(this.viewModel.ButtonMessage, "Try again");
            Assert.AreEqual(this.viewModel.Count, 0);
        }

        [TestMethod]
        public void TestRightAnswerFinish()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();
            this.viewModel.Count = 9;
            Assert.IsTrue(this.viewModel.ShowList);
            double one = Convert.ToDouble(this.viewModel.PlayersPick[0].FPPG);
            double two = Convert.ToDouble(this.viewModel.PlayersPick[1].FPPG);

            if (one > two)
            {
                this.viewModel.RightAnswer(this.viewModel.PlayersPick[0].FPPG);
            }
            else
            {
                this.viewModel.RightAnswer(this.viewModel.PlayersPick[1].FPPG);
            }

            Assert.AreEqual(this.viewModel.SuccessMessage, "You did it! Go again?");
            Assert.AreEqual(this.viewModel.ButtonMessage, "Go!");
            Assert.AreEqual(this.viewModel.Count, 10);
            Assert.IsFalse(this.viewModel.ShowList);
        }

        [TestMethod]
        public void TestStartChoosingFirstTime()
        {
            this.viewModel.OnNavigatedTo(null);
            Assert.AreEqual(this.viewModel.PlayersList.Count, 0);
            Assert.AreEqual(this.viewModel.PlayersPick.Count, 0);

            this.viewModel.StartChoosing(null).Wait();

            Assert.AreNotEqual(this.viewModel.PlayersList.Count, 0);
            Assert.AreNotEqual(this.viewModel.PlayersPick.Count, 0);
        }

        [TestMethod]
        public void TestStartChoosing()
        {
            this.viewModel.OnNavigatedTo(null);
            this.viewModel.GetJson().Wait();

            this.viewModel.StartChoosing(null).Wait();

            Assert.AreEqual(this.viewModel.SuccessMessage, "Pick who has the highest FPPG");
            Assert.AreEqual(this.viewModel.NumberPlayers, "2");
            Assert.AreEqual(this.viewModel.ButtonMessage, "Go!");
            Assert.AreEqual(this.viewModel.Count, 0);
            Assert.IsTrue(this.viewModel.ShowList);
        }
    }
}
