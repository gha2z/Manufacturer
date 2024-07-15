using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Auth;

public static class VerifyUser
{
    public class Query : IRequest<Result<bool>>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Query, Result<bool>>
    {



        public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Users
                .Include(x => x.Type)
                .Where(x => x.Name.ToLower().Trim() == request.Username.ToLower().Trim() &&
                    x.Password.ToLower().Trim() == request.Password.Trim().ToLower()).FirstOrDefaultAsync();

            return Result.Success(result!=null);

        }
    }
}

public class VerifyUserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/VerifyUser", async (LoginRequest request, ISender sender) =>
        {
            var query = request.Adapt<VerifyUser.Query>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Verify a login without authorized features returned",
            Summary = "Verify User",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Auth"
                }
            }
        });
    }
}
