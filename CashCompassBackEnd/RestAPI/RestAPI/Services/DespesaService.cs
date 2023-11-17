using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.DTO.Request;
using RestAPI.Repositories;

namespace RestAPI.Services;

public class DespesaService
{
    private readonly DespesaRepository _repository;

    public DespesaService(DespesaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Despesa>> GetAllAsync()
    {
        return _repository
            .Despesas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Despesa> GetById(int id)
    {
        var despesa = _repository
            .Despesas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DespesaId == id);

        if(despesa == null)
        {
            throw new ArgumentException($"A despesa com o id {id} não existe !");
        }

        return despesa;
    }

    public async Task<Despesa> Create(DespesaRequestDTO request)
    {
        var despesa = new Despesa(request);

       await  _repository.Despesas
            .AddAsync(despesa);

       await _repository.SaveChangesAsync();

       return despesa;
    }

    private Despesa Swap(Despesa old, Despesa updated)
    {
        old.Value = updated.Value;
        old.WasPaid = updated.WasPaid;
        old.Description = updated.Description;
        old.Categoria = updated.Categoria;
        old.Date = updated.Date;
        

        return old;
    }

    public async Task<Despesa> Update(int id, DespesaRequestDTO newDespesa)
    {
        var despesa = await _repository
            .Despesas
            .FirstOrDefaultAsync(x => x.CardId == id);

        if(despesa == null)
        {
            throw new ArgumentException($"O cartão com o id {id} não existe !");
        }


        despesa = Swap(despesa, newDespesa);

        await _repository.SaveChangesAsync();

        return despesa;
    }

    public async Task<void> DeleteAsync(int id)
    {
        var despesa = _repository
            .Despesas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DespesaId == id);

        if (despesa == null)
        {
            throw new ArgumentException($"A despesa com o id {id} não existe !");
        }

        _repository
            .Despesas
            .Remove(despesa);

        await _repository.SaveChangesAsync();
    }
}