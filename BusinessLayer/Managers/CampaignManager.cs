using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Managers
{
    public class CampaignManager : ICampaignService
    {
        public List<EntityLayer.Concrete.Campaign> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(EntityLayer.Concrete.Campaign t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(EntityLayer.Concrete.Campaign t)
        {
            throw new NotImplementedException();
        }

        public EntityLayer.Concrete.Campaign TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(EntityLayer.Concrete.Campaign t)
        {
            throw new NotImplementedException();
        }
    }
}
