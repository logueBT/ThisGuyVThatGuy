// <copyright file="MainPageViewModel.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Prism.Mvvm;
    using Prism.Navigation;
    using ThisGuyVThatGuy.Models;
    using ThisGuyVThatGuy.Services;
    using ThisGuyVThatGuy.Services.Interfaces;
    using Xamarin.Forms;

    /// <summary>
    /// View model for the main page.
    /// </summary>
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Backing field for navigation service
        /// </summary>
        private readonly INavigationService navigationService;

        /// <summary>
        /// Backing field for get HSON service
        /// </summary>
        private IGetJsonService getJsonService;

        /// <summary>
        /// Backing field for random number generator
        /// </summary>
        private Random rnd1 = new Random();

        /// <summary>
        /// Backing field for the list of all players
        /// </summary>
        private ObservableCollection<Player> playersList;

        /// <summary>
        /// Backing field for the list of players to show in the list view
        /// </summary>
        private ObservableCollection<Player> playersPick;

        /// <summary>
        /// Backing field for the player chosen in the list view
        /// </summary>
        private Player selectedPlayer;

        /// <summary>
        /// Backing field for the success message
        /// </summary>
        private string successMessage;

        /// <summary>
        /// Backing field for the button label
        /// </summary>
        private string buttonMessage;

        /// <summary>
        /// Backing field for whether to show fppg
        /// </summary>
        private bool showScore;

        /// <summary>
        /// Backing field for whether to show list of players
        /// </summary>
        private bool showList;

        /// <summary>
        /// Backing field for whether the list view is enabled
        /// </summary>
        private bool enabled;

        /// <summary>
        /// Backing field for whether the button is enabled
        /// </summary>
        private bool buttonEnabled;

        /// <summary>
        /// Backing field for number of correct guesses
        /// </summary>
        private int count;

        /// <summary>
        /// Backing field for number of players to choose from
        /// </summary>
        private string numberPlayers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service</param>
        /// <param name="getJsonService">get json service</param>
        public MainPageViewModel(INavigationService navigationService, IGetJsonService getJsonService)
        {
            this.navigationService = navigationService;
            this.getJsonService = getJsonService;
            this.GoCommand = new Command((obj) => { this.StartChoosing(obj); });
        }

        /// <summary>
        /// Gets the command to start/continue in game
        /// </summary>
        public Command GoCommand { get; private set; }

        /// <summary>
        /// Gets or sets observable collection with player list
        /// </summary>
        public ObservableCollection<Player> PlayersList
        {
            get
            {
                if (this.playersList == null)
                {
                    this.playersList = new ObservableCollection<Player>();
                }

                return this.playersList;
            }

            set
            {
                this.SetProperty(ref this.playersList, value);
            }
        }

        /// <summary>
        /// Gets or sets observable collection with players to pick from
        /// </summary>
        public ObservableCollection<Player> PlayersPick
        {
            get
            {
                if (this.playersPick == null)
                {
                    this.playersPick = new ObservableCollection<Player>();
                }

                return this.playersPick;
            }

            set
            {
                this.SetProperty(ref this.playersPick, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected player chosen from the list
        /// </summary>
        public Player SelectedPlayer
        {
            get
            {
                if (this.selectedPlayer == null)
                {
                    this.selectedPlayer = new Player();
                }

                return this.selectedPlayer;
            }

            set
            {
                this.SetProperty(ref this.selectedPlayer, value);
                if (value != null)
                {
                    this.RightAnswer(value.FPPG);
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of players to choose from
        /// </summary>
        public string NumberPlayers
        {
            get
            {
                return this.numberPlayers;
            }

            set
            {
                this.SetProperty(ref this.numberPlayers, value);
                if (value != null)
                {
                    try
                    {
                        string num = value;
                        int number = Convert.ToInt32(num);
                        this.GetRandomPlayers(number);
                    }
                    catch (Exception)
                    {
                        // exception
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the success message to show to the user
        /// </summary>
        public string SuccessMessage
        {
            get
            {
                return this.successMessage;
            }

            set
            {
                this.SetProperty(ref this.successMessage, value);
            }
        }

        /// <summary>
        /// Gets or sets the button label
        /// </summary>
        public string ButtonMessage
        {
            get
            {
                return this.buttonMessage;
            }

            set
            {
                this.SetProperty(ref this.buttonMessage, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the fppg
        /// </summary>
        public bool ShowScore
        {
            get
            {
                return this.showScore;
            }

            set
            {
                this.SetProperty(ref this.showScore, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the list view
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.SetProperty(ref this.enabled, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button is enabled
        /// </summary>
        public bool ButtonEnabled
        {
            get
            {
                return this.buttonEnabled;
            }

            set
            {
                this.SetProperty(ref this.buttonEnabled, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the list of players
        /// </summary>
        public bool ShowList
        {
            get
            {
                return this.showList;
            }

            set
            {
                this.SetProperty(ref this.showList, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of correct answers
        /// </summary>
        public int Count
        {
            get
            {
                return this.count;
            }

            set
            {
                this.SetProperty(ref this.count, value);
            }
        }

        /// <summary>
        /// Event handler for when the View Model is navigated from.
        /// </summary>
        /// <param name="parameters">Navigation parameters</param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Event handler for when the View Model is navigated to.
        /// </summary>
        /// <param name="parameters">Navigation parameters</param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.SuccessMessage = "Pick who has the highest FPPG";
            this.ShowList = false;
            this.ShowScore = false;
            this.NumberPlayers = "2";
            this.Count = 0;
            this.ButtonMessage = "Go!";
            this.Enabled = true;
            this.ButtonEnabled = true;
        }

        /// <summary>
        /// Event handler for when the View Model is navigating to another page.
        /// </summary>
        /// <param name="parameters">Navigation parameters</param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Button handler to get data or get new players to choose
        /// </summary>
        /// <param name="obj">button data</param>
        public void StartChoosing(object obj)
        {
            if (this.PlayersList.Count > 0)
            {
                this.ShowScore = false;
                this.Enabled = true;

                if (this.ShowList)
                {
                    this.GetRandomPlayers(Convert.ToInt32(this.NumberPlayers));
                }
                else
                {
                    this.GetRandomPlayers(Convert.ToInt32(this.NumberPlayers));
                    this.ShowList = true;
                    this.Count = 0;
                    this.SuccessMessage = "Pick who has the highest FPPG";
                }
            }
            else
            {
                this.GetJson();
            }
        }

        /// <summary>
        /// Check if fppg selected is maximum in list
        /// </summary>
        /// <param name="fppgSelected">value chosen by user</param>
        public void RightAnswer(string fppgSelected)
        {
            if (this.ShowScore == false)
            {
                this.ShowScore = true;
                this.Enabled = false;
            }

            double selected = Convert.ToDouble(fppgSelected);
            List<double> scores = new List<double>();
            foreach (var item in this.PlayersPick)
            {
                double pick = Convert.ToDouble(item.FPPG);
                scores.Add(pick);
            }

            double max = scores.Max();

            if (selected == max)
            {
                this.Count++;
                if (this.Count > 9)
                {
                    this.SuccessMessage = "You did it! Go again?";
                    this.ShowList = false;
                    this.ButtonMessage = "Go!";
                }
                else
                {
                    this.SuccessMessage = "Correct, keep going";
                    this.ButtonMessage = "Go again";
                }
            }
            else
            {
                this.SuccessMessage = "Wrong, try again";
                this.ButtonMessage = "Try again";
            }
        }

        /// <summary>
        /// Gets the JSON list
        /// </summary>
        public async void GetJson()
        {
            this.ButtonEnabled = false;
            string url = "http://www.google.co.uk";

            if (await NetworkStatus.HasConnectivity(url))
            {
                try
                {
                    Tuple<bool, string> getJsonResponse = await this.getJsonService.GetJsonAsync();
                    bool isSuccessful = getJsonResponse.Item1;
                    ObservableCollection<Player> players = new ObservableCollection<Player>();
                    if (isSuccessful)
                    {
                        string content = getJsonResponse.Item2;

                        players = this.ParseJsonResponse(content);

                        if (players.Count > 0)
                        {
                            this.PlayersList = players;
                            this.StartChoosing(null);
                            this.ButtonEnabled = true;
                        }
                    }
                }
                catch (Exception)
                {
                    this.ButtonEnabled = true;
                    await App.Current.MainPage.DisplayAlert("Something went wrong", "Please try again", "OK");
                }
            }
            else
            {
                this.ButtonEnabled = true;
                await App.Current.MainPage.DisplayAlert("No internet", "You're internet connection is rubbish. Please try again", "OK");
            }
        }

        /// <summary>
        /// Parses the JSON response to get list of players
        /// </summary>
        /// <param name="response">response string to parse</param>
        /// <returns>list of players</returns>
        public ObservableCollection<Player> ParseJsonResponse(string response)
        {
            ObservableCollection<Player> players = new ObservableCollection<Player>();
            JObject parsedPayload = JsonConvert.DeserializeObject(response) as JObject;

            JToken data = parsedPayload.SelectToken("$.players");

            if (data.HasValues)
            {
                if (data.Type == JTokenType.Array)
                {
                    var dataArray = data as JArray;
                    players = JsonConvert.DeserializeObject<ObservableCollection<Player>>(dataArray.ToString());
                }
                else if (data.Type == JTokenType.Object)
                {
                    var player = JsonConvert.DeserializeObject<Player>(data.ToString());
                    players.Add(player);
                }
            }

            return players;
        }

        /// <summary>
        /// Gets the list of random players
        /// </summary>
        /// <param name="numPlayers">number of players to choose from</param>
        public void GetRandomPlayers(int numPlayers)
        {
            ObservableCollection<Player> list = new ObservableCollection<Player>();
            var list1 = Enumerable.Range(0, this.PlayersList.Count - 1).OrderBy(x => this.rnd1.Next()).Take(numPlayers);
            foreach (var item in list1)
            {
                Player p = this.PlayersList[item];
                list.Add(p);
            }

            this.PlayersPick = list;
        }
    }
}
