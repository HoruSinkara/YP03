using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class ActivityJuries
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int IdActivity { get; set; }

        public List<Activity?> activities { get; set; } = new List<Activity?>();
        public Jury? Jury { get; set; } = new Jury();
    }
}
