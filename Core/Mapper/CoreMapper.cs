using System.Data;
using AutoMapper;
using Domain.Dto.DiscountProduct;
using Domain.Dto.Product;
using Domain.Models;

namespace Core.Mapper
{
    public class CoreMapper : Profile
    {
        public CoreMapper()
        {

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src => GetDiscount(src)))
                .ReverseMap();

            CreateMap<sp_GetAllProducts, Product>()
                .ForMember(dest => dest.DiscountProducts, opt => opt.MapFrom(src => GetDiscountProducts(src)))
                .ReverseMap();

            CreateMap<ProductDto, ProductInsertDto>().ReverseMap();

            CreateMap<DiscountProduct, DiscountProductDto>().ReverseMap();
            CreateMap<DiscountProduct, DiscountProductInsertDto>().ReverseMap();
        }

        private List<DiscountProduct> GetDiscountProducts(sp_GetAllProducts product)
        {

            List<DiscountProduct> discountProducts = new List<DiscountProduct>();
            if (product.DiscountPrice is not null)
            {

                discountProducts.Add(
                      new DiscountProduct
                      {
                          Id = product.DiscountPrice.Value,
                          DiscountPrice = 0,
                          StartDate = null,
                          EndDate = null,
                          IdProduct = product.Id
                      });

            }
            return discountProducts;
        }
        private decimal GetDiscount(Product product)
        {


            var discount = product.DiscountProducts
       .Where(x => x.StartDate <= DateTime.Now &&
                  (x.EndDate == null || x.EndDate >= DateTime.Now))
       .OrderByDescending(x => x.StartDate)
       .Select(x => x.DiscountPrice)
       .FirstOrDefault();

            if (discount != null)
            {
                return (decimal)discount;
            }
            else
            {
                return 0;
            }


        }

    }
}
