using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Data;
using InteractiveSpaces.Models;
using InteractiveSpaces.Model.EntityDTO;
using System.Net;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Entity3dController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public Entity3dController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Entity3d
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<EntityDetail3DDTO>>> GetEntities3d()
        {
            return await _context.Entity3d
                .Include(e => e.Animations).Select(e=>new EntityDetail3DDTO(e))
                .ToListAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<EntityDetail3DDTO>>> GetEntities3dByName(string name)
        {
            return await _context.Entity3d.Include(e => e.Animations)
                .Where(e=>e.Name.Contains(name))
                .Select(e => new EntityDetail3DDTO(e))
                .ToListAsync();         
        }

        //// GET: api/Entity3d/5
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<EntityDetail3DDTO>> GetEntity3dById(int id)
        {
            var entity3d = await _context.Entity3d.Include(e => e.Animations)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (entity3d == null)
            {
                return NotFound();
            }
            
            return new EntityDetail3DDTO(entity3d);
        }

        ////// PUT: api/Entity3d/5
        ////// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        ////[HttpPut("{id}")]
        ////public async Task<IActionResult> PutEntity3d(int id, Entity3D entity3d)
        ////{
        ////    if (id != entity3d.Id)
        ////    {
        ////        return BadRequest();
        ////    }

        ////    _context.Entry(entity3d).State = EntityState.Modified;

        ////    try
        ////    {
        ////        await _context.SaveChangesAsync();
        ////    }
        ////    catch (DbUpdateConcurrencyException)
        ////    {
        ////        if (!Entity3dExists(id))
        ////        {
        ////            return NotFound();
        ////        }
        ////        else
        ////        {
        ////            throw;
        ////        }
        ////    }

        ////    return NoContent();
        ////}

        // POST: api/Entity3d
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<EntityDetail3DDTO>> AddEntity3d(Entity3DBriefDTO entity3d)
        {
            if (Entity3dExists(entity3d))
                ModelState.AddModelError("Name", $"There is already another entity registered whose name is {entity3d.Name} and/or path is {entity3d.Path}");
            //var existingAnimations = AnimationExist(entity3d.Animations.Select(a=>a.AnimationId).ToList());
            //if (existingAnimations.Count > 1)
            //{
            //    string animations="";
            //    foreach(string name in existingAnimations)
            //     animations += name+" ";
            //    ModelState.AddModelError("Animation",
            //        $"The following AnimationIDs are already registered {animations}");
            //}
            if (ModelState.IsValid)
            {
                var entity = new Entity3D(entity3d);
                _context.Entity3d.Add(entity);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(ex.Message);
                }
                var entityDetailDTO = new EntityDetail3DDTO(entity);
                return CreatedAtAction("GetEntity3dById", new { id = entity.Id }, entityDetailDTO);
            }
            else
                return BadRequest(ModelState);
        }

        // DELETE: api/Entity3d/5
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEntity3d(int id)
        {
            var entity3d = await _context.Entity3d.FindAsync(id);
            if (entity3d == null)
            {
                return NotFound(id);
            }

            _context.Entity3d.Remove(entity3d);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool Entity3dExists(Entity3DBriefDTO entity)
        {
            return _context.Entity3d.Any(e => e.Name == entity.Name || e.Path==entity.Path);
        }

        private List<string> AnimationExist(List<string> AnimationIds)
        {
            return _context.Animation
                .Where(a=>AnimationIds.Contains( a.AnimationId))
                .Select(a => a.AnimationId)
                .ToList();
        }
    }
}
