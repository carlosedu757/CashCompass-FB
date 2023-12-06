using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Models;
using RestAPI.Models.Enum;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesaController : ControllerBase
{
    private readonly AppDbContext _context;

    public DespesaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Despesa>>> GetAll()
    {
        try
        {
            var despesas = await _context
                .Despesa
                .AsNoTracking()
                .ToListAsync();

            return Ok(despesas);
        }

        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("TotalValue")]
    public async Task<ActionResult<decimal>> GetTotalValue()
    {
        try
        {
            var totalValue = await _context.Despesa
                .AsNoTracking()
                .SumAsync(d => d.Value);

            return Ok(totalValue);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Despesa>> GetDespesaById([FromRoute] int id)
    {

        try
        {
            var despesa = await _context
                .Despesa
                .FirstOrDefaultAsync(x => x.DespesaId == id);

            if (despesa is null)
                return NotFound($"Não encontrado despesa com o id {id} !");

            return Ok(despesa);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Despesa>> Create([FromBody] Despesa despesaRequest)
    {
        try
        {
            var despesa = await _context.Despesa.AddAsync(despesaRequest);

            await _context.SaveChangesAsync();

            return Ok(despesa);
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Despesa>> Update(int id, Despesa updateDespesa)
    {
        try
        {
            if (id != updateDespesa.DespesaId)
                return BadRequest();

            var despesa = await _context.Despesa.FirstOrDefaultAsync(x => x.DespesaId == id);

            if (despesa == null)
                return NotFound();

            if (updateDespesa.Value != null)
                despesa.Value = updateDespesa.Value;

            if (updateDespesa.Description != null)
                despesa.Description = updateDespesa.Description;

            if (updateDespesa.NumParcelas != null)
                despesa.NumParcelas = updateDespesa.NumParcelas;

            if (updateDespesa.Date != null)
                despesa.Date = updateDespesa.Date;

            if (updateDespesa.CategoriaId != null)
                despesa.CategoriaId = updateDespesa.CategoriaId;

            if (updateDespesa.FormaPagamento != null)
                despesa.FormaPagamento = (FormaPagamento)updateDespesa.FormaPagamento;

            if (updateDespesa.CardId != null)
                despesa.CardId = updateDespesa.CardId;

            if (updateDespesa.WasPaid != null)
                despesa.WasPaid = updateDespesa.WasPaid;

            _context.Despesa.Update(despesa);

            await _context.SaveChangesAsync();

            return Ok(despesa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var despesa = await _context.Despesa.FirstOrDefaultAsync(x => x.DespesaId == id);

        if (despesa is null)
            return NotFound();

        _context.Despesa.Remove(despesa);

        await _context.SaveChangesAsync();

        return Ok(despesa);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Despesa>>> GetDespesasByDataCategoriaFormaPgmtWasPaid([FromQuery] DateTime? data, [FromQuery] FormaPagamento? formaPgmt, [FromQuery] int? categoriaId, [FromQuery] bool? wasPaid)
    {
        try
        {
            var query = _context.Despesa.AsQueryable();

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

            if (wasPaid != null)
            {
                query = query.Where(c => c.WasPaid == wasPaid);
            }

            var receitas = await query.AsNoTracking().ToListAsync();

            return Ok(receitas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}