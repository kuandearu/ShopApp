using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using api.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var usersDTO = users.Select(u => u.ToUserDTO());
            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            var userDTO = user.ToUserDTO();
            return Ok(userDTO);
        }
    }
}