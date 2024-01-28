using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class IdActivityJury
    {
        public int Id { get; set; }
        public int IdActivity { get; set; }
        public int IdJury { get; set; }

        public List<Activity> activities { get; set; } = new List<Activity>();
        public Jury Jury { get; set; } = new Jury();
    }
}
