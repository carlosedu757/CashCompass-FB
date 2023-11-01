using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Services;

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
	public ActionResult<List<Card>> GetAllCards()
	{
		var cards = _cardService.GetAll();
		
		return Ok(cards);
	}
}
