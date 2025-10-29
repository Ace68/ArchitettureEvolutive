using System.Diagnostics.CodeAnalysis;
using BrewUp.Rest.Modules;
using NetArchTest.Rules;

namespace BrewUp.Rest.Tests.Architecture;

[ExcludeFromCodeCoverage]
public class BrewUpArchitectureTests
{
    [Fact]
    public void Should_BrewUpArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(IModule).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "BrewUp.Warehouse.Domain",
            "BrewUp.Warehouse.Infrastructure",
            "BrewUp.Warehouse.ReadModel",
            "BrewUp.Warehouse.SharedKernel",
            
            "BrewUp.Purchase.Domain",
            "BrewUp.Purchase.Infrastructure",
            "BrewUp.Purchase.ReadModel",
            "BrewUp.Purchase.SharedKernel",
            
            "BrewUp.Sales.Domain",
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
}