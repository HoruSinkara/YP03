using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Direction
    {
        public int Id { get; set; }
        public int NameDirection { get; set; }
        public int IdEvent { get; set; }

        public Activity Activity { get; set; } = new Activity();
        public Jury Jury { get; set; } = new Jury();
        public Moderator? Moderator { get; set; } = new Moderator();
    }
}
