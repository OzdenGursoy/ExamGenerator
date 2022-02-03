using ExamGenerator.Data;
using ExamGenerator.Models;
using ExamGenerator.ViewModel;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamGenerator.Controllers
{
    [Authorize]
    public class ExamCreationController : Controller
    {
        private readonly ExamGeneratorDbContext _context;

        public const int nQuestions = 4;
        public const string baseURL = "https://www.wired.com";

        readonly List<Article> articles = new List<Article>();

        public ExamCreationController(ExamGeneratorDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateExam()
        {
            CreateExamViewModel createExamViewModel = new CreateExamViewModel();
            createExamViewModel.Article = new Article();
            createExamViewModel.Exam = new Exam();
            createExamViewModel.Question = new Question();

            FetchArticles();
            ViewBag.articleList = articles;

            return View(createExamViewModel);
        }

        [HttpPost]
        public IActionResult CreateExam(CreateExamViewModel model)
        {
            //Insert Exam to Exam table in DB
            Exam exam = new Exam();

            exam.Title = Request.Form["title"];
            exam.Content = Request.Form["select1"];
            exam.Cdate = DateTime.Now.ToString("G");

            _context.Exam.Add(exam);
            _context.SaveChanges();

            //Insert Questions separately to Question table in DB

            int examId = exam.ExamId;

            saveQuestionsToDB(nQuestions, examId);

            FetchArticles();
            ViewBag.articleList = articles;

            return View(model);
        }

        public void saveQuestionsToDB(int numberOfQuestions, int examId)
        {
            for(int i = 1; i < numberOfQuestions+1; i++)
            {
                string questionText = Request.Form["q"+i];
                string opA = Request.Form["q"+i+"a"];
                string opB = Request.Form["q"+i+"b"];
                string opC = Request.Form["q"+i+"c"];
                string opD = Request.Form["q"+i+"d"];
                string CorrectOp = Request.Form["selectAnswer"+i];

                Question question = new Question();
                question.QuestionText = questionText;
                question.OpA = opA;
                question.OpB = opB;
                question.OpC = opC;
                question.OpD = opD;
                question.CorrectOp = CorrectOp;
                question.ExamId = examId;
                question.QNumber = i;

                _context.Question.Add(question);
                _context.SaveChanges();
            }

        }
        private void FetchArticles()
        {
            //Web Scraping to get article's titles and URL's
            String linkPath = "//div[@class='summary-list__items']";
            Dictionary<string, string> articleURLs = new();

            var articleLinkNode = getNodeFromPath(baseURL, linkPath);

            foreach (var item in articleLinkNode[1].ChildNodes)
            {
                var articleInfo = item.ChildNodes[1].FirstChild;
                var articleTitle = item.InnerText;
                string articleURL = baseURL + articleInfo.Attributes["href"].Value;
                articleURLs.Add(articleTitle, articleURL);
            }

            //Web Scraping for each item to get title and description
            string path = "//div[@class='body__inner-container']";

            foreach (var item in articleURLs)
            {
                string title = item.Key;
                var articleNodes = getNodeFromPath(item.Value, path);
                if (articleNodes != null)
                {
                    var articleNode = articleNodes[0];
                    string content = articleNode.InnerText;

                    articles.Add(new Article()
                    {
                        Title = title,
                        Content = content
                    });
                }
            }
        }

        //Web Scraping Method
        private HtmlNodeCollection getNodeFromPath(string url, string path)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);

            return doc.DocumentNode.SelectNodes(path);
        }
    }
}
