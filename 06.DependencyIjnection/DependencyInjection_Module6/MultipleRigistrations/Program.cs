var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedTransient<IPerson, Student>("std"); 
builder.Services.AddKeyedTransient<IPerson, Employee>("emp");


var app = builder.Build();

app.MapGet("/GetStudentFullName/{ID}", (int ID,[FromKeyedServicesAttribute("std")] IPerson people) =>
{
  return people.GetFullName(ID); 
});


app.MapGet("/GetEmployeeFullName/{ID}", (int ID,[FromKeyedServicesAttribute("emp")] IPerson people) =>
{
    
    return people.GetFullName(ID); 

});



app.Run();


public interface IPerson
{
    string GetFullName(int ID);
}

public class Student : IPerson
{
    public string GetFullName(int ID) => $"student with ID {ID}";

}

public class Employee : IPerson
{
    public string GetFullName(int ID) => $"Employee with ID {ID}";
}





