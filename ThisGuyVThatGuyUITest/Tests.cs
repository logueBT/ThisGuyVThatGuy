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

        /// <summary>
        /// Test to open the REPL window
        /// </summary>
        [Test]
        public void RunRepl()
        {
            this.app.Repl();
        }

        /// <summary>
        /// Test the opening screen
        /// </summary>
        [Test]
        public void TestFirstScreen()
        {
            this.app.WaitForElement("pageTitle");
            this.app.WaitForElement("pageInstructions");
            this.app.WaitForElement("pickerExplainer");
            this.app.WaitForElement("numberPicker");
            this.app.WaitForElement("numberCorrectLabel");
            this.app.WaitForElement("numberCorrectCount");
            this.app.WaitForElement("successMessage");
            this.app.WaitForElement("goButton");
            this.app.WaitForNoElement("playerListView");

            
            Assert.AreEqual(this.app.Query("pageTitle").First().Text, "This Guy V That Guy", "title text not right");
            Assert.AreEqual(this.app.Query("pageInstructions").First().Text, "Pick which player has the largest FPPG, get 10 right and you win", "instructions text not right");
            Assert.AreEqual(this.app.Query("pickerExplainer").First().Text, "How many players do you want to choose from?", "picker text not right");
            Assert.AreEqual(this.app.Query("numberCorrectLabel").First().Text, "Number correct:", "Number correct text not right");
            Assert.AreEqual(this.app.Query("numberCorrectCount").First().Text, "0", "count not right");
            Assert.AreEqual(this.app.Query("successMessage").First().Text, "Pick who has the highest FPPG", "success text not right");
            Assert.AreEqual(this.app.Query("goButton").First().Text, "Go!", "button label not right");
            Assert.AreEqual(this.app.Query("numberPicker").First().Text, "2", "picker label not right");
        }

        /// <summary>
        /// Start game with 2 players, select first player
        /// </summary>
        [Test]
        public void TestStartGame()
        {
            this.app.WaitForElement("goButton");
            this.app.WaitForNoElement("playerListView");

            this.app.Tap("goButton");

            this.app.WaitForElement("playerListView");

            int count = this.app.Query("playerListViewName").Count();
            int countPicker = Int32.Parse(this.app.Query("numberPicker").First().Text);

            Assert.AreEqual(count, countPicker, "not correct length of list");

            this.app.WaitForNoElement("playerListViewFppg");

            var elements = this.app.Query(c => c.Marked("playerListViewName"));

            this.app.TapCoordinates(elements[0].Rect.CenterX, elements[0].Rect.CenterY);

            this.app.WaitForElement("playerListViewFppg");

            var fppgElements = this.app.Query(c => c.Marked("playerListViewFppg"));
            double first = Convert.ToDouble(fppgElements[0].Text);
            double second = Convert.ToDouble(fppgElements[1].Text);

            if (first > second)
            {
                this.app.WaitForElement("Correct, keep going");
                Assert.AreEqual(this.app.Query("numberCorrectCount").First().Text, "1", "count not right");
                Assert.AreEqual(this.app.Query("successMessage").First().Text, "Correct, keep going", "success text not right");
                Assert.AreEqual(this.app.Query("goButton").First().Text, "Go again", "button label not right");
            }
            else
            {
                Assert.AreEqual(this.app.Query("numberCorrectCount").First().Text, "0", "count not right");
                Assert.AreEqual(this.app.Query("successMessage").First().Text, "Wrong, try again", "success text not right");
                Assert.AreEqual(this.app.Query("goButton").First().Text, "Try again", "button label not right");
            }
        }

        /// <summary>
        /// Start game with 2 players then change number of players
        /// </summary>
        [Test]
        public void TestChangeNumberPlayers()
        {
            this.app.WaitForElement("goButton");
            this.app.WaitForNoElement("playerListView");

            this.app.Tap("numberPicker");

            this.app.Tap("3");

            this.app.Tap("goButton");

            this.app.WaitForElement("playerListView");

            int count = this.app.Query("playerListViewName").Count();
            int countPicker = Int32.Parse(this.app.Query("numberPicker").First().Text);

            Assert.AreEqual(count, countPicker, "not correct length of list");

            this.app.Tap("numberPicker");

            this.app.Tap("4");

            int count2 = this.app.Query("playerListViewName").Count();
            int countPicker2 = Int32.Parse(this.app.Query("numberPicker").First().Text);

            Assert.AreEqual(count2, countPicker2, "not correct length of list");
        }

        /// <summary>
        /// Try to complete game
        /// </summary>
        [Test]
        public void TestCompleteGame()
        {
            this.app.WaitForElement("goButton");
            this.app.WaitForNoElement("playerListView");

            this.app.Tap("goButton");

            this.app.WaitForElement("playerListView");

            

            bool stillPlaying = true;

            while (stillPlaying)
            {
                var elements = this.app.Query(c => c.Marked("playerListViewName"));

                this.app.TapCoordinates(elements[0].Rect.CenterX, elements[0].Rect.CenterY);

                string count = this.app.Query("numberCorrectCount").First().Text;

                if (count == "10")
                {
                    stillPlaying = false;
                }
                else
                {
                    this.app.WaitForElement("playerListViewFppg");
                    this.app.Tap("goButton");
                    this.app.WaitForNoElement("playerListViewFppg");
                }
            }

            Assert.AreEqual(this.app.Query("successMessage").First().Text, "You did it! Go again?", "success text not right");
            Assert.AreEqual(this.app.Query("goButton").First().Text, "Go!", "button label not right");
        }
    }
}

