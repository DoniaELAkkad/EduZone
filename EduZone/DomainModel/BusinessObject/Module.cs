using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class Module
    {
        public int Id { set; get; }
        public Games Game { set; get;}
        public String ModuleName { set; get; }
        public int ModuleId { set; get; }
    }
}
