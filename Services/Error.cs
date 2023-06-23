﻿using System;
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
        ServerInternalError,
        ClientError
    }

    public class Error
    {
        public ErrorCode? Code { get; set; } 
        public string? Message { get; set; }

        public Error() { }
    }
}