using EntityLayer.Enums;

namespace EntityLayer.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public Segment Segment { get; set; }

        // İlişkiler
        public List<CampaignAssignment> CampaignAssignments { get; set; }
        public UserMetrics UserMetrics { get; set; } // Bire-bir ilişki
        public List<Notification> Notifications { get; set; } // Bire-çok ilişki
    }
}
