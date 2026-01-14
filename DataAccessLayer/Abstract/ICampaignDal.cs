using EntityLayer.Concrete;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
     public interface ICampaignDal : IGenericDal<EntityLayer.Concrete.Campaign>
    {
    }
}
