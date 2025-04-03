using System;
using GameStock.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStock.Api.Data;

public class GameStockContext(DbContextOptions<GameStockContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();


}
