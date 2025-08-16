namespace DataProtection.Services;

using DataProtection.Modules;
using DataProtection.Requestes;
using DataProtection.Responses;

public interface IBidService
{
    Task<IEnumerable<BidResponse>> GetAllBidsAsync(CancellationToken ct);
    Task<BidResponse> GetBdByIDAsync(Guid ID,CancellationToken ct);
    Task<BidResponse> AddNewBidAsync(BidRequest newBid,CancellationToken ct);

}