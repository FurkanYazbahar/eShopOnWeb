using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;


namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(HttpService httpService, ILogger<OrderService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<Orders> Edit(Orders order)
    {
        return (await _httpService.HttpPut<EditOrderResult>("orders", order)).Order;
    }

    public async Task<Orders> GetById(int id)
    {
        var itemGetTask = _httpService.HttpGet<EditOrderResult>($"orders/{id}");
        await Task.WhenAll(itemGetTask);
        var orderItem = itemGetTask.Result.Order;
        
        return orderItem;
    }

    public async Task<List<Orders>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching Orders from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders?PageSize=10");
        await Task.WhenAll(itemListTask);
        var items = itemListTask.Result.Orders;
       
        return items;
    }

    public async Task<List<Orders>> List()
    {
        _logger.LogInformation("Fetching Orders from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders");
        await Task.WhenAll(itemListTask);        
        var items = itemListTask.Result.Orders;
        
        return items;
    }

}
