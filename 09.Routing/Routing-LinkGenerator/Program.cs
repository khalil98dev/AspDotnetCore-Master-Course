var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();



app.MapGet("Orders/{OrderID:int}", (int OrderID,LinkGenerator link,HttpContext contex) =>
{
    var EditUrl = link.GetUriByName("Editorder",
                                    new{OrderID},
                                    contex.Request.Scheme,
                                    contex.Request.Host);
    var CreateUrl = link.GetUriByName("CreateOrder",
                                      new{OrderID},
                                      contex.Request.Scheme,
                                      contex.Request.Host);
    return Results.Ok(
        new {
        OrderID = OrderID,
        _links = new
        {
            self = contex.Request.Path,
            Update = EditUrl, 
            Createnew=CreateUrl
        }
        }
       

    );
});


app.MapPut("/Orders/Update/{OrderID:int}", (int? OrderID) =>
{
    return Results.NoContent();
}
).WithName("Editorder");

app.MapPost("/Orders/Create/", (Order newOrder) =>
{
    return newOrder;
}
).WithName("CreateOrder");


app.Run();


public class Order
{
    public int OrderID { get; set; }

    public string? Name { get; set; }
    
    public decimal Price { get; set; }  
}