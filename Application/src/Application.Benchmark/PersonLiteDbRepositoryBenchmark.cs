using System;
using Application.Domain.Extensions;
using Application.Domain.Models;
using Application.Domain.Repositories;
using Application.Domain.Repositories.Impl;
using BenchmarkDotNet.Attributes;
using Serilog;

namespace Application.Benchmark
{
    public class PersonLiteDbRepositoryBenchmark
    {
        private Bogus.Faker<Person> _personFaker;
        private IPersonRepository _repository;

        [Benchmark]
        public void Create()
        {
            var person = _personFaker.Generate();
            _repository.Post(person);
        }

        [Benchmark]
        public void Delete()
        {
            var person = _repository.GetAll().RandomElement();
            _repository.Delete(person);
        }

        [Benchmark]
        public void Edit()
        {
            var person = _repository.GetAll().RandomElement();
            _personFaker.Populate(person);
            _repository.Put(person);
        }

        [Benchmark]
        public void GetAll()
        {
            _repository.GetAll();
        }

        [GlobalSetup]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:32784/")
                .CreateLogger();

            _repository = new PersonLiteDbRepository();
            _personFaker = new Bogus.Faker<Person>()
                .RuleFor(p => p.BirthDate, b => b.Date.Past(18))
                .RuleFor(p => p.Height, h => h.Random.Decimal(1.45M, 1.99M))
                .RuleFor(p => p.Name, n => n.Name.FullName())
                .FinishWith((f, p) =>
                {
                    Console.WriteLine("Person Created! Name={0}", p.Name);
                });

            // Creates the 100 first elements
            for (var i = 0; i < 100; i++)
            {
                Create();
            }
        }
    }
}
