using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingSmart.DataContext;
using TestingSmart.Models;

namespace TestingSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillAmountsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BillAmountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BillAmounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillAmount>>> GetBillAmount()
        {
            return await _context.BillAmount.ToListAsync();
        }

        // GET: api/BillAmounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillAmount>> GetBillAmount(int id)
        {
            var billAmount = await _context.BillAmount.FindAsync(id);

            if (billAmount == null)
            {
                return NotFound();
            }
            //billAmount.UserId = billAmount.UserDetails.UserName;
            //if(billAmount.CurrentUnit==0)
            //{
            //    billAmount.CurrentUnit = billAmount.TotalUnit;
            //}
            return billAmount;
        }

        // PUT: api/BillAmounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillAmount(int id, BillAmount billAmount)
        {
            if (id != billAmount.Id)
            {
                return BadRequest();
            }

            _context.Entry(billAmount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillAmountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        // POST: api/BillAmounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BillAmount>> PostBillAmount(Unit billAmount)
        {
            var userIds = _context.Users.Where(x => x.UserName == billAmount.UserId).FirstOrDefault().Id;
            billAmount.UserId = userIds;
            var checkPreviousBill = _context.BillAmount.Where(x => x.UserId == billAmount.UserId).OrderByDescending(x => x.Id).FirstOrDefault();

            var currentUnit = 0;
            var baki = 0;
            var price = 0;
            if(checkPreviousBill!=null)
            {
                currentUnit = checkPreviousBill.CurrentUnit;
               // var checkInPayment = _context.BillPayment.Where(x => x.BillAmountDetails.UserId == checkPreviousBill.UserId).OrderByDescending(x=>x.CreatedDate).FirstOrDefault();
               // if (checkInPayment != null)
                //{
                //     //baki = checkInPayment.Due / 10;
                //     currentUnit = (billAmount.Unites - checkPreviousBill.CurrentUnit )-checkPreviousBill.TotalUnit;
                //}
                //else
                //{
                //    currentUnit = checkPreviousBill.TotalUnit;
                //}

            }
         
            BillAmount bill = new BillAmount();
            bill.TotalUnit = billAmount.Unites;
            bill.UserId = billAmount.UserId;
            bill.TotalPrice = price;
            if (bill.TotalUnit<=22)
            {
                currentUnit = billAmount.Unites;
                if (currentUnit == 22)
                {
                    var count = billAmount.Unites / currentUnit;
                    var pay = _context.BillPayment.Count();
                    if (count > pay)
                        currentUnit = 22;
                    else
                        currentUnit = 0;
                }
            }
            else
            {
                currentUnit = billAmount.Unites % 22;
                if(currentUnit==0)
                {
                    currentUnit = 22;
                    var count = billAmount.Unites / currentUnit;
                    var pay = _context.BillPayment.Count();
                    if (count > pay)
                        currentUnit = 22;
                    else
                        currentUnit = 0;
                }
            }

            bill.CurrentUnit = currentUnit;
            if (bill.CurrentUnit < 0)
                bill.CurrentUnit = bill.CurrentUnit * -1;
            if (currentUnit <= 20)
            { if (currentUnit == 0)
                    price = 0;
               else
                price = 80;
            }
            else
            {
                var extraUnit = currentUnit - 20;
                price = 80 + extraUnit * 10;
            }
            bill.TotalPrice = price;
            _context.BillAmount.Add(bill);
            await _context.SaveChangesAsync();
           
            bill.UserId = billAmount.UserId;
            return bill;            
        }

        
        
        // DELETE: api/BillAmounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillAmount>> DeleteBillAmount(int id)
        {
            var billAmount = await _context.BillAmount.FindAsync(id);
            if (billAmount == null)
            {
                return NotFound();
            }

            _context.BillAmount.Remove(billAmount);
            await _context.SaveChangesAsync();

            return billAmount;
        }

        private bool BillAmountExists(int id)
        {
            return _context.BillAmount.Any(e => e.Id == id);
        }
    }
}
