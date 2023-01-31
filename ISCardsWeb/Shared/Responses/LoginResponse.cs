using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Shared.Responses
{
    public class LoginResponse : BaseResponse
    {

        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
