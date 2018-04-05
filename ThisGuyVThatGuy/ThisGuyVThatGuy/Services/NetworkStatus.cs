// <copyright file="NetworkStatus.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class NetworkStatus
    {
        /// <summary>
        /// Pings a URL to check connection
        /// </summary>
        /// <param name="url">The URL to check connectivity to</param>
        /// <returns>whether the internet connection available</returns>
        public static async Task<bool> HasConnectivity(string url)
        {
            Uri inputURI = new Uri(url);
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage resp = await client.GetAsync(inputURI);
                string content = await resp.Content.ReadAsStringAsync();

                if (resp.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
