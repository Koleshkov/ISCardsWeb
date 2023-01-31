using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCardsWeb.Shared.Responses
{
    public class UserResponse : BaseResponse
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Organization { get; set; } = "";
        public string Position { get; set; } = "";
        public string Department { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        
    }
}
