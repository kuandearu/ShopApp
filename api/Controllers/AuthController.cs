using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Enums;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ShopAppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ShopAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDTO.Email);

            if(existingEmail != null){
                return Conflict("Email already existed! Please try again!");
            }

            var existingPhone = await _context.Users.FirstOrDefaultAsync(u => u.Phone == registerDTO.PhoneNumber);
            if(existingPhone != null){
                return Conflict("Phone number already existed! Please try again!");
            }
            
            // Map the DTO to the User entity
            var user = registerDTO.ToRegisterAccountResponse();
            
            // Create the user using UserManager
            user.Role = UserRole.RegisteredUser;

            // Assign the "RegisteredUser" role to the user
            // await _userManager.AddToRoleAsync(user, UserRole.RegisteredUser.ToString());
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new {
                Message = "User registered successfully",
                User = user
            });

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
             if (user == null)
                return Unauthorized("User not found or invalid credentials.");

            // Validate password
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                return Unauthorized("User or password is incorrect.");            


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenValue, User = user });

        } 


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _context.Users.Include(u => u.Orders)
                                            .ThenInclude(o => o.OrderDetails)
                                            .Include(u => u.Feedbacks) 
                                            .ToListAsync();
            var usersDTO = users.Select(u => u.ToUserDTO()).ToList();
             return Ok(usersDTO);
        }

    }
}