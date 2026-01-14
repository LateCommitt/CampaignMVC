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

        public List<CampaignAssignment> GetAllList()
        {
            return _campaignAssignmentDal.GetAllList();
        }

        public List<CampaignAssignment> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(CampaignAssignment t)
        {
            _campaignAssignmentDal.Insert(t);
        }

        public void TDelete(CampaignAssignment t)
        {
            _campaignAssignmentDal.Delete(t);
        }

        public CampaignAssignment TGetById(int id)
        {
            return _campaignAssignmentDal.GetById(id);
        }

        public void TUpdate(CampaignAssignment t)
        {
            _campaignAssignmentDal.Update(t);
        }
    }
}
