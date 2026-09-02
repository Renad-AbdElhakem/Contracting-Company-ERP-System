using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application
{
    public class GeneralResponse<T>
    {
        public T? Data { get; set; } = default(T);
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public static GeneralResponse<T> Success(T ?data = default(T), string? Message = null)
        {

            return new GeneralResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = Message
            };


        }
        public static GeneralResponse<T> Fail(string? Message = null)
        {

            return new GeneralResponse<T>
            {

                IsSuccess = false,
                Message = Message
            };


        }
    }
}
