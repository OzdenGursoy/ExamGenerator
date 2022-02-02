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
    }
}