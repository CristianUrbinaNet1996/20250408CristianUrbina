using AutoMapper;
using Core.Exceptions;
using Core.IRepositories;
using Core.IServices;
using Domain.Dto.Product;
using Domain.Models;

namespace Core.Services
{
    public class ProductService : IcommonService<ProductDto, ProductInsertDto, ProductUpdateDto>
    {
        public List<string> Errors { get; }

        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }



        public async Task<ProductDto?> Create(ProductInsertDto entity)
        {
            if (!await Validate(entity))

            {
                throw new InsertProductFailException("Error al insertar la entidad Producto", Errors);
            }


            Product product = _mapper.Map<Product>(entity);
            await _productRepository.Add(product);
            await _productRepository.Save();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> Delete(Guid id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                if (product != null)
                {
                    _productRepository.Delete(product);
                    await _productRepository.Save();
                }
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception es)
            {
                throw es;
            }

        }

        public async Task<IEnumerable<ProductDto?>> GetAll()
        {
            var result = await _productRepository.GetAll();
            return _mapper.Map<List<ProductDto>>(result);
        }

        public async Task<ProductDto?> GetById(Guid id)
        {
            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<ProductDto?> Update(Guid id, ProductUpdateDto entity)
        {
            var Product = await _productRepository.GetById(id);

            if (Product == null)
            {
                Errors.Add("Producto no encontrado");
                return null;
            }

            if (!await Validate(entity, id))
            {
                throw new UpdateProductFailException("Error al actualizar la entidad Producto", Errors);
            }

            Product.Name = entity.Name;
            Product.Description = entity.Description;
            Product.BasePrice = entity.BasePrice;
            if (entity.Image != null)
            {
                Product.Image = entity.Image;
            }

            _productRepository.Update(Product);
            await _productRepository.Save();


            return _mapper.Map<ProductDto>(Product);
        }

        public async Task<bool> Validate(ProductUpdateDto entity, Guid id)
        {
            var result = _productRepository
              .Find(x => x.Name == entity.Name && x.Id != id)
              .Any();

            if (result)
            {
                Errors.Add("El producto ya existe");
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> Validate(ProductInsertDto entity)
        {

            var result = _productRepository
              .Find(x => x.Name == entity.Name)
              .Any();

            if (result)
            {
                Errors.Add("El producto ya existe");
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
