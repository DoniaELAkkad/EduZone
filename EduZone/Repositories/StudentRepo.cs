using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BusinessObject;
using DatabaseModel;

namespace Repositories
{
    public class StudentRepo : IStudent
    {
        EduZoneDBEntities _dbx;
        public StudentRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }
        public void AddStudent(DomainModel.BusinessObject.Student studentDomain)
        {
            DatabaseModel.Student student = new DatabaseModel.Student();
            student.studentName = studentDomain.StudentName;
            student.parentName = studentDomain.ParentName;
            student.birthDate = studentDomain.BirthDate;
            student.MobileNumber = studentDomain.MobileNumber;
            student.studentFingurePrint = studentDomain.StudentFingurePrint;
            student.parentFingrePrint = studentDomain.ParentFingurePrint;
            _dbx.Students.Add(student);
            _dbx.SaveChanges();
        }

        public List<DomainModel.BusinessObject.Student> searchStudentByFingerPrint(string fingerPrint)
        {
            List<DomainModel.BusinessObject.Student> students = new List<DomainModel.BusinessObject.Student>();
            List<DatabaseModel.Student> lst_Students = (from l in _dbx.Students
                                                        where l.parentFingrePrint == fingerPrint
                                                        select l).ToList();
            foreach (DatabaseModel.Student student in lst_Students)
            {
                DomainModel.BusinessObject.Student studentDomain = new DomainModel.BusinessObject.Student();
                studentDomain.StudentId = student.Id;
                studentDomain.StudentName = student.studentName;
                studentDomain.BirthDate = student.birthDate;
                studentDomain.ParentName = student.parentName;
                studentDomain.MobileNumber = student.MobileNumber;
                students.Add(studentDomain);
            }
            return students;
        }

        public List<DomainModel.BusinessObject.Student> searchStudentByMobile(String mobileNumber)
        {
            List<DomainModel.BusinessObject.Student> students = new List<DomainModel.BusinessObject.Student>();
            List<DatabaseModel.Student> lst_Students = (from l in _dbx.Students
                                                   where l.MobileNumber==mobileNumber
                                                   select l).ToList();
            foreach (DatabaseModel.Student student in lst_Students)
            {
                DomainModel.BusinessObject.Student studentDomain = new DomainModel.BusinessObject.Student();
                studentDomain.StudentId = student.Id;
                studentDomain.StudentName = student.studentName;
                studentDomain.BirthDate = student.birthDate;
                studentDomain.ParentName = student.parentName;
                studentDomain.MobileNumber = student.MobileNumber;
                students.Add(studentDomain);
            }
            return students;
        }
    }
}
