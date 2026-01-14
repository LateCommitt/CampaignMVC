using DataAccessLayer.Abstract;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    class EfCampainAssignmentDal : GenericRepository<EntityLayer.Concrete.CampaignAssignment>, ICampaignAssignmentDal
    {
    }
}
