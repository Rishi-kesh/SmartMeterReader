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
    public class UnitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UnitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            var item = await _context.Units.Select(t => new Unit()
            {
                CreatedDate = t.CreatedDate,
                Id = t.Id,
                ModifiedDate = t.ModifiedDate,
                UserId = t.UserDetails.UserName,
                CurrentUnit=t.CurrentUnit,
                Price=t.Price,
                Unites = t.Unites
            }).ToListAsync();
            return item;
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Unit unit)
        {
            if (unit.Id == 0)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Units
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        [HttpPost]
        public async Task<ActionResult<bool>> PostUnit(Unit unit)
        {
            var data = _context.Units.Any();

            if (data == true)
            {
                var item = _context.Units.OrderByDescending(x => x.Id).FirstOrDefault();
                bool cutLine = false;
                item.Unites = unit.Unites;
                item.CurrentUnit = item.Unites - item.CalcUnit;
                var checkLimitAmount = item.Price - item.CurrentUnit * 10;
                if (checkLimitAmount <= 0)
                {
                    cutLine = true;
                }
                item.ModifiedDate = DateTime.Now;
                _context.Units.Update(item);
                await _context.SaveChangesAsync();
                return cutLine;
                // return CreatedAtAction("GetUnit", new { id = item.Id }, unit);
            }
            else
                return false;
            //unit.CreatedDate = DateTime.Now;
            //unit.ModifiedDate = DateTime.Now;
            //_context.Units.Add(unit);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return unit;
        }

        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
