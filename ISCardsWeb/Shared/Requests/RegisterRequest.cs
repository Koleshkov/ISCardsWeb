using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ISCardsWeb.Shared.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string MiddleName { get; set; } = "";

        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [EmailAddress(ErrorMessage = "Введите адрес электронной почты.")]
        public string Email { get; set; } = "";


        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль должен содержать от {2} до {1} символов.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Пароль иметь одну заглавную, одну прописную букву, " +
            "состоять из латинских букв и содержать спец символ.")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; } = "";
    }
}

