using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class Branches
    {
        public int Id { set; get; }
        public string branchName { set; get; }
        public int branchNumPerStem { set; get;}
        public STEM stem { set; get; }
    }
}
