using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class MoneyList
    {
        public int Id { set; get; }
        public int Time { set; get; }
        public Decimal Amount { set; get; }
        public string Description { set; get; }
    }
}
