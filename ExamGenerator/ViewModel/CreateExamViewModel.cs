using ExamGenerator.Models;

namespace ExamGenerator.ViewModel
{
    //This class is for using multiple models in view
    public class CreateExamViewModel
    {
        public Article Article { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
