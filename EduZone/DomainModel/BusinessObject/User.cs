using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class User
    {
        public int UserId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
