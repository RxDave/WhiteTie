using System;
using System.IO;
using System.Runtime.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;

namespace WhiteTie.UnitTests
{
  public abstract class ProjectNuGetTestsBase
  {
    private const string testingFolder = @"..\..\..\..\Deployment\Release\";

    protected ZipPackage GetProjectOutput(string projectName, string outputName = null, string version = "1.0.0.0")
    {
      var packageFile = Path.Combine(Path.Combine(testingFolder, projectName), (outputName ?? projectName) + "." + version + ".nupkg");

      Assert.IsTrue(File.Exists(packageFile), packageFile + " does not exist.");

      return new ZipPackage(packageFile);
    }

    protected string FullNameOrNull(FrameworkName frameworkName, string suffix = null)
    {
      return frameworkName == null ? null : frameworkName.FullName + suffix;
    }

    protected string AssertionPath(IPackageFile file)
    {
      return file.Path.StartsWith("lib", StringComparison.OrdinalIgnoreCase)
           ? file.EffectivePath
           : file.Path.Substring(0, file.Path.IndexOf('\\') + 1) + file.EffectivePath;
    }
  }
}