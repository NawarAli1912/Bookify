using System.Diagnostics.CodeAnalysis;

namespace Bookify.Domain.Abstraction;
public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);


    public static implicit operator Result(Error error) => new(false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);

    public static Result<T> Success<T>(T value) => new(value, true, Error.None);

    public static Result<T> Failure<T>(Error error) => new(default, false, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);

    public static implicit operator Result<TValue>(Error error) => new(default, false, error);

    public void Switch(Action<TValue> onSuccess, Action<Error> onFailure)
    {
        if (IsFailure)
        {
            onFailure(Error);
            return;
        }

        onSuccess(Value);
    }

}

public class PagedResult<T> : Result
{
    private readonly IEnumerable<T> _items;

    protected internal PagedResult(
        IEnumerable<T> items,
        int count,
        bool isSuccess,
        Error error) : base(isSuccess, error)
    {
        _items = items;
        Count = count;
    }

    public IEnumerable<T> Items => IsSuccess
        ? _items!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public int Count { get; init; }

    public static PagedResult<TResult> Create<TResult>(IEnumerable<TResult> items, int count) => new(items, count, true, Error.None);

    public static implicit operator PagedResult<T>(List<T> items) => Create(items, -1);

    public static implicit operator PagedResult<T>(Error error) => new([], -1, false, error);
}