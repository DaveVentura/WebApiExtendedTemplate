using AutoMapper;
using DaveVentura.WebApiExtendedTemplate.Constants;
using DaveVentura.WebApiExtendedTemplate.Contracts.Requests;
using DaveVentura.WebApiExtendedTemplate.Contracts.Responses;
using DaveVentura.WebApiExtendedTemplate.Domain.Documents;
using DaveVentura.WebApiExtendedTemplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaveVentura.WebApiExtendedTemplate.Controllers {
    [ApiController]
    [Route(ApiRoutes.Posts.ROUTE)]
    public class PostController : CommonController {
        private readonly PostService _postService;

        public PostController(PostService postService, IMapper mapper) : base(mapper) {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest, CancellationToken cancellationToken) {
            var post = Mapper.Map<Post>(postRequest);
            await _postService.CreatePostAsync(post, cancellationToken);
            return base.Created($"api/posts/{post.Id}", Mapper.Map<PostResponse>(post));
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(CancellationToken cancellationToken) {
            var posts = await _postService.GetAllPostsAsync(cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PostResponse>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id, CancellationToken cancellationToken) {
            var post = await _postService.GetPostByIdAsync(id, cancellationToken);
            return base.Ok(Mapper.Map<PostResponse>(post));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostRequest postRequest, CancellationToken cancellationToken) {
            var post = Mapper.Map<Post>(postRequest);
            await _postService.UpdatePostAsync(id, post, cancellationToken);
            return base.Ok(Mapper.Map<PostResponse>(post));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id, CancellationToken cancellationToken) {
            await _postService.DeletePostAsync(id, cancellationToken);
            return base.Ok();
        }
    }
}
