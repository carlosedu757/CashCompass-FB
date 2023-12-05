namespace RestAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Context;
using Models;
using RestAPI.Models.Enum;

[ApiController]
[Route("api/v1/[controller]")]
public class CardController : ControllerBase
{
    private readonly AppDbContext _context;
    public CardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
	public async Task<ActionResult<List<Card>>> GetAllCards()
	{
		try
		{
			var cards = await _context?
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
	public async Task<ActionResult<Card>> GetCardById([FromRoute] int id)
	{
		try
		{
			var card = await _context
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

    [HttpGet("cardtype")]
    public async Task<ActionResult<Card>> GetCardByType([FromRoute] CardType cardType)
    {
        try
        {
            var cards = await _context
                .Card
                .Where(x => x.Type == cardType)
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

    [HttpPut("{id:int}")]
	public async Task<ActionResult<Card>> Update(int id, CardDTO updateCard)
	{
        try
        {
            if (id != updateCard.CardId)
                return BadRequest();

            var card = await _context.Card.FirstOrDefaultAsync(x => x.CardId == id);

            if (card == null)
                return NotFound();

            if (updateCard.CardNumber != null)
                card.CardNumber = updateCard.CardNumber;

            if (updateCard.LimitValue != null)
                card.LimitValue = (decimal)updateCard.LimitValue;

            if (updateCard.CurrentValue != null)
                card.CurrentValue = (decimal)updateCard.CurrentValue;

            if (updateCard.DateClose != null)
                card.DateClose = (long)updateCard.DateClose;

            if (updateCard.Bandeira != null)
                card.Bandeira = (Bandeira)updateCard.Bandeira;

            if (updateCard.Type != null)
                card.Type = (CardType)updateCard.Type;

            _context.Card.Update(card);

            await _context.SaveChangesAsync();

            return Ok(card);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

	[HttpPost]
	public async Task<ActionResult<Card>> Create([FromBody] Card card)
	{
		await _context.Card.AddAsync(card);

		await _context.SaveChangesAsync();

		return Created(nameof(GetCardById), new {Id = card.CardId});
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> Delete(int id)
	{
		var card = await _context.Card.FirstOrDefaultAsync(x => x.CardId == id);

		if (card is null)
			return NotFound();

        _context.Card.Remove(card);

		await _context.SaveChangesAsync();

		return Ok(card);
	}

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Card>>> GetCardsByParameters(
    [FromQuery] string? cardNumber = null,
    [FromQuery] CardType? cardType = null,
    [FromQuery] Bandeira? bandeira = null,
    [FromQuery] long? closedDate = null)
    {
        try
        {
            var query = _context.Card.AsQueryable();

            if (!string.IsNullOrEmpty(cardNumber))
            {
                query = query.Where(c => EF.Functions.Like(c.CardNumber, $"%{cardNumber}"));
            }

            if (cardType != null)
            {
                query = query.Where(c => c.Type == cardType);
            }

            if (bandeira != null)
            {
                query = query.Where(c => c.Bandeira == bandeira);
            }

            if (closedDate != null)
            {
                query = query.Where(c => c.DateClose == closedDate);
            }

            var cards = await query.AsNoTracking().ToListAsync();

            return Ok(cards);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}
