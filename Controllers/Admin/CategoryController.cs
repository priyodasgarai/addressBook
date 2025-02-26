
using System.Net;
using addressBook.Dtos.Category;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Mappers;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace addressBook.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _catRepo;
        private readonly ILogger<CategoryController> _logger;
       
        public CategoryController(ICategoryRepository catRepo, ILogger<CategoryController> logger)
        {
            _catRepo = catRepo;
            _logger = logger;
           
        }
 [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoryQueryObject query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred!", ModelState, HttpStatusCode.BadRequest);
                var categories = await _catRepo.GetAllAsync(query);
                var categoryDto = categories.Select(c => c.ToCategoryDto()).ToList();
                if (categoryDto == null || categoryDto.Count() == 0)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data loaded successfully", categoryDto, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }
        [HttpPost()]
        public async Task<IActionResult> Create(CreateCtegoryRequestDto ctegoryDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
              
                var catModel = ctegoryDto.ToCategoryFromCreateDTO();              
               await _catRepo.CreateAsync(catModel);
                var result = CreatedAtAction(nameof(GetById), new { id = catModel.Id }, catModel.ToCategoryDto());
                return CustomResult("Data added successfully", result.Value, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var Category = await _catRepo.GetByIdAsync(id);
                if (Category == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data loaded successfully", Category.ToCategoryDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
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
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var categoryModel = await _catRepo.DeleteAsync(id);
                if (categoryModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data delete successfully", HttpStatusCode.OK);
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var categoryModel = await _catRepo.UpdateAsync(id, categoryDto);
                if (categoryModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully", categoryModel.ToCategoryDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}