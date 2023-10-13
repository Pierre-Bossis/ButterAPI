using Butter.Models;
using Butter.Repositories.Interfaces;
using ButterAPI.Models;
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

            if (user is not null)
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
            if (user is not null)
            {
                return Created($"https://localhost:7199/{user.UserId}", user);
            }
            else
            {
                return BadRequest("Nickname Already used.");
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                _repo.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult Update([FromBody] UserUpdate model)
        {
            if (model is not null)
            {
                var user = _repo.Update(model);
                if (user is not null)
                {
                    return Created($"https://localhost:7199/{user.UserId}", user);
                }
                else
                {
                    return BadRequest("Nickname Already used.");
                }
            }
            return NotFound();
        }
    }
}