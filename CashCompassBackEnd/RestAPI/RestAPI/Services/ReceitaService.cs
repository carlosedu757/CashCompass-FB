using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.DTO.Request;
using RestAPI.Repositories;

namespace RestAPI.Services;

public class ReceitaService
{
    private readonly ReceitaRepository _repository;

    public ReceitaService(ReceitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Receita> Create(ReceitaRequestDTO request)
    {
        var receita = new Receita(request);

        await _repository.Receitas.AddAsync(receita);
        await _repository.SaveChangesAsync();

        return receita;
    }

    private async Task<Receita> Swap(Receita oldReceita, Receita newReceita)
    {
        var receita = await _repository
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

        _repository.Update(receita);

       await _repository.SaveChangesAsync();

       return receita;
    }

    public async Task DeleteById(int id)
    {
        try
        {
            var receita = await _repository
            .Receitas
            .FirstOrDefaultAsync(x => x.ReceitaId == id);


            if (receita is null)
            {
                throw new ArgumentException($"Não existe receita com o id {id} !");
            }

            _repository.Remove(receita);
            await _repository.SaveChangesAsync();
        }

        catch(Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }
}