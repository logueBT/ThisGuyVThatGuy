// <copyright file="MainPageViewModel.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Prism.Mvvm;
    using Prism.Navigation;
    using ThisGuyVThatGuy.Models;
    using ThisGuyVThatGuy.Services.Interfaces;
    using Xamarin.Forms;

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;

        private IGetJsonService getJsonService;

        private Random rnd1 = new Random();

        private ObservableCollection<Player> playersList;

        private ObservableCollection<Player> playersPick;

        private Player selectedPlayer;

        private string successMessage;

        private string buttonMessage;

        private bool showScore;

        private bool showList;

        private int count;

        private string numberPlayers;

        public MainPageViewModel(INavigationService navigationService, IGetJsonService getJsonService)
        {
            this.navigationService = navigationService;
            this.getJsonService = getJsonService;
            this.GoCommand = new Command(async (obj) => { await this.StartChoosing(obj); });
        }

        /// <summary>
        /// Gets the command to go go
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
        /// Gets or sets observable collection with player list
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.SuccessMessage = "Pick who has the highest FPPG";
            this.ShowList = false;
            this.ShowScore = false;
            this.NumberPlayers = "2";
            this.Count = 0;
            this.ButtonMessage = "Go!";
            this.GetJson();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public async Task StartChoosing(object obj)
        {
            this.ShowScore = false;

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

        public void RightAnswer(string fppgSelected)
        {
            if (this.ShowScore == false)
            {
                this.ShowScore = true;
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

        public async void GetJson()
        {
            try
            {
                Tuple<bool, string> getJsonResponse = await this.getJsonService.GetJsonAsync();
                bool isSuccessful = getJsonResponse.Item1;

                if (isSuccessful)
                {
                    string content = getJsonResponse.Item2;

                    JObject parsedPayload = JsonConvert.DeserializeObject(content) as JObject;

                    JToken data = parsedPayload.SelectToken("$.players");

                    ObservableCollection<Player> players = new ObservableCollection<Player>();

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

                    if (players.Count > 0)
                    {
                        this.PlayersList = players;
                    }
                }
            }
            catch (Exception)
            {
                // exception here
            }
        }

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
