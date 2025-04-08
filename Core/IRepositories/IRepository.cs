namespace Core.IRepositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetById(Guid id);

        Task Add(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task Save();

        IEnumerable<T> Find(Func<T, bool> predicate);

    }
}
