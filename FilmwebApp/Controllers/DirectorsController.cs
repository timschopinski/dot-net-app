using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FilmwebApp.Contracts;
using FilmwebApp.Models;
using FilmwebApp.Models.DTOs;

namespace FilmwebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DirectorsController : ControllerBase
{
    private readonly IDirectorRepository _repository;

    public DirectorsController(IDirectorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<DirectorDto>>> GetAll()
    {
        var directors = await _repository.GetAllAsync();
        var dtos = directors.Select(d => new DirectorDto(
            d.Id, d.FirstName, d.LastName, d.BirthDate, d.Nationality));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<DirectorDto>> GetById(int id)
    {
        var director = await _repository.GetByIdAsync(id);
        if (director == null)
            return NotFound();

        var dto = new DirectorDto(
            director.Id, director.FirstName, director.LastName, 
            director.BirthDate, director.Nationality);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<DirectorDto>> Create(CreateDirectorDto dto)
    {
        var director = new Director
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate,
            Nationality = dto.Nationality
        };

        await _repository.CreateAsync(director);
        var result = new DirectorDto(
            director.Id, director.FirstName, director.LastName,
            director.BirthDate, director.Nationality);
        return CreatedAtAction(nameof(GetById), new { id = director.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DirectorDto>> Update(int id, UpdateDirectorDto dto)
    {
        var director = await _repository.GetByIdAsync(id);
        if (director == null)
            return NotFound();

        director.FirstName = dto.FirstName;
        director.LastName = dto.LastName;
        director.BirthDate = dto.BirthDate;
        director.Nationality = dto.Nationality;

        await _repository.UpdateAsync(director);
        var result = new DirectorDto(
            director.Id, director.FirstName, director.LastName,
            director.BirthDate, director.Nationality);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}