using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelRecordsAPI.Models;

namespace TravelRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public StagesController(CoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stage>>> GetStages()
        {
            return await _context.Stages.ToListAsync();
        }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<IEnumerable<Stage>>> GetTripStages(int tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip==null)
            {
                return NotFound();
            }
            
            List<Stage> tripStages = _context.Stages.Where(x =>x.TripId==tripId).ToList();
            return tripStages;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stage>> GetStage(int id)
        {
            var stage = await _context.Stages.FindAsync(id);

            if (stage == null)
            {
                return NotFound();
            }

            return stage;
        }

        [HttpPost]
        public async Task<ActionResult<Trip>> PostStage(Stage stage)
        {
            var user = await _context.Users.FindAsync(stage.UserId);

            if (user == null)
            {
                return BadRequest();
            }

            stage.StageId = this.GetStageId();
            _context.Stages.Add(stage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StageExists(stage.TripId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStage", new { id = stage.TripId }, stage);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStage(int id, Stage stage)
        {
            if (id != stage.StageId)
            {
                return BadRequest();
            }

            //checking for assigning stage to non existent trip or user
            var trip = await _context.Trips.FindAsync(stage.TripId);
            var user = await _context.Users.FindAsync(stage.UserId);
            if (trip == null||user==null)
            {
                return BadRequest();
            }
            _context.Entry(stage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(id))
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

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }

            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetStageId()
        {
            if (_context.Stages.Count() == 0)
            {
                return 1;
            }
            int maxStageId = _context.Stages.Max(x => x.StageId);
            maxStageId++;
            return maxStageId;
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageId == id);
        }
    }
}
