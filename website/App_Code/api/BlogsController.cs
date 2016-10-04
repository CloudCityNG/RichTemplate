using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Business = rich.Business;
using Models = rich.Models;

namespace api
{
    public class BlogsController : ApiController
    {
        private static Business.Blog _blogBusiness;
        public static Business.Blog BlogBusiness
        {
            get { return _blogBusiness ?? (_blogBusiness = new Business.Blog()); }
        }

        // GET api/<controller>
        public IEnumerable<Models.Blog> Get()
        {
            return BlogBusiness.GetAll().ToList();
        }

        // GET api/<controller>/5
        public Models.Blog Get(string id)
        {
            return BlogBusiness.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody] Models.Blog blog)
        {
            if (blog.Id == null)
            {
                blog.DateCreated = DateTime.Now;
                BlogBusiness.Add(blog);
            }
            else
            {
                BlogBusiness.Update(MergeBlog(blog));
            }
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            BlogBusiness.Delete(id);
        }

        private Models.Blog MergeBlog(Models.Blog blog)
        {
            var existingBlog = BlogBusiness.Get(blog.Id);
            existingBlog.Title = blog.Title;
            existingBlog.Description = blog.Description;
            return existingBlog;
        }
    }
}