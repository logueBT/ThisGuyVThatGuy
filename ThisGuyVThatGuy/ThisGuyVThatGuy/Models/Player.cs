// <copyright file="Player.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// player object
    /// </summary>
    [DataContract]
    public class Player
    {
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fppg
        /// </summary>
        [DataMember(Name = "fppg")]
        public string FPPG
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image
        /// </summary>
        [DataMember(Name = "images")]
        public Dictionary<string, PlayerImage> Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the first Consig.
        /// </summary>
        public string ImageUrl
        {
            get
            {
                try
                {
                    return this.Image["detail"].Url;
                }
                catch (System.Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}
