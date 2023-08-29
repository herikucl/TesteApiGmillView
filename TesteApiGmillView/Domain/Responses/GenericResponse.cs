using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteApiGmillView.Domain.Response
{
    public class GenericResponse
    {
        public string Message { get; set; }

        public static GenericResponse Fail(string message) => new GenericResponse { Message = message };
        public static GenericResponse<T> Fail<T>(string message) => new GenericResponse<T> { Message = message };

        public static GenericResponse Ok(string message) => new GenericResponse {Message = message };
        public static GenericResponse<T> Ok<T>(T data) => new GenericResponse<T> {Data = data };
    }

    public class GenericResponse<T> : GenericResponse
    {
        public T Data { get; set; }
    }
}
