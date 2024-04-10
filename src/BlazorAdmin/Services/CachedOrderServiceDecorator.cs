using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class CachedOrderServiceDecorator : IOrderService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly OrderService _orderService;
    private ILogger<CachedOrderServiceDecorator> _logger;

    public CachedOrderServiceDecorator(ILocalStorageService localStorageService,
        OrderService orderService,
        ILogger<CachedOrderServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<List<Orders>> ListPaged(int pageSize)
    {
        string key = "order";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Orders>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading items from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _orderService.ListPaged(pageSize);
        var entry = new CacheEntry<List<Orders>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<List<Orders>> List()
    {
        string key = "order";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<Orders>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading items from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _orderService.List();
        var entry = new CacheEntry<List<Orders>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<Orders> GetById(int id)
    {
        var item = await _orderService.GetById(id);
        return item;
    }

    public async Task<Orders> Edit(Orders order)
    {
        var result = await _orderService.Edit(order);
        await RefreshLocalStorageList();

        return result;
    }

    private async Task RefreshLocalStorageList()
    {
        string key = "order";

        await _localStorageService.RemoveItemAsync(key);
        var items = await _orderService.List();
        var entry = new CacheEntry<List<Orders>>(items);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
