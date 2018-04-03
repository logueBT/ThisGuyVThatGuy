// <copyright file="MainPageViewModel.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Prism.Mvvm;
    using Prism.Navigation;
    using ThisGuyVThatGuy.Models;
    using ThisGuyVThatGuy.Services.Interfaces;

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;

        private IGetJsonService getJsonService;

        private Random rnd1 = new Random();

        private ObservableCollection<Player> playersList;

        private ObservableCollection<Player> playersPick;

        private Player selectedPlayer;

        private string url1;

        private string url2;

        private string name1;

        private string name2;

        private string score1;

        private string score2;

        private string successMessage;

        private bool showScore;

        private int count;

        public MainPageViewModel(INavigationService navigationService, IGetJsonService getJsonService)
        {
            this.navigationService = navigationService;
            this.getJsonService = getJsonService;
        }

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
                    this.GetRandomPlayers(4);
                }
            }
        }

        public string Url1
        {
            get
            {
                return this.url1;
            }

            set
            {
                this.SetProperty(ref this.url1, value);
            }
        }

        public string Url2
        {
            get
            {
                return this.url2;
            }

            set
            {
                this.SetProperty(ref this.url2, value);
            }
        }

        public string Name1
        {
            get
            {
                return this.name1;
            }

            set
            {
                this.SetProperty(ref this.name1, value);
            }
        }

        public string Name2
        {
            get
            {
                return this.name2;
            }

            set
            {
                this.SetProperty(ref this.name2, value);
            }
        }

        public string Score1
        {
            get
            {
                return this.score1;
            }

            set
            {
                this.SetProperty(ref this.score1, value);
            }
        }

        public string Score2
        {
            get
            {
                return this.score2;
            }

            set
            {
                this.SetProperty(ref this.score2, value);
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

            this.ShowScore = false;
            this.Count = 0;
            this.GetJson();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
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
                        this.GetRandomPlayers(5);
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

            for (int i = 0; i < numPlayers; i++)
            {
                int num = this.rnd1.Next(this.PlayersList.Count - 1);
                Player p = this.PlayersList[num];
                list.Add(p);
            }

            this.PlayersPick = list;
        }
    }
}
