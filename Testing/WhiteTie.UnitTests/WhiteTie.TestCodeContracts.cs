using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class TestCodeContracts : ProjectNuGetTestsBase
  {
    private const string projectName = "WhiteTie.TestCodeContracts";

    [TestMethod]
    public void NuGet_Package_CanReadTitle()
    {
      var title = GetProjectOutput(projectName).Title;

      Assert.AreEqual(projectName, title);
    }

    [TestMethod]
    public void NuGet_Package_SupportedFrameworks()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1"
        },
        (from name in package.GetSupportedFrameworks()
         select name.FullName)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_Dependencies()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDependency:1.0.0.0",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDuplicateDependency:1.0.0.0"
        },
        (from set in package.DependencySets
         from dependency in set.Dependencies
         select FullNameOrNull(set.TargetFramework, ":") + dependency.Id + ":" + dependency.VersionSpec)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_References()
    {
      var package = GetProjectOutput(projectName);

      // References are included only to ensure that the Code Contracts assembly (WhiteTie.TestCodeContracts.Contracts.dll) is not referenced.
      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestCodeContracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceWithContracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceProjectReference.dll"
        },
        (from set in package.PackageAssemblyReferences
         from reference in set.References
         select FullNameOrNull(set.TargetFramework, ":") + reference)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_Files()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestCodeContracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestCodeContracts.Contracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestCodeContracts.chm",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.XML",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceProjectReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceWithContracts.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceWithContracts.Contracts.dll"
        },
        (from file in package.GetFiles()
         select file.TargetFramework.FullName + ":" + file.EffectivePath)
         .ToList());
    }
  }
}