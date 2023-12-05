using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Models;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DespesaController : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<List<Despesa>>> GetAll([FromServices] AppDbContext context)
    {
        try
        {
            var despesas = await context
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Despesa>> GetDespesaById([FromRoute] int id, [FromServices] AppDbContext context)
    {

        try
        {
            var despesa = await context
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

    //[HttpPost]
    //public async Task<ActionResult<Despesa>> Create([FromBody]Despesa request, [FromServices] AppDbContext context)
    //{
    //    try
    //    {
    //        var despesa = await context
    //            .Despesa
    //            .AddAsync(request);

    //        await context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(GetDespesaById), new { Id = request.CardId }, request);
    //    }

    //    catch(Exception ex)
    //    {
    //        return StatusCode(500);
    //    }
    //}

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Despesa>> Update([FromRoute] int id, 
        [FromBody] Despesa request, [FromServices] AppDbContext context)
    {
        var despesa = await context
            .Despesa
            .FirstOrDefaultAsync(x => x.DespesaId == id);

        if (despesa is null)
            return NotFound($"Não encontrado nenhuma despesa com o id {id} !");

        despesa.Description = request.Description;
        despesa.Date = request.Date;

        context.Despesa.Update(despesa);

        await context.SaveChangesAsync();

        return Ok(despesa);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteById([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var despesa = await context
            .Despesa
            .FirstOrDefaultAsync(x => x.DespesaId == id);

        if (despesa is null)
            return NotFound($"Não encontrado nenhuma despesa com o id {id} !");

        context.Despesa.Remove(despesa);

        await context.SaveChangesAsync();
        
        return NoContent();
    }
}