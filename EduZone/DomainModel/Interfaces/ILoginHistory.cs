using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface ILoginHistory
    {
        void AddLoginRecord(DomainModel.BusinessObject.LoginHistory loginHistoryDomain);
        void UpdateCashValue(DomainModel.BusinessObject.User user, Decimal addedCashAmount);
        void UpdateLogoutDateTime(DomainModel.BusinessObject.User user);
        decimal GetTotalCashPerDay(DateTime dateTime);
    }
}
