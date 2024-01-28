using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string NameActivity { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public int IdModerator { get; set; }

        public IdActivityJury IdActivityJury { get; set; } = new IdActivityJury();

        public EventActivity EventActivity { get; set; } = new EventActivity();
        public List<Moderator> moderators { get; set; } = new List<Moderator>();
    }

}
