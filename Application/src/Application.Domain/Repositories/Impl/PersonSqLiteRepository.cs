using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Application.Domain.Models;
using Dapper;
using Serilog;

namespace Application.Domain.Repositories.Impl
{
    public class PersonSqLiteRepository : IPersonRepository
    {
        static PersonSqLiteRepository()
        {
            if (File.Exists("sqlite.db")) return;

            SQLiteConnection.CreateFile("sqlite.db");
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            db.Open();
            using var cmd = new SQLiteCommand("CREATE TABLE people (id integer primary key, name nvarchar(100), height decimal(10,2), birthDate datetime)", db);
            cmd.ExecuteNonQuery();
        }

        public void Delete(Person person)
        {
            Log.Logger.Information("Delete SqLite: {Id}", person.Id);
            try
            {
                using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
                db.Execute("DELETE FROM people WHERE id = @id", new { id = person.Id });
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Delete LqLite");
                throw;
            }
        }

        public Person Get(int id)
        {
            Log.Logger.Information("Get SqLite: {id}", id);
            try
            {
                using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
                return db.QueryFirstOrDefault<Person>("SELECT id as Id, name as Name, height as Height, birthDate as BirthDate FROM people WHERE id = @id", new { id });
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Get SqLite");
                throw;
            }
        }

        public IEnumerable<Person> GetAll()
        {
            Log.Logger.Information("GetAll SqLite");
            try
            {
                using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
                return db.Query<Person>("SELECT id as Id, name as Name, height as Height, birthDate as BirthDate FROM people").ToList();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "GetAll SqLite");
                throw;
            }
        }

        public void Post(Person person)
        {
            Log.Logger.Information("Post SqLite: {@person}", person);
            try
            {
                using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
                db.Execute(
                    "INSERT INTO people (name, height, birthDate) VALUES (@name, @height, @birthDate)",
                    new { name = person.Name, height = person.Height, birthDate = person.BirthDate });
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Post SqLite");
                throw;
            }
        }

        public void Put(Person person)
        {
            Log.Logger.Information("Put SqLite: {@person}", person);
            try
            {
                using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
                db.Execute(
                    "UPDATE people SET name = @name, height = @height, birthDate = @birthDate WHERE id = @id",
                    new { name = person.Name, height = person.Height, birthDate = person.BirthDate, id = person.Id });
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Put SqLite");
                throw;
            }
        }
    }
}
