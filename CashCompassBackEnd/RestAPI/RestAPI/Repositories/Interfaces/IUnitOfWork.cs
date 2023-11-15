namespace RestAPI.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }

    Task Commit();
}
