using System;
using GameStock.Api.Data;
using GameStock.Api.Dtos;
using GameStock.Api.Entities;
using GameStock.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStock.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    /*
    private static readonly List<GameSummaryDto> games = [
        new (1, "Street Fighter III", "Fighting", 19.99f, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy XIV", "JRPG", 59.99f, new DateOnly(2010, 9, 30)),
        new (3, "The Legend of Zelda: Ocarina of Time", "Adventure", 39.99f, new DateOnly(1998, 11, 21)),
        new (4, "Super Mario 64", "Platforms", 29.99f, new DateOnly(1996, 6, 23)),
        new (5, "Dark Souls", "Action RPG", 49.99f, new DateOnly(2011, 9, 22)),
        new (6, "Halo: Combat Evolved", "FPS", 39.99f, new DateOnly(2001, 11, 15)),
        new (7, "God of War", "Action", 59.99f, new DateOnly(2018, 4, 20)),
        new (8, "Minecraft", "Sandbox", 26.95f, new DateOnly(2011, 11, 18)),
        new (9, "Cyberpunk 2077", "Open-world RPG", 69.99f, new DateOnly(2020, 12, 10)),
        new (10, "The Witcher 3: Wild Hunt", "Open-world RPG", 49.99f, new DateOnly(2015, 5, 19))
    ];
    */

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", async (GameStockContext dbContext) =>
            await dbContext.Games
                     .Include(game => game.Genre)
                     .Select(game => game.ToGameSummaryDto())
                     .AsNoTracking()
                     .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStockContext dbContext) =>
        {
            // Check if game exists
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStockContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGameEndPointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStockContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            // Check if index exists
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.Ok($"Game with ID {id} has been updated successfully.");
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStockContext dbContext) =>
        {
            await dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.Ok($"Game with ID {id} has been deleted successfully.");
        });
        return group;
    }
}
