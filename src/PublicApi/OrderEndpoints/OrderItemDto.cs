using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderItemDto
{
    //public CatalogItemOrdered ItemOrdered { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
