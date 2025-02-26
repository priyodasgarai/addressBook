using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using addressBook.Dtos.Brand;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Mappers;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace addressBook.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : BaseController
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandRepository _brandRepo;
        // private readonly IFileService _fileService;

        public BrandController(ILogger<BrandController> logger, IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
            _logger = logger;
            //    _fileService = fileService;

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var Brand = await _brandRepo.GetByIdAsync(id);
                if (Brand == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data loaded successfully", Brand.ToBrandDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateBrandRequestDto brandDTO)
        {

            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                var brandModel = brandDTO.ToBrandFromCreateDTO();
                await _brandRepo.CreateAsync(brandModel);
                var result = CreatedAtAction(nameof(GetById), new { id = brandModel.Id }, brandModel.ToBrandDto());
                return CustomResult("Data added successfully", result.Value, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BrandQueryObject query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred!", ModelState, HttpStatusCode.BadRequest);
                var brands = await _brandRepo.GetAllAsync(query);
                var brandDTO = brands.Select(b => b.ToBrandDto()).ToList();
                if (brandDTO == null || brandDTO.Count() == 0)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data loaded successfully", brandDTO, HttpStatusCode.OK);

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
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var brandModel = await _brandRepo.DeleteAsync(id);
                if (brandModel == null)
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateBrandRequestDto brandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                // var brandDetails = await _brandRepo.GetByIdAsync(id);
                var brandModel = brandDto.ToBrandFromUpdateDto();
                await _brandRepo.UpdateAsync(id, brandDto);

                if (brandModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully", brandModel.ToBrandDto(), HttpStatusCode.OK);
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