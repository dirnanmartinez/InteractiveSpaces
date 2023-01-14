using InteractiveSpaces.Model.EntityStepDTO;
using System.Net;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityStepsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EntityStepsController(ApplicationDBContext context)
        {
            _context = context;
        }



        // GET: api/EntitySteps/5
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<EntityStepDetail3DLocationDTO>> GetEntityStep(int id)
        {
            var entityStep = await _context.EntityStep
                .Include(es => es.LocatedIn)
                .Include(es => es.HasActions).ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                .SingleOrDefaultAsync(es => es.Id == id);

            if (entityStep == null)
            {
                return NotFound();
            }
            var entityDTO = new EntityStepDetail3DLocationDTO(entityStep);
            return entityDTO;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IList<EntityStepDetail3DLocationDTO>>> GetEntityStepofStepDescription(int id)
        {
            var entitiesStep = await _context.EntityStep
                .Include(es => es.LocatedIn).Include(es => es.StepDescription)
                .Include(es => es.HasActions).ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                .Where(es => es.StepDescription.Id == id)
                .Select(es=>new EntityStepDetail3DLocationDTO(es))
                .ToListAsync();

            if (entitiesStep == null)
            {
                return NotFound(id);
            }

            return entitiesStep;
        }

        //// PUT: api/EntitySteps/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEntityStep(int id, EntityStep entityStep)
        //{
        //    if (id != entityStep.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(entityStep).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EntityStepExists(id))
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

        // POST: api/EntitySteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<EntityStepDetail3DLocationDTO>> AddEntity2Step(EntityStepBriefDTO entityStepDTO)
        {
            var entity = await _context.Entity3d.Include(e=>e.Animations).FirstOrDefaultAsync(e => e.Id == entityStepDTO.EntityId);
            if (entity == null)
                ModelState.AddModelError("Entity", $"There is no entity with id {entityStepDTO.EntityId}");
            else
            {
                var animationIdsSelected = entityStepDTO.HasActions.Select(a=>a.AnimationId).ToArray();
                var animationIdsOfEntity = entity.Animations.Select(a => a.Id).ToArray();
                foreach (int animationId in animationIdsSelected)
                { 
                    if (!animationIdsOfEntity.Contains(animationId))
                        ModelState.AddModelError("Action", $"Tne animation {animationId} is not related to the Entity {entity.Name}");
                }
            }
            var stepDescription = await _context.StepDescription.FirstOrDefaultAsync(e => e.Id == entityStepDTO.StepDescriptionId);
            if (stepDescription == null)
                ModelState.AddModelError("StepDescription", $"There is no StepDescription with id {entityStepDTO.StepDescriptionId}");

            if(!entityStepDTO.HasActions.Any(a=>a.ActionType==TypeOfActionEntityStep.Required))
                ModelState.AddModelError("Action", $"At least one action must be required");


            if (ModelState.IsValid)
            {
                var location = new Location3D(entityStepDTO.X,entityStepDTO.Y,entityStepDTO.Z,entityStepDTO.RotX,
                    entityStepDTO.RotY,entityStepDTO.RotZ,entityStepDTO.ScaleX, entityStepDTO.ScaleY, entityStepDTO.ScaleZ);
                var actions=entityStepDTO.HasActions.Select(a=>new ActionEntityStep(a.Description,
                    a.FeedbackMessage,a.ActionType, entity.Animations.First(an=>an.Id==a.AnimationId))).ToList();
                var entityStep = new EntityStep(stepDescription, entity,location, actions);
                _context.EntityStep.Add(entityStep);
                await _context.SaveChangesAsync();
                var entity3dDTO = new EntityStepDetail3DLocationDTO(entityStep);
                return CreatedAtAction("GetEntityStep", new { id = entityStep.Id }, entity3dDTO);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/EntitySteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityStep(int id)
        {
            var entityStep = await _context.EntityStep.FindAsync(id);
            if (entityStep == null)
            {
                return NotFound();
            }

            _context.EntityStep.Remove(entityStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityStepExists(int id)
        {
            return _context.EntityStep.Any(e => e.Id == id);
        }
    }
}
