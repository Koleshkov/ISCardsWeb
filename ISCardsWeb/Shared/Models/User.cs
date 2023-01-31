using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ISCardsWeb.Shared.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string MiddleName { get; set; } = "";

        public string Organization { get; set; } = "";

        public string Position { get; set; } = "";

        public string Department { get; set; } = "";

    }
}
