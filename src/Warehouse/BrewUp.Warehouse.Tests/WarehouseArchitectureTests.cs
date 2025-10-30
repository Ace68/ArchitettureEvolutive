using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BrewUp.Warehouse.Facade;
using NetArchTest.Rules;

namespace BrewUp.Warehouse.Tests;

[ExcludeFromCodeCoverage]
public class WarehouseArchitectureTests
{
    [Fact]
    public void Should_WarehouseArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(WarehouseFacadeHelper).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "BrewUp.Sales.Domain",
            "BrewUp.Sales.Facade",
            "BrewUp.Sales.Infrastructure",
            "BrewUp.Sales.ReadModel",
            "BrewUp.Sales.SharedKernel",
            
            "BrewUp.Purchase.Domain",
            "BrewUp.Purchase.Facade",
            "BrewUp.Purchase.Infrastructure",
            "BrewUp.Purchase.ReadModel",
            "BrewUp.Purchase.SharedKernel"
        };
        
        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenAssemblies.ToArray())
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
    
    [Fact]
    public void WarehouseProjects_Should_Having_Namespace_StartingWith_Warehouse()
    {
        var warehouseModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Warehouse");
        var subFolders = Directory.GetDirectories(warehouseModulePath);

        var netVersion = Environment.Version;

        var warehouseAssemblies = (from folder in subFolders
            let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
            where Directory.Exists(binFolder)
            let files = Directory.GetFiles(binFolder)
            let folderArray = folder.Split(Path.DirectorySeparatorChar)
            select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
            into assemblyFilename
            where assemblyFilename != null && !assemblyFilename.Contains("Test")
            select Assembly.LoadFile(assemblyFilename)).ToList();
        
        var warehouseTypes = Types.InAssemblies(warehouseAssemblies)
            .That()
            .DoNotHaveNameStartingWith("<>")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var typesWithCorrectNamespace = Types.InAssemblies(warehouseAssemblies)
            .That()
            .ResideInNamespaceStartingWith("BrewUp.Warehouse")
            .And()
            .AreNotNested()
            .GetTypes();
        
        // Find types with incorrect namespace (difference between the two sets)
        var warehouseTypeArray = warehouseTypes as Type[] ?? warehouseTypes.ToArray();
        var typesWithIncorrectNamespace = warehouseTypeArray.Except(typesWithCorrectNamespace).ToList();

        foreach (var type in typesWithIncorrectNamespace)
        {
            if (type.Namespace != null)
                Assert.Fail(
                    $"Namespace violation detected: {type.FullName} in assembly {type.Assembly.GetName().Name} should start " +
                    $"with 'BrewUp.Warehouse' but is in namespace '{type.Namespace}'");
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