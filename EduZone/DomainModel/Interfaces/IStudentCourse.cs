using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IStudentCourse
    {
         void AddStudentCourse(DomainModel.BusinessObject.Student student, DomainModel.BusinessObject.GameCoding gameCoding, DomainModel.BusinessObject.Module module);
        List<int> GetModulesIdTaken(DomainModel.BusinessObject.Student student);
    }
}
