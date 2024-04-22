using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddDbContext<IntrManDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    o.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(assembly));

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
