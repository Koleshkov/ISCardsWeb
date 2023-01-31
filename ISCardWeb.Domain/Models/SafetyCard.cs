

namespace ISCardsWeb.Domain.Models
{
    public class SafetyCard :BaseCard
    {
        public string Position { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Organization { get; set; } = "";
        public string Department { get; set; } = "";
        public string JobObject { get; set; } = "";
        public string TypeOfAction { get; set; } = "";
        public string Description { get; set; } = "";
        public string Actions { get; set; } = "";
        public string Reasons { get; set; } = "";
        public string PlannedEvents { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
