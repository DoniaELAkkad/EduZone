using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public User user { get; set; }
        public DateTime loginDateTimeStamp { get; set; }
        public DateTime logoutDateTimeStamp { get; set; }
        public Decimal totalCashAmount { get; set; }
    }
}
