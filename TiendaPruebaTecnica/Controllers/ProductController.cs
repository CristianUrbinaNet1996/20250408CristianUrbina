using Core.Exceptions;
using Core.IServices;
using Domain.Dto.Product;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TiendaPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IcommonService<ProductDto, ProductInsertDto, ProductUpdateDto> _productService;
        public ProductController(IcommonService<ProductDto, ProductInsertDto, ProductUpdateDto> icommonService)
        {
            _productService = icommonService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<BaseResponse<IEnumerable<ProductDto?>>> GetProducts()
        {
            var result = await _productService.GetAll();

            return new BaseResponse<IEnumerable<ProductDto?>>(result);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ProductDto>>> GetProduct(Guid id)
        {

            var result = await _productService.GetById(id);
            if (result == null)
            {
                return NotFound(new BaseResponse<ProductDto?>(null)
                {
                    Status = false,
                    Errors = _productService.Errors,
                    Message = "No se encontró el Product"
                });
            }
            return new BaseResponse<ProductDto>(result);

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<ActionResult<BaseResponse<ProductDto>>> PutProduct(Guid id, ProductUpdateDto Product)
        {
            try
            {
                var result = await _productService.Update(id, Product);
                return new BaseResponse<ProductDto>(result);
            }
            catch (UpdateProductFailException ex)
            {
                return new BaseResponse<ProductDto>(null)
                {
                    Status = false,
                    Errors = ex.Errors,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductDto>(null)
                {
                    Status = false,
                    Errors = new List<string> { ex.Message },
                    Message = "Error inesperado"
                };
            }


        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<BaseResponse<ProductDto>> PostProduct(ProductInsertDto Product)
        {
            try
            {
                var result = await _productService.Create(Product);
                return new BaseResponse<ProductDto>(result);
            }

            catch (InsertProductFailException ex)
            {
                return new BaseResponse<ProductDto>(null)
                {
                    Status = false,
                    Errors = ex.Errors,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductDto>(null)
                {
                    Status = false,
                    Errors = new List<string> { ex.Message },
                    Message = "Error inesperado"
                };
            }


        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.Delete(id);
            return NoContent();
        }


    }
}
