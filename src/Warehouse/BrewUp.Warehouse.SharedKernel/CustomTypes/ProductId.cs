using Muflone.Core;

namespace BrewUp.Warehouse.SharedKernel.CustomTypes;

public sealed class ProductId(string value) : DomainId(value)
{
    public static ProductId New() => new(Guid.NewGuid().ToString());
}