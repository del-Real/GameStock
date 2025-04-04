namespace GameStock.Api.Dtos;

public record class GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    float Price,
    DateOnly ReleaseDate);
