using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RestAPI.Context;
using RestAPI.Models;
using RestAPI.Models.Enum;

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

    [HttpGet("TotalValue")]
    public async Task<ActionResult<decimal>> GetTotalValue()
    {
        try
        {
            var totalValue = await _context.Receita
                .AsNoTracking()
                .SumAsync(d => d.Value);

            return Ok(totalValue);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("TotalValueGeral")]
    public async Task<ActionResult<decimal>> GetTotalValueGeral()
    {
        try
        {
            var totalValue1 = await _context.Receita
                .AsNoTracking()
                .SumAsync(d => d.Value);

            var totalValue2 = await _context.Despesa
                .AsNoTracking()
                .SumAsync(d => d.Value);

            return Ok(totalValue1 - totalValue2);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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

        return Ok();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Receita>>> GetReceitasByDataCategoriaFormaPgmt([FromQuery] DateTime? data, [FromQuery] FormaPagamento? formaPgmt, [FromQuery] int? categoriaId)
    {
        try
        {
            var query = _context.Receita.AsQueryable();

            if (data != null)
            {
                query = query.Where(c => c.Date == data);
            }

            if (formaPgmt != null)
            {
                query = query.Where(c => c.FormaPagamento == formaPgmt);
            }

            if (categoriaId != null)
            {
                query = query.Where(c => c.CategoriaId == categoriaId);
            }

            var receitas = await query.AsNoTracking().ToListAsync();

            return Ok(receitas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Receita>> Update(int id, Receita updateReceita)
    {
        try
        {
            if (id != updateReceita.ReceitaId)
                return BadRequest();

            var receita = await _context.Receita.FirstOrDefaultAsync(x => x.ReceitaId == id);

            if (receita == null)
                return NotFound();

            if (updateReceita.Value != null)
                receita.Value = updateReceita.Value;

            if (updateReceita.Description != null)
                receita.Description = updateReceita.Description;

            if (updateReceita.Fornecedor != null)
                receita.Fornecedor = updateReceita.Fornecedor;

            if (updateReceita.Date != null)
                receita.Date = updateReceita.Date;

            if (updateReceita.CategoriaId != null)
                receita.CategoriaId = updateReceita.CategoriaId;

            if (updateReceita.FormaPagamento != null)
                receita.FormaPagamento = (FormaPagamento)updateReceita.FormaPagamento;

            if (updateReceita.CardId != null)
                receita.CardId = updateReceita.CardId;

            _context.Receita.Update(receita);

            await _context.SaveChangesAsync();

            return Ok(receita);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var receita = await _context.Receita.FirstOrDefaultAsync(x => x.ReceitaId == id);

        if (receita is null)
            return NotFound();

        _context.Receita.Remove(receita);

        await _context.SaveChangesAsync();

        return Ok(receita);
    }
}