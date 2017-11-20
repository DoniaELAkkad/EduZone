using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ParentFingurePrint { get; set; }
        public string StudentFingurePrint { get; set; }
        public string MobileNumber { get; set; }

    }
}
