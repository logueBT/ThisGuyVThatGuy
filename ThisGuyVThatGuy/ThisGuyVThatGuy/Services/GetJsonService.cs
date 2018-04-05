// <copyright file="GetJsonService.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ThisGuyVThatGuy.Services.Interfaces;

    /// <summary>
    /// The get JSON service
    /// </summary>
    public class GetJsonService : IGetJsonService
    {
        /// <summary>
        /// Attempts to get JSON string
        /// </summary>
        /// <returns>JSON string</returns>
        public async Task<Tuple<bool, string>> GetJsonAsync()
        {
            try
            {
                string content = string.Empty;
                string urlString = string.Format("https://gist.githubusercontent.com/liamjdouglas/bb40ee8721f1a9313c22c6ea0851a105/raw/6b6fc89d55ebe4d9b05c1469349af33651d7e7f1/Player.json");
                HttpClient hc = new HttpClient();
                content = await hc.GetStringAsync(urlString);

                return new Tuple<bool, string>(true, content);
            }
            catch (Exception)
            {
               return new Tuple<bool, string>(false, "exception");
            }
        }
    }
}
