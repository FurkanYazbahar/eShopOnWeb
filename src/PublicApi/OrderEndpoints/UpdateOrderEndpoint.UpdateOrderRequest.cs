using System.ComponentModel.DataAnnotations;
using Microsoft.eShopWeb.ApplicationCore.Enums;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    public int Id { get; set; }    
    public DataStatus Status { get; set; }    
}
