using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Business = rich.Business;
using Models = rich.Models;

namespace api
{
    public class EventsController : ApiController
    {
        private static Business.Event _eventBusiness;
        public static Business.Event EventBusiness
        {
            get { return _eventBusiness ?? (_eventBusiness = new Business.Event()); }
        }

        // GET api/<controller>
        public IEnumerable<Models.Event> Get()
        {
            return EventBusiness.GetAll().ToList();
        }

        // GET api/<controller>/5
        public Models.Event Get(string id)
        {
            return EventBusiness.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody] Models.Event e)
        {
            if (e.Id == null)
            {
                e.DateCreated = DateTime.Now;
                EventBusiness.Add(e);
            }
            else
            {
                EventBusiness.Update(MergeEvent(e));
            }
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            EventBusiness.Delete(id);
        }

        private Models.Event MergeEvent(Models.Event e)
        {
            var existingEvent = EventBusiness.Get(e.Id);
            existingEvent.Title = e.Title;
            existingEvent.Description = e.Description;
            return existingEvent;
        }
    }
}