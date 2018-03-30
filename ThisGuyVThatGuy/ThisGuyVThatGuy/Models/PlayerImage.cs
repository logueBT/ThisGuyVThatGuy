// <copyright file="PlayerImage.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// player images object
    /// </summary>
    [DataContract]
    public class PlayerImage
    {
        /// <summary>
        /// Gets or sets the url
        /// </summary>
        [DataMember(Name = "url")]
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        [DataMember(Name = "height")]
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        [DataMember(Name = "width")]
        public int Width
        {
            get;
            set;
        }
    }
}
