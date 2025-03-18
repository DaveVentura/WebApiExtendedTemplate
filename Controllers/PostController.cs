using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Documents;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Controllers {
    [ApiController]
    //#if(UseAuth)
    [Authorize]
    //#endif
    [Route(ApiRoutes.Posts.ROUTE)]
    public class PostController : CommonControllerBase {
        private readonly PostService _postService;

        public PostController(PostService postService, UriService uriService, IMapper mapper) : base(uriService, mapper) {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest, CancellationToken cancellationToken) {
            var post = Mapper.Map<Post>(postRequest);
            await _postService.CreatePostAsync(post, cancellationToken);
            return base.Created(
                UriService.GetUri($"{ApiRoutes.Posts.ROUTE}/{post.Id}"),
                Mapper.Map<PostResponse>(post)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(CancellationToken cancellationToken) {
            var posts = await _postService.GetAllPostsAsync(cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PostResponse>>(posts));
        }

        [HttpGet(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> GetPost(string id, CancellationToken cancellationToken) {
            var post = await _postService.GetPostByIdAsync(id, cancellationToken);
            return base.Ok(Mapper.Map<PostResponse>(post));
        }

        [HttpPut(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostRequest postRequest, CancellationToken cancellationToken) {
            var post = Mapper.Map<Post>(postRequest);
            await _postService.UpdatePostAsync(id, post, cancellationToken);
            return base.Ok(Mapper.Map<PostResponse>(post));
        }

        [HttpDelete(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> DeletePost(string id, CancellationToken cancellationToken) {
            await _postService.DeletePostAsync(id, cancellationToken);
            return base.Ok();
        }
    }
}
