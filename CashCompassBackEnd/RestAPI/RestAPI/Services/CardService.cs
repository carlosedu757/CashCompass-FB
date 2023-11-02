using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Repositories;

namespace RestAPI.Services;

public class CardService
{
    private readonly CardRepository _cardRepository;

    public CardService(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public List<Card> GetAll()
    {
        return _cardRepository
            .Cards
            .AsNoTracking()
            .ToList();
    }

    public Card FindByNumber(string number)
    {
        var card = _cardRepository
            .Cards
            .AsNoTracking()
            .FirstOrDefault(x => x.Number.Equals(number));

        if (card is null)
            throw new ArgumentException("Não existe nenhum cartão com esse número !");

        return card;
    }
}