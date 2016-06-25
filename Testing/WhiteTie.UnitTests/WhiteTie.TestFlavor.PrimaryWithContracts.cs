using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class TestFlavor_PrimaryWithContracts : ProjectNuGetTestsBase
  {
    private const string projectName = "WhiteTie.TestFlavor.PrimaryWithContracts";

    [TestMethod]
    public void NuGet_Package_CanReadTitle()
    {
      var title = GetProjectOutput().Title;

      Assert.AreEqual(projectName, title);
    }

    [TestMethod]
    public void NuGet_Package_SupportedFrameworks()
    {
      var package = GetProjectOutput();

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1",
          ".NETStandard,Version=v1.1",
          ".NETPortable,Version=v0.0,Profile=net4+sl5+netcore45+MonoAndroid1+MonoTouch1"
        },
        (from name in package.GetSupportedFrameworks()
         select name.FullName)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_Dependencies()
    {
      var package = GetProjectOutput();

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDependency:1.0.0",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDuplicateDependency:1.0.0",
          ".NETPortable,Version=v0.0,Profile=Profile37:Microsoft.Bcl:1.1.10",
          ".NETPortable,Version=v0.0,Profile=Profile37:Microsoft.Bcl.Build:1.0.21",
          ".NETPortable,Version=v0.0,Profile=Profile37:Microsoft.Net.Http:2.2.29"
        },
        (from set in package.DependencySets
         from dependency in set.Dependencies
         select FullNameOrNull(set.TargetFramework, ":") + dependency.Id + ":" + dependency.VersionSpec)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_References()
    {
      var package = GetProjectOutput();

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.dll",
          ".NETPortable,Version=v0.0,Profile=Profile37:WhiteTie.TestFlavor.dll"
        },
        (from set in package.PackageAssemblyReferences
         from reference in set.References
         select FullNameOrNull(set.TargetFramework, ":") + reference)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_Files()
    {
      var package = GetProjectOutput();

      /* TODO: Update unit test after problem is resolved.
       * White Tie allows NuGet to import the target assembly itself, though it's importing it into a different profile than what my code derives based on profile mappings,
       * which causes a profile conflict with the .chm file.
       * Consider disallowing NuGet to import the target assembly itself, if possible.  May have to resort to command line "push" rather than the MSBuild task.
       */
      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.Contracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.PrimaryWithContracts.chm",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.Contracts.dll",
          "portable-net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.dll",
          "portable-net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.XML",
          "portable-net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.Contracts.dll"
        },
        (from file in package.GetFiles()
         select file.TargetFramework.FullName + ":" + file.EffectivePath)
         .ToList());
    }

    private ZipPackage GetProjectOutput()
    {
      return base.GetProjectOutput(projectName, "WhiteTie.TestFlavor", "1.0.0.0-Beta");
    }
  }
}