using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BusinessObject
{
    public class ToyReservationHistory
    {
        public int Id { set; get; }
        public DateTime StartDateTime { set; get; }
        public DateTime EndDateTime { set; get;}
        public Games Game { set; get; }
    }
}
