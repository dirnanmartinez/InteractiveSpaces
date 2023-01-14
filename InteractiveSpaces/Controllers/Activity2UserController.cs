using InteractiveSpaces.Model.Activity2UserDTOs;
using InteractiveSpaces.Models;
using System.Net;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Activity2UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public Activity2UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Activity2User
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Activity2UserDetailsDTO>>> GetActivitiesAssigned2User(string user)
        {
            var activity2Users= await _context.Activity2User.Include(a=>a.Activity)
                .Where(a=>a.User == user)
                .Select(a=>new Activity2UserDetailsDTO(a.User,a.Activity))
                .ToListAsync();

            if (activity2Users == null)
            {
                return NotFound();
            }
            return Ok(activity2Users);
        }

        // GET: api/Activity2User
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetUsersAssigned2Activity(int id)
        {
            var activity2Users = await _context.Activity2User
                .Where(a => a.ActivityId == id)
                .Select(a => a.User)
                .ToListAsync();

            if (activity2Users == null)
            {
                return NotFound();
            }
            return Ok(activity2Users);
        }

        // GET: api/Activity2User/5
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Activity2UserDetailsDTO>> GetActivityAssigned2User(string user, int activityId)
        {
            var activity2User = await _context.Activity2User
                .Include(a=>a.Activity)
                .FirstOrDefaultAsync(a=>a.ActivityId== activityId&& a.User== user);

            if (activity2User == null)
            {
                return NotFound(new { user, activityId });

            }
            var activity2UserDetailsDTO = new Activity2UserDetailsDTO(activity2User.User, activity2User.Activity);

            return Ok(activity2UserDetailsDTO);
        }


        // POST: api/Activity2User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<Activity2UserDetailsDTO>> AssignActivity2User(Activity2UserDTO activity2UserDTO)
        {
            if(ModelState.IsValid)
            { 
                Activity? activity=await _context.Activity.FirstOrDefaultAsync(a=>a.Id==activity2UserDTO.ActivityId);
                if (activity == null) {
                    ModelState.AddModelError("ActivityId", $"The activity {activity2UserDTO.ActivityId} is not registered in the system");
                    return BadRequest(ModelState);
                }
                Activity2User activity2User = new Activity2User();
                activity2User.User=activity2UserDTO.User;
                activity2User.Activity = activity;
                _context.Activity2User.Add(activity2User);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException exception)
                {
                    if (Activity2UserExists(activity2UserDTO.ActivityId,activity2UserDTO.User))
                    {
                        return Conflict();
                    }
                    else
                    {
                        return BadRequest(exception.Message);
                    }
                }

                return CreatedAtAction("AssignActivity2User", new Activity2UserDetailsDTO(activity2UserDTO.User,activity));

            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Activity2User/5
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnassignActivity2User(Activity2UserDTO activity2UserDTO)
        {
            var activity2User = await _context.Activity2User.FirstOrDefaultAsync(a => a.ActivityId == activity2UserDTO.ActivityId && a.User == activity2UserDTO.User);
            if (activity2User == null)
            {
                return NotFound();
            }

            _context.Activity2User.Remove(activity2User);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool Activity2UserExists(int activityId, string user)
        {
            return _context.Activity2User.Any(e => e.ActivityId == activityId && e.User==user);
        }
    }
}
