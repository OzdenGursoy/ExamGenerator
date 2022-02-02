using System.ComponentModel.DataAnnotations;

namespace ExamGenerator.Models
{
    public class Article
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
