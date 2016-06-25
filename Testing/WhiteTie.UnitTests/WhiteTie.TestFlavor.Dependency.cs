using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class TestFlavor_Dependency : ProjectNuGetTestsBase
  {
    private const string projectName = "WhiteTie.TestFlavor.Dependency";

    [TestMethod]
    public void NuGet_Package_CanReadTitle()
    {
      var title = GetProjectOutput(projectName).Title;

      // Assert.AreEqual(projectName, title);
      Assert.AreEqual("Title", title);    // NuGet bug workaround; see comment in WhiteTie.TestFlavor.Dependency.csproj for link to work item.
    }

    [TestMethod]
    public void NuGet_Package_SupportedFrameworks()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETStandard,Version=v1.2"
        },
        (from name in package.GetSupportedFrameworks()
         select name.FullName)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_Dependencies()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new string[0],
        (from set in package.DependencySets
         from dependency in set.Dependencies
         select FullNameOrNull(set.TargetFramework, ":") + dependency.Id + ":" + dependency.VersionSpec)
         .ToList());
    }

    [TestMethod]
    public void NuGet_Package_References()
    {
      var package = GetProjectOutput(projectName);

      CollectionAssert.AreEquivalent(new string[0],
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
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.Dependency.dll",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.Dependency.chm"
        },
        (from file in package.GetFiles()
         select file.TargetFramework.FullName + ":" + file.EffectivePath)
         .ToList());
    }
  }
}