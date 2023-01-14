using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Data;
using InteractiveSpaces.Models;
using System.Net;
using System.Diagnostics;
using InteractiveSpaces.Model.ResourceDTO;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ResourcesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ResourceDetailsDTO>>> GetResources()
        {
            return await _context.Resource
                .Select(r=>new ResourceDetailsDTO(r))
                .ToListAsync();
        }

        // GET: api/Resources/5
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Resource>> GetResource(int? id, string? name)
        {
            var resource = await _context.Resource
                .FirstOrDefaultAsync(res=>res.Id==id||res.Name==name);

            if (resource == null)
            {
                return NotFound(new { id,name});
            }

            return Ok(resource);
        }

        //// PUT: api/Resources/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutResource(int id, Resource resource)
        //{
        //    if (id != resource.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(resource).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ResourceExists(id))
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

        // POST: api/Resources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("[action]")]
        public async Task<ActionResult<Resource>> AddResource(ResourceDetailsDTO resourceDTO)
        {
            if (ResourceExists(resourceDTO.Name))
                ModelState.AddModelError("ResourceName", $"There is another resource registered whose name is {resourceDTO.Name}");

            if (ModelState.IsValid)
            {
                var resource = new Resource(resourceDTO);
                _context.Resource.Add(resource);
                await _context.SaveChangesAsync();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(ex.Message);
                }
                return CreatedAtAction("GetResourceById", new { id = resource.Id }, resource);
            }
            return BadRequest(ModelState);

         
        }

        // DELETE: api/Resources/5
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resource = await _context.Resource.FindAsync(id);
            if (resource == null)
            {
                return NotFound(id);
            }

            _context.Resource.Remove(resource);
            await _context.SaveChangesAsync();

            return Ok(id);
        }

        private bool ResourceExists(string name)
        {
            return _context.Resource.Any(e => e.Name == name);
        }
    }
}
