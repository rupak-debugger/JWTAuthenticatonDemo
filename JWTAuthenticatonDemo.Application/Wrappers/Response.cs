using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Wrappers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public ICollection<string>? Error { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }

        public Response(T data, string? message )
        {
            this.Data = data;
            this.Message = message;
            this.Success = true;
        }

    }
}
