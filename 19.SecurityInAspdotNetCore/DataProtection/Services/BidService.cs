using DataProtection.Modules;
using DataProtection.Requestes;
using DataProtection.Responses;
using Microsoft.EntityFrameworkCore;

namespace DataProtection.Services;

public class BidService(Data.AppContext _context) : IBidService
{

    public async Task<BidResponse> AddNewBidAsync(BidRequest request, CancellationToken ct)
    {
        var newBid = new Bid
        {
            ID = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Ammount = request.Ammount,
            Address = request.Address,
            Phone = request.Phone,
            BidDate = DateTime.UtcNow

        };


        await _context.Bids.AddAsync(newBid, ct);

        if (await _context.SaveChangesAsync(ct) > 0)
        {
            return BidResponse.FromModel(newBid);
        }
        else
            return null;   
    }

    public async Task<IEnumerable<BidResponse>> GetAllBidsAsync(CancellationToken ct)
    {
        var itmes =await  _context.Bids.ToListAsync(ct);

        return BidResponse.FromModels(itmes); 

    }

    public async Task<BidResponse> GetBdByIDAsync(Guid ID, CancellationToken ct)
    {
        var BidItem = await _context.Bids.FirstOrDefaultAsync(p => p.ID == ID, ct);

        return  BidResponse.FromModel(BidItem); 
    }

}