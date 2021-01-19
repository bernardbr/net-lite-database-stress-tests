using Application.Domain.Models;
using Application.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    [Route("api/v1/people")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Create([FromBody] Person person)
        {
            _repository.Post(person);
            return Created($"{HttpContext.Request.Path.Value}/{person.Id}", person);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var person = _repository.Get(id);
            _repository.Delete(person);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public ActionResult Edit([FromRoute] int id, [FromBody] Person person)
        {
            person.Id = id;
            _repository.Put(person);
            return Ok(person);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var person = _repository.Get(id);
            return Ok(person);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var people = _repository.GetAll();
            return Ok(people);
        }
    }
}
