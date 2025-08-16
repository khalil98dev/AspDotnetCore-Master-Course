namespace DataProtection.Modules;

public class Bid
{
    public Guid ID { get; set; }
    public decimal Ammount { get; set; }
    public DateTime BidDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
   
}