using System;

namespace Domain.Exceptions;

public class CpfDuplicate
{
      public class CpfDuplicateException : Exception
    {
        public CpfDuplicateException(string mensagem) : base(mensagem) { }
    }
}
