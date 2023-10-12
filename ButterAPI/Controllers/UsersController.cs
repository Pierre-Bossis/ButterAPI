using Butter.Models;
using Butter.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ButterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _repo.Get();

            if (users is not null)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = _repo.GetById(id);

            if(user is not null)
            {
                return Ok(user);
            }
            return NotFound();

        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] UserModel model)
        {
            var user = _repo.Add(model);

            return Created($"https://localhost:7199/{user.UserId}",user);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            if(id != 0)
            {
                _repo.Delete(id);
                return Ok();
            }
            return NotFound();
        }
    }
}