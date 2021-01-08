using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Application.Api.Models;
using Dapper;

namespace Application.Api.Repositories.Impl
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
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            db.Execute("DELETE FROM people WHERE id = @id", new { id = person.Id });
        }

        public Person Get(int id)
        {
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            return db.QueryFirst<Person>("SELECT id as Id, name as Name, height as Height, birthDate as BirthDate FROM people WHERE id = @id", new { id });
        }

        public IEnumerable<Person> Get()
        {
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            return db.Query<Person>("SELECT id as Id, name as Name, height as Height, birthDate as BirthDate FROM people").ToList();
        }

        public void Post(Person person)
        {
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            db.Execute(
                "INSERT INTO people (name, height, birthDate) VALUES (@name, @height, @birthDate)",
                new { name = person.Name, height = person.Height, birthDate = person.BirthDate });
        }

        public void Put(Person person)
        {
            using var db = new SQLiteConnection(@"Data Source=sqlite.db;Version=3;");
            db.Execute(
                "UPDATE people SET name = @name, height = @height, birthDate = @birthDate WHERE id = @id",
                new { name = person.Name, height = person.Height, birthDate = person.BirthDate, id = person.Id });
        }
    }
}
