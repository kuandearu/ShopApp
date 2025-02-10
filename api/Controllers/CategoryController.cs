using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Category;
using api.Mappers;
using api.Repositories.Categories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(){
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDTO = categories.Select(c => c.ToCategoryDTO()).ToList();
            return Ok(categoryDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryById(int id){
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if(category == null){
                return NotFound("Id not found");
            }
            var categoryDTO = category.ToCategoryDTO();
            return Ok(categoryDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryResponseDTO dTO){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var newCategory = dTO.ToCreateCategoryResponse();
            var createdCategory = await _categoryRepository.CreateCategoryAsync(newCategory);
            var categoryDTO = createdCategory.ToCategoryDTO();
            return CreatedAtAction(nameof(GetCategoryById), new {id = categoryDTO.Id}, categoryDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryResponseDTO dTO, int id){
            var categoryToUpdate = dTO.ToUpdateCategoryResponse();
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(categoryToUpdate, id);
            if(updatedCategory == null){
                return NotFound("ID not found");
            }
            var categoryDTO = updatedCategory.ToCategoryDTO();
            return Ok(categoryDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id){
            var deletedCategory = await _categoryRepository.DeleteCategoryAsync(id);
            if(deletedCategory == null){
                return NotFound("ID not found");
            }
            return Ok("Delete Category Successfully");
        }

    }
}