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
    public class StudentCourseRepo : IStudentCourse
    {
        EduZoneDBEntities _dbx;
        public StudentCourseRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }
        public void AddStudentCourse(DomainModel.BusinessObject.Student student, DomainModel.BusinessObject.GameCoding gameCoding, DomainModel.BusinessObject.Module module)
        {
            DatabaseModel.StudentCours studentCourse = new StudentCours();
            studentCourse.studentId = student.StudentId;
            studentCourse.gameCodingId = gameCoding.Id;
            studentCourse.moduleId = module.Id;
            _dbx.StudentCourses.Add(studentCourse);
            _dbx.SaveChanges();
        }

        public List<int> GetModulesIdTaken(DomainModel.BusinessObject.Student student)
        {
            List<int> lstModules = (from l in _dbx.StudentCourses
                                    where l.studentId == student.StudentId
                                    select l.moduleId).ToList();
            return lstModules;
        }
    }
}
