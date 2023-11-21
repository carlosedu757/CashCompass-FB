namespace RestAPI.Controllers;

using Models.DTO.Request;
using Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
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

	[HttpGet]
	public async Task<ActionResult<List<CardResponseDTO>>> GetAllCards()
	{
		var cards = await _cardService.GetAllCards();

		return Ok(cards);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<CardResponseDTO>> GetCardById([FromRoute] int id)
	{
		var card = await _cardService
			.GetCardById(id);

		var cardResponse = new CardResponseDTO(card);

		return Ok(cardResponse);
	}

	[HttpPost]
	public async Task<ActionResult<CardResponseDTO>> Create([FromBody] CardRequestDTO cardRequestDto)
	{
		var card = await _cardService
			.CreateCard(cardRequestDto);

		return Created(nameof(GetCardById), new {Id = card.CardId});
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] string id)
	{
		await _cardService.DeleteCard(id);

		return NoContent();
	}
}
