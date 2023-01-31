using ISCardsWeb.Client.Services.Authentication;
using ISCardsWeb.Shared.Requests;
using Microsoft.AspNetCore.Components;

namespace ISCardsWeb.Client.PageModels
{
    public class RegisterPageModel
    {
        private readonly IAuthenticationService authenticationService;
        private readonly NavigationManager navigationManager;
        

        public RegisterRequest Request { get; set; } = new();
        public List<string> Errors { get; set; } = new();
        public bool IsVisibleErrorList { get; set; }
        public bool IsVisibleMsgBox { get; set; }

        public bool IsVisibleSpinner { get; set; }

        public RegisterPageModel(IAuthenticationService authenticationService, NavigationManager navigationManager)
        {
            this.authenticationService=authenticationService;
            this.navigationManager=navigationManager;
        }

        public async Task RegisterAction()
        {
            IsVisibleSpinner = true;

            var response = await authenticationService.RegisterUserAsync(Request);

            if (response != null)
            {
                Errors = response.Errors ?? new List<string>();

                if (Errors.Count==0) IsVisibleMsgBox=true;
                else IsVisibleErrorList = true;
            }
            else
            {
                Errors = new List<string> { "Нет ответа от сервера." };
            }

            IsVisibleSpinner = false;
        }

        public void GoToLoginPage() =>
            navigationManager.NavigateTo("/Authentication/Login");
    }
}
