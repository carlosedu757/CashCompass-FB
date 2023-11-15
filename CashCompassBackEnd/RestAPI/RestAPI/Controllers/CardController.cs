namespace RestAPI.Controllers;

using Models.DTO.Response;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;


[ApiController]
[Route("api/v1/[controller]")]
public class CardController : ControllerBase
{
	private readonly CardService _cardService;

	public CardController(CardService cardService)
	{
		_cardService = cardService;
	}

	/*[HttpGet]
	[Route("/{quantity:int}")]
	public async Task<ActionResult<List<CardResponseDTO>>> GetAllCards()
	{
		var cards = await _cardService.GetAllCards(quantity);

		return Ok(cards);
	}*/

	[HttpGet]
	[Route("/{id:int}")]
	public async Task<ActionResult<CardResponseDTO>> GetCardById([FromRoute] int id)
	{
		var card = await _cardService
			.GetCardById(id);

		return Ok(card);
	}

	[HttpPost]
	public async Task<ActionResult<CardResponseDTO>> Create([FromBody] Card card)
	{
		await _cardService.CreateCard(card);

		return CreatedAtAction(nameof(GetCardById), new {Id = card.CardId}, card);
	}

	[HttpDelete]
	[Route("/{id:int}")]
	public async Task<ActionResult> Delete([FromRoute] string id)
	{
		await _cardService.DeleteCard(id);

		return NoContent();
	}
}
