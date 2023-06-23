using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public enum ErrorCode
    {
        Success,
        InvalidCredentials,
        InvalidData,
        NotFound,
        ServerInternalError,
        ClientError,
        NoResponse
    }

    public class Error
    {
        public ErrorCode? Code { get; set; } 
        public string? Message { get; set; }

        public Error() { }
    }
}
