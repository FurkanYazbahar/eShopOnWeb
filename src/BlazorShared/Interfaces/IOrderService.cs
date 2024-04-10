using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;
public interface IOrderService
{    
    Task<Orders> Edit(Orders order);    
    Task<Orders> GetById(int id);
    Task<List<Orders>> ListPaged(int pageSize);
    Task<List<Orders>> List();
}
