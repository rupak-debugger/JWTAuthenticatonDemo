using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Wrappers
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public ICollection<string>? Error { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public Response(T data)
        {
            this.Data = data;
            this.Success = true;
        }
        public Response(T data, string? message )
        {
            this.Data = data;
            this.Message = message;
            this.Success = true;
        }
        public Response(string message)
        {
            this.Success = false;
            this.Message = message;
        }
        public Response(List<string> errors)
        {
            this.Error = errors;
            this.Success = false;
        }

    }
}
