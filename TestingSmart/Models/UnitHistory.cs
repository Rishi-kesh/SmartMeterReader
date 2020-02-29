using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSmart.Models
{
    public class UnitHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Units { get; set; }
        public DateTime CreatedDate { get; set; }
       
        [ForeignKey("UserId")]
        public virtual User UserDetails { get; set; }
    }
}
