using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace rich.Tests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var eventBusiness = new Business.Event();
            eventBusiness.DeleteAll();
            Assert.IsTrue(!eventBusiness.GetAll().Any());
            eventBusiness.Add(new Models.Event()
            {
                Title = "TestEventTitle1",
                Description = "TestEventDescription1 Goes Here...",
                EventType = 1,
                DateCreated = DateTime.Now
            });
            eventBusiness.Add(new Models.Event()
            {
                Title = "TestEventTitle2",
                Description = "TestEventDescription2 Goes Here...",
                EventType = 2,
                DateCreated = DateTime.Now
            });
            eventBusiness.Add(new Models.Event()
            {
                Title = "TestEventTitle3",
                Description = "TestEventDescription3 Goes Here...",
                EventType = 3,
                DateCreated = DateTime.Now
            });
            var events = eventBusiness.GetAll().ToList();
            Assert.IsTrue(eventBusiness.GetAll().Count() == 3);

            var testEvent = eventBusiness.Get(events[1].Id);
            Assert.IsNotNull(testEvent);
            Assert.AreEqual(testEvent.Title, events[1].Title);
            eventBusiness.Delete(events[1].Id);
            Assert.IsTrue(eventBusiness.GetAll().Count() == 2);

            events[0].Title = string.Format("{0}_{1}", "UPDATED", events[0].Title);
            eventBusiness.Update(events[0]);

            var updatedEvent = eventBusiness.Get(events[0].Id);
            Assert.AreEqual(updatedEvent.Title, events[0].Title);

            Assert.IsTrue(eventBusiness.GetAll().Count() == 2);
        }
    }
}
