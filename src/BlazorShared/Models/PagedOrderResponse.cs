using System.Collections.Generic;

namespace BlazorShared.Models;

public class PagedOrderResponse
{
    public List<Orders> Orders { get; set; } = new List<Orders>();
    public int PageCount { get; set; } = 0;
}
