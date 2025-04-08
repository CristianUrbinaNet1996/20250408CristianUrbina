using Core.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class DiscountProductRepository : IRepository<DiscountProduct>
    {
        private readonly TiendaPtContext _context;

        public DiscountProductRepository(TiendaPtContext context)
        {
            _context = context;
        }
        public async Task Add(DiscountProduct entity) => await _context.DiscountProducts.AddAsync(entity);
        public void Delete(DiscountProduct entity)
        {

            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public IEnumerable<DiscountProduct> Find(Func<DiscountProduct, bool> predicate) => _context.DiscountProducts.Where(predicate).ToList();

        public async Task<IEnumerable<DiscountProduct>> GetAll() => await _context.DiscountProducts
            .Include(x => x.IdProductNavigation)
            .ToListAsync();

        public async Task<DiscountProduct?> GetById(Guid id) => await _context.DiscountProducts.Include(x => x.IdProductNavigation).FirstOrDefaultAsync(x => x.Id == id);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(DiscountProduct entity)
        {
            _context.DiscountProducts.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
