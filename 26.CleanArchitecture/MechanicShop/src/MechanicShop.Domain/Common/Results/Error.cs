namespace MechanicShop.Domain.Common.Results;

public readonly record struct Error
{
    public string Code { get; }

    public string Description { get; }

    public ErrorKind Type { get; }

    private Error(string Code, string Description, ErrorKind Type)
    {
        this.Code = Code;
        this.Description = Description;
        this.Type = Type;
    }
    public static Error Failure(string Code = nameof(Failure), string Description = "General Failure Error") =>
        new Error(Code, Description, ErrorKind.Failure);

    public static Error Unexpected(string Code = nameof(Unexpected), string Description = "General Unexpected Error") =>
       new Error(Code, Description, ErrorKind.Unexpected);
    public static Error Validation(string Code = nameof(Validation), string Description = "General Validation Error") =>
       new Error(Code, Description, ErrorKind.Validation);

    public static Error Conflict(string Code = nameof(Conflict), string Description = "General Conflict Error") =>
       new Error(Code, Description, ErrorKind.Conflict);
    public static Error Unauthorise(string Code = nameof(Unauthorise), string Description = "General Unauthorise Error") =>
       new Error(Code, Description, ErrorKind.Unauthorise);

    public static Error Forbidden(string Code = nameof(Forbidden), string Description = "General Forbidden Error") =>
       new Error(Code, Description, ErrorKind.Forbidden);
        
     public static Error Create(int Type, string Code , string Description) =>
       new Error(Code, Description, (ErrorKind)Type);
} 
