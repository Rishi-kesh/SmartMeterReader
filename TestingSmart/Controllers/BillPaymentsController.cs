using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TestingSmart.DataContext;
using TestingSmart.Models;
using TestingSmart.ViewModels;

namespace TestingSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillPaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BillPaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BillPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillPayment>>> GetBillPayment()
        {
            return await _context.BillPayment.ToListAsync();
        }

        // GET: api/BillPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillPayment>> GetBillPayment(int id)
        {
            var billPayment = await _context.BillPayment.FindAsync(id);

            if (billPayment == null)
            {
                return NotFound();
            }

            return billPayment;
        }

        // PUT: api/BillPayments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillPayment(int id, BillPayment billPayment)
        {
            if (id != billPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(billPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillPaymentExists(id))
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

        //POST: api/BillPayments
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult<BillPayment>> PostBillPayment(PaymentViewModel model)
        {
            try
            {
                var userIds = _context.Users.Where(x => x.Id == model.UserId).FirstOrDefault();

                StripeConfiguration.ApiKey = "sk_test_ZaCMK1tIBQMLZdgH6Q19ZTDt00iikZzyE6";

                var options = new ChargeCreateOptions
                {
                    Amount = model.PayingAmount*100,
                    Currency = "usd",
                    Source = "card_1Fcv6oGPgALw4vMOQzgEwJeG",
                   //Source= "cus_G9Cu9VyJa3dPgx",
                    ReceiptEmail = userIds.Email,
                    Customer = "cus_G9Cu9VyJa3dPgx",
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);



                //var paymentIntentService = new PaymentIntentService();
                //var createOptions = new PaymentIntentCreateOptions
                //{
                //    Amount = model.PayingAmount,
                //    Currency = "usd",
                //  //  PaymentMethodTypes = new List<string> { "card" },
                //    ReceiptEmail = "sahavanish2014@gmail.com",
                //    Customer= "cus_G9Cu9VyJa3dPgx",
                //  //  PaymentMethod= "Card"

                //};
                //paymentIntentService.Create(createOptions);

                BillPayment pay = new BillPayment();
                pay.Amount = model.PayingAmount;
                pay.UserId =userIds.Id;
                pay.CreatedDate = DateTime.Now;
                _context.BillPayment.Add(pay);





                var billAmountData = _context.Units.Where(x => x.UserId == userIds.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                if (billAmountData == null)
                {
                    Unit ba = new Unit();
                    ba.CurrentUnit = 0;
                    ba.Unites = 0;
                    // var limitUnit = pay.Amount / 10;
                    ba.CalcUnit = 0;
                    ba.Price = pay.Amount;
                    ba.CreatedDate = DateTime.Now;
                    ba.UserId = userIds.Id;
                    ba.ModifiedDate = DateTime.Now;
                    _context.Units.Add(ba);
                }
                else
                {

                    billAmountData.Price = pay.Amount;
                    billAmountData.Unites = billAmountData.Unites;
                    // var limitUnit = pay.Amount / 10;
                    billAmountData.CurrentUnit = 0;
                    billAmountData.CalcUnit = billAmountData.Unites;
                    billAmountData.ModifiedDate = DateTime.Now;
                    _context.Units.Update(billAmountData);

                }


                await _context.SaveChangesAsync();
                return Ok(pay);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        // DELETE: api/BillPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillPayment>> DeleteBillPayment(int id)
        {
            var billPayment = await _context.BillPayment.FindAsync(id);
            if (billPayment == null)
            {
                return NotFound();
            }

            _context.BillPayment.Remove(billPayment);
            await _context.SaveChangesAsync();

            return billPayment;
        }

        private bool BillPaymentExists(int id)
        {
            return _context.BillPayment.Any(e => e.Id == id);
        }
    }
}
