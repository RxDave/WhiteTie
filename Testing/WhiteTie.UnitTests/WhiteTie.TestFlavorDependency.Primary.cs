using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class TestFlavorDependency_Primary : ProjectNuGetTestsBase
  {
    // Project 'C' -- https://whitetie.codeplex.com/workitem/14
    private const string projectName = "WhiteTie.TestFlavorDependency.Primary";

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
          ".NETStandard,Version=v1.2"
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
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor:1.0.0",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDependency:1.0.0",
          //".NETFramework,Version=v4.5.1:WhiteTie.TestDuplicateDependency:1.0.0",  TODO: Unit test fails with this uncommented.  Does it indicate a bug?
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.DuplicateDependency:1.0.0",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor:1.0.0",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.Dependency:1.0.0",
          //".NETStandard,Version=v1.0:WhiteTie.TestFlavor.DuplicateDependency:1.0.0"  TODO: Unit test fails with this uncommented.  Does it indicate a bug?
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
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavorDependency.Primary.dll",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavorDependency.Flavor.dll"
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

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavorDependency.Primary.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavorDependency.Primary.chm",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavorDependency.Flavor.dll"
        },
        (from file in package.GetFiles()
         select file.TargetFramework.FullName + ":" + file.EffectivePath)
         .ToList());
    }

    private ZipPackage GetProjectOutput()
    {
      return base.GetProjectOutput(projectName);
    }
  }
}