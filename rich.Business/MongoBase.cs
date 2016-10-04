using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Linq;
using rich.Models;

namespace rich.Business
{
    public class MongoBase<T> where T : BaseEntity
    {
        private MongoCollection<T> GetCollection()
        {
            return MongoDb.Instance.MongoDatabase.GetCollection<T>(typeof(T).FullName);
        }

        public MongoCursor<T> GetAll()
        {
            return GetCollection().FindAll();
        }

        public T Get(string id)
        {
            return GetCollection().FindOne(Query<T>.EQ(t => t.Id, id));
        }


        public void Add(T t)
        {
            GetCollection().Save(t);
        }

        public void Update(T t)
        {
            GetCollection().Save(t);
        }

        public void DeleteAll()
        {
            GetCollection().RemoveAll();
        }

        public void Delete(string id)
        {
            GetCollection().Remove(Query<T>.EQ(t => t.Id, id));
        }
    }
}
