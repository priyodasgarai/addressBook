using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using addressBook.Dtos.Product;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Mappers;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace addressBook.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepo;

        private readonly ILogger<ProductController> _logger;

        private readonly ICategoryRepository _categoryRepo;

        private readonly IBrandRepository _brandRepo;

        public ProductController(
            IProductRepository productRepo,
            ILogger<ProductController> logger,
            ICategoryRepository categoryRepo,
            IBrandRepository brandRepo
        )
        {
            _productRepo = productRepo;
            _logger = logger;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = await _productRepo.GetByIdAsync(id);
            // return Ok(product);
            if (product == null)
            {
                return CustomResult("Product does not exist",
                HttpStatusCode.BadRequest);
            }
            return CustomResult("Data loaded successfully",
            product.ToProductDto(),
            HttpStatusCode.OK);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred",
                    ModelState,
                    HttpStatusCode.BadRequest);
                if (!await _categoryRepo.CategoryExists(productDto.CategoryId))
                {
                    return CustomResult("Category does not exist",
                    HttpStatusCode.BadRequest);
                }
                if (!await _brandRepo.BrandExists(productDto.BrandId))
                {
                    return CustomResult("Brand does not exist",
                    HttpStatusCode.BadRequest);
                }

                var productModel = productDto.ToProductFromCreateDTO();
                await _productRepo.CreateAsync(productModel);
                var result =
                    CreatedAtAction(nameof(GetById),
                    new { id = productModel.Id },
                    productModel.ToProductDto());
                return CustomResult("Data added successfully",
                result.Value,
                HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryObject query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred!",
                    ModelState,
                    HttpStatusCode.BadRequest);
                var Products = await _productRepo.GetAllAsync(query);
                var productDto = Products.Select(p => p.ToProductDto()).ToList();
                if (productDto == null || productDto.Count() == 0)
                {
                    return CustomResult("Data not found",
                    HttpStatusCode.NotFound);
                }
                return CustomResult("Data loaded successfully",
                productDto,
                HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("TotalRecord")]
        public async Task<IActionResult> TotalRecord([FromQuery] ProductQueryObject query)
        {
            try
            {
                var product = await _productRepo.ProductCountAsync(query);
                return CustomResult("Data loaded successfully", product, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
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
                    return CustomResult("One or more validation errors occurred",
                    ModelState,
                    HttpStatusCode.BadRequest);
                var productModel = await _productRepo.DeleteAsync(id);
                if (productModel == null)
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
        public async Task<IActionResult>
        Update([FromRoute] int id, [FromBody] UpdateProductRequestDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred",
                    ModelState,
                    HttpStatusCode.BadRequest);

                if (!await _categoryRepo.CategoryExists(productDto.CategoryId))
                {
                    return CustomResult("Category does not exist",
                    HttpStatusCode.BadRequest);
                }
                if (!await _brandRepo.BrandExists(productDto.BrandId))
                {
                    return CustomResult("Brand does not exist",
                    HttpStatusCode.BadRequest);
                }
                var productModel =               await _productRepo.UpdateAsync(id, productDto);
                if (productModel == null)
                {
                    return CustomResult("Data not found",
                    HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully",
                productModel.ToProductDto(),
                HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}