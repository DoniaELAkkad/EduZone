using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class Group
    { 
        public AgeRange ageRange { get; set; }
        public List<Student> students { get; set; }
    }
}
