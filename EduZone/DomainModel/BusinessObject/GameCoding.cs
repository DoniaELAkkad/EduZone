using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class GameCoding
    {
        public int Id { set; get; }
        public STEM Stem { set; get; }
        public Branches Branch { set; get; }
        public AgeRange AgeRange { set; get;}
        public Games Game { set; get; }
        public List<Module> Modules { set; get; }

    }
}
