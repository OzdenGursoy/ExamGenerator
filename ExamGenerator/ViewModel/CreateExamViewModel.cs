using ExamGenerator.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamGenerator.ViewModel
{
    //This class is for using multiple models in view
    public class CreateExamViewModel
    {
        [Required]
        public Article? Article { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
