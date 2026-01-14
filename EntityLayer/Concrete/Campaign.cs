using System;
using EntityLayer.Enums;

namespace EntityLayer.Concrete
{
    public class Campaign
    {
        public int Id { get; set; }
        public CampaignType Type { get; set; }
        public Segment TargetSegment { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public List<CampaignAssignment> CampaignAssignments { get; set; }
    }
}
