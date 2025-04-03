using System;
using GameStock.Api.Data;
using GameStock.Api.Dtos;
using GameStock.Api.Entities;

namespace GameStock.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter III", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy XIV", "JRPG", 59.99M, new DateOnly(2010, 9, 30)),
        new (3, "The Legend of Zelda: Ocarina of Time", "Adventure", 39.99M, new DateOnly(1998, 11, 21)),
        new (4, "Super Mario 64", "Platforms", 29.99M, new DateOnly(1996, 6, 23)),
        new (5, "Dark Souls", "Action RPG", 49.99M, new DateOnly(2011, 9, 22)),
        new (6, "Halo: Combat Evolved", "FPS", 39.99M, new DateOnly(2001, 11, 15)),
        new (7, "God of War", "Action", 59.99M, new DateOnly(2018, 4, 20)),
        new (8, "Minecraft", "Sandbox", 26.95M, new DateOnly(2011, 11, 18)),
        new (9, "Cyberpunk 2077", "Open-world RPG", 69.99M, new DateOnly(2020, 12, 10)),
        new (10, "The Witcher 3: Wild Hunt", "Open-world RPG", 49.99M, new DateOnly(2015, 5, 19))
    ];

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            // Check if game exists
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStockContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        })
        .WithParameterValidation();

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            // Check if index exists
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.Ok($"Game with ID {id} has been updated successfully.");
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.Ok($"Game with ID {id} has been deleted successfully.");
        });
        return group;
    }
}
