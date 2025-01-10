using CoffeePeek.Data.Models;

namespace CoffeePeek.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
{
    public readonly 

    public TEntity? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}