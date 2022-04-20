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
    public class LikeBlogsController : ControllerBase
    {
        private readonly SoftwareStudioBlogContext _context;

        public LikeBlogsController(SoftwareStudioBlogContext context)
        {
            _context = context;
        }

        // GET: api/LikeBlogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeBlog>>> GetLikeBlog()
        {
            return await _context.LikeBlog.ToListAsync();
        }

        // GET: api/LikeBlogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeBlog>> GetLikeBlog(int id)
        {
            var likeBlog = await _context.LikeBlog.FindAsync(id);

            if (likeBlog == null)
            {
                return NotFound();
            }

            return likeBlog;
        }

        // PUT: api/LikeBlogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikeBlog(int id, LikeBlog likeBlog)
        {
            if (id != likeBlog.Id)
            {
                return BadRequest();
            }

            _context.Entry(likeBlog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeBlogExists(id))
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

        // POST: api/LikeBlogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikeBlog>> PostLikeBlog(LikeBlog likeBlog)
        {
            _context.LikeBlog.Add(likeBlog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLikeBlog", new { id = likeBlog.Id }, likeBlog);
        }

        // DELETE: api/LikeBlogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikeBlog(int id)
        {
            var likeBlog = await _context.LikeBlog.FindAsync(id);
            if (likeBlog == null)
            {
                return NotFound();
            }

            _context.LikeBlog.Remove(likeBlog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeBlogExists(int id)
        {
            return _context.LikeBlog.Any(e => e.Id == id);
        }
    }
}
