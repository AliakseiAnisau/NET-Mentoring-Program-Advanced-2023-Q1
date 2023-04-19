using Catalog.Domain.Entities;

namespace Catalog.Domain.Events;

public class ProductUpdatedEvent : BaseEvent
{
    public ProductUpdatedEvent(Item oldProduct, Item newProduct)
    {
        OldProduct = oldProduct;
        NewProduct = newProduct;
    }

    public Item OldProduct { get; init; }
    public Item NewProduct { get; init; }

}
