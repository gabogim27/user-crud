﻿namespace UserCrud.Domain.Exceptions
{
    using System.Net;
    
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
