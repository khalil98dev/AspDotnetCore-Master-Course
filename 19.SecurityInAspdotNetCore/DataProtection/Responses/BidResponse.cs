using System.Reflection.Metadata.Ecma335;
using DataProtection.Modules;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DataProtection.Responses;

public class BidResponse
{
    public Guid ID { get; set; }
    public decimal Ammount { get; set; }
    public DateTime BidDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    static public  BidResponse FromModel(Bid? bidModel)
    {
        if (bidModel is null)
            throw new ArgumentException("Model ca'nt be null");
        
        var response= new BidResponse
        {
            ID = bidModel.ID,
            FirstName = bidModel.FirstName,
            LastName = bidModel.LastName,
            Address = bidModel.Address,
            Ammount = bidModel.Ammount,
            BidDate = bidModel.BidDate,
            Email = bidModel.Email,
            Phone = bidModel.Phone
        };
        return response;
    }

    static public IEnumerable<BidResponse> FromModels(IEnumerable<Bid>? bidModeles)
    {
        if (bidModeles is null)
        {
            throw new ArgumentException("Model ca'nt be null");
        }
        IEnumerable<BidResponse> responses = bidModeles.Select(FromModel);
        return responses!;    
    }
}