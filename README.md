# WhiteTie
* No configuration necessary.[**|#ConfigurationNote]
* By default, White Tie only executes when building in *Release* mode.
* Provides many MSBuild properties and items, enabling complete customization if desired.
* Controls whether common static analysis tools are executed.
* Builds documentation for your project if [url:Sandcastle|https://shfb.codeplex.com] is installed.
* Builds a NuGet package for your project (NOTE: In Visual Studio 2013, [url:NuGet Package Restore|http://docs.nuget.org/docs/reference/package-restore] must be enabled on your solution.)
** [url:Code Contract|https://github.com/CodeContractsDotNet/CodeContracts] assemblies, referenced assemblies, symbols, documentation files and NuGet dependency packages are included in the NuGet Package automatically.
** Additional target framework flavors can be included by adding {{NuGetFlavor}} items to your project.
* Copies build output, including any _Content_ project items, to a local deployment directory under the solution.  (Note that your project's output folder is unaffected.  Output files will also appear in your project's _bin_ folder as normal.)
* Uses a consistent solution folder structure for various build dependencies and artifacts.
*See also:*
* [Getting Started|Documentation]
* [Configuration]
* [url:How To Videos|https://www.youtube.com/playlist?list=PLzLa5EktSmlzcEuE66oC0YGobqrrwnK2o]
*{"**"} Configuration Note:*{anchor:ConfigurationNote}
White Tie generates a _.nuspec_ file with some of the default values derived from attributes in the output assembly.  NuGet may generate an error when required values are missing.  To avoid these errors, you must either: 
* Include all required assembly attributes as per [url:NuGet's replacement token documentation|http://docs.nuget.org/docs/reference/nuspec-reference/#Replacement_Tokens].
* Set the required [MSBuild NuSpec Properties|Configuration#NuSpecProperties] in your project file.
The first choice is the simplest.  In general, you only have to enter non-empty strings for {{AssemblyCompanyAttribute}} and {{AssemblyDescriptionAttribute}} to resolve any errors; however, you should review all of the attributes anyway to ensure that you get the desired output; e.g., {{AssemblyTitleAttribute}}, {{AssemblyCopyrightAttribute}} and {{AssemblyVersionAttribute}} are also used in the NuGet package.