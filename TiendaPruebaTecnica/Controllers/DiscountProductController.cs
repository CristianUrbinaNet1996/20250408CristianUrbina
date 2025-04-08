using Core.Exceptions;
using Core.IServices;
using Domain.Dto.DiscountProduct;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TiendaPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountProductController : ControllerBase
    {

        private readonly IcommonService<DiscountProductDto, DiscountProductInsertDto, DiscountProductUpdateDto> _discountProdService;
        public DiscountProductController(IcommonService<DiscountProductDto, DiscountProductInsertDto, DiscountProductUpdateDto> icommonService)
        {
            _discountProdService = icommonService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<BaseResponse<IEnumerable<DiscountProductDto?>>> GetDiscountProducts()
        {
            var result = await _discountProdService.GetAll();

            return new BaseResponse<IEnumerable<DiscountProductDto?>>(result);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<DiscountProductDto>>> GetDiscountProd(Guid id)
        {

            var result = await _discountProdService.GetById(id);
            if (result == null)
            {
                return NotFound(new BaseResponse<DiscountProductDto?>(null)
                {
                    Status = false,
                    Errors = _discountProdService.Errors,
                    Message = "No se encontró el Cliente"
                });
            }
            return new BaseResponse<DiscountProductDto>(result);

        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<ActionResult<BaseResponse<DiscountProductDto>>> PutDiscountProd(Guid id, DiscountProductUpdateDto cliente)
        {
            try
            {
                var result = await _discountProdService.Update(id, cliente);
                return new BaseResponse<DiscountProductDto>(result);
            }
            catch (UpdateDiscountProductFailException ex)
            {
                return BadRequest(new BaseResponse<DiscountProductDto>(null)
                {
                    Status = false,
                    Errors = ex.Errors,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<DiscountProductDto>(null)
                {
                    Status = false,
                    Errors = new List<string> { ex.Message },
                    Message = "Error al actualizar el descuento del producto"
                });

            }
        }
        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaseResponse<DiscountProductDto>>> PostDiscountProd(DiscountProductInsertDto cliente)
        {
            try
            {
                var result = await _discountProdService.Create(cliente);
                return new BaseResponse<DiscountProductDto>(result);
            }
            catch (InsertDiscountProductFailException ex)
            {
                return BadRequest(new BaseResponse<DiscountProductDto>(null)
                {
                    Status = false,
                    Errors = ex.Errors,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<DiscountProductDto>(null)
                {
                    Status = false,
                    Errors = new List<string> { ex.Message },
                    Message = "Error al actualizar el descuento del producto"
                });

            }


        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            await _discountProdService.Delete(id);
            return NoContent();
        }


    }
}
