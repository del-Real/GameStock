using GameStock.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
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

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

app.Run();
