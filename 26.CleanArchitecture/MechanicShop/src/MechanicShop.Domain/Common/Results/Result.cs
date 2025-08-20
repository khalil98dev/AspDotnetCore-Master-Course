
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace MechanicShop.Domain.Common.Results;


public class Result<TValue> : IResult<TValue>
{
    private readonly TValue? _value = default;

    private readonly List<Error>? _errors = null;

    public bool IsSuccess { get; }

    public bool IsError => !IsSuccess;
    public TValue Value => IsSuccess ? _value! : default!;
    public List<Error>? Errors => IsError ? _errors! : [];

    private Result(Error error)
    {
        _errors = [error];
    }

    private Result(List<Error> errors)
    {
        if (errors is null || errors.Count == 0)
            throw new ArgumentException("Cannot create An error<TValue> from an empty collection of errors. Provide at least one error.",
            nameof(errors)
            );

        _errors = errors;
        IsSuccess = false;

    }
    //Result Pattern; 
    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
        => IsSuccess ? onValue(Value!) : onError(Errors!); 

    private Result(TValue value)
    {
        if (value is null)
            throw new ArgumentException(nameof(value));

        _value = value;
        IsSuccess = true;
    }

    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors);

    


    [JsonConstructor]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For serialization only.", true)]

    public Result(TValue value, List<Error> errors, bool IsSuccess)
    {
        if (IsSuccess)
        {
            _value = value;
            _errors = [];
            this.IsSuccess = true;
        }
        else
        {
            if (errors is null || errors.Count == 0)
                throw new ArgumentException("Cannot create An error<TValue> from an empty collection of errors. Provide at least one error.",
                nameof(errors)
                );

            _errors = errors;
            this.IsSuccess = false;
            _value = default!;
        }

    }

}

public readonly record struct Success; 
public readonly record struct Updated; 
public readonly record struct Created; 
public readonly record struct Deleted; 
