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

namespace SoftwareStudioBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly SoftwareStudioBlogContext _context;

        public BlogsController(SoftwareStudioBlogContext context)
        {
            _context = context;
        }


        // ******* Blogs Getter *******

        // GET: api/Blogs
        // See all blog exclude Hidden blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var blogs = (from x in _context.Blog where x.IsHidden == "False" select x).ToListAsync();
            return await blogs;
        }

        // GET: api/Blogs/5
        // See specific blog
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.Blog.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            else
            {
                if (blog.IsHidden == "False")
                {
                    return blog;
                }
                else
                {
                    return BadRequest($"This Blog {id} is Hidden.");
                }
            }
        }

        // GET: api/Blogs/MyBlog/{userId}
        // See all my Blog
        [HttpGet("MyBlog/{id}")]
        public async Task<ActionResult<IEnumerable<Blog>>> MyGetBlogs(int id)
        {
            var user = await _context.User.FindAsync(id);
            var blogs = (from x in _context.Blog where x.IsHidden == "False" && x.Username == user.Username select x).ToListAsync();
            
            return await blogs;
        }

        // GET : api/Blogs/Admin
        // (Admin) See all blog include Hidden blogs
        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<Blog>>> AdminGetBlogs()
        {
            return await _context.Blog.ToListAsync();
        }

        /// ************* update blogs *************

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {

            if (!BlogExists(id) || id != blog.Id)
            {
                return BadRequest("This Blog does not exit or blogId didn't match.");
            }
            else
            {
                _context.Entry(blog).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok($"Your Blog {id} have been Edited.");
            }
        }

        // PUT: api/Blogs/hide/{blogId}
        // Hide blogs by Admin
        [HttpPut("hide/{id}")]
        public async Task<IActionResult> HideBlog(int id, User u)
        {

            if (!BlogExists(id))
            {
                return BadRequest("This Blog does not exist.");
            }
            else
            {
                if (u.IsAdmin == "True")
                {
                    var blog = await _context.Blog.FindAsync(id);
                    blog.IsHidden = "True";
                    _context.Entry(blog).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok($"Blog {id} is now Hidden.");
                }
                else
                {
                    return BadRequest("You don't have permission to this Action.");
                }
                
            }
        }

        // PUT: api/Blogs/unhidden/{blogId}
        // Unhidden blog by Admin
        [HttpPut("unhidden/{id}")]
        public async Task<IActionResult> UnhiddenBlog(int id, User u)
        {

            if (!BlogExists(id))
            {
                return BadRequest("This Blog does not exist.");
            }
            else
            {
                if (u.IsAdmin == "True")
                {
                    var blog = await _context.Blog.FindAsync(id);
                    blog.IsHidden = "False";
                    _context.Entry(blog).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok($"Blog {id} is now Show.");
                }
                else
                {
                    return BadRequest("You don't have permission to this Action.");
                }

            }
        }


        /// ***********  add blogs  ************

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            var user = (from x in _context.User where x.Username == blog.Username select x).SingleOrDefault();

            if (user.IsBan == "False" || user.IsAdmin == "True")
            {
                _context.Blog.Add(blog);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
            }
            else
            {
                return BadRequest("You cannot do this action, because you are Banned");
            }   
        }


        /// ************* delete blogs *************

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id, User u)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            else
            {
                if (blog.Username == u.Username || u.IsAdmin == "True")
                {
                    _context.Blog.Remove(blog);
                    await _context.SaveChangesAsync();

                    return Ok("Remove content complete.");
                }
                else
                {
                    return BadRequest("You don't have any permission to this blog.");
                }
             }
            
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
