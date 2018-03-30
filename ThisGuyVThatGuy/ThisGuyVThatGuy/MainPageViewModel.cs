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

        private ObservableCollection<Player> playersList;

        private string url1;


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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
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
                        this.Url1 = this.PlayersList[0].Image["default"].Url;
                    }
                }
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}
