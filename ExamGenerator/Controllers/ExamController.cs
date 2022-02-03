using ExamGenerator.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExamGenerator.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamGeneratorDbContext _context;

        public ExamController(ExamGeneratorDbContext context)
        {
            _context = context;
        }

        public IActionResult TakeExam(int id)
        {
            var questions = _context.Question.Where(obj => obj.ExamId == id).ToList();
            var exam = _context.Exam.Find(id);

            ViewBag.q1 = questions[0];
            ViewBag.q2 = questions[1];
            ViewBag.q3 = questions[2];
            ViewBag.q4 = questions[3];

            return View(exam);
        }
    }
}
