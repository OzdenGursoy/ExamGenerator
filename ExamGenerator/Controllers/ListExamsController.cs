using ExamGenerator.Data;
using ExamGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamGenerator.Controllers
{
    [Authorize]
    public class ListExamsController : Controller
    {
        private readonly ExamGeneratorDbContext _context;

        public ListExamsController(ExamGeneratorDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Exam.ToList();
            return View(data);
        }

        public IActionResult DeleteExam(int id)
        {
            var questions = _context.Question.Where(obj => obj.ExamId == id).Select(col => col.QId);
            foreach (var question in questions.ToList())
            {
                var row = _context.Question.Find(question);
                _context.Question.Remove(row);
                _context.SaveChanges();
            }

            var exam = _context.Exam.Find(id);
            _context.Exam.Remove(exam);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}