using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Domain.Shared
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; set; }
        public Result(bool isSuccess, string? message)
        {
            if(isSuccess && !string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException();
            }
            if(!isSuccess && string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            ErrorMessage = message;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }
        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }
}
