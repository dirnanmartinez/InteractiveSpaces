using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Data;
using InteractiveSpaces.Models;
using InteractiveSpaces.Model.InteractiveSpaceDTO;
using System.Net;
using InteractiveSpaces.Model.Activity2UserDTOs;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractiveSpace3DController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public InteractiveSpace3DController(ApplicationDBContext context)
        {
            _context = context;
        }


        // GET: api/InteractiveSpace3D
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<InteractiveSpaceDetail3DDTO>>> GetInteractiveSpaces3D(string? name)
        {
            var spaces = await _context.InteractiveSpace3D
                .Where(x =>(name==null|| x.Name.Contains(name)) && x.Visibility==TypeOfVisibility.Public)
                .Select(x => new InteractiveSpaceDetail3DDTO(x))
                .ToListAsync();

            return spaces;
        }


        // GET: api/InteractiveSpace3D/5
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<InteractiveSpaceDetail3DDTO>> GetInteractiveSpace3DById(int id)
        {
            var interactiveSpace3D = await _context.InteractiveSpace3D
                .FirstOrDefaultAsync(i=>i.Id==id);
           
            if (interactiveSpace3D == null)
            {
                return NotFound();
            }
            var interactiveSpaceDTO = new InteractiveSpaceDetail3DDTO(interactiveSpace3D);
            return interactiveSpaceDTO;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<InteractiveSpaceDetail3DDTO>> GetInteractiveSpace3DByOwner(string owner, string? name)
        {
            var interactiveSpace3D = await _context.InteractiveSpace3D
                .FirstOrDefaultAsync(i => (name==null || i.Name.Equals(name)) && i.Owner.Equals(owner));

            if (interactiveSpace3D == null)
            {
                return NotFound();
            }
            var interactiveSpaceDTO = new InteractiveSpaceDetail3DDTO(interactiveSpace3D);
            return interactiveSpaceDTO;
        }

        //// PUT: api/InteractiveSpace3D/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutInteractiveSpace3D(int id, InteractiveSpace3D interactiveSpace3D)
        //{
        //    if (id != interactiveSpace3D.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(interactiveSpace3D).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!InteractiveSpace3DExists(id))
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

        // POST: api/InteractiveSpace3D
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<InteractiveSpaceDetail3DDTO>> AddInteractiveSpace3D(InteractiveSpace3DBriefDTO interactiveSpace3D)
        {
            if (InteractiveSpace3DExists(interactiveSpace3D.Name, interactiveSpace3D.Owner))
                ModelState.AddModelError("Name", $"There is already an interactive space whose name is also {interactiveSpace3D.Name} owned by {interactiveSpace3D.Owner}");
            if (ModelState.IsValid)
            {
                var interactiveSpace = new InteractiveSpace3D(interactiveSpace3D);
                _context.InteractiveSpace3D.Add(interactiveSpace);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException exception)
                {
                    return BadRequest(exception.Message);
                }
                var interactiveSpaceDTO = new InteractiveSpaceDetail3DDTO(interactiveSpace);
                return CreatedAtAction("GetInteractiveSpace3DById", new { id = interactiveSpace.Id }, interactiveSpaceDTO);
            }
            return BadRequest(ModelState);

        }

        // DELETE: api/InteractiveSpace3D/5
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteInteractiveSpace3D(int id)
        {
            var interactiveSpace3D = await _context.InteractiveSpace3D.FindAsync(id);
            if (interactiveSpace3D == null)
            {
                return NotFound();
            }

            _context.InteractiveSpace3D.Remove(interactiveSpace3D);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InteractiveSpace3DExists(string name, string owner)
        {
            return _context.InteractiveSpace3D.Any(e => e.Name == name && e.Owner==owner);
        }
    }
}
