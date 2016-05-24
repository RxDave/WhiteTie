using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class Test : ProjectNuGetTestsBase
  {
    private const string projectName = "WhiteTie.Test";

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
          ".NETFramework,Version=v4.5.1"
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
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDependency:1.0.0.0",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestDuplicateDependency:1.0.0.0",
          ".NETFramework,Version=v4.5.1:Microsoft.Bcl:1.1.9",
          ".NETFramework,Version=v4.5.1:Microsoft.Bcl.Build:1.0.14",
          ".NETFramework,Version=v4.5.1:Microsoft.Net.Http:2.2.22"
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

      // There aren't any references because there's no need to generate them without any Code Contracts output.
      CollectionAssert.AreEquivalent(new string[0],
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
          ".NETFramework,Version=v4.5.1:WhiteTie.Test.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.Test.XML",
          ".NETFramework,Version=v4.5.1:WhiteTie.Test.chm",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.XML",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceProjectReference.dll",
          @"content\ContentFile1.txt",
        },
        (from file in package.GetFiles()
         select FullNameOrNull(file.TargetFramework, ":") + AssertionPath(file))
         .ToList());
    }

    private ZipPackage GetProjectOutput()
    {
      return base.GetProjectOutput(projectName, version: "1.2.3-Alpha");
    }
  }
}