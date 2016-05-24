# WhiteTie
* No configuration necessary. [(*)](#configuration-note)
* By default, White Tie only executes when building in **Release** mode.
* Builds a NuGet package for your project. (Note that in *Visual Studio 2013*, [NuGet Package Restore](http://docs.nuget.org/docs/reference/package-restore) must be enabled on your solution.)
  * [Code Contract](https://github.com/CodeContractsDotNet/CodeContracts) assemblies, referenced assemblies, symbols, documentation files and NuGet dependency packages are included in the NuGet Package automatically.
  * Additional target framework flavors can be included by adding [`<NuGetFlavor />`](../../wiki/Configuration#nugetflavor) items to your project.
* Builds documentation for your project, if [Sandcastle](https://shfb.codeplex.com) is already installed.
* Controls whether common static analysis tools are executed.
* Copies build output, including any `<Content />` project items, to a local deployment directory under the solution. (Note that your project's output folder is unaffected. Output files will also appear in your project's _bin_ folder as normal.)
* Uses a consistent solution folder structure for various build dependencies and artifacts.
* Provides many MSBuild properties and items, enabling complete customization if desired.

##### See Also
* [Getting Started](../../wiki/GettingStarted)
* [Configuration](../../wiki/Configuration)
* [How To Videos](https://www.youtube.com/playlist?list=PLzLa5EktSmlzcEuE66oC0YGobqrrwnK2o)

# Configuration Note
White Tie generates a _.nuspec_ file with some of the default values derived from attributes in the output assembly. NuGet may generate an error when required values are missing. To avoid these errors, you must either: 

* Include all required assembly attributes as per [NuGet's replacement token documentation](http://docs.nuget.org/docs/reference/nuspec-reference/#Replacement_Tokens).
* Set the required [MSBuild NuSpec Properties](../../wiki/Configuration#nuspecproperties) in your project file.

The first choice is the simplest. In general, you only have to enter non-empty strings for `AssemblyCompanyAttribute` and `AssemblyDescriptionAttribute` to resolve any errors; however, you should review all of the attributes anyway to ensure that you get the desired output; e.g., `AssemblyTitleAttribute`, `AssemblyCopyrightAttribute` and `AssemblyVersionAttribute` are also used in the NuGet package.
