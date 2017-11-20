using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class AgeRange
    {
        public string ID { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }
    }
}
