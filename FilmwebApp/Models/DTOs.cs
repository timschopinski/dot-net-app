namespace FilmwebApp.Models.DTOs;

public record RegisterDto(string Username, string Email, string Password);
public record LoginDto(string Username, string Password);
public record AuthResponseDto(string Token, string Username);

public record DirectorDto(int Id, string FirstName, string LastName, DateTime? BirthDate, string? Nationality);
public record CreateDirectorDto(string FirstName, string LastName, DateTime? BirthDate, string? Nationality);
public record UpdateDirectorDto(string FirstName, string LastName, DateTime? BirthDate, string? Nationality);

public record MovieDto(int Id, string Title, string? Description, int ReleaseYear, string? Genre, int DirectorId, string DirectorName);
public record CreateMovieDto(string Title, string? Description, int ReleaseYear, string? Genre, int DirectorId);
public record UpdateMovieDto(string Title, string? Description, int ReleaseYear, string? Genre, int DirectorId);