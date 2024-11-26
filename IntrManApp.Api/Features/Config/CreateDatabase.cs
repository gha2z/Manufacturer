using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Serilog;
using System.Data;
using System.Security;


namespace IntrManApp.Api.Config;

public static class CreateDatabase
{
    public class Command : IRequest<Result<bool>>
    {
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory, Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        private readonly Gha2zErpDbContext _context = dbContext;

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();


                using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

                var sps = Path.Combine(AppContext.BaseDirectory, "SPs.sql");
                string queryString = string.Empty;


                if (!File.Exists(sps))
                {
                    Log.Logger.Information($"Copying {sps}");
                    File.WriteAllText(sps, File.ReadAllText(Path.Combine("SPs.sql")));
                }
                if (File.Exists(sps))
                {
                    queryString = File.ReadAllText(sps);
                    var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var query in queries)
                    {
                        await connection.ExecuteAsync(query);
                    }

                }

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(new Error("CreateDatabase.Execution", $"{ex.Message}\n\n{ex}"));
            }
        }
    }
}

public class CreateDatabaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/CreateDatabase", async (ISender sender) =>
        {
            var command = new CreateDatabase.Command();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Drops (if any) and creates the database",
            Summary = "Create Database",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Config"
                }
            }
        });
    }
}
