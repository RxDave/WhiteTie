  White Tie
--==================================--
  https://github.com/RxDave/WhiteTie
--==================================--

Build your project in Release mode to:

* Generate a NuGet package. (See below for Visual Studio version support.)
* Generate reference documentation. (Only if Sandcastle is already installed.)


NuGet Package
-------------
White Tie builds a NuGet package for your project automatically under certain conditions.

If you're using Visual Studio 2015 or later, then you must only build your project in Release 
mode and a NuGet package will be generated. If you want White Tie to generate a NuGet package 
for other solution configurations, then you must set <BuildPackage>True</BuildPackage> in your
MSBuild project file for each of the configurations that you want.

If you're using Visual Studio 2013 or earlier, then you must first enable NuGet Package Restore.

To enable NuGet Package Restore for your solution in Visual Studio 2013, simply right-mouse click 
your solution in Solution Explorer and select "Enable NuGet Package Restore". This option is 
not available if Package Restore has already been enabled.

When Package Restore is enabled, you will see a ".nuget" folder next to your solution.

See also: http://docs.nuget.org/docs/reference/package-restore

To disable this feature in Release mode or any other configuration, simply add the following 
property to your project file: 

  <PropertyGroup>
    <BuildPackage>False</BuildPackage>
  </PropertyGroup>


Reference Documentation
-----------------------
White Tie builds reference documentation for your project automatically if Sandcastle is installed.
A compile help file (.chm) is generated in your project's output directory; e.g., bin\Release\

Download and Install Sandcastle
http://shfb.codeplex.com

** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** **
  Warning

  Sandcastle can take a very long time to run, even for small projects.
  Visual Studio will be unresponsive while Sandcastle is running.
** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** **

To disable this feature, simply add the following property to your project file: 

  <PropertyGroup>
    <BuildDocumentationEnabled>False</BuildDocumentationEnabled>
  </PropertyGroup>
  

More Information
----------------

Getting Started
https://github.com/RxDave/WhiteTie/wiki

Configuration
https://github.com/RxDave/WhiteTie/wiki/Configuration

Author's Blog
http://davesexton.com/blog