using System.Transactions;
using AutoMapper;
using Core.Exceptions;
using Core.IRepositories;
using Core.IServices;
using Domain.Dto.DiscountProduct;
using Domain.Models;

namespace Core.Services
{
    public class DiscountProductService : IDiscountProductService<DiscountProductDto, DiscountProductInsertDto, DiscountProductUpdateDto>
    {
        public List<string> Errors { get; }
        public readonly IRepository<DiscountProduct> _repository;
        public readonly IMapper _mapper;
        public readonly IRepository<Product> _productRepository;
        public DiscountProductService(IRepository<DiscountProduct> repository, IMapper mapper, IRepository<Product> repositoryProduct)
        {
            Errors = new List<string>();
            _repository = repository;
            _mapper = mapper;
            _productRepository = repositoryProduct;
        }
        public async Task<DiscountProductDto?> Create(DiscountProductInsertDto entity)
        {
            DiscountProduct discountProduct = _mapper.Map<DiscountProduct>(entity);

            if (!await Validate(entity))
            {
                throw new InsertDiscountProductFailException("Error al actualizar la entidad Descuento de producto", Errors);
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var previous = _repository.Find(x =>
                    x.IdProduct == entity.IdProduct &&
                    x.EndDate == null
                    )
                    .OrderByDescending(x => x.StartDate)
                    .FirstOrDefault();

                if (previous != null)
                {
                    previous.EndDate = entity.StartDate;
                    _repository.Update(previous);
                }

                await _repository.Add(discountProduct);
                await _repository.Save();
                scope.Complete();
            }

            return _mapper.Map<DiscountProductDto>(discountProduct);
        }

        public async Task<DiscountProductDto?> Delete(Guid id)
        {
            try
            {
                var discountProduct = await _repository.GetById(id);
                if (discountProduct != null)
                {
                    _repository.Delete(discountProduct);
                    await _repository.Save();
                }
                return _mapper.Map<DiscountProductDto>(discountProduct);
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<IEnumerable<DiscountProductDto?>> GetAll()
        {
            return _mapper.Map<IEnumerable<DiscountProductDto>>(await _repository.GetAll());
        }

        public async Task<DiscountProductDto?> GetById(Guid id)
        {
            return _mapper.Map<DiscountProductDto>(await _repository.GetById(id));
        }

        public async Task<DiscountProductDto?> Update(Guid id, DiscountProductUpdateDto entity)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                Errors.Add("Descuento de producto no encontrado");
                throw new UpdateDiscountProductFailException("Error al actualizar la entidad Descuento de producto", Errors);
            }
            if (!await Validate(entity, id))
            {
                throw new UpdateDiscountProductFailException("Error al actualizar la entidad Descuento de producto", Errors);
            }

            result.DiscountPrice = entity.DiscountPrice;
            result.StartDate = entity.StartDate;

            _repository.Update(result);
            await _repository.Save();


            return _mapper.Map<DiscountProductDto>(result);


        }

        public async Task<bool> Validate(DiscountProductUpdateDto entity, Guid id)
        {
            bool isValid = true;



            var Product = await _productRepository.GetById(entity.IdProduct.Value);

            var DiscountList = _repository.Find(x =>
              x.IdProduct == entity.IdProduct &&
               (
              (entity.EndDate == null || x.StartDate < entity.EndDate) &&
              (x.EndDate == null || entity.StartDate < x.EndDate)
               )
              ).Any();

            if (DiscountList)
            {
                Errors.Add("Las fechas que estas agregando descuento se traslapan con otro existente.");
                return false;
            }


            if (Product == null)
            {
                Errors.Add("El producto no existe");
                isValid = false;
            }
            if (Product.BasePrice >= entity.DiscountPrice)
            {
                Errors.Add("El precio de descuento no puede ser mayor o igual al precio base");
                isValid = false;
            }

            if (entity.StartDate <= DateTime.Now)
            {
                Errors.Add("La fecha de inicio no puede ser menor o igual a la fecha actual");
                isValid = false;
            }

            return isValid;
        }

        public async Task<bool> Validate(DiscountProductInsertDto entity)
        {
            bool isValid = true;
            var Product = await _productRepository.GetById(entity.IdProduct.Value);
            var DiscountList = _repository.Find(x =>
           x.IdProduct == entity.IdProduct &&
           ((x.EndDate > entity.StartDate) &&
               x.StartDate < entity.StartDate
            )
            ).Any();

            if (DiscountList)
            {
                Errors.Add("Ya existe un descuento vigente que se traslapa con la fecha de inicio ingresada.");
                return false;
            }

            if (Product == null)
            {
                Errors.Add("El producto no existe");
                isValid = false;
            }
            if (Product.BasePrice <= entity.DiscountPrice)
            {
                Errors.Add("El precio de descuento no puede ser mayor o igual al precio base");
                isValid = false;
            }

            if (entity.StartDate <= DateTime.Now)
            {
                Errors.Add("La fecha de inicio no puede ser menor o igual a la fecha actual");
                isValid = false;
            }

            return isValid;
        }

        public async Task<bool> SetEndDate(Guid Id)
        {
            var result = _repository.Find(x => x.Id == Id && x.EndDate is null).FirstOrDefault();
            if (result is null)
            {
                Errors.Add(string.Format("No existe el descuento {0}", Id));
                throw new UpdateDiscountProductFailException("Error al actualizar el descuento de producto", Errors);
            }


            result.EndDate = DateTime.Now;

            _repository.Update(result);
            await _repository.Save();

            return true;

        }
    }
}
