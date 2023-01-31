

namespace ISCardsWeb.Domain.Models
{
    public class BaseCard : EntityBase
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public string CardType { get; set; } = "";
        public string CreatorName { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string RespName { get; set; } = "";
        public DateTime Deadline { get; set; }

    }
}
