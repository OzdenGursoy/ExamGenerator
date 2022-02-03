using ExamGenerator.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExamGenerator.Controllers
{
    public class AjaxHelperController : Controller
    {
        private readonly ExamGeneratorDbContext _context;

        public AjaxHelperController(ExamGeneratorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult getColorList(int id,string op1, string op2, string op3, string op4)
        {
            var questions = _context.Question.Where(obj => obj.ExamId == id).ToList();

            List<string> colorList = new List<string>();

            colorList.Add((op1 == questions[0].CorrectOp) ? "green" : "red");
            colorList.Add((op2 == questions[1].CorrectOp) ? "green" : "red");
            colorList.Add((op3 == questions[2].CorrectOp) ? "green" : "red");
            colorList.Add((op4 == questions[3].CorrectOp) ? "green" : "red");

            return Json(colorList);
        }
    }
}
    