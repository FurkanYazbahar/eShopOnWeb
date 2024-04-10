namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class ListPagedOrderRequest : BaseRequest
{
    public int PageSize { get; init; }
    public int PageIndex { get; init; }
    public string? BuyerId { get; init; }

    public ListPagedOrderRequest(int? pageSize, int? pageIndex, string? buyerId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        BuyerId = buyerId;
    }
}
