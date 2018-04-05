// <copyright file="IGetJsonService.cs" company="Josh Logue">
// Copyright (c) Josh Logue. All rights reserved.
// </copyright>

namespace ThisGuyVThatGuy.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the Get JSON Service
    /// </summary>
    public interface IGetJsonService
    {
        /// <summary>
        /// Attempts to get JSON string
        /// </summary>
        /// <returns>JSON string</returns>
        Task<Tuple<bool, string>> GetJsonAsync();
    }
}
