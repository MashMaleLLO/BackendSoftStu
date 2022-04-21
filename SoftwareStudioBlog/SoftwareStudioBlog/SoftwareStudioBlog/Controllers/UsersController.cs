#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareStudioBlog.Data;
using SoftwareStudioBlog.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SoftwareStudioBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SoftwareStudioBlogContext _context;

        public UsersController(SoftwareStudioBlogContext context)
        {
            _context = context;
        }

        // ******* User Get ********

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // ******* User Update *******

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
                
            }
        }


        //  ********* Add User **********

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            int userId = (from x in _context.User where x.Username == user.Username select x.Id).SingleOrDefault();

            if (userId > 0)
            {

                return BadRequest("This username is already taken.");
            }
            else
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
        }

        // POST: api/Users/Admin
        [HttpPost("Admin")]
        public async Task<ActionResult<User>> PostAdmin(User user)
        {

            int userId = (from x in _context.User where x.Username == user.Username select x.Id).SingleOrDefault();

            if (userId > 0)
            {

                return BadRequest("This Username is Taken.");
            }
            else
            {
                user.IsAdmin = "True";
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
        }

        // Ban user by ID:
        // api/Users/ban/{id}

        [HttpPut("ban/{id}")]
        public async Task<IActionResult> BanUser(int id, User u)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if(u.IsAdmin == "True")
                {
                    user.IsBan = "True";
                    _context.Entry(user).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok("You are ADMIN and Bsanned user");
                }
                else
                {
                    return BadRequest("You don't have a permission to do this Action.");
                }
            }
        }

        // Unbanned user by ID:
        // api/Users/unbanned/{id}

        [HttpPut("unbanned/{id}")]
        public async Task<IActionResult> UnbannedUser(int id, User u)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (u.IsAdmin == "True")
                {
                    user.IsBan = "False";
                    _context.Entry(user).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok("You are ADMIN and Unbanned user");
                }
                else
                {
                    return BadRequest("You don't have a permission to do this Action.");
                }
            }
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, User u)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (u.IsAdmin == "True" || u.Id == id)
                {
/*                    //// Show delete user
                    System.Diagnostics.Debug.WriteLine($"delete this --> ", user);
*/
                    _context.User.Remove(user);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return BadRequest("You do not have permission to delete this User.");
                }   
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }


        // #################### Login here!! #########################

        // POST: https://localhost:7198/api/Users/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            int userId = (from x in _context.User where x.Username == user.Username select x.Id).SingleOrDefault();
            var u = await _context.User.FindAsync(userId);
            if (u == null)
            {
                return NotFound("Username not Found");
            }
            else
            {
                if (u.Password == user.Password)
                {
                    return Ok(u);
                }
                else
                {
                    Console.WriteLine("Boommmmm!!! 5555");
                    return BadRequest("Pass not Match");
       
                }
            }
        }
    }
}
