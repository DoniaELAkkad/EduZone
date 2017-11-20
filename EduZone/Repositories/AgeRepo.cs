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
    public class AgeRepo : IAgeRange
    {
        EduZoneDBEntities _dbx;

        public AgeRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }
        public List<DomainModel.BusinessObject.AgeRange> loadRanges()
        {

            return _dbx.AgeRanges.Select(b => new DomainModel.BusinessObject.AgeRange()
            {
                ID = b.Id,
                minAge = b.minAge,
                maxAge = b.maxAge
            }).ToList();
        }
    }
}
