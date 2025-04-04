using System;
using GameStock.Api.Data;
using GameStock.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStock.Api.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");
        group.MapGet("/", async (GameStockContext dbContext) =>
            await dbContext.Genres
                           .Select(genre => genre.ToDto())
                           .AsNoTracking()
                           .ToListAsync());

        return group;
    }
}
