using System.ComponentModel.DataAnnotations;

namespace GameStock.Api.Dtos;

public record class UpdateGameDto(
    string Name,
    int GenreId,
    [Range(1, 100)] float Price,
    DateOnly ReleaseDate
);
