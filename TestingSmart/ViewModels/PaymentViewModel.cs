using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSmart.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalAmount { get; set; }
        public int PayingAmount { get; set; }
    }
}
