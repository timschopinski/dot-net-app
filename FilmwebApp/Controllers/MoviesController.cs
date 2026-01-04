using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FilmwebApp.Contracts;
using FilmwebApp.Models;
using FilmwebApp.Models.DTOs;

namespace FilmwebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;
    private readonly IDirectorRepository _directorRepository;

    public MoviesController(IMovieRepository movieRepository, IDirectorRepository directorRepository)
    {
        _movieRepository = movieRepository;
        _directorRepository = directorRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
    {
        var movies = await _movieRepository.GetAllAsync();
        var dtos = movies.Select(m => new MovieDto(
            m.Id, m.Title, m.Description, m.ReleaseYear, m.Genre,
            m.DirectorId, $"{m.Director.FirstName} {m.Director.LastName}"));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<MovieDto>> GetById(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
            return NotFound();

        var dto = new MovieDto(
            movie.Id, movie.Title, movie.Description, movie.ReleaseYear,
            movie.Genre, movie.DirectorId, 
            $"{movie.Director.FirstName} {movie.Director.LastName}");
        return Ok(dto);
    }

    [HttpGet("by-director/{directorId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetByDirector(int directorId)
    {
        var movies = await _movieRepository.GetByDirectorIdAsync(directorId);
        var dtos = movies.Select(m => new MovieDto(
            m.Id, m.Title, m.Description, m.ReleaseYear, m.Genre,
            m.DirectorId, $"{m.Director.FirstName} {m.Director.LastName}"));
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> Create(CreateMovieDto dto)
    {
        if (!await _directorRepository.ExistsAsync(dto.DirectorId))
            return BadRequest(new { message = "Director not found" });

        var movie = new Movie
        {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseYear = dto.ReleaseYear,
            Genre = dto.Genre,
            DirectorId = dto.DirectorId
        };

        await _movieRepository.CreateAsync(movie);
        var result = new MovieDto(
            movie.Id, movie.Title, movie.Description, movie.ReleaseYear,
            movie.Genre, movie.DirectorId,
            $"{movie.Director.FirstName} {movie.Director.LastName}");
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieDto>> Update(int id, UpdateMovieDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
            return NotFound();

        if (!await _directorRepository.ExistsAsync(dto.DirectorId))
            return BadRequest(new { message = "Director not found" });

        movie.Title = dto.Title;
        movie.Description = dto.Description;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Genre = dto.Genre;
        movie.DirectorId = dto.DirectorId;

        await _movieRepository.UpdateAsync(movie);
        var result = new MovieDto(
            movie.Id, movie.Title, movie.Description, movie.ReleaseYear,
            movie.Genre, movie.DirectorId,
            $"{movie.Director.FirstName} {movie.Director.LastName}");
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _movieRepository.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}