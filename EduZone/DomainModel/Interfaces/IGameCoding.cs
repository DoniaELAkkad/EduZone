using DomainModel.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IGameCoding
    {
        List<GameCoding> LoadGameCodeByAgeAndSTEM(AgeRange ageRange, STEM stem);
        List<Module> GetGameModule(Games game);
        GameCoding LoadGameCoding(string gameCode, String ageGroup);
        List<Branches> LoadBranchsPerSTEM(String stemId);
        List<DomainModel.BusinessObject.GameCoding> LoadGameCodeByAgeAndSTEMAndBranch(DomainModel.BusinessObject.AgeRange ageRange, DomainModel.BusinessObject.STEM stem, DomainModel.BusinessObject.Branches branch);
    }
}
