using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InteractiveSpaces.Data;
using InteractiveSpaces.Models;
using InteractiveSpaces.Models.StepDTO;
using System.Net;
using Microsoft.OpenApi.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InteractiveSpaces.Model.StepDTO;
using InteractiveSpaces.Model.EntityStepDTO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace InteractiveSpaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StepsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Steps
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<StepDetailsDTO>>> GetSteps(int? idActivity)
        {
            return await _context.Step
                .Include(s => s.InteractiveSpace)
                .Include (s=>s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.LocatedIn)
                .Include(s => s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.HasActions)
                            .ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                .Where(s => (idActivity==null || s.Activity.Id == idActivity))
                .Select(s => new StepDetailsDTO(s))
                .ToListAsync();

        }

        // GET: api/Steps/5
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("[action]")]
        public async Task<ActionResult<StepDetailsDTO>> GetStep(int id)
        {
            var step = await _context.Step
                .Include(s => s.InteractiveSpace)
                .Include(s => s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.LocatedIn)
                .Include(s => s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.HasActions)
                            .ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                        .FirstOrDefaultAsync(s => s.Id == id);

            if (step == null)
            {
                return NotFound(id);
            }



            var stepDTO = new StepDetailsDTO(step);
            return Ok(stepDTO);

        }

        [HttpGet()]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<StepDetailsDTO>> NextStep(int id)
        {

            var step = await _context.Step.Include(s => s.InteractiveSpace).FirstOrDefaultAsync(x => x.Id == id);

            if (step == null)
            {
                return NotFound(id);
            }

            var steps = await _context.StepDescription
                .Include(sd => sd.Step)
                .Include(sd => sd.EntityStep).ThenInclude(es => es.LocatedIn)
                .Include(sd => sd.EntityStep).ThenInclude(es => es.HasActions)
                .ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                .Where(sd => sd.Step.PreviousStep.Id == id)
                .Select(sd => new StepDescriptionDetailsDTO(sd))
                .ToListAsync();

            var stepDTO = new StepDetailsDTO(step);
            stepDTO.StepDescriptions = steps;

            return Ok(stepDTO);
            return Ok(steps.First());
        }

        //// PUT: api/Steps/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStep(int id, Step step)
        //{
        //    if (id != step.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(step).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StepExists(id))
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<StepDetailsDTO>> AddStepToActivity(StepCreateDTO stepDTO)
        {
            Step step = null;
            if (ModelState.IsValid) {
                step = await CreateStep(stepDTO);
                if (ModelState.IsValid)
                {
                    if (step.Activity.NoStepsDefined)
                    {
                        step.Activity.FirstStep = step;
                        step.Activity.LastStep = step;
                    }
                    else {
                        step.Activity.LastStep.NextStep=step;
                        step.Activity.LastStep = step;
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        return BadRequest(ex.Message + ex.InnerException);
                    }
                    var stepDetails = new StepDetailsDTO(step);
                    return CreatedAtAction("GetStep", new { id = step.Id }, stepDetails);
                }
            }
            return BadRequest(ModelState);

    }

        private async Task<Step> CreateStep(StepCreateDTO stepDTO) {

            Step step = null;
            InteractiveSpaces.Models.Activity? activity = await _context.Activity
                .Include(a => a.FirstStep).Include(a => a.LastStep)
                .FirstOrDefaultAsync(s => s.Id == stepDTO.ActivityId);
            if (activity == null)
                ModelState.AddModelError("ActivityId", $"ActivityId: {stepDTO.ActivityId} is not registered in the system");
            InteractiveSpace3D? interactiveSpace = await _context.InteractiveSpace3D.FirstOrDefaultAsync(s => s.Id == stepDTO.InteractiveSpace3DId);
            if (interactiveSpace == null)
                ModelState.AddModelError("InteractiveSpaceId", $"InteractiveSpace: {stepDTO.InteractiveSpace3DId} is not registered in the system");

            switch (stepDTO.TypeOfStep)
            {

                case TypeOfStep.Optional:
                case TypeOfStep.Mandatory:
                    if (stepDTO.StepDescriptions.Count != 1) ModelState.AddModelError("PreviousStepId", $"Mandatory and Optional steps only allow one DescriptionStep");
                    break;
                default:
                    if (stepDTO.StepDescriptions.Count < 2) ModelState.AddModelError("PreviousStepId", $"Alternative steps require more than one DescriptionStep");
                    break;
            }

            if (ModelState.IsValid)
            {
                step= new Step(stepDTO, interactiveSpace);
                step.Activity = activity;
                _context.Step.Add(step);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("Step:", ex.Message + ex.InnerException);
                }

                return step;
            }    
            else return null;

        }

        // POST: api/Steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<StepDetailsDTO>> AddPreviousStep(StepCreateDTO stepDTO)
        {
            Step step=null;
            if (ModelState.IsValid) {
                Step? nextStep = await _context.Step.Include(s => s.NextStep)
                    .FirstOrDefaultAsync(s => s.Id == stepDTO.NextStep);


                if (nextStep == null)
                     ModelState.AddModelError("NextStepId",$"Such previous step {stepDTO.NextStep} is not registered in the system");
                else
                    step = await CreateStep(stepDTO);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //it is the last step of Activity
                if (nextStep.FirstStep)
                {
                    step.Activity.FirstStep = step;
                    
                }
                else { 
                    step.PreviousStep= nextStep.PreviousStep;

                }
               
                step.NextStep = nextStep;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(ex.Message + ex.InnerException);
                }
                var stepDetailDTO = new StepDetailsDTO(step);
                return CreatedAtAction("GetStep", new { id = step.Id }, stepDetailDTO);
            }
            return BadRequest(ModelState);

        }

        // DELETE: api/Steps/5
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteStep(int id)
        {
            var step = await _context.Step
                .Include(s => s.Activity).Include(s=>s.NextStep).Include(s=>s.PreviousStep)
                .Include(s => s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.LocatedIn)
                .Include(s => s.StepDescriptions).ThenInclude(sd => sd.EntityStep).ThenInclude(es => es.HasActions)
                            .ThenInclude(a => a.Animation).ThenInclude(an => an.Entity)
                .FirstOrDefaultAsync(s => s.Id == id); ;
            if (step == null)
            {
                return NotFound(id);
            }

            //it is the first step
            if (step.PreviousStep == null)
                step.Activity.FirstStep = step.NextStep == null ? null : step.Activity.FirstStep = step.NextStep;
            else
            {
                step.PreviousStep.NextStep = step.NextStep;
                if (step.NextStep != null)
                    step.NextStep.PreviousStep=step.PreviousStep;
            }
                

            _context.Step.Remove(step);
            await _context.SaveChangesAsync();

            return Ok(id);
        }

        private bool StepExists(int id)
        {
            return _context.Step.Any(e => e.Id == id);
        }
    }
}
