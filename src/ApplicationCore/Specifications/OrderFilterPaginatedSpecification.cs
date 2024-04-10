using System;
using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take, string? buyerId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(i => (String.IsNullOrEmpty(buyerId) || i.BuyerId == buyerId))
            .Skip(skip).Take(take)
            .Include(o => o.OrderItems);
    }
}
