namespace CoffeePeek.Data;

public interface IRepository<TEntity>
{
    public TEntity? GetById(int id);
    public IEnumerable<TEntity> GetAll();
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(int id);
}