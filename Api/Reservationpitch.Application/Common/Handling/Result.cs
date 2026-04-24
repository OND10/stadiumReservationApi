using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Common.Handling
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;

        public static Task<Result<T>> SuccessAsync(T model, string message)
        {
            return Task.FromResult<Result<T>>(new Result<T> { Data = model, Message = message });
        }

        public static Task<Result<T>> SuccessAsync(T model, string message, bool success)
        {
            return Task.FromResult<Result<T>>(new Result<T> { Data = model, IsSuccess = success, Message = message });
        }

        public static Task<Result<T>> SuccessAsync(string message, bool success)
        {
            return Task.FromResult<Result<T>>(new Result<T> { IsSuccess = success, Message = message });
        }

        public static Task<Result<T>> FaildAsync(bool fail, string message)
        {
            return Task.FromResult<Result<T>>(new Result<T> { Message = message, IsSuccess = fail });
        }
    }
}
