using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSmart.Models
{
    public class BillAmount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalUnit { get; set; }
        public int CurrentUnit { get; set; }
        public int TotalPrice { get; set; }
       
        [ForeignKey("UserId")]
        public virtual User UserDetails { get; set; }
    }
}
