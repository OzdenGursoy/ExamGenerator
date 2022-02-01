using System.ComponentModel.DataAnnotations;

namespace ExamGenerator.Models
{
    public class Article
    {
        [Required(ErrorMessage = "Please select an article")]
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
    }
}
