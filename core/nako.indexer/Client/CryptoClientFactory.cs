// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CryptoClientFactory.cs" company="SoftChains">
//   Copyright 2016 Dan Gershony
//   //  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//   //  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
//   //  EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
//   //  OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Nako.Client
{
    using Microsoft.Extensions.Caching.Memory;
    #region Using Directives

    using System;
   // using System.Runtime.Caching;

    #endregion

    /// <summary>
    ///  a factory to create clients.
    /// </summary>
    public class CryptoClientFactory
    {
        #region Static Fields

        /// <summary>
        ///     Defines a cache object to hold storage sources.
        /// </summary>
        private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        ///     Defines a lock object for the cache.
        /// </summary>
        private static readonly object CacheLock = new object();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// A static method to create a client.
        /// </summary>
        public static BitcoinClient Create(string connection, int port, string user, string encPass, bool secure)
        {
            // Put a lock on the cache to avoid creating random number of clients on startup.
            lock (Cache)
            {
                // Set cache key name
                var cacheKey = string.Format("{0}:{1}:{2}:{3}", connection, port, user, secure);
                return Cache.GetOrCreate(cacheKey, t => BitcoinClient.Create(connection, port, user, encPass, secure));
            }
        }

        #endregion
    }
}
