using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AndroidPitalica.DAL.Entities;

namespace AndroidPitalica.Controllers
{
    public class QuestionResultsController : Controller
    {
        private readonly PitalicaContext _context;

        public QuestionResultsController(PitalicaContext context)
        {
            _context = context;
        }

        // GET: QuestionResults
        public async Task<IActionResult> Index()
        {
            var pitalicaContext = _context.QuestionResults.Include(q => q.Question).Include(q => q.User);
            return View(await pitalicaContext.ToListAsync());
        }

        // GET: QuestionResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionResult = await _context.QuestionResults
                .Include(q => q.Question)
                .Include(q => q.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (questionResult == null)
            {
                return NotFound();
            }

            return View(questionResult);
        }

        // GET: QuestionResults/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: QuestionResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Answered,CorrectAnswer,QuestionId,UserId")] QuestionResult questionResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", questionResult.QuestionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questionResult.UserId);
            return View(questionResult);
        }

        // GET: QuestionResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionResult = await _context.QuestionResults.SingleOrDefaultAsync(m => m.Id == id);
            if (questionResult == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", questionResult.QuestionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questionResult.UserId);
            return View(questionResult);
        }

        // POST: QuestionResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Answered,CorrectAnswer,QuestionId,UserId")] QuestionResult questionResult)
        {
            if (id != questionResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionResultExists(questionResult.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", questionResult.QuestionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", questionResult.UserId);
            return View(questionResult);
        }

        // GET: QuestionResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionResult = await _context.QuestionResults
                .Include(q => q.Question)
                .Include(q => q.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (questionResult == null)
            {
                return NotFound();
            }

            return View(questionResult);
        }

        // POST: QuestionResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionResult = await _context.QuestionResults.SingleOrDefaultAsync(m => m.Id == id);
            _context.QuestionResults.Remove(questionResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionResultExists(int id)
        {
            return _context.QuestionResults.Any(e => e.Id == id);
        }
    }
}
