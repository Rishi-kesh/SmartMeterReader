using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSmart.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Unites { get; set; }
        public int CurrentUnit { get; set; }
        public int CalcUnit { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserDetails { get; set; }



    }
}
