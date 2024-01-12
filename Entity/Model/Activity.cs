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
    }

}
