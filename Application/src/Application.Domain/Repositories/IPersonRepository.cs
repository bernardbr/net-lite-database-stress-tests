using System.Collections.Generic;
using Application.Domain.Models;

namespace Application.Domain.Repositories
{
    public interface IPersonRepository
    {
        void Delete(Person person);

        Person Get(int id);

        IEnumerable<Person> GetAll();

        void Post(Person person);

        void Put(Person person);
    }
}
