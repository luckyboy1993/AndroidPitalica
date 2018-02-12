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
    [Route("api/UserExamTakens")]
    public class UserExamTakensController : Controller
    {
        private readonly PitalicaContext _context;

        public UserExamTakensController(PitalicaContext context)
        {
            _context = context;
        }

        // GET: api/UserExamTakens
        [HttpGet]
        public IEnumerable<UserExamTaken> GetUserExamTaken()
        {
            return _context.UserExamTaken;
        }

        // GET: api/UserExamTakens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserExamTaken([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExamTaken = await _context.UserExamTaken.SingleOrDefaultAsync(m => m.UserId == id);

            if (userExamTaken == null)
            {
                return NotFound();
            }

            return Ok(userExamTaken);
        }

        // PUT: api/UserExamTakens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserExamTaken([FromRoute] int id, [FromBody] UserExamTaken userExamTaken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userExamTaken.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userExamTaken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExamTakenExists(id))
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

        // POST: api/UserExamTakens
        [HttpPost]
        public async Task<IActionResult> PostUserExamTaken([FromBody] UserExamTaken userExamTaken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserExamTaken.Add(userExamTaken);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExamTakenExists(userExamTaken.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserExamTaken", new { id = userExamTaken.UserId }, userExamTaken);
        }

        // DELETE: api/UserExamTakens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserExamTaken([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExamTaken = await _context.UserExamTaken.SingleOrDefaultAsync(m => m.UserId == id);
            if (userExamTaken == null)
            {
                return NotFound();
            }

            _context.UserExamTaken.Remove(userExamTaken);
            await _context.SaveChangesAsync();

            return Ok(userExamTaken);
        }

        private bool UserExamTakenExists(int id)
        {
            return _context.UserExamTaken.Any(e => e.UserId == id);
        }
    }
}