using System.Reflection.Metadata.Ecma335;
using DataProtection.Modules;
using Microsoft.AspNetCore.DataProtection;
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

    static public  BidResponse FromModel(Bid? bidModel,IDataProtector protector)
    {
        if (bidModel is null)
            throw new ArgumentException("Model ca'nt be null");
        
        var response= new BidResponse
        {
            ID = bidModel.ID,
            FirstName = protector.Unprotect(bidModel.FirstName!),
            LastName = protector.Unprotect(bidModel.LastName!),
            Address = protector.Unprotect(bidModel.Address!),
           
            Ammount = bidModel.Ammount,
            BidDate = bidModel.BidDate,

            Email = protector.Unprotect(bidModel.Email!),
            Phone = protector.Unprotect(bidModel.Phone!)
        };
        return response;
    }

    static public IEnumerable<BidResponse> FromModels(IEnumerable<Bid>? bidModeles,IDataProtector protector)
    {
        if (bidModeles is null)
        {
            throw new ArgumentException("Model ca'nt be null");
        }
        IEnumerable<BidResponse> responses = bidModeles.Select(p=>FromModel(p,protector));
        return responses!;    
    }
}