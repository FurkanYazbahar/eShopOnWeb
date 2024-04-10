using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetByIdEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;

    public OrderGetByIdEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{OrderId}",
            async (int orderId, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderRequest(orderId), itemRepository);
            })
            .Produces<GetByIdOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var orderSpec = new OrderSpecification(request.OrderId);
        var item = await itemRepository.FirstOrDefaultAsync(orderSpec);
        if (item is null)
            return Results.NotFound();

        response.Order = new OrderDto
        {
            Id = item.Id,
            OrderDate = item.OrderDate,
            BuyerId = item.BuyerId,
            OrderItems = item.OrderItems.Select(oi => new OrderItemDto
            {
                ProductName = oi.ItemOrdered.ProductName,
                UnitPrice = oi.UnitPrice,
                Units = oi.Units
            }).ToList(),
            TotalPrice = item.Total(),
            Status = item.Status
        };
        return Results.Ok(response);
    }
}
