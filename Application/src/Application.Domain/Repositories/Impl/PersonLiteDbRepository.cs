using System;
using System.Collections.Generic;
using System.Linq;
using Application.Domain.Models;
using LiteDB;
using Serilog;

namespace Application.Domain.Repositories.Impl
{
    public class PersonLiteDbRepository : IPersonRepository
    {
        static PersonLiteDbRepository()
        {
            using var db = new LiteDatabase(@"litedb.db");
            var collection = db.GetCollection<Person>("people");
            collection.EnsureIndex(x => x.Id, true);
        }

        public void Delete(Person person)
        {
            Log.Logger.Information("Delete LiteDb: {Id} {@event}", person.Id, new { type = "delete", source = "litedb" });
            try
            {
                using var db = new LiteDatabase(@"litedb.db");
                var collection = db.GetCollection<Person>("people");
                collection.Delete(person.Id);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Delete LiteDb {@event}", new { type = "delete", source = "litedb", parameters = person });
                throw;
            }
        }

        public Person Get(int id)
        {
            Log.Logger.Information("Get LiteDb: {id} {@event}", id, new { type = "get", source = "litedb" });
            try
            {
                using var db = new LiteDatabase(@"litedb.db");
                var collection = db.GetCollection<Person>("people");
                return collection.FindById(id);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Get LiteDb {@event}", new { type = "get", source = "litedb", parameters = id });
                throw;
            }
        }

        public IEnumerable<Person> GetAll()
        {
            Log.Logger.Information("GetAll LiteDb {@event}", new { type = "getAll", source = "litedb" });
            try
            {
                using var db = new LiteDatabase(@"litedb.db");
                var collection = db.GetCollection<Person>("people");
                return collection.FindAll().ToList();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "GetAll LiteDb {@event}", new { type = "getAll", source = "litedb" });
                throw;
            }
        }

        public void Post(Person person)
        {
            Log.Logger.Information("Post LiteDb: {@person} {@event}", person, new { type = "Post", source = "litedb" });
            try
            {
                using var db = new LiteDatabase(@"litedb.db");
                var collection = db.GetCollection<Person>("people");
                collection.Insert(person);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Post LiteDb {@event}", new { type = "post", source = "litedb", parameters = person });
                throw;
            }
        }

        public void Put(Person person)
        {
            Log.Logger.Information("Put LiteDb: {@person} {@event}", person, new { type = "get", source = "litedb" });
            try
            {
                using var db = new LiteDatabase(@"litedb.db");
                var collection = db.GetCollection<Person>("people");
                collection.Update(person);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Put LiteDb {@event}", new { type = "get", source = "litedb", parameters = person });
                throw;
            }
        }
    }
}
