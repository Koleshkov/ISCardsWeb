using ISCardsWeb.Client.Data;
using ISCardsWeb.Shared.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace ISCardsWeb.Client.PageModels
{
    public class LoginPageModel
    {
        //services
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly NavigationManager navigationManager;

        //props
        public LoginRequest Request { get; set; } = new();
        public List<string> Errors { get; set; } = new();
        public bool IsVisibleErrorList { get; set; }
        public bool IsVisibleSpinner { get; set; }

        public LoginPageModel(AuthenticationStateProvider authenticationStateProvider, 
            NavigationManager navigationManager)
        {
            this.authenticationStateProvider=authenticationStateProvider;
            this.navigationManager=navigationManager;
        }


        public async Task LoginAction()
        {
            IsVisibleSpinner = true;

            var customAuthProvider = ((CustomAuthenticationStateProvider)authenticationStateProvider);

            var response = await customAuthProvider.MarkUserAsAuthenticatedAsync(Request);

            if (response!=null)
            {
                Errors = response.Errors ?? new List<string>();

                if (Errors.Count==0) navigationManager.NavigateTo("/profile");
                else IsVisibleErrorList = true;
            }
            else
            {
                Errors = new List<string> { "Нет ответа от сервера." };
            }

            IsVisibleSpinner=false;
        }
    }
}
