using DomainModel.BusinessObject;
using DomainModel.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class StudentManager
    {
        private IStudent studentRepo;
        private IAgeRange ageRangeRepo;

        public StudentManager()
        {
            DatabaseModel.EduZoneDBEntities database = new DatabaseModel.EduZoneDBEntities();
            studentRepo = new StudentRepo(database);
            ageRangeRepo = new AgeRepo(database);
        }
        public void AddStudent(DomainModel.BusinessObject.Student student)
        {
            studentRepo.AddStudent(student);
        }
        public List<Student> GetStudentsByMobileNumber(String mobileNumber)
        {
            return studentRepo.searchStudentByMobile(mobileNumber);
        }
        public List<Student> GetStudentsByFingerPrint(String fingrePrint)
        {
            return studentRepo.searchStudentByFingerPrint(fingrePrint);
        }

        public List<AgeRange> LoadAgeRanges()
        {
            return ageRangeRepo.loadRanges();
        }
    }
}
