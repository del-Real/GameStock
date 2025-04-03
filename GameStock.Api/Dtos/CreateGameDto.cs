using System.ComponentModel.DataAnnotations;

namespace GameStock.Api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Range(1, 100)] float Price,
    DateOnly ReleaseDate
);