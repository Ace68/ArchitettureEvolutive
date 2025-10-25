using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BrewUp.Purchase.Facade;
using NetArchTest.Rules;

namespace BrewUp.Purchase.Tests;

[ExcludeFromCodeCoverage]
public class PurchaseArchitectureTests
{
    [Fact]
    public void Should_PurchaseArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(PurchaseFacadeHelper).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "BrewUp.Warehouse.Domain",
            "BrewUp.Warehouse.Facade",
            "BrewUp.Warehouse.Infrastructure",
            "BrewUp.Warehouse.ReadModel",
            "BrewUp.Warehouse.SharedKernel",
            "BrewUp.Sales.Domain",
            "BrewUp.Sales.Facade",
            "BrewUp.Sales.Infrastructure",
            "BrewUp.Sales.ReadModel",
            "BrewUp.Sales.SharedKernel"
        };
        
        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenAssemblies.ToArray())
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
    
    [Fact]
    public void PurchaseProjects_Should_Having_Namespace_StartingWith_Purchase()
    {
        var purchaseModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Purchase");
        var subFolders = Directory.GetDirectories(purchaseModulePath);

        var netVersion = Environment.Version;

        var purchaseAssemblies = (from folder in subFolders
            let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
            where Directory.Exists(binFolder)
            let files = Directory.GetFiles(binFolder)
            let folderArray = folder.Split(Path.DirectorySeparatorChar)
            select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
            into assemblyFilename
            where assemblyFilename != null && !assemblyFilename.Contains("Test")
            select Assembly.LoadFile(assemblyFilename)).ToList();
        
        var purchaseTypes = Types.InAssemblies(purchaseAssemblies)
            .That()
            .DoNotHaveNameStartingWith("<>")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var typesWithCorrectNamespace = Types.InAssemblies(purchaseAssemblies)
            .That()
            .ResideInNamespaceStartingWith("BrewUp.Purchase")
            .And()
            .AreNotNested()
            .GetTypes();
        
        // Find types with incorrect namespace (difference between the two sets)
        var purchaseTypeArray = purchaseTypes as Type[] ?? purchaseTypes.ToArray();
        var typesWithIncorrectNamespace = purchaseTypeArray.Except(typesWithCorrectNamespace).ToList();

        foreach (var type in typesWithIncorrectNamespace)
        {
            if (type.Namespace != null)
                Assert.Fail(
                    $"Namespace violation detected: {type.FullName} in assembly {type.Assembly.GetName().Name} should start " +
                    $"with 'BrewUp.Purchase' but is in namespace '{type.Namespace}'");
        }
    }
    
    private static class VisualStudioProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory!
                   ?? throw new DirectoryNotFoundException("Solution directory not found. Make sure to run this test from a solution folder.");
        }
    }
}