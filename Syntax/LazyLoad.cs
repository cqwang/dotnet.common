using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class LazyLoad
    {
        public static void TestLazyLoad()
        {
            BlogUser blogUser = new BlogUser(1);//创建对象，但属性Articles直到使用时才会初始化
            Console.WriteLine(blogUser.Articles.IsValueCreated);//false 未初始化
            foreach (var article in blogUser.Articles.Value)//使用时会初始化
            {
                Console.WriteLine(blogUser.Articles.IsValueCreated);
                Console.WriteLine(article.Title);
            }
        }
    }

    /// <summary>
    /// 博客用户类
    /// </summary>
    public class BlogUser
    {
        public int Id
        {
            get;
            private set;
        }

        public Lazy<List<Article>> Articles
        {
            get;
            private set;
        }

        public BlogUser(int id)
        {
            this.Id = id;
            Articles = new Lazy<List<Article>>(() => GetArticesByID(id));
            Console.WriteLine("BlogUser Created");
        }

        private List<Article> GetArticesByID(int blogUserID)
        {
            List<Article> articles = new List<Article>
            {
                new Article{Id=1,Title="Lazy Load",PublishDate=DateTime.Parse("2011-4-20")},
                new Article{Id=2,Title="Delegate",PublishDate=DateTime.Parse("2011-4-21")},
                new Article{Id=3,Title="Event",PublishDate=DateTime.Parse("2011-4-22")},
                new Article{Id=4,Title="Thread",PublishDate=DateTime.Parse("2011-4-23")}
            };
            Console.WriteLine("BlogUser.Article Initalized"); return articles;
        }
    }

    /// <summary>
    /// 文章实体类
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
