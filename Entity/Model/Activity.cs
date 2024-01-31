using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Day { get; set; }
        public DateTime StartDate { get; set; }
        public int IdModerator { get; set; }

        public ActivityJuries? IdActivityJury { get; set; } = new ActivityJuries();

        public EventActivity? EventActivity { get; set; } = new EventActivity();
        public List<Moderator?> moderators { get; set; } = new List<Moderator?>();
    }

}
