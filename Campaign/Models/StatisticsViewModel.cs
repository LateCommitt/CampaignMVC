using EntityLayer.Concrete;
using EntityLayer.Enums;

namespace Campaign.Models
{
    public class StatusCountInfo
    {
        public CampaignStatus Status { get; set; }
        public int Count { get; set; }
    }

    public class CampaignUsageInfo
    {
        public int CampaignId { get; set; }
        public EntityLayer.Concrete.Campaign? Campaign { get; set; }
        public int Total { get; set; }
        public int Used { get; set; }
    }
}
