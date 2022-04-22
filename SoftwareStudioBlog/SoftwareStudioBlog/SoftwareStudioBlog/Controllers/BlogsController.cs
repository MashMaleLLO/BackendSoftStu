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
using SoftwareStudioBlog.Display;
using Newtonsoft.Json.Linq;

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

        // GET: api/Blogs/Display
        [HttpGet("Display")]
        public async Task<ActionResult<IEnumerable<DisplayBlog>>> GetDisplay()
        {
            var blogs = (from x in _context.Blog where x.IsHidden == "False" select x).ToList();

            List<DisplayBlog> dps = new List<DisplayBlog>();

            foreach (var blog in blogs)
            {
                List<DisplayComment> dcm = new List<DisplayComment>();
                var comment = (from x in _context.Comment where x.BlogId == blog.Id && x.IsHidden == "False" select x).ToList();
                var likeBlog = (from x in _context.LikeBlog where x.BlogId == blog.Id select x).ToList();

                foreach (var com in comment)
                {
                    var likeCom = (from x in _context.LikeComment where x.CommentId == com.Id select x).ToList();
                    DisplayComment cm = new DisplayComment
                    {
                        Id = com.Id,
                        Username = com.Username,
                        BlogId = com.BlogId,
                        Message = com.Message,
                        IsHidden = com.IsHidden,
                        Date = com.CreatedDate,
                        Likes = likeCom.Count
                    };

                    dcm.Add(cm);
                }

                DisplayBlog dp = new DisplayBlog
                {
                    Id = blog.Id,
                    Username = blog.Username,
                    Tag = blog.Tag,
                    Img = blog.Image,
                    Detail = blog.Detail,
                    IsHidden = blog.IsHidden,
                    Date = blog.CreatedDate
                };

                dp.Comments.AddRange(dcm);
                dp.Likes = likeBlog.Count;

                dps.Add(dp);
            }

            return dps;
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

        //GET: api/Blogs/Display/{blogId}
        //Display Specific blog
        [HttpGet("Display/{blogId}")]
        public async Task<ActionResult<DisplayBlog>> DisplayIdBlog(int blogId)
        {
            var blog = (from x in _context.Blog where x.Id == blogId && x.IsHidden == "False" select x).SingleOrDefault();
            if (blog == null)
            {
                return NotFound("This blog doesn't exist Or This blog is Hiddened by Admin.");
            }
            else
            {
                var comments = (from x in _context.Comment where x.BlogId == blogId select x).ToList();
                var likeBlogs = (from x in _context.LikeBlog where x.BlogId == blogId select x).ToList();
                DisplayBlog db = new DisplayBlog();
                List<DisplayComment> dcms = new List<DisplayComment>();
                foreach (var comment in comments)
                {
                    var likeCom = (from x in _context.LikeComment where x.CommentId == comment.Id select x).ToList();
                    DisplayComment cm = new DisplayComment
                    {
                        Id = comment.Id,
                        Username = comment.Username,
                        BlogId = blogId,
                        Message = comment.Message,
                        IsHidden = comment.IsHidden,
                        Date = comment.CreatedDate,
                        Likes = likeCom.Count
                    };
                    dcms.Add(cm);
                }
                db.Id = blogId;
                db.Username = blog.Username;
                db.Tag = blog.Tag;
                db.Img = blog.Image;
                db.Detail = blog.Detail;
                db.IsHidden = blog.IsHidden;
                db.Date = blog.CreatedDate;
                db.Comments = dcms;
                db.Likes = likeBlogs.Count;
                return db;
            }
        }

        // GET: api/Blogs/MyBlog/{userId}
        // See all my Blog
        [HttpGet("MyBlog/{userId}")]
        public async Task<ActionResult<IEnumerable<Blog>>> MyGetBlogs(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("You did not register yet or this user is doesn't exist.");
            }
            var blogs = (from x in _context.Blog where x.IsHidden == "False" && x.Username == user.Username select x).ToListAsync();
            
            return await blogs;
        }

        // GET : api/Blogs/Display/MyBlog/{userId}
        [HttpGet("Display/MyBlog/{userId}")]
        public async Task<ActionResult<IEnumerable<DisplayBlog>>> DisplayMyBlogs(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            List<DisplayBlog> dps = new List<DisplayBlog>();
            if (user == null)
            {
                return BadRequest("You did not register yet or this user is doesn't exist.");
            }
            var blogs = (from x in _context.Blog where x.IsHidden == "False" && x.Username == user.Username select x).ToListAsync();
            foreach (var blog in await blogs)
            {
                List<DisplayComment> dcm = new List<DisplayComment>();
                var comment = (from x in _context.Comment where x.BlogId == blog.Id && x.IsHidden == "False" select x).ToList();
                var likeBlog = (from x in _context.LikeBlog where x.BlogId == blog.Id select x).ToList();

                foreach (var com in comment)
                {
                    var likeCom = (from x in _context.LikeComment where x.CommentId == com.Id select x).ToList();
                    DisplayComment cm = new DisplayComment
                    {
                        Id = com.Id,
                        Username = com.Username,
                        BlogId = com.BlogId,
                        Message = com.Message,
                        IsHidden = com.IsHidden,
                        Date = com.CreatedDate,
                        Likes = likeCom.Count
                    };

                    dcm.Add(cm);
                }

                DisplayBlog dp = new DisplayBlog
                {
                    Id = blog.Id,
                    Username = blog.Username,
                    Tag = blog.Tag,
                    Img = blog.Image,
                    Detail = blog.Detail,
                    IsHidden = blog.IsHidden,
                    Date = blog.CreatedDate
                };

                dp.Comments.AddRange(dcm);
                dp.Likes = likeBlog.Count;

                dps.Add(dp);
            }

            return dps;
        }

        // GET : api/Blogs/Admin
        // (Admin) See all blog include Hidden blogs
        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<Blog>>> AdminGetBlogs()
        {
            return await _context.Blog.ToListAsync();
        }


        // GET : api/Blogs/Display/Admin
        [HttpGet("Display/Admin")]
        public async Task<ActionResult<IEnumerable<DisplayBlog>>> AdminGetDisplay(User u)
        {
            if(u.IsAdmin == "True")
            { 
                var blogs = (from x in _context.Blog select x).ToList();

                List<DisplayBlog> dps = new List<DisplayBlog>();

                foreach (var blog in blogs)
                {
                    List<DisplayComment> dcm = new List<DisplayComment>();
                    var comment = (from x in _context.Comment where x.BlogId == blog.Id select x).ToList();
                    var likeBlog = (from x in _context.LikeBlog where x.BlogId == blog.Id select x).ToList();

                    foreach (var com in comment)
                    {
                        var likeCom = (from x in _context.LikeComment where x.CommentId == com.Id select x).ToList();
                        DisplayComment cm = new DisplayComment
                        {
                            Id = com.Id,
                            Username = com.Username,
                            BlogId = com.BlogId,
                            Message = com.Message,
                            IsHidden = com.IsHidden,
                            Date = com.CreatedDate,
                            Likes = likeCom.Count
                        };

                        dcm.Add(cm);
                    }

                    DisplayBlog dp = new DisplayBlog
                    {
                        Id = blog.Id,
                        Username = blog.Username,
                        Tag = blog.Tag,
                        Img = blog.Image,
                        Detail = blog.Detail,
                        IsHidden = blog.IsHidden,
                        Date = blog.CreatedDate
                    };

                    dp.Comments.AddRange(dcm);
                    dp.Likes = likeBlog.Count;

                    dps.Add(dp);
                }

                return dps;
            }
            else
            {
                return BadRequest("You don't have permission with this action");
            }
        }


        /// ************* update blogs *************

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            //string blog = data[0];
            //Blog blog = data["blogData"].ToObject<Blog>();
            // string username = data["username"].ToString();

            if (!BlogExists(id))
            {
                return BadRequest("This Blog does not exit or blogId didn't match.");
            }
            else
            {
                var b = await _context.Blog.FindAsync(id);
                if (b.Username == blog.Username)
                {
                    b.Tag = blog.Tag;
                    b.Image = blog.Image;
                    b.Detail = blog.Detail;
                    b.IsHidden = blog.IsHidden;
                    b.CreatedDate = DateTime.Now;

                    _context.Entry(b).State = EntityState.Modified;

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
                else
                {
                    return BadRequest("You don't have permisson to this blog.");
                }
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
/*                DateTime dt = DateTime.Now;
                string stringDateTime = dt.ToString("F");
                DateTime createdTime = Convert.ToDateTime(stringDateTime);
                blog.CreatedDate = createdTime;*/

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
                return NotFound("This blog is doesn't exist");
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
