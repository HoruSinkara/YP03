using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
       
        public List<Jury?> juries { get; set; } = new List<Jury?>();
        public List<Moderator?> moderators { get; set; } = new List<Moderator?>();
        public List<Participant?> participants { get; set; } = new List<Participant?>();
        public List<Organizer?> organizers { get; set; } = new List<Organizer?>();
    }
}
