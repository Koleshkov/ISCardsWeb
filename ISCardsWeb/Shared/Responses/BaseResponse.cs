using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace ISCardsWeb.Shared.Responses
{
    public class BaseResponse
    {
        public List<string>? Errors { get; set; }
    }
}
