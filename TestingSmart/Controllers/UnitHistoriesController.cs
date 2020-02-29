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
    public class UnitHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UnitHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitHistory>>> GetUnitHistories()
        {
            return await _context.UnitHistories.Select(t => new UnitHistory()
            {
                CreatedDate = t.CreatedDate,
                Id = t.Id,
                Units = t.Units,
                UserId = t.UserDetails.UserName
            }).ToListAsync();
        }

        // GET: api/UnitHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitHistory>> GetUnitHistory(int id)
        {
            var unitHistory = await _context.UnitHistories.FindAsync(id);

            if (unitHistory == null)
            {
                return NotFound();
            }

            return unitHistory;
        }

        // PUT: api/UnitHistories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitHistory(int id, UnitHistory unitHistory)
        {
            if (id != unitHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(unitHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitHistoryExists(id))
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

        // POST: api/UnitHistories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UnitHistory>> PostUnitHistory(UnitHistory unitHistory)
        {
            unitHistory.CreatedDate = DateTime.Now;
            _context.UnitHistories.Add(unitHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitHistory", new { id = unitHistory.Id }, unitHistory);
        }

        // DELETE: api/UnitHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UnitHistory>> DeleteUnitHistory(int id)
        {
            var unitHistory = await _context.UnitHistories.FindAsync(id);
            if (unitHistory == null)
            {
                return NotFound();
            }

            _context.UnitHistories.Remove(unitHistory);
            await _context.SaveChangesAsync();

            return unitHistory;
        }

        private bool UnitHistoryExists(int id)
        {
            return _context.UnitHistories.Any(e => e.Id == id);
        }
    }
}
