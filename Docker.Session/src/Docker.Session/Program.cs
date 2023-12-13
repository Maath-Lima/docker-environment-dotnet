using Docker.Session.API.Setup;
using Docker.Session.Data;
using Docker.Session.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.PostgreConfigureDbContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/categorias", async (AppDbContext db) => await db.Categorias
                                                            .Include(p => p.Produtos)
                                                            .ToListAsync());

app.MapGet("api/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync());

app.MapGet("api/produtos/{id:int}", async (int id, AppDbContext db) =>
{
    return await db.Produtos.FindAsync(id)
            is Produto produto
                ? Results.Ok(produto)
                : Results.NotFound();
});

app.Run();
