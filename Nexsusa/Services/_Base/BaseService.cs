using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Services._Base
{
    public abstract class BaseService<C> where C : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly IMapper _mapper;
        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        public BaseService(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Error
        internal ResponseResult<T> Error<T>(Exception ex)
        {

            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = new List<string> { ex.GetError() },
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Error<T>(Exception ex, HttpStatusCode httpStatus)
        {

            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = new List<string> { ex.GetError() },
                StatusCode = httpStatus,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Error<T>(string errorMsg)
        {
            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = new List<string> { errorMsg },
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Error<T>(string errorMsg, HttpStatusCode httpStatus)
        {

            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = new List<string> { errorMsg },
                StatusCode = httpStatus,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Error<T>(List<string> errorMsgs)
        {
            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = errorMsgs,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Error<T>(List<string> errorMsgs, HttpStatusCode statusCode)
        {
            return new ResponseResult<T>
            {
                IsSuccess = false,
                Errors = errorMsgs,
                StatusCode = statusCode,
            };
        }
        #endregion


        #region Success
        //============//============//============//============//============
        internal ResponseResult<T> Success<T>()
        {
            return new ResponseResult<T>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Success<T>(HttpStatusCode statusCode)
        {
            return new ResponseResult<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Success<T>(T data)
        {
            return new ResponseResult<T>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = data,
            };
        }

        //============//============//============//============//============
        internal ResponseResult<T> Success<T>(T data, HttpStatusCode statusCode)
        {
            return new ResponseResult<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Data = data,
            };
        }

        #endregion



    }


}
