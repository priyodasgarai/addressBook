using addressBook.Dtos.Category;
using addressBook.Dtos.Product;
using addressBook.Dtos.ProductAttribute;
using addressBook.Interfaces;
using addressBook.Mappers;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace addressBook.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : BaseController
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ILogger<ProductAttributeController> _logger;
        private readonly IProductRepository _productRepository;
        public ProductAttributeController(IProductAttributeRepository attributeRepository,
            ILogger<ProductAttributeController> logger,
            IProductRepository productRepository)
        {
            _productAttributeRepository = attributeRepository;
            _logger = logger;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var attribute = await _productAttributeRepository.GetAllAsync();
                var attributeDto = attribute.Select(a => a.ToProductAttributeDto()).ToList();
                if (attributeDto == null || attributeDto.Count() == 0)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data loaded successfully", attributeDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try {

                if (!ModelState.IsValid) return BadRequest(ModelState);
                var attribute = await _productAttributeRepository.GetByIdAsync(id);
                // return Ok(product);
                if (attribute == null)
                {
                    return CustomResult("ProductAttribute does not exist",
                    HttpStatusCode.BadRequest);
                }
                return CustomResult("Data loaded successfully", attribute.ToProductAttributeDto(),HttpStatusCode.OK);

            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeRequestDto attributeRequestDto)
        {
            try {

                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred",ModelState,HttpStatusCode.BadRequest);
                if (!await _productRepository.ProductExists(attributeRequestDto.ProductId))
                {
                    return CustomResult("Product does not exist",
                    HttpStatusCode.BadRequest);
                }

                var attributeModel = attributeRequestDto.ToProductAttributeFromCreateDTO();
                await _productAttributeRepository.CreateAsync(attributeModel);
                var result =CreatedAtAction(nameof(GetById),new { id = attributeModel.Id },attributeModel.ToProductAttributeDto());
                return CustomResult("Data added successfully", result.Value,HttpStatusCode.OK);

            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred",ModelState,HttpStatusCode.BadRequest);
                var attributeModel = await _productAttributeRepository.DeleteAsync(id);
                if (attributeModel == null)
                {
                    return CustomResult("Data not found",
                    HttpStatusCode.NotFound);
                }

                return CustomResult("Data delete successfully",
                HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductAttributeRequestDto attributeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred",
                    ModelState,
                    HttpStatusCode.BadRequest);
                var attributeModel = await _productAttributeRepository.UpdateAsync(id, attributeDto);
                if (attributeModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully",attributeModel.ToProductAttributeDto(),HttpStatusCode.OK);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        }
}
