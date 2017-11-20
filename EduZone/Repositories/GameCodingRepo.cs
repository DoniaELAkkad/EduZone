using DatabaseModel;
using DomainModel.BusinessObject;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GameCodingRepo:IGameCoding
    {
        EduZoneDBEntities _dbx;

        public GameCodingRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }

        public List<DomainModel.BusinessObject.Module> GetGameModule(Games game)
        {
            List<DomainModel.BusinessObject.Module> modules = new List<DomainModel.BusinessObject.Module>();
            List<DatabaseModel.Module> gameModule = (from l in _dbx.Modules
                                                           where l.GameId == game.Id
                                                           select l).ToList();

            foreach (DatabaseModel.Module module in gameModule)
            {
                DomainModel.BusinessObject.Module moduleDomain = new DomainModel.BusinessObject.Module();
                moduleDomain.Id = module.Id;
                moduleDomain.ModuleName = module.ModuleName;
                moduleDomain.ModuleId = module.ModuleId;
                DomainModel.BusinessObject.Games gameDomain = new DomainModel.BusinessObject.Games();
                gameDomain.Id = module.Game.Id;
                gameDomain.GameName = module.Game.gameName;
                moduleDomain.Game=gameDomain;
                modules.Add(moduleDomain);
            }
            return modules;
        }
        public List<DomainModel.BusinessObject.GameCoding> LoadGameCodeByAgeAndSTEM(DomainModel.BusinessObject.AgeRange ageRange, DomainModel.BusinessObject.STEM stem)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCoding = new List<DomainModel.BusinessObject.GameCoding>();
            List<DatabaseModel.GameCoding> lst_gameCode= (from l in _dbx.GameCodings
                                  where l.STEM.Id == stem.Id && l.AgeRange.Id==ageRange.ID
                                  select l).ToList();
            foreach (DatabaseModel.GameCoding game in lst_gameCode)
            {
                DomainModel.BusinessObject.GameCoding gameCodingDomain = new DomainModel.BusinessObject.GameCoding();

                gameCodingDomain.Id = game.Id;
                DomainModel.BusinessObject.STEM stemdomain = new DomainModel.BusinessObject.STEM();
                stemdomain.Id = game.STEM.Id;
                stemdomain.Description = game.STEM.Description;
                gameCodingDomain.Stem = stemdomain;

                DomainModel.BusinessObject.Branches branchDomain = new DomainModel.BusinessObject.Branches();
                branchDomain.Id = game.Branch.Id;
                branchDomain.branchNumPerStem = game.Branch.branchNumPerSTEM;
                branchDomain.branchName = game.Branch.branchName;
                branchDomain.stem = gameCodingDomain.Stem;
                gameCodingDomain.Branch = branchDomain;

                DomainModel.BusinessObject.AgeRange ageRangeDomain = new DomainModel.BusinessObject.AgeRange();
                ageRangeDomain.ID = game.AgeRange.Id;
                ageRangeDomain.minAge = game.AgeRange.minAge;
                ageRangeDomain.maxAge = game.AgeRange.maxAge;
                gameCodingDomain.AgeRange = ageRangeDomain;

                DomainModel.BusinessObject.Games gameDomain = new DomainModel.BusinessObject.Games();
                gameDomain.Id = game.Game.Id;
                gameDomain.GameName = game.Game.gameName;
                gameCodingDomain.Game = gameDomain;
                gameCoding.Add(gameCodingDomain);
            }
            return gameCoding;
        }
        public List<DomainModel.BusinessObject.GameCoding> LoadGameCodeByAgeAndSTEMAndBranch(DomainModel.BusinessObject.AgeRange ageRange, DomainModel.BusinessObject.STEM stem,DomainModel.BusinessObject.Branches branch)
        {
            List<DomainModel.BusinessObject.GameCoding> gameCoding = new List<DomainModel.BusinessObject.GameCoding>();
            List<DatabaseModel.GameCoding> lst_gameCode = (from l in _dbx.GameCodings
                                                           where l.STEM.Id == stem.Id && l.AgeRange.Id == ageRange.ID && l.BranchId==branch.Id
                                                           select l).ToList();
            foreach (DatabaseModel.GameCoding game in lst_gameCode)
            {
                DomainModel.BusinessObject.GameCoding gameCodingDomain = new DomainModel.BusinessObject.GameCoding();

                gameCodingDomain.Id = game.Id;
                DomainModel.BusinessObject.STEM stemdomain = new DomainModel.BusinessObject.STEM();
                stemdomain.Id = game.STEM.Id;
                stemdomain.Description = game.STEM.Description;
                gameCodingDomain.Stem = stemdomain;

                DomainModel.BusinessObject.Branches branchDomain = new DomainModel.BusinessObject.Branches();
                branchDomain.Id = game.Branch.Id;
                branchDomain.branchNumPerStem = game.Branch.branchNumPerSTEM;
                branchDomain.branchName = game.Branch.branchName;
                branchDomain.stem = gameCodingDomain.Stem;
                gameCodingDomain.Branch = branchDomain;

                DomainModel.BusinessObject.AgeRange ageRangeDomain = new DomainModel.BusinessObject.AgeRange();
                ageRangeDomain.ID = game.AgeRange.Id;
                ageRangeDomain.minAge = game.AgeRange.minAge;
                ageRangeDomain.maxAge = game.AgeRange.maxAge;
                gameCodingDomain.AgeRange = ageRangeDomain;

                DomainModel.BusinessObject.Games gameDomain = new DomainModel.BusinessObject.Games();
                gameDomain.Id = game.Game.Id;
                gameDomain.GameName = game.Game.gameName;
                gameCodingDomain.Game = gameDomain;
                gameCoding.Add(gameCodingDomain);
            }
            return gameCoding;
        }

        public DomainModel.BusinessObject.GameCoding LoadGameCoding(string gameCode, String ageGroup)
        {
            String stemId = gameCode[0].ToString();
            int AgeGroupIndex = gameCode.IndexOf(ageGroup[0]);
            int branchIdPerStem = int.Parse(gameCode.Substring(1, AgeGroupIndex-1));

            String ageRangeId = ageGroup;
            int bracIndex = gameCode.IndexOf("(");
            int end = bracIndex - (AgeGroupIndex + 1);
            int gameId =int.Parse(gameCode.Substring(AgeGroupIndex+1,end));
            Branch branch = _dbx.Branches.SingleOrDefault(p=>p.stemID==stemId && p.branchNumPerSTEM==branchIdPerStem);
            DatabaseModel.GameCoding game = _dbx.GameCodings.SingleOrDefault(p => p.STEM.Id == stemId && p.BranchId == branch.Id && p.AgeRange.Id==ageRangeId && p.Game.Id==gameId);
            DomainModel.BusinessObject.GameCoding gameCodingDomain = new DomainModel.BusinessObject.GameCoding();

            gameCodingDomain.Id = game.Id;
            DomainModel.BusinessObject.STEM stemdomain = new DomainModel.BusinessObject.STEM();
            stemdomain.Id = game.STEM.Id;
            stemdomain.Description = game.STEM.Description;
            gameCodingDomain.Stem = stemdomain;

            DomainModel.BusinessObject.Branches branchDomain = new DomainModel.BusinessObject.Branches();
            branchDomain.Id = game.Branch.Id;
            branchDomain.branchNumPerStem = game.Branch.branchNumPerSTEM;
            branchDomain.branchName = game.Branch.branchName;
            branchDomain.stem = gameCodingDomain.Stem;
            gameCodingDomain.Branch = branchDomain;

            DomainModel.BusinessObject.AgeRange ageRangeDomain = new DomainModel.BusinessObject.AgeRange();
            ageRangeDomain.ID = game.AgeRange.Id;
            ageRangeDomain.minAge = game.AgeRange.minAge;
            ageRangeDomain.maxAge = game.AgeRange.maxAge;
            gameCodingDomain.AgeRange = ageRangeDomain;

            DomainModel.BusinessObject.Games gameDomain = new DomainModel.BusinessObject.Games();
            gameDomain.Id = game.Game.Id;
            gameDomain.GameName = game.Game.gameName;
            gameCodingDomain.Game = gameDomain;

            return gameCodingDomain;
        }

        public List<Branches> LoadBranchsPerSTEM(String stemId)
        {
            List<Branches> lstBranchesDomain = new List<Branches>();
            List<DatabaseModel.Branch> lstBranches = (from l in _dbx.Branches
                                    where l.stemID == stemId
                                          select l).ToList();
            foreach (DatabaseModel.Branch branch in lstBranches)
            {
                Branches branchDomain = new Branches();
                branchDomain.Id = branch.Id;
                branchDomain.branchName = branch.branchName;
                branchDomain.branchNumPerStem = branch.branchNumPerSTEM;
                lstBranchesDomain.Add(branchDomain);
            }
            return lstBranchesDomain;
        }
    }
}
