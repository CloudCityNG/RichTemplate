using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace rich.Tests
{
    [TestClass]
    public class BlogTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var blogBusiness = new Business.Blog();
            blogBusiness.DeleteAll();
            Assert.IsTrue(!blogBusiness.GetAll().Any());
            blogBusiness.Add(new Models.Blog()
            {
                Title = "TestBlogTitle1",
                Description = "TestBlogDescription1 Goes Here...",
                BlogType = 1,
                DateCreated = DateTime.Now
            });
            blogBusiness.Add(new Models.Blog()
            {
                Title = "TestBlogTitle2",
                Description = "TestBlogDescription2 Goes Here...",
                BlogType = 2,
                DateCreated = DateTime.Now
            });
            blogBusiness.Add(new Models.Blog()
            {
                Title = "TestBlogTitle3",
                Description = "TestBlogDescription3 Goes Here...",
                BlogType = 3,
                DateCreated = DateTime.Now
            });
            var blogs = blogBusiness.GetAll().ToList();
            Assert.IsTrue(blogBusiness.GetAll().Count() == 3);

            var testBlog = blogBusiness.Get(blogs[1].Id);
            Assert.IsNotNull(testBlog);
            Assert.AreEqual(testBlog.Title, blogs[1].Title);
            blogBusiness.Delete(blogs[1].Id);
            Assert.IsTrue(blogBusiness.GetAll().Count() == 2);

            blogs[0].Title = string.Format("{0}_{1}", "UPDATED", blogs[0].Title);
            blogBusiness.Update(blogs[0]);

            var updatedBlog = blogBusiness.Get(blogs[0].Id);
            Assert.AreEqual(updatedBlog.Title, blogs[0].Title);
            Assert.IsTrue(blogBusiness.GetAll().Count() == 2);
        }
    }
}
