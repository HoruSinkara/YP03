using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string NameEvent { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string IDCity { get; set; }  
        public int IdParticipant { get; set; }


    }
}
