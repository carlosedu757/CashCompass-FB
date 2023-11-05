using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
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
    
    public async Task<List<Card>> GetAllCards(int quantity)
    {
        var cards = await _cardRepository
            .Cards
            .AsNoTracking()
            .Take(quantity)
            .ToListAsync();

        return cards;
    }

    public async Task<Card> GetCardById(int id)
    {
        var card = await _cardRepository
            .Cards
            .FirstOrDefaultAsync(x => x.Id == id);

        if (card is null)
        {
            throw new ArgumentException($"O cartão com o id {id} não existe !");
        }

        return card;
    }

    public async Task<Card> CreateCard(Card card)
    {
         await _cardRepository.Cards.AddAsync(card);

         await _cardRepository.SaveChangesAsync();

         return card;
    }

    public Card FindCard(string number, EBandeira bandeira, ECardType cardType, int dateClose)
    {
        var card = _cardRepository
            .Cards
            .FirstOrDefaultAsync(x => 
                x.Number.Contains(number) && 
                x.Number.EndsWith(number[3]) &&
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

    public async void DeleteCard(Card card)
    {
        _cardRepository
            .Cards
            .Remove(card);

        await _cardRepository.SaveChangesAsync();
    }
}