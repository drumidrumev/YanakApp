using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YanakApp.Contracts;
using YanakApp.DTOs.Product;
using YanakApp.DTOs.User;
using YanakApp.Models;

namespace YanakApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
           _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUsersDto>>> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser(int id)
        {
            var user = await _usersService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

           await _usersService.UpdateUserAsync(id, updateDto);

           return NoContent();
        }


        [HttpPut("{userId}/products/{productId}")]
        public async Task<IActionResult> UserBuy(int userId,int productId)
        {
 

            await _usersService.UserBuyAsync(userId, productId);    

            return NoContent();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<GetUserDto>> PostUser(CreateUserDto createDto)
        {

            var resultDto = await _usersService.CreateUserAsync(createDto);

            return CreatedAtAction(nameof(GetUser), new { id = resultDto.Id }, resultDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usersService.DeleteUserAsync(id);

            return NoContent();
        }


    }
}
