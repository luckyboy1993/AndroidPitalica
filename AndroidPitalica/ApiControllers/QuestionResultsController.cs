using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndroidPitalica.DAL.Entities;

namespace AndroidPitalica.ApiControllers
{
    [Produces("application/json")]
    [Route("api/QuestionResults")]
    public class QuestionResultsController : Controller
    {
        private readonly PitalicaContext _context;

        public QuestionResultsController(PitalicaContext context)
        {
            _context = context;
        }

        // GET: api/QuestionResults
        [HttpGet]
        public IEnumerable<QuestionResult> GetQuestionResults()
        {
            return _context.QuestionResults;
        }

        // GET: api/QuestionResults/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionResult = await _context.QuestionResults.SingleOrDefaultAsync(m => m.Id == id);

            if (questionResult == null)
            {
                return NotFound();
            }

            return Ok(questionResult);
        }

        // PUT: api/QuestionResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionResult([FromRoute] int id, [FromBody] QuestionResult questionResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionResultExists(id))
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

        // POST: api/QuestionResults
        [HttpPost]
        public async Task<IActionResult> PostQuestionResult([FromBody] QuestionResult questionResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.QuestionResults.Add(questionResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionResult", new { id = questionResult.Id }, questionResult);
        }

        // DELETE: api/QuestionResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionResult = await _context.QuestionResults.SingleOrDefaultAsync(m => m.Id == id);
            if (questionResult == null)
            {
                return NotFound();
            }

            _context.QuestionResults.Remove(questionResult);
            await _context.SaveChangesAsync();

            return Ok(questionResult);
        }

        private bool QuestionResultExists(int id)
        {
            return _context.QuestionResults.Any(e => e.Id == id);
        }
    }
}