using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class OrderItem
{
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
