using DatabaseModel;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BusinessObject;

namespace Repositories
{
    public class MoneyListRepo :IMoneyList
    {
        EduZoneDBEntities _dbx;

        public MoneyListRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }

        public DomainModel.BusinessObject.MoneyList LoadMoneyList(string Description)
        {
            DatabaseModel.MoneyList moneyList = _dbx.MoneyLists.SingleOrDefault(p => p.Description == Description);
            DomainModel.BusinessObject.MoneyList moneyListDomain = null;
            if (moneyList != null)
            {
                moneyListDomain = new DomainModel.BusinessObject.MoneyList();
                moneyListDomain.Id = moneyList.Id;
                moneyListDomain.Time = moneyList.Tme;
                moneyListDomain.Amount = moneyList.Amount;
                moneyListDomain.Description = moneyList.Description;
            }
            return moneyListDomain;
        }
    }
}
