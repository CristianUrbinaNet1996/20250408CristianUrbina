using AutoMapper;
using Core.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class ProductoRepository : IRepository<Product>
    {
        private readonly TiendaPtContext _context;
        private readonly IMapper _mapper;
        public ProductoRepository(TiendaPtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Product entity) => await _context.Products.AddAsync(entity);
        public void Delete(Product entity)
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

        public IEnumerable<Product> Find(Func<Product, bool> predicate) => _context.Products.Where(predicate).ToList();

        public async Task<IEnumerable<Product>> GetAll()
        {
            var productos = await _context.Sp_GetAllProducts
                             .FromSqlRaw("EXEC sp_getAllProducts")
                             .ToListAsync();



            var result = _mapper.Map<List<Product>>(productos);

            foreach (var item in result)
            {
                var discountProducts = await _context.DiscountProducts
                    .Where(x => x.IdProduct == item.Id)
                    .ToListAsync();
                item.DiscountProducts.Clear();
                item.DiscountProducts = discountProducts;
            }


            return result;

        }

        public async Task<Product> GetById(Guid id) => await
            _context
            .Products
            .Include(x => x.DiscountProducts)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Product entity)
        {
            _context.Products.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
