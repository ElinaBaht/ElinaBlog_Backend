using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostBackend.Data;
using PostBackend.Models;

namespace PostBackend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
       
        private readonly DataContext dataContext;

        public PostsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> Get()
        {

            return Ok(await dataContext.Posts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> Get(int id)
        {
            var post = await dataContext.Posts.FindAsync(id);
            if (post == null)
                return BadRequest("Post not found");
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<List<PostModel>>> AddPost(PostModel post)
        {
            dataContext.Posts.Add(post);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Posts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<PostModel>>> UpdatePost(PostModel request)
        {
            var post = await dataContext.Posts.FindAsync(request.Id);
            if (post == null)
                return BadRequest("Post not found");
            
            post.Title = request.Title;
            post.Description = request.Description;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Posts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PostModel>>> Delete(int id)
        {
            var post = await dataContext.Posts.FindAsync(id);
            if (post == null)
                return BadRequest("Post not found");


            dataContext.Posts.Remove(post);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Posts.ToListAsync());
        }
    }
}
