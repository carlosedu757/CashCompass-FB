using RestAPI.Models;
using RestAPI.Repositories;

namespace RestAPI.Services;

public class ReceitaService
{
    private readonly ReceitaRepository _receitaRepository;

    public async Task<Receita> Create(Receita receita)
    {
        await _receitaRepository
            .Receitas
            .AddAsync(receita);

        await _receitaRepository.SaveChangesAsync();

        return receita;
    }

    private async Task<Receita> Swap(Receita oldReceita, Receita newReceita)
    {
        var receita = await _receitaRepository
            .Receitas
            .FindAsync(oldReceita);

        if (receita is null)
        {
            throw new ArgumentException("A receita informada não existe !");
        }

        receita.Description = newReceita.Description;
        receita.Value = newReceita.Value;
        receita.Fornecedor = newReceita.Fornecedor;

        return receita;
    }

    public async Task<Receita> Update(Receita oldReceita, Receita newReceita)
    {
        var receita = await Swap(oldReceita, newReceita);

        _receitaRepository.Update(receita);

       await _receitaRepository.SaveChangesAsync();

       return receita;
    }
}