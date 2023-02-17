namespace UserCrud.Domain.Exceptions
{
    using System.Net;
    
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException() : base(HttpStatusCode.InternalServerError)
        {
        }

        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}
