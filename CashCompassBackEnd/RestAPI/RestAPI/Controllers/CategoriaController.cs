using Microsoft.EntityFrameworkCore;

namespace RestAPI.Controllers;


using Microsoft.AspNetCore.Mvc;

using Context;
using Models;


[ApiController]
[Route("api/v1/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get([FromRoute] int id)
    {
        try
        {
            var categoria = await _context
                .Categoria
                .FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound($"A categoria com o id {id} não existe !");

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetAllCategorias()
    {
        try
        {
            var categorias = _context
                .Categoria
                .ToListAsync();

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(Categoria categoria)
    {
        try
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Categoria>> Update([FromRoute] int id, [FromBody] String name)
    {
        var categoria = await _context
            .Categoria
            .FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (categoria is null)
            return NotFound($"Não foi encontrado nenhuma categoria com o id ${id}");

        categoria.Nome = name;

        _context.Update(categoria);

        await _context.SaveChangesAsync();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsyncById([FromRoute] int id)
    {
        var categoria = await _context
            .Categoria
            .FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (categoria is null)
            return NotFound($"Nenhuma categoria com o id {id} foi encontrada !");

        _context.Categoria.Remove(categoria);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

