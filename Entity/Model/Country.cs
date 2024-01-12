using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Country
    {
       
        public int Id { get; set; }
        public string NameRussian { get; set; }
        public string NameEnglish { get; set; }
        public int Code { get; set;}
       public int CodeTwo { get; set; }
       
        public List<Jury> juries { get; set; } = new List<Jury>();
        public List<Moderator> moderators { get; set; } = new List<Moderator>();
        public List<Participant> participants { get; set; } = new List<Participant>();
        public List<Organizer> organizers { get; set; } = new List<Organizer>();
    }
}
