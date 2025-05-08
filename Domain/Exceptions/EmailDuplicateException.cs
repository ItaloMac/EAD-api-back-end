using System;

namespace Domain.Exceptions;

public class EmailDuplicate
{
     public class EmailDuplicateException : Exception
    {
        public EmailDuplicateException(string mensagem) : base(mensagem) { }
    }
}
