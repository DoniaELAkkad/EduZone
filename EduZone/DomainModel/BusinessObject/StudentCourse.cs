using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class StudentCourse
    {
        public int Id { set; get; }
        public Student Student{set; get;}
        public GameCoding GameCoding { set; get; }
        public Module Module { set; get; }
    }
}
