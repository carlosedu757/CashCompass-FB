namespace RestAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

[ApiController]
[Route("api/v1/cards")]
public class CardController : ControllerBase
{
	private readonly CardService _cardService;

	public CardController(CardService cardService)
	{
		_cardService = cardService;
	}

	[HttpGet]
	public async Task<ActionResult<List<Card>>> GetAllCards([FromRoute] int quantity)
	{
		var cards = await _cardService.GetAllCards(quantity);

		return Ok(cards);
	}

	[HttpGet]
	public async Task<ActionResult<Card>> GetCardById([FromRoute] int id)
	{
		var card = await _cardService
			.GetCardById(id);

		return Ok(card);
	}

	[HttpPost]
	public async Task<ActionResult<Card>> Create([FromBody] Card card)
	{
		await _cardService.CreateCard(card);

		return CreatedAtAction(nameof(GetCardById), new {Id = card.Id}, card);
	}
}
