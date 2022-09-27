using AutoMapper;
using Domain.Interfaces.Repositories;
using FluentValidation.Results;
using System.Net;

namespace Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result RequestError(string message, ValidationResult validationResult)
        {
            return new Result()
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
            return new Result<TObject>()
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

        public Result NotFound(string message) => new Result()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };

        public Result<TObject> NotFound<TObject>(string message) => new Result<TObject>()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };
        
        public Result Fail(string message) => new Result()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        public Result<TObject> Fail<TObject>(string message) => new Result<TObject>()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        public Result Ok() => new Result()
        {
            Success = true
        };

        public Result<TObject> Ok<TObject>() => new Result<TObject>()
        {
            Success = true
        };

        public Result Ok(string message) => new Result()
        {
            Success = true,
            Message = message
        };

        public Result<TObject> Ok<TObject>(string message) => new Result<TObject>()
        {
            Success = true,
            Message = message
        };

        public Result<TObject> Ok<TObject>(TObject data) => new Result<TObject>()
        {
            Success = true,
            Data = data
        };

        public Result<TObject> Ok<TObject>(TObject data, string message) => new Result<TObject>()
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
}
