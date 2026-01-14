using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Managers
{
    public class CampaignAssignmentManager : ICampaignAssignmentService
    {
        ICampaignAssignmentDal _campaignAssignmentDal;

        public CampaignAssignmentManager(ICampaignAssignmentDal campaignAssignmentDal)
        {
            _campaignAssignmentDal = campaignAssignmentDal;
        }
        public List<CampaignAssignment> GetList()
        {
            return _campaignAssignmentDal.GetListAll();
        }

        public void TAdd(CampaignAssignment t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(CampaignAssignment t)
        {
            throw new NotImplementedException();
        }

        public CampaignAssignment TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(CampaignAssignment t)
        {
            throw new NotImplementedException();
        }
    }
}
