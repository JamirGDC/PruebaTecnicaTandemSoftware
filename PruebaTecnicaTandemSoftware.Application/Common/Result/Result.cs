using System.Net;

namespace PruebaTecnicaTandemSoftware.Application.Common.Result;


public abstract class ResultBase
{
    protected ResultBase(bool isSuccess, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        HttpStatusCode = statusCode;
    }
    public bool IsSuccess { get; protected set; }
    public HttpStatusCode HttpStatusCode { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public List<string> Errors { get; protected set; } = new List<string>();
}

public class Result<T> : ResultBase
{
    private Result(bool isSuccess, HttpStatusCode statusCode) : base(isSuccess, statusCode)
    {
    }
    public T? Payload { get; private set; }

    public static Result<T> Success(HttpStatusCode statusCode)
    {
        return new Result<T>(true, statusCode);
    }

    public static Result<T> Failure(HttpStatusCode statusCode)
    {
        return new Result<T>(false, statusCode);
    }

    public Result<T> WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Result<T> WithErrors(params string[] errors)
    {
        Errors.AddRange(errors);
        return this;
    }

    public Result<T> WithPayload(T payload)
    {
        Payload = payload;
        return this;
    }
}