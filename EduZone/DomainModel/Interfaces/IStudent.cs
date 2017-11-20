using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IStudent
    {
        void AddStudent(DomainModel.BusinessObject.Student student);
        List<DomainModel.BusinessObject.Student> searchStudentByMobile(String mobileNumber);

        List<DomainModel.BusinessObject.Student> searchStudentByFingerPrint(String fingerPrint);
    }
}
