// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EFCachingProvider.Caching;
//using Microsoft.Data.Caching;
using Microsoft.ApplicationServer.Caching;

namespace EFAppFabricCacheAdapter
{
  /// <summary>
  /// Implementation of <see cref="ICache"/> which works with Microsoft Distributed Cache codename "AppFabric" CTP3.
  /// </summary>
  public class AppFabricCache : ICache
  {
    private DataCache cache;

    /// <summary>
    /// Initializes a new instance of the AppFabricCache class.
    /// </summary>
    /// <param name="cache">The cache to be used.</param>
    public AppFabricCache(DataCache cache)
    {
      this.cache = cache;
    }

    /// <summary>
    /// Tries to the get cached entry by key.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The retrieved value.</param>
    /// <returns>
    /// A value of <c>true</c> if entry was found in the cache, <c>false</c> otherwise.
    /// </returns>
    public bool GetItem(string key, out object value)
    {
      key = GetCacheKey(key);
      value = this.cache.Get(key);

      return value != null;
    }

    /// <summary>
    /// Adds the specified entry to the cache.
    /// </summary>
    /// <param name="key">The entry key.</param>
    /// <param name="value">The entry value.</param>
    /// <param name="dependentEntitySets">The list of dependent entity sets.</param>
    /// <param name="slidingExpiration">The sliding expiration - IGNORED.</param>
    /// <param name="absoluteExpiration">The absolute expiration.</param>
    public void PutItem(string key, object value, IEnumerable<string> dependentEntitySets, TimeSpan slidingExpiration, DateTime absoluteExpiration)
    {
      key = GetCacheKey(key);
      this.cache.Put(key, value, absoluteExpiration - DateTime.Now, dependentEntitySets.Select(c => new DataCacheTag(c)).ToList());

      foreach (var dep in dependentEntitySets)
      {
        this.CreateRegionIfNeeded(dep);
        this.cache.Put(key, " ", dep);
      }

    }

    /// <summary>
    /// Invalidates all cache entries which are dependent on any of the specified entity sets.
    /// </summary>
    /// <param name="entitySets">The entity sets.</param>
    public void InvalidateSets(IEnumerable<string> entitySets)
    {
      // Go through the list of objects in each of the set. 
      foreach (var dep in entitySets)
      {
        foreach (var val in this.cache.GetObjectsInRegion(dep))
        {
          this.cache.Remove(val.Key);
        }
      }
    }

    /// <summary>
    /// Invalidates cache entry with a given key.
    /// </summary>
    /// <param name="key">The cache key.</param>
    public void InvalidateItem(string key)
    {
      key = GetCacheKey(key);

      DataCacheItem item = this.cache.GetCacheItem(key);
      this.cache.Remove(key);

      // Remove the keys from the region..
      foreach (var tag in item.Tags)
      {
        this.cache.Remove(key, tag.ToString());
      }
    }

    /// <summary>
    /// Hashes the query to produce cache key..
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Hashed query which becomes a cache key.</returns>
    private static string GetCacheKey(string query)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(query);
      string hashString = Convert.ToBase64String(MD5.Create().ComputeHash(bytes));
      return hashString;
    }

    /// <summary>
    /// Creates the region if needed.
    /// </summary>
    /// <param name="regionName">Name of the region.</param>
    private void CreateRegionIfNeeded(string regionName)
    {
      try
      {
        this.cache.CreateRegion(regionName); //old code removed--> , false);
      }
      catch (DataCacheException de)
      {
        if (de.ErrorCode != DataCacheErrorCode.RegionAlreadyExists)
        {
          throw;
        }
      }
    }
  }
}
