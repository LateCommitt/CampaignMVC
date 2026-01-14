using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    interface IUserDal : IGenericDal<EntityLayer.Concrete.User>
    {
    }
}
