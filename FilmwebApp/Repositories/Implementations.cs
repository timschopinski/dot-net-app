using Microsoft.EntityFrameworkCore;
using FilmwebApp.Contracts;
using FilmwebApp.Data;
using FilmwebApp.Models;

namespace FilmwebApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsAsync(string username, string email) =>
        await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
}

public class DirectorRepository : IDirectorRepository
{
    private readonly AppDbContext _context;

    public DirectorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Director>> GetAllAsync() =>
        await _context.Directors.Include(d => d.Movies).ToListAsync();

    public async Task<Director?> GetByIdAsync(int id) =>
        await _context.Directors.FindAsync(id);

    public async Task<Director?> GetByIdWithMoviesAsync(int id) =>
        await _context.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Director> CreateAsync(Director director)
    {
        _context.Directors.Add(director);
        await _context.SaveChangesAsync();
        return director;
    }

    public async Task<Director> UpdateAsync(Director director)
    {
        _context.Directors.Update(director);
        await _context.SaveChangesAsync();
        return director;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var director = await GetByIdAsync(id);
        if (director == null) return false;
        
        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Directors.AnyAsync(d => d.Id == id);
}

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync() =>
        await _context.Movies.Include(m => m.Director).ToListAsync();

    public async Task<Movie?> GetByIdAsync(int id) =>
        await _context.Movies.Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Movie>> GetByDirectorIdAsync(int directorId) =>
        await _context.Movies.Include(m => m.Director)
            .Where(m => m.DirectorId == directorId).ToListAsync();

    public async Task<Movie> CreateAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(movie.Id) ?? movie;
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(movie.Id) ?? movie;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;
        
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Movies.AnyAsync(m => m.Id == id);
}