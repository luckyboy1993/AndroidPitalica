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
            return _context.Exams.Include(e => e.Questions);
        }

        // GET: api/Exams/5
        [HttpPost("{id}")]
        public IActionResult GetExam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = _context.Exams.Include(e => e.Questions).SingleOrDefault(e => e.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        [HttpPost("GetExamsTaken/{id:int?}")]
        public IActionResult GetExamsTaken(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examsTaken = _context.UserExamTaken.Where(uet => uet.UserId == id).Select(uet => uet.Exam).Include(uet => uet.Questions);
            //var exams = _context.Exams.Where(e => e. == id);

            if (examsTaken == null)
            {
                return NotFound();
            }

            return Ok(examsTaken);
        }

        [HttpPost("GetExamsNotTaken/{id:int?}")]
        public IActionResult GetExamsNotTaken(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var e = _context.UserExamTaken.Where(uet => uet.UserId != id);

            var examsNotTaken = _context.Exams.Where(ex => !ex.Students.Select(s => s.UserId).Contains(id)).Where(ex => ex.CreatorId != id).Include(ex => ex.Questions);

            //var examsNotTaken = e.Where(uet => uet.Exam.CreatorId != id).Select(uet => uet.Exam).Include(uet => uet.Questions);
            
            //var examsNotTaken = _context.UserExamTaken.Where(uet => uet.UserId != id).Select(uet => uet.Exam).Include(uet => uet.Questions).Where(uet => uet.CreatorId != id);
            //var exams = _context.Exams.Where(e => e. == id);

            if (examsNotTaken == null)
            {
                return NotFound();
            }

            return Ok(examsNotTaken);
        }

        [HttpPost("GetExamStudents/{id:int?}")]
        public IActionResult GetExamStudents(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examStudents = _context.UserExamTaken.Where(uet => uet.ExamId == id).Select(uet => uet.User);

            if (examStudents == null)
            {
                return NotFound();
            }

            return Ok(examStudents);
        }

        [HttpPost("GetExamsCreated/{id:int?}")]
        public IActionResult GetExamCreated(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examsCreated = _context.Exams.Where(e => e.CreatorId == id).Include(e => e.Questions);

            if (examsCreated == null)
            {
                return NotFound();
            }

            return Ok(examsCreated);
        }        

        [HttpPost("GetExamResults/{examId:int?}/{userId:int?}")]
        public IActionResult GetExamResults(int examId, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionResults = _context.QuestionResults.Where(qr => qr.ExamId == examId && qr.UserId == userId).Include(qr => qr.Question).Include(qr => qr.Exam);

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
        [HttpPost(("InsertExam"))]
        public async Task<IActionResult> InsertExam([FromBody] Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("InsertExam", new { id = exam.Id }, exam);
        }

        [HttpPost("InsertExamResults")]
        public async Task<IActionResult> InsertExamResults([FromBody] QuestionResultList questionResultList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExamTaken = new UserExamTaken
            {
                UserId = questionResultList.QuestionResults.First().UserId,
                ExamId = questionResultList.QuestionResults.First().ExamId
            };

            _context.UserExamTaken.Add(userExamTaken);

            _context.QuestionResults.AddRange(questionResultList.QuestionResults);

            await _context.SaveChangesAsync();

            return CreatedAtAction("InsertExamResults", new { id = userExamTaken.Id }, userExamTaken);
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