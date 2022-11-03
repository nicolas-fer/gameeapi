using AutoMapper;
using Domain.Interfaces.Repositories;
using FluentValidation.Results;
using System.Net;

namespace Application.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public Result RequestError(string message, ValidationResult validationResult)
        {
            return new Result
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = validationResult.Errors.Select(x => new ErrorField()
                {
                    Field = x.PropertyName,
                    Message = x.ErrorMessage
                }).AsEnumerable(),
            };
        }
        public Result<TObject> RequestError<TObject>(string message, ValidationResult validationResult)
        {
            return new Result<TObject>
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = validationResult.Errors.Select(x => new ErrorField()
                {
                    Field = x.PropertyName,
                    Message = x.ErrorMessage
                }).AsEnumerable(),
            };
        }

        public Result NotFound(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };

        public Result<TObject> NotFound<TObject>(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };

        public Result Fail(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        public Result<TObject> Fail<TObject>(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        public Result Ok() => new()
        {
            Success = true,
            StatusCode = HttpStatusCode.OK
        };

        public Result<TObject> Ok<TObject>() => new()
        {
            Success = true,
            StatusCode = HttpStatusCode.OK
        };

        public Result Ok(string message) => new()
        {
            Success = true,
            Message = message,
            StatusCode = HttpStatusCode.OK
        };

        public Result<TObject> Ok<TObject>(string message) => new()
        {
            Success = true,
            Message = message,
            StatusCode = HttpStatusCode.OK
        };

        public Result<TObject> Ok<TObject>(TObject data) => new()
        {
            Success = true,
            Data = data,
            StatusCode = HttpStatusCode.OK
        };

        public Result<TObject> Ok<TObject>(TObject data, string message) => new()
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.OK
        };
    }
}
