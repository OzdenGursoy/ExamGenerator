using ExamGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamGenerator.Controllers
{
    [Authorize]
    public class ListExamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}