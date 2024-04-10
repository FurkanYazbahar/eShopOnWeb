using System;
using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderSpecification : Specification<Order>
{
    public OrderSpecification(int orderId)
    {
        Query.Where(c => c.Id == orderId)
            .Include(o => o.OrderItems);
    }
}
