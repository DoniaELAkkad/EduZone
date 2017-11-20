using DomainModel.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IToyReservationHistory
    {
        void AddToyReservation(DateTime startDateTime, DateTime endDateTime,Games game);
        List<int> GetGamesIdsInUse(DateTime sessionDateTime);
    }
}
