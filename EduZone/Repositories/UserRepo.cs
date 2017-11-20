using DatabaseModel;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepo: IUser
    {
        EduZoneDBEntities _dbx;

        public UserRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }

        public DomainModel.BusinessObject.User isUserValid(string username, string password)
        {
            User user = _dbx.Users.SingleOrDefault(p => p.username == username && p.password==password);
            DomainModel.BusinessObject.User userDomain = null;
            if (user != null)
            {
                userDomain = new DomainModel.BusinessObject.User();
                userDomain.UserId = user.userID;
                userDomain.username = user.username;
            }
            return userDomain;
        }
    }
}
