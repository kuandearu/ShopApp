using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Brand;
using api.Mappers;
using api.Repositories.Brands;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBrands(){
            var brands = await _brandRepository.GetAllBrandsAsync();
            var brandDTO = brands.Select(b => b.ToBrandDTO()).ToList();
            return Ok(brandDTO);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBrandById(int id){
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if(brand == null){
                return BadRequest("Id not found");
            }
            var brandDTO = brand.ToBrandDTO();
            return Ok(brandDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBrand([FromBody] CreateBrandResponseDTO createBrandResponseDTO){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var newBrand = createBrandResponseDTO.ToCreateBrandResponse();
            var createdBrand = await _brandRepository.CreateBrandAsync(newBrand);
            var brandDTO = createdBrand.ToBrandDTO();
            return CreatedAtAction(nameof(GetBrandById), new {id = brandDTO.Id}, brandDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandResponseDTO updateBrandResponseDTO, int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var brandToUpdate = updateBrandResponseDTO.ToUpdateBrandResponse();
            var updatedBrand = await _brandRepository.UpdateBrandAsync(brandToUpdate,id);
            if(updatedBrand == null){
                return NotFound("Brand not found");
            }
            var brandDTO = updatedBrand.ToBrandDTO();
            return Ok(brandDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id){
            
            var deletedBrand = await _brandRepository.DeleteBrandAsync(id);
            if(deletedBrand == null){
                return NotFound("Id not found");
            }
            return Ok("Brand Deleted successfully!!!");
        }
    }
}