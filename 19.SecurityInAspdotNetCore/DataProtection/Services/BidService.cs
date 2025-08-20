using DataProtection.Modules;
using DataProtection.Requestes;
using DataProtection.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace DataProtection.Services;

public class BidService(Data.AppContext _context,IDataProtectionProvider dataProtectionProvider) : IBidService
{
    private readonly IDataProtector _protector =
     dataProtectionProvider.CreateProtector("Bidding.Protector");

    public async Task<BidResponse> AddNewBidAsync(BidRequest request, CancellationToken ct)
    {
        var newBid = new Bid
        {
            ID = Guid.NewGuid(),

            FirstName = _protector.Protect(request.FirstName!),
            LastName = _protector.Protect(request.LastName!),

            Ammount = request.Ammount,

            Address = _protector.Protect(request.Address!),
            Phone = _protector.Protect(request.Phone!),
            Email = _protector.Protect(request.Email!),

            BidDate = DateTime.UtcNow
        };


        await _context.Bids.AddAsync(newBid, ct);

        if (await _context.SaveChangesAsync(ct) > 0)
        {
            return BidResponse.FromModel(newBid,_protector);
        }
        else
            return null;   
    }

    public async Task<IEnumerable<BidResponse>> GetAllBidsAsync(CancellationToken ct)
    {
        var itmes =await  _context.Bids.ToListAsync(ct);

        return BidResponse.FromModels(itmes,_protector); 

    }

    public async Task<BidResponse> GetBdByIDAsync(Guid ID, CancellationToken ct)
    {
        var BidItem = await _context.Bids.FirstOrDefaultAsync(p => p.ID == ID, ct);

        return  BidResponse.FromModel(BidItem,_protector); 
    }

}