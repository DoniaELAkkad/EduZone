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
    public class ToyReservationHistoryRepo : IToyReservationHistory
    {
        EduZoneDBEntities _dbx;
        public ToyReservationHistoryRepo(EduZoneDBEntities dbx)
        {
            _dbx = dbx;
        }
        public void AddToyReservation(DateTime startDateTime, DateTime endDateTime, Games game)
        {
            DatabaseModel.ToyReservationHistory toyReservationHistory = new DatabaseModel.ToyReservationHistory();
            toyReservationHistory.StartDateTime = startDateTime;
            toyReservationHistory.EndDateTime = endDateTime;
            toyReservationHistory.GameId = game.Id;
            _dbx.ToyReservationHistories.Add(toyReservationHistory);
            _dbx.SaveChanges();
        }

        public List<int> GetGamesIdsInUse(DateTime sessionDateTime)
        {
            List<int> lstGamesId = (from l in _dbx.ToyReservationHistories
                                    where l.StartDateTime >= sessionDateTime && l.EndDateTime<= sessionDateTime
                                    select l.GameId).ToList();
            return lstGamesId;
        }
    }
}
