using Catalog.Domain.Entities;

namespace Catalog.Domain.Events;

public class ProductDeletedEvent : BaseEvent
{
    public ProductDeletedEvent(Item product)
    {
        Product = product;
    }

    public Item Product { get; init; }
}
