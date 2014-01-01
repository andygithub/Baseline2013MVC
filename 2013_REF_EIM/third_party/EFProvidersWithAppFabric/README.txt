*******************************************************************************
*This solution was modified for the MSDN Magazine Sept 2011 Data Points Column 
* by Julie Lerman
*msdn.com/magazine
*Second-Level Caching in the Entity Framework and AppFabric
*Added Projects:
  1) EFAppFabricCacheAdapter
  2) EFAppFabricCachedDemo
  3) NorthwindModelDbFirst
Running the sample code requires that you have the Windows Server AppFabric and its Cache running.
The article (which this sample accompanies) provides links and basic info on doing this.

You'll need to rebuild the projects in this applicaitons. 
The compiled dlls were removed to reduce the size of the distributed solutino.

*Removed other projects from the original demo solution that do not pertain to the article.
*Get the complete ORIGINAL solution that this comes from at 
* http://code.msdn.microsoft.com/EFProviderWrappers
***************************************************************************************
*BELOW ARE NOTES FROM ORIGINAL SAMPLE
*******************************************************************************************

Tracing and Caching Provider Wrappers for Entity Framework is a sample which demonstrates how to implement 
wrapping providers which add interesting functionality to an EF application.

Included are:

    EFProviderWrapperToolkit - common toolkit for writing provider wrappers.
    EFCachingProvider - provides caching of LINQ and ESQL query results
    EFCachingProvider.Web - provides caching in ASP.NET environment.
    EFTracingProvider - enables tracing (logging) of SQL commands 

Other projects:

    AspNetCachingDemo - demonstrates using tracing and caching in ASP.NET application using Entity Data Source
    ConfigOnlyInjection - explains injecting tracing behavior into an application by changing App.config and ssdl file
    EFProviderWrapperDemo - couple of end-to-end demos of using tracing and caching providers.
    EFCachingProvider.Tests - unit tests for in-memory cache implementation
    NorthwindEF - NorthwindEF model used by the samples

The sample works with Entity Framework 4.0. 

What is new in .NET 4.0 version
===============================
                             
1. Upgraded all projects to .NET 4.0 and retargetted assemblies to use .NET Framework 4 Client Profile

2. Removed reflection-based code to invoke CreateCommandDefinition()

3. Moved AspNetCache to a separate assembly (which depends on .NET Framework 4 Extended Profile)

4. Added wrappers for
     DbProviderManifest:
       public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
       public override string EscapeLikeArgument(string argument)

     DbProviderServices:
        protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
        protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
        protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
        protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)

5. Wrappers now work in partial trust - check out AspNetCachingDemo

6. Removed Velocity adapter and the demo.

7. Recreated sample models using edmx (instead of explicit csdl/ssdl/msl)

8. Removed the need to explicitly install the database in SQL Server - instead using |DataDirectory| and mdf files local to the project.

