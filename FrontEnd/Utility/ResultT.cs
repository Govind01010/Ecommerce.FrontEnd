using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Domain.Shared
{
    public class Result<TValue>:Result
    {
        public TValue? _value;

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("The Value in the failure result can not be accessed.");

        protected internal Result(TValue? value, bool isSuccess ,  string? errorMessage) : base(isSuccess,errorMessage)
        {
            _value = value;
        }

        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value,true,null);
        }
        public static Result<TValue> Failure(string? errorMessage)
        {
            return new Result<TValue>(default, false, errorMessage);
        }

    }
}
