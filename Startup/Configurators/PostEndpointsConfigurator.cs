using DaveVentura.WebApiExtendedTemplate.Domain.Documents;
using DaveVentura.WebApiExtendedTemplate.Middlewares;
using DaveVentura.WebApiExtendedTemplate.Services;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Configurators;

public class PostEndpointsConfigurator : IAppConfigurator {
    public int Order => 5001;

    public void ConfigureApp(WebApplication app) {
        app.MapGet("/api/posts", async (PostService postService, CancellationToken cancellationToken) =>
            Results.Ok(await postService.GetAllPostsAsync(cancellationToken)));

        app.MapGet("/api/posts/{id}", async (PostService postService, string id, CancellationToken cancellationToken) => {
            var post = await postService.GetPostByIdAsync(id, cancellationToken);
            return Results.Ok(post);
        });

        app.MapPost("/api/posts", async (PostService postService, Post post, CancellationToken cancellationToken) => {
            await postService.CreatePostAsync(post, cancellationToken);
            return Results.Created($"/api/posts/{post.Id}", post);
        });

        app.MapPut("/api/posts/{id}", async (PostService postService, string id, Post post, CancellationToken cancellationToken) => {
            await postService.UpdatePostAsync(id, post, cancellationToken);
            return Results.Ok(post);
        });

        app.MapDelete("/api/posts/{id}", async (PostService postService, string id, CancellationToken cancellationToken) => {
            await postService.DeletePostAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
