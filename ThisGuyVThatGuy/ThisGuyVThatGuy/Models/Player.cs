// <copyright file="Player.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// player object
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fppg
        /// </summary>
        [JsonProperty("fppg")]
        public string FPPG
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image
        /// </summary>
        [JsonProperty("images")]
        public Dictionary<string, PlayerImage> Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name
        /// </summary>
        public string FullName => string.Format("{0} {1}", this.FirstName, this.LastName);
    }
}
