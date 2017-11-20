using DatabaseModel;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class LoginHistoryRepo : ILoginHistory
    {
        EduZoneDBEntities _dbx;

        public LoginHistoryRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }
        public void AddLoginRecord(DomainModel.BusinessObject.LoginHistory loginHistoryDomain)
        {
            LoginHistory loginHistory = new LoginHistory();
            loginHistory.userId = loginHistoryDomain.user.UserId;
            loginHistory.username = loginHistoryDomain.user.username;
            loginHistory.loginDateTimeStamp = DateTime.Now;
            loginHistory.totalCashAmount = new Decimal(0.0);
            _dbx.LoginHistories.Add(loginHistory);
            _dbx.SaveChanges();
        }

        public decimal GetTotalCashPerDay(DateTime dateTime)
        {
            decimal totalCashPerDay = 0;
            List<LoginHistory> lst_loginHistory = (from l in _dbx.LoginHistories
                                                   where System.Data.Entity.DbFunctions.TruncateTime(l.loginDateTimeStamp) == dateTime.Date
                                                   select l).ToList();
            foreach (LoginHistory loginHistory in lst_loginHistory)
            {
                totalCashPerDay += loginHistory.totalCashAmount;
            }
            return totalCashPerDay;
        }

        public void UpdateCashValue(DomainModel.BusinessObject.User user,decimal addedCashAmount)
        {
            LoginHistory loginHistory = (from l in _dbx.LoginHistories
                                         where l.userId == user.UserId
                                         select l).OrderByDescending(l => l.Id).FirstOrDefault();
            loginHistory.totalCashAmount += addedCashAmount;
            _dbx.SaveChanges();
        }

        public void UpdateLogoutDateTime(DomainModel.BusinessObject.User user)
        {
            LoginHistory loginHistory = (from l in _dbx.LoginHistories
                          where l.userId==user.UserId
                          select l).OrderByDescending(l=>l.Id).FirstOrDefault();
            loginHistory.logoutDateTimeStamp = DateTime.Now;
            _dbx.SaveChanges();
        }
    }
}
