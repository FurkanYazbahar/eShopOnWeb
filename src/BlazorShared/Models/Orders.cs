using System;
using System.Collections.Generic;

namespace BlazorShared.Models;
public class Orders
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderDataStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

public enum OrderDataStatus
{
    PENDING=0, APPROVED=1, FAILED=2, SUBMITTED = 3
}
