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
    public class CommentsController : ControllerBase
    {
        private readonly SoftwareStudioBlogContext _context;

        public CommentsController(SoftwareStudioBlogContext context)
        {
            _context = context;
        }


        // ********** Comments Getter **********

        // GET: api/Comments
        // See all comments in the blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comments = (from x in _context.Comment where x.IsHidden == "False" select x).ToListAsync();

            return await comments;
        }

        // GET: api/Comments/5
        // See specific comment
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                if (comment.IsHidden == "False")
                {
                    return comment;
                }
                else
                {
                    return BadRequest($"This comment {id} is Hidden.");
                }
            }
        }

        // GET: api/Comments/MyComment/{userId}
        // Can see all my comments
        [HttpGet("MyComment/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> MyGetComments(int id)
        {
            var user = await _context.User.FindAsync(id);
            var comments = (from x in _context.Comment where x.IsHidden == "False" && x.Username == user.Username select x).ToListAsync();

            return await comments;
        }

        // GET: api/Comments/Admin
        // (Admin) Can see all comments in the blogs
        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<Comment>>> AdminGetComments()
        {
            return await _context.Comment.ToListAsync();
        }

        // GET: api/Comments/Blog/{BlogId}
        // Can see all comment in the blogs by Blogs ID
        [HttpGet("Blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByBlogId(int blogId)
        {
            var comments = (from x in _context.Comment where x.BlogId == blogId && x.IsHidden == "False" select x).ToListAsync();
            
            return await comments;
        }


        // ********** Update Comments ***********

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (!CommentExists(id) ||id != comment.Id)
            {
                return BadRequest("This Comment does not exit or CommentId didn't match.");
            }
            else
            {
                _context.Entry(comment).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok($"Your Comment {id} have been Edited.");
            }
        }

        // PUT: api/Comments/hide/{commentId}
        // Hide comments by Admin

        [HttpPut("hide/{id}")]
        public async Task<IActionResult> HideComment(int id, User u)
        {
            if(!CommentExists(id))
            {
                return BadRequest("This Comment is does not exist.");
            }
            else
            {
                if(u.IsAdmin == "True")
                {
                    var comment = await _context.Comment.FindAsync(id);
                    comment.IsHidden = "True";
                    _context.Entry(comment).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CommentExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok($"Comment {id} is now Hidden.");
                }
                else
                {
                    return BadRequest("You don't have permission to this Action");
                }
            }

        }

        // PUT: api/Comments/unhidden/{commentId}
        // Unhidden comments by Admin
        [HttpPut("unhidden/{id}")]
        public async Task<IActionResult> UnhiddenComment(int id, User u)
        {
            if (!CommentExists(id))
            {
                return BadRequest("This Comment is does not exist.");
            }
            else
            {
                if (u.IsAdmin == "True")
                {
                    var comment = await _context.Comment.FindAsync(id);
                    comment.IsHidden = "False";
                    _context.Entry(comment).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CommentExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok($"Comment {id} is now Show.");
                }
                else
                {
                    return BadRequest("You don't have permission to this Action");
                }
            }
        }


        // ********** Add Comments ***********

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            var user = (from x in _context.User where x.Username == comment.Username select x).SingleOrDefault();

            //if (true)
            if (user.IsBan == "False" || user.IsAdmin == "True")
            {
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComment", new { id = comment.Id }, comment);  
            }
            else
            {
                return BadRequest("You don't have permission to this action, because you are banned.");
            }
        }


        // ********** Delete Comments ***********

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id, User u)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                if (comment.Username == u.Username || u.IsAdmin == "True")
                {
                    _context.Comment.Remove(comment);
                    await _context.SaveChangesAsync();

                    return Ok("Remove comment complete.");  
                }
                else
                {
                    return BadRequest("You don't have any permission to this comment.");
                }
            }
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
