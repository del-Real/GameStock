using GameStock.Api.Data;
using GameStock.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStock");
builder.Services.AddSqlite<GameStockContext>(connString);

var app = builder.Build();
app.MapGamesEndPoints();
app.MigrateDb();

app.Run();
