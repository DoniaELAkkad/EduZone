using DomainModel.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class MoneyListManager
    {
        IMoneyList moneyListRepo;
        public MoneyListManager()
        {
            DatabaseModel.EduZoneDBEntities database = new DatabaseModel.EduZoneDBEntities();
            moneyListRepo = new MoneyListRepo(database);
        }
        public DomainModel.BusinessObject.MoneyList LoadSelectedMoneyList(String description)
        {
            return moneyListRepo.LoadMoneyList(description);
        }
    }
}
