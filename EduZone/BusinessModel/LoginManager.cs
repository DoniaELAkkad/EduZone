using DomainModel.BusinessObject;
using DomainModel.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class LoginManager
    {
       private IUser userRepo;
       private ILoginHistory loginHistoryRepo;

        public LoginManager()
        {
            DatabaseModel.EduZoneDBEntities database = new DatabaseModel.EduZoneDBEntities();
            userRepo = new UserRepo(database);
            loginHistoryRepo = new LoginHistoryRepo(database);
        }
       
        public DomainModel.BusinessObject.User validateUser(String username, String password)
        {
            User user= userRepo.isUserValid(username, password);
            if (user != null)
            {
                LoginHistory loginHistory = new LoginHistory();
                loginHistory.user = user;
                loginHistoryRepo.AddLoginRecord(loginHistory);
            }
            return user;
        }

        public void LogoutUser(DomainModel.BusinessObject.User user)
        {
            loginHistoryRepo.UpdateLogoutDateTime(user);   
        }

        public void updateCashPerUser(DomainModel.BusinessObject.User user,decimal amountAdded)
        {
            loginHistoryRepo.UpdateCashValue(user, amountAdded);
        }
        public Decimal GetTotalCashPerDay()
        {
            return loginHistoryRepo.GetTotalCashPerDay(DateTime.Now);
        }
    }
}
