﻿using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Auth;

public static class Login
{
    public class Query : IRequest<Result<LoginResponse>>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Query, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Users
                .Include(x => x.Type)
                .Where(x => x.Name.ToLower().Trim() == request.Username.ToLower().Trim() &&
                    x.Password.ToLower().Trim() == request.Password.Trim().ToLower()).FirstOrDefaultAsync();

            var ret = new LoginResponse();
            if (result != null)
            {
                ret.Token = Guid.NewGuid();
                ret.UserId = result.Id;
                ret.Username = result.Name ?? string.Empty;
                ret.Role = result.Type?.Name ?? string.Empty;
                ret.FeatureAccesses = await dbContext.UserTypeFeatures
                    .Include(x => x.Feature)
                    .Where(y => y.UserTypeId == result.TypeId && (y.Feature.ParentId==null || y.Feature.ParentId==Guid.Empty))
                    .Select(x => new FeatureAccessResponse
                    {
                         Id = x.FeatureId,
                         Name = x.Feature.Name ?? string.Empty,
                         CanView = x.Accessible ?? false,
                         Icon = x.Feature.Icon ?? string.Empty,
                         Path = x.Feature.Path ?? string.Empty
                    }).ToListAsync();
                
                foreach(var feature in ret.FeatureAccesses)
                {
                    feature.ChildrenFeatures = await dbContext.UserTypeFeatures
                        .Include(x => x.Feature)
                        .Where(y => y.UserTypeId == result.TypeId && y.Feature.ParentId == feature.Id)
                        .Select(x => new FeatureAccessResponse
                        {
                            Id = x.FeatureId,
                            Name = x.Feature.Name ?? string.Empty,
                            CanView = x.Accessible ?? false,
                            Icon = x.Feature.Icon ?? string.Empty,
                            Path = x.Feature.Path ?? string.Empty
                        }).ToListAsync();
                }
            } else
            {
                var allFeatures = await dbContext.Features.ToListAsync(cancellationToken);
                ret.FeatureAccesses = allFeatures.Adapt<List<FeatureAccessResponse>>();
            }
            return Result.Success(ret);
           
        }
    }
}

public class LoginEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/login", async (LoginRequest request, ISender sender) =>
        {
            var query = request.Adapt<Login.Query>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "It is clear enough",
            Summary = "User login",
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
