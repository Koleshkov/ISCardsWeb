using ISCardsWeb.Aplication.Common.Exceptions;

namespace ISCardsWeb.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"Не удалось найти {name}")
        { }
    }
}
