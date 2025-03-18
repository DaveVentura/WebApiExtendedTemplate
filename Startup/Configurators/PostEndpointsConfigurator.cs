using AutoMapper;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Documents;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class PostEndpointsConfigurator : IAppConfigurator {
        public int Order => 5001;

        public void ConfigureApp(WebApplication app) {
            var create = app.MapPost(ApiRoutes.Posts.ROUTE,
                async (
                    PostRequest postRequest,
                    PostService postService,
                    UriService uriService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var post = mapper.Map<Post>(postRequest);
                        await postService.CreatePostAsync(post, cancellationToken);
                        return Results.Created(
                            uriService.GetUri($"{ApiRoutes.Posts.ROUTE}/{post.Id}"),
                            mapper.Map<PostResponse>(post));
                    });

            var getAll = app.MapGet(ApiRoutes.Posts.ROUTE,
                async (
                    PostService postService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var posts = await postService.GetAllPostsAsync(cancellationToken);
                        return Results.Ok(mapper.Map<IEnumerable<PostResponse>>(posts));
                    });

            var getById = app.MapGet(ApiRoutes.Posts.ROUTE_BY_ID,
                async (
                    string id,
                    PostService postService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var post = await postService.GetPostByIdAsync(id, cancellationToken);
                        return Results.Ok(mapper.Map<PostResponse>(post));
                    });

            var update = app.MapPut(ApiRoutes.Posts.ROUTE_BY_ID,
                async (
                    string id,
                    PostRequest postRequest,
                    PostService postService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var post = mapper.Map<Post>(postRequest);
                        await postService.UpdatePostAsync(id, post, cancellationToken);
                        return Results.Ok(mapper.Map<PostResponse>(post));
                    });

            var delete = app.MapDelete(ApiRoutes.Posts.ROUTE_BY_ID,
                async (
                    string id,
                    PostService postService,
                    CancellationToken cancellationToken) => {
                        await postService.DeletePostAsync(id, cancellationToken);
                        return Results.Ok();
                    });

            //#if(UseAuth)
            create.RequireAuthorization();
            getAll.RequireAuthorization();
            getById.RequireAuthorization();
            update.RequireAuthorization();
            delete.RequireAuthorization();
            //#endif
        }
    }
}
