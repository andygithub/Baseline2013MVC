rd /q /s ConfigOnlyInjection\bin
rd /q /s EFCachingProvider\bin
rd /q /s EFAppFabricCacheDemo\bin
rd /q /s EFProviderWrapperToolkit\bin
rd /q /s EFTracingProvider\bin
rd /q /s ConfigOnlyInjection\obj
rd /q /s EFCachingProvider\obj
rd /q /s EFProviderWrapperToolkit\obj
rd /q /s EFTracingProvider\obj
attrib -r -h EFProviderWrappers.suo
del EFProviderWrappers.suo
del EFProviderWrappers.sln.cache
del AspNetCachingDemo\sqllog.txt

