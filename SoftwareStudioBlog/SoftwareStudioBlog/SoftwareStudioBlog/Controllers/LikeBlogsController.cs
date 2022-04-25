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

        // ********** Like Get ***********

        // GET: api/LikeBlogs
        // Get all LikeBlogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeBlog>>> GetLikeBlog()
        {
            return await _context.LikeBlog.ToListAsync();
        }

        // GET: api/LikeBlogs/5
        // Get only one
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

        // GET: api/LikeBlogs/getLikeBlog/{blogId}
        // Get like by blogID
        [HttpGet("getLikeBlog/{blogId}")]
        public async Task<ActionResult<List<int>>> GetLikeByBlogId(int blogId)
        {
            List<int> likes = new List<int>();
            var likeBlogs = await (from x in _context.LikeBlog where x.BlogId == blogId select x).ToListAsync();
            foreach (var likeBlog in likeBlogs)
            {
                likes.Add(likeBlog.UserId);
            }

            return likes;
        }

        // ******** update like ********

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



        // ********** add like ***********

        // like and unlike here

        // POST: api/LikeBlogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikeBlog>> PostLikeBlog(LikeBlog likeBlog)
        {
            var blog = await _context.Blog.FindAsync(likeBlog.BlogId);
            var like = (from x in _context.LikeBlog where x.UserId == likeBlog.UserId && x.BlogId == likeBlog.BlogId select x).SingleOrDefault();

            if (like != null)
            {
                string printResult = $"User {like.UserId} unlike blog {like.BlogId}";

                _context.LikeBlog.Remove(like);
                await _context.SaveChangesAsync();

                return Ok(printResult);
            }
            else
            {
                _context.LikeBlog.Add(likeBlog);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLikeBlog", new { id = likeBlog.Id }, likeBlog);
            }

        }


        // ********** delete like ***********

        // DELETE: api/LikeBlogs/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteLikeBlog(int userId, Blog blog)
        {
            var likeBlog = (from x in _context.LikeBlog where x.UserId == userId && x.BlogId == blog.Id select x).SingleOrDefault();
            if (likeBlog == null)
            {
                return NotFound();
            }
            else
            {
                _context.LikeBlog.Remove(likeBlog);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        private bool LikeBlogExists(int id)
        {
            return _context.LikeBlog.Any(e => e.Id == id);
        }
    }
}
