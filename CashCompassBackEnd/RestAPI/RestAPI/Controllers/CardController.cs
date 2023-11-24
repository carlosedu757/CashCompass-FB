namespace RestAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Context;
using Models;


[ApiController]
[Route("api/v1/[controller]")]
public class CardController : ControllerBase
{
	
	[HttpGet]
	public async Task<ActionResult<List<Card>>> GetAllCards([FromServices] AppDbContext context)
	{
		try
		{
			var cards = await context?
				.Card?
				.AsNoTracking()
				.ToListAsync();

			if (cards is null)
				return NotFound();

			return Ok(cards);
		}
		catch (Exception e)
		{
			return StatusCode(500);
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Card>> GetCardById([FromRoute] int id, 
		[FromServices] AppDbContext context)
	{
		try
		{
			var card = await context
				.Card
				.FirstOrDefaultAsync(x => x.CardId == id);

			if (card is null)
				return NotFound();

			return Ok(card);
		}
		catch (Exception e)
		{
			return StatusCode(500);
		}
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult<Card>> Update([FromRoute] int id, Card requestUpdate,
		[FromServices] AppDbContext context)
	{
		var card = await context
			.Card
			.FirstOrDefaultAsync(x => x.CardId == id);

		if (card is null)
			return NotFound();

		return Ok(card);
	}

	[HttpPost]
	public async Task<ActionResult<Card>> Create([FromBody] Card card,
		[FromServices] AppDbContext context)
	{
		await context.Card.AddAsync(card);

		await context.SaveChangesAsync();

		return Created(nameof(GetCardById), new {Id = card.CardId});
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] string id, [FromServices] AppDbContext context)
	{
		var card = context
			.Card
			.FirstOrDefaultAsync(x => x.CardId.Equals(id));

		if (card is null)
			return NotFound();

		context.Remove(card);

		await context.SaveChangesAsync();

		return NoContent();
	}
}
