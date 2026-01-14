using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Managers
{
    public class UserManager : IUserService
    {
        public List<User> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(User t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(User t)
        {
            throw new NotImplementedException();
        }

        public User TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(User t)
        {
            throw new NotImplementedException();
        }
    }
}
