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
    public class GameCodingManager
    {
        IGameCoding gameCodingRepo;
        IStudentCourse studentCourseRepo;
        IToyReservationHistory toyReservationRepo;

        public GameCodingManager()
        {
            DatabaseModel.EduZoneDBEntities database = new DatabaseModel.EduZoneDBEntities();
            gameCodingRepo = new GameCodingRepo(database);
            studentCourseRepo = new StudentCourseRepo(database);
            toyReservationRepo = new ToyReservationHistoryRepo(database);
        }
        public List<DomainModel.BusinessObject.GameCoding> LoadGameModuleByAgeAndSTEM(DomainModel.BusinessObject.AgeRange ageRange, DomainModel.BusinessObject.STEM stem)
        {
            List<DomainModel.BusinessObject.GameCoding> lstGameCoding = new List<DomainModel.BusinessObject.GameCoding>();
            lstGameCoding= gameCodingRepo.LoadGameCodeByAgeAndSTEM(ageRange, stem);
            int i;
            for (i=0;i<lstGameCoding.Count;i++)
            {
                List<DomainModel.BusinessObject.Module> gameModule=gameCodingRepo.GetGameModule(lstGameCoding.ElementAt(i).Game);
                lstGameCoding.ElementAt(i).Modules = gameModule;
            }
            return lstGameCoding;
        }
        public List<DomainModel.BusinessObject.GameCoding> LoadGameModuleByAgeAndSTEMAndBranch(DomainModel.BusinessObject.AgeRange ageRange, DomainModel.BusinessObject.STEM stem,DomainModel.BusinessObject.Branches branch)
        {
            List<DomainModel.BusinessObject.GameCoding> lstGameCoding = new List<DomainModel.BusinessObject.GameCoding>();
            lstGameCoding = gameCodingRepo.LoadGameCodeByAgeAndSTEMAndBranch(ageRange, stem,branch);
            int i;
            for (i = 0; i < lstGameCoding.Count; i++)
            {
                List<DomainModel.BusinessObject.Module> gameModule = gameCodingRepo.GetGameModule(lstGameCoding.ElementAt(i).Game);
                lstGameCoding.ElementAt(i).Modules = gameModule;
            }
            return lstGameCoding;
        }


        public DomainModel.BusinessObject.GameCoding LoadGameCoding(String gameCode,String ageGroup)
        {
           return gameCodingRepo.LoadGameCoding(gameCode,ageGroup);
        }

        public List<int> GetGameCodeInUse(DateTime sessionTime)
        {
            return toyReservationRepo.GetGamesIdsInUse(sessionTime);
        }

        public void SaveStudentTakenCousres(DomainModel.BusinessObject.Student student,DomainModel.BusinessObject.GameCoding gameCoding,DomainModel.BusinessObject.Module module)
        {
            studentCourseRepo.AddStudentCourse(student, gameCoding, module);
        }

        public List<int> GetModulesIdTaken(Student student)
        {
            return studentCourseRepo.GetModulesIdTaken(student);
        }
        public void AddToyReservation(DateTime startTime,DateTime endTime,Games game)
        {
            toyReservationRepo.AddToyReservation(startTime, endTime, game);
        }
        public Module GetSelectedModule (int moduleId,DomainModel.BusinessObject.Games game)
        {
            Module module = new Module();

            List<Module> listModule=gameCodingRepo.GetGameModule(game);
            foreach (Module model in listModule)
            {
                if (model.ModuleId == moduleId)
                    module = model;
            }
            return module;
        }

        public List<Branches> LoadBranchesPerStem(String stemId)
        {
            return gameCodingRepo.LoadBranchsPerSTEM(stemId);
        }
    }
}
