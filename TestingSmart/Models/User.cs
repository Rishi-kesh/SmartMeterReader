using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSmart.Models
{
    public class User: IdentityUser
    {
        public string FullName { get; set; }
            
        public string Address { get; set; }
        
        public string UserType { get; set; }

    }
}

