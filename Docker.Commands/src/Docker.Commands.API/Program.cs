using Docker.Commands.Data;
using Docker.Commands.Models;
using Docker.Commands.API.Setup;

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

app.MapPost("api/categorias/", async (Categoria categoria, AppDbContext db) =>
{
    db.Categorias.Add(categoria);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("api/produtos/", async (Produto produto, AppDbContext db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/produtos/{id:int}", async (int id, AppDbContext db) =>
{

    var produto = await db.Produtos.FindAsync(id);

    if (produto is not null)
    {
        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
    }

    return Results.NoContent();
});

app.Run();
