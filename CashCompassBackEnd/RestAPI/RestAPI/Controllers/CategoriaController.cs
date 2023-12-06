using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace RestAPI.Controllers;

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

            if (categoria == null)
                return NotFound($"Categoria com id = {id} não encontrada...");

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAllCategorias()
    {
        try
        {
            var categorias = await _context
                .Categoria
                .AsNoTracking()
                .ToListAsync();

            if (categorias == null || !categorias.Any())
                return NotFound("Nenhuma categoria encontrada...");

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("GanhosPorCategoria")]
    public async Task<ActionResult<IEnumerable<GanhoPorCategoria>>> GetGanhosPorCategoria()
    {
        try
        {
            var categorias = await _context.Categoria
                .AsNoTracking()
                .ToListAsync();

            if (categorias == null || !categorias.Any())
                return NotFound("Nenhuma categoria encontrada...");

            var ganhosPorCategoria = new List<GanhoPorCategoria>();

            foreach (var categoria in categorias)
            {
                var totalGanhos = await _context.Receita
                    .Where(r => r.CategoriaId == categoria.CategoriaId) 
                    .SumAsync(r => r.Value);

                ganhosPorCategoria.Add(new GanhoPorCategoria
                {
                    Categoria = categoria.Nome,
                    Valor = (decimal)totalGanhos 
                });
            }

            return Ok(ganhosPorCategoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(Categoria categoria)
    {
        try
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", 
                new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Categoria update)
    {
        try
        {
            if (id != update.CategoriaId)
                return BadRequest();

            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.CategoriaId == id);

            categoria.Nome = update.Nome;

            _context.Categoria.Update(categoria);

            await _context.SaveChangesAsync();

            return Ok(update);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Categoria>> Delete(int id)
    {
        try
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound($"Não encontrado nenhuma categoria com o id {id} !");

            _context.Categoria.Remove(categoria);

            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasPorNome([FromQuery] string nome)
    {
        try
        {
            var categorias = await _context
                .Categoria
                .Where(c => EF.Functions.Like(c.Nome, $"%{nome}%"))
                .AsNoTracking()
                .ToListAsync();

            if (categorias == null || !categorias.Any())
                return NotFound("Nenhuma categoria encontrada com esse nome...");

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

}