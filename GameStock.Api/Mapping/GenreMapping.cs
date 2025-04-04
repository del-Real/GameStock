using System;
using GameStock.Api.Dtos;
using GameStock.Api.Entities;

namespace GameStock.Api.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
