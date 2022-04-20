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
    public class LikeCommentsController : ControllerBase
    {
        private readonly SoftwareStudioBlogContext _context;

        public LikeCommentsController(SoftwareStudioBlogContext context)
        {
            _context = context;
        }

        // GET: api/LikeComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeComment>>> GetLikeComment()
        {
            return await _context.LikeComment.ToListAsync();
        }

        // GET: api/LikeComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeComment>> GetLikeComment(int id)
        {
            var likeComment = await _context.LikeComment.FindAsync(id);

            if (likeComment == null)
            {
                return NotFound();
            }

            return likeComment;
        }

        // PUT: api/LikeComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikeComment(int id, LikeComment likeComment)
        {
            if (id != likeComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(likeComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeCommentExists(id))
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

        // POST: api/LikeComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikeComment>> PostLikeComment(LikeComment likeComment)
        {
            _context.LikeComment.Add(likeComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLikeComment", new { id = likeComment.Id }, likeComment);
        }

        // DELETE: api/LikeComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikeComment(int id)
        {
            var likeComment = await _context.LikeComment.FindAsync(id);
            if (likeComment == null)
            {
                return NotFound();
            }

            _context.LikeComment.Remove(likeComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeCommentExists(int id)
        {
            return _context.LikeComment.Any(e => e.Id == id);
        }
    }
}
