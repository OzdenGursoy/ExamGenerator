using System.ComponentModel.DataAnnotations;

namespace ExamGenerator.Models
{
    public class Question
    {
        [Key]
        public int QId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string OpA { get; set; } = string.Empty;
        public string OpB { get; set; } = string.Empty;
        public string OpC { get; set; } = string.Empty;
        public string OpD { get; set; } = string.Empty;
        public string CorrectOp { get; set; } = string.Empty;
        public int ExamId { get; set;}
        public int QNumber { get; set;}
    }
}
