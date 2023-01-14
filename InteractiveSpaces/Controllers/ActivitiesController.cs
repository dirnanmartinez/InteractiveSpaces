using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Data;
using InteractiveSpaces.Models;
using InteractiveSpaces.Model.ActivityDTO;
using System.Net;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger _logger;

        public ActivitiesController(ApplicationDBContext context, ILogger<ActivitiesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ActivityBriefDTO>>> GetActivities(string? name, string? owner)
        {
            return await _context.Activity.Include(a=>a.FirstStep)
                .Where(a => (name==null || a.Name.Contains(name)) &&
                            (owner==null || a.Owner.Equals(owner)))
                .Select(a => new ActivityBriefDTO(a))
                .ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<ActivityDetailsDTO>> GetActivityById(int id)
        {

            var activity = await _context.Activity
                .Include(a => a.FirstStep)
                .Include(a=>a.ActivityImage)
                .Include(a => a.InitialHelp)
                .Include(a => a.FinalMessage)
                .SingleOrDefaultAsync(a=>a.Id==id);

            if (activity == null)
            {

                return NotFound();
            }         

            return new ActivityDetailsDTO(activity);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutActivity(int id, Activity activity)
        //{
        //    if (id != activity.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(activity).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException e)
        //    {
        //        _logger.LogError(
        //            string.Concat(DateTime.UtcNow.ToLongTimeString(), ": Activity update error:", e.ToString()));
        //        if (!ActivityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("[action]")]
        public async Task<ActionResult<ActivityDetailsDTO>> AddActivity(ActivityCreateDTO activityDTO)
        {
            if (ActivityExists(activityDTO.Name))
                ModelState.AddModelError("ActivityName", $"There is another activity registered whose name is {activityDTO.Name}");

            if (ModelState.IsValid)
            { 

                Resource? activityImage = await _context.Resource.FirstOrDefaultAsync(s => s.Id == activityDTO.ActivityImagId);
                Resource? initialHelp = await _context.Resource.FirstOrDefaultAsync(s => s.Id == activityDTO.InitialHelpId);
                Resource? finalMessage = await _context.Resource.FirstOrDefaultAsync(s => s.Id == activityDTO.FinalMessageId);
                Activity activity = new Activity(activityDTO, activityImage,initialHelp,finalMessage);
            
                _context.Activity.Add(activity);
         
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(ex.Message + ex.InnerException);
                }

                var activityDetails = new ActivityDetailsDTO(activity);

                return CreatedAtAction("GetActivityById", new { id = activity.Id }, activityDetails);
            }
            return BadRequest(ModelState);

        }

        // DELETE: api/Activities/5
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activity.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(string name)
        {
            return _context.Activity.Any(e => e.Name == name);
        }
    }
}
