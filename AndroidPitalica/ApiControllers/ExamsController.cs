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
    [Route("api/Exams")]
    public class ExamsController : Controller
    {
        private readonly PitalicaContext _context;

        public ExamsController(PitalicaContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        public IEnumerable<Exam> GetExams()
        {
            return _context.Exams;
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = await _context.Exams.SingleOrDefaultAsync(m => m.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        [HttpGet("GetExamsTaken/{id:int?}")]
        public IActionResult GetExamsTaken(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examsTaken = _context.UserExamTaken.Where(uet => uet.UserId == id).Select(uet => uet.Exam);
            //var exams = _context.Exams.Where(e => e. == id);

            if (examsTaken == null)
            {
                return NotFound();
            }

            return Ok(examsTaken);
        }

        [HttpGet("GetExamsCreated/{id:int?}")]
        public IActionResult GetExamCreated(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examsCreated = _context.Exams.Where(e => e.CreatorId == id);

            if (examsCreated == null)
            {
                return NotFound();
            }

            return Ok(examsCreated);
        }

        [HttpGet("GetExamResults/{examId:int?}/{userId:int?}")]
        public IActionResult GetExamResults(int examId, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionResults = _context.QuestionResults.Where(qr => qr.ExamId == examId && qr.UserId == userId);

            if (questionResults == null)
            {
                return NotFound();
            }

            return Ok(questionResults);
        }

        // PUT: api/Exams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam([FromRoute] int id, [FromBody] Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam.Id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/Exams
        [HttpPost]
        public async Task<IActionResult> PostExam([FromBody] Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = await _context.Exams.SingleOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return Ok(exam);
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}