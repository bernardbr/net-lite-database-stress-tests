using System.Collections.Generic;
using System.Linq;
using Application.Api.Models;
using LiteDB;

namespace Application.Api.Repositories.Impl
{
    public class PersonLiteDbRepository : IPersonRepository
    {
        public void Delete(Person person)
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            collection.Delete(person.Id);
        }

        public Person Get(int id)
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            return collection.FindById(id);
        }

        public IEnumerable<Person> Get()
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            return collection.FindAll().ToList();
        }

        public void Post(Person person)
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            collection.EnsureIndex(x => x.Id, true);
            collection.Insert(person);
        }

        public void Put(Person person)
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            collection.EnsureIndex(x => x.Id, true);
            collection.Update(person);
        }
    }
}
