namespace GameStock.Api.Dtos;

public record class GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    float Price,
    DateOnly ReleaseDate);
