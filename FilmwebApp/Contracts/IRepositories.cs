using FilmwebApp.Models;

namespace FilmwebApp.Contracts;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user);
    Task<bool> ExistsAsync(string username, string email);
}

public interface IDirectorRepository
{
    Task<IEnumerable<Director>> GetAllAsync();
    Task<Director?> GetByIdAsync(int id);
    Task<Director?> GetByIdWithMoviesAsync(int id);
    Task<Director> CreateAsync(Director director);
    Task<Director> UpdateAsync(Director director);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task<IEnumerable<Movie>> GetByDirectorIdAsync(int directorId);
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}