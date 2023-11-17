using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.DTO;
using RestAPI.Models.DTO.Request;
using RestAPI.Models.Enum;
using RestAPI.Repositories;

namespace RestAPI.Services;

public class CardService
{
    private readonly CardRepository _cardRepository;

    public CardService(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    
    public async Task<List<Card>> GetAllCards()
    {
        var cards = await _cardRepository
            .Cards
            .AsNoTracking()
            .ToListAsync();

        return cards;
    }

    public async Task<Card> GetCardById(int id)
    {
        var card = await _cardRepository
            .Cards
            .FirstOrDefaultAsync(card => card.CardId == id);

        if (card is null)
        {
            throw new ArgumentException($"O cartão com o id {id} não existe !");
        }

        return card;
    }

    public async Task<Card> CreateCard(CardRequestDTO cardRequest)
    {
        var card = new Card(cardRequest);

        await _cardRepository
            .Cards
            .AddAsync(card);

        await _cardRepository.SaveChangesAsync();

        return card;
    }

    public Card FindCard(string number, Bandeira bandeira, CardType cardType, int dateClose)
    {
        var card = _cardRepository
            .Cards
            .FirstOrDefaultAsync(x => 
                x.CardNumber.Contains(number) && 
                x.CardNumber.EndsWith(number[3]) &&
                 x.Bandeira.Equals(bandeira) &&
                x.DateClose.Day == dateClose
                )
            .Result;

        if (card == null)
        {
            throw new ArgumentException("Cartão não encontrado !");
        }

        return card;
    }

    public async Task DeleteCard(string id)
    {
        var card = await _cardRepository
            .Cards
            .AsNoTracking()
            .FirstOrDefaultAsync(card => card.CardId.Equals(id));

        if (card is null)
            throw new ArgumentException($"O cartão com o id {id} não existe !");

        _cardRepository.Cards
            .Remove(card);

        await _cardRepository.SaveChangesAsync();
    }
}