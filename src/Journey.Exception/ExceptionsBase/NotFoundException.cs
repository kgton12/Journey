﻿using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException(string message) : JorneyException(message)
    {
        public override IList<string> GetErrorMessages()
        {
            return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
