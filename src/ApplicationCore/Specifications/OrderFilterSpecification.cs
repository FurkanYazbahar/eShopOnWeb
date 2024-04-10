using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderFilterSpecification : Specification<Order>
{
    public OrderFilterSpecification(string? buyerId)
    {
        Query.Where(i => i.BuyerId == buyerId);
    }
}
