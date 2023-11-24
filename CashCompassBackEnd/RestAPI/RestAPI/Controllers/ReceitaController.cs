using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Models;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceitaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReceitaController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Receita>>> GetAllAsync()
    {
        var list = await _context
            .Receita
            .AsNoTracking()
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Receita>> GetByIdAsync([FromRoute] int id)
    {
        var receita = await _context
            .Receita
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReceitaId == id);

        if (receita is null)
            return NotFound($"Não encontrada nenhuma receita com o id {id} !");
        
        return Ok(receita);
    }

    [HttpPost]
    public async Task<ActionResult<Receita>> CreateAsync([FromBody] Receita request)
    {
        var receita = await _context
            .Receita
            .AddAsync(request);

        await _context.SaveChangesAsync();

        var actionName = nameof(GetByIdAsync);

        return CreatedAtAction(actionName, new { Id = request.ReceitaId }, request);
    }
}