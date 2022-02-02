namespace ExamGenerator.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Title { get; set;} = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Cdate { get; set; } = string.Empty;
    }
}
