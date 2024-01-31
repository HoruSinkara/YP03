using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP03.Entity.Model
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string NameEvent { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }

        public List<EventActivity?> EventActivity { get; set; } = new List<EventActivity?>(); 
        public List<City?> cities { get; set; }= new List<City?>();  
        public Participant? participantWinnerId { get; set; } = new Participant();
        

    }
}
