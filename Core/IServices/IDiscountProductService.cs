namespace Core.IServices
{
    public interface IDiscountProductService<T, TI, TU>
    {


        public List<string> Errors { get; }

        Task<IEnumerable<T?>> GetAll();

        Task<T?> GetById(Guid id);

        Task<T?> Create(TI entity);
        Task<T?> Update(Guid id, TU entity);

        Task<T?> Delete(Guid id);

        Task<bool> Validate(TU entity, Guid id);

        Task<bool> Validate(TI entity);
        Task<bool> SetEndDate(Guid Id);


    }
}
