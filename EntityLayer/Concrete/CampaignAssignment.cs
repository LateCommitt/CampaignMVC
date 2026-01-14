using System;
using EntityLayer.Enums;

namespace EntityLayer.Concrete
{
    public class CampaignAssignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }         
        public User User { get; set; }          

        public int CampaignId { get; set; }     
        public Campaign Campaign { get; set; }  

        public int Score { get; set; }
        public CampaignStatus Status { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
