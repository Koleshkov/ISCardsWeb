using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Shared.Responses
{
    public class RegisterResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
