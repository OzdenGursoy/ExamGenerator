using ExamGenerator.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ExamGenerator.Controllers
{
    public class ExamCreationController : Controller
    {
        public const string baseURL = "https://www.wired.com";

        public IActionResult CreateExam()
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

            List<Article> articles = new List<Article>();

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
                        title = title,
                        content = content
                    });
                }
            }

            return View(articles);
        }

        //Web Scraping Method
        private HtmlNodeCollection getNodeFromPath(string url, string path)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);

            return doc.DocumentNode.SelectNodes(path);
        }

        //HTML parser to get the text parts only
  /*      private String ParseHTML(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var articleStrings = htmlDoc.DocumentNode.Descendants("p").ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var item in articleStrings)
            {
                if (item.FirstChild.Attributes.Count > 0)
                    sb.AppendLine(item.FirstChild.Attributes[0].Value);
            }

            return sb.ToString();
        }
  */
    }
}
