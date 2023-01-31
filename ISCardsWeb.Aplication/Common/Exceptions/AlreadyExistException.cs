using ISCardsWeb.Aplication.Common.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCardsWeb.Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string name) : base($"Пользователь с иминем {name} уже существует")
        { }

    }
}
