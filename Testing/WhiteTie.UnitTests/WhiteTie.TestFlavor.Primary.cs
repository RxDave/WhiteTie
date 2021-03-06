﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;

namespace WhiteTie.UnitTests
{
  [TestClass]
  public class TestFlavor_Primary : ProjectNuGetTestsBase
  {
    private const string projectName = "WhiteTie.TestFlavor.Primary";

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
          ".NETStandard,Version=v1.2",
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
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.DuplicateDependency:1.0.0",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.Dependency:1.0.0",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.DuplicateDependency:1.0.0",
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
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceProjectReference.dll",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.dll",
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

      CollectionAssert.AreEquivalent(new[]
        {
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.XML",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestFlavor.Primary.chm",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.dll",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceAssemblyReference.XML",
          ".NETFramework,Version=v4.5.1:WhiteTie.TestReferenceProjectReference.dll",
          ".NETStandard,Version=v1.2:WhiteTie.TestFlavor.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.SecondReference.Contracts.dll",
          ".NETStandard,Version=v1.1:WhiteTie.TestFlavor.dll",
          ".NETPortable,Version=v0.0,Profile=net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.XML",
          ".NETPortable,Version=v0.0,Profile=net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.Contracts.dll",
          ".NETPortable,Version=v0.0,Profile=net4+sl5+netcore45+MonoAndroid1+MonoTouch1:WhiteTie.TestFlavor.dll"
        },
        (from file in package.GetFiles()
         select file.TargetFramework.FullName + ":" + file.EffectivePath)
         .ToList());
    }

    private ZipPackage GetProjectOutput()
    {
      return base.GetProjectOutput(projectName, "WhiteTie.TestFlavor");
    }
  }
}