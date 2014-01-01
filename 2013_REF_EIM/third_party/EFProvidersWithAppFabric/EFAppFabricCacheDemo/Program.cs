// Copyright (c) Microsoft Corporation.  All rights reserved.
//This class has been modified from the original version as part of
//MSDN Magazine Sept 2011 Data Points Column Sample Code
//(Julie Lerman)

using System;
using System.Linq;
using EFCachingProvider;
using EFCachingProvider.Caching;
using EFAppFabricCacheAdapter;
using System.Diagnostics;
using EFTracingProvider;
using Microsoft.ApplicationServer.Caching;
using System.Security;
using System.Collections.Generic;
using NorthwindModelDbFirst;

namespace EFAppFabricCacheDemo
{
    class Program
    {
        static void Main(string[] args)
        {      
          
       //NOTE: Be sure that AppFabric is running. I use 
          //http://mdcadmintool.codeplex.com/ (run as admin!)

             // Create and return the data cache
            ICache dataCache = CreateAppFabricCache();
            // set up default cache and caching policy
            EFCachingProviderConfiguration.DefaultCache = dataCache;
            EFCachingProviderConfiguration.DefaultCachingPolicy = CachingPolicy.CacheAll;
     
            //// log SQL from all connections to the console
            //EFTracingProviderConfiguration.LogToConsole = true;

            SimpleCachingDemo();
            CacheInvalidationDemo();
            //NonDeterministicQueryCachingDemo();

            Console.WriteLine("Demos Complete. Press any key to continue...");
            Console.ReadKey();
        }
     
        /// <summary>
        /// In this demo we are running a set of queries 3 times and logging SQL commands to the console.
        /// Note that queries are actually executed only in the first pass, while in second and third they are fulfilled
        /// completely from the cache.
        /// </summary>
        private static void SimpleCachingDemo()
        {
          EFTracingProviderConfiguration.LogToConsole = true;

            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine();
                Console.WriteLine("*** Pass #{0}...", i);
                Console.WriteLine();
                using (var context = new ExtendedNorthwindEntities())
                {
                    Console.WriteLine("Loading customer...");
                    var cust = context.Customers.First(c => c.CustomerID == "ALFKI");
                    Console.WriteLine("Customer name: {0}", cust.ContactName);
                    Console.WriteLine("Loading orders...");
                    cust.Orders.Load();
                    Console.WriteLine("Order count: {0}", cust.Orders.Count);
                }
            }

            Console.WriteLine("Finish reading from cache");
         

        }

        /// <summary>
        /// In this demo we are running a set of queries and updates and 3 times and logging SQL commands to the console.
        /// Notice how performing an update on Customer table causes the cache entry to be invalidated so we get 
        /// a query in each pass. Because we aren't modifying OrderDetails table, the collection of order details
        /// for the customer doesn't require a query in second and third pass.
        /// </summary>
        private static void CacheInvalidationDemo()
        {
            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine();
                Console.WriteLine("*** Pass #{0}...", i);
                Console.WriteLine();
                using (var context = new ExtendedNorthwindEntities())
                {
                    Console.WriteLine("Loading customer...");
                    var cust = context.Customers.First(c => c.CustomerID == "ANTON");
                    Console.WriteLine("Customer name: {0}", cust.ContactName);
                    cust.ContactName = "Change" + Environment.TickCount;
                    Console.WriteLine("Loading orders...");
                    cust.Orders.Load();
                    Console.WriteLine("Order count: {0}", cust.Orders.Count);
                    context.SaveChanges();
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// In this demo we are a query based on time (whose results are non-deterministic, because it uses DateTime.Now)
        /// Such queries are not cached, so we get a store query every time.
        ///
        /// Note that non-cacheable queries don't count as cache misses.
        /// </summary>
        private static void NonDeterministicQueryCachingDemo()
        {
            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine();
                Console.WriteLine("*** Pass #{0}...", i);
                Console.WriteLine();
                using (var context = new ExtendedNorthwindEntities())
                {
                    Console.WriteLine("Loading orders...");
                    context.Orders.Where(c => c.ShippedDate < DateTime.Now).ToList();
                }
            }

            Console.WriteLine();
        }

        //private static ICache CreateAppFabricCache(bool useLocalCache)
        //{
        //    DataCacheServerEndpoint endpoint = new DataCacheServerEndpoint("localhost", 22233, "DistributedCacheService");
        //    DataCacheFactory fac = new DataCacheFactory(new DataCacheServerEndpoint[] { endpoint }, useLocalCache, useLocalCache);

        //    return new AppFabricCache(fac.GetCache("AppFabric"));
        //}

        private static ICache CreateAppFabricCache()
        {
            // Declare an array for the cache host
            var server = new List<DataCacheServerEndpoint>();
            server.Add(new DataCacheServerEndpoint("localhost", 22233));
            // Set up the DataCacheFactory configuration
            var conf = new DataCacheFactoryConfiguration();
            conf.Servers = server;
            DataCacheFactory fac = new DataCacheFactory(conf);
            return new AppFabricCache(fac.GetDefaultCache());

        }
      

        static private SecureString Secure(string token) {
            SecureString secureString = new SecureString();
            foreach (char c in token) {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }
    }
}
