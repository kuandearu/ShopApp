using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Newss;
using api.Mappers;
using api.Repositories.Newss;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsRepository.GetAllNewsAsync();
            var newsDTOs = news.Select(n => n.ToNewsDTO()).ToList();
            return Ok(newsDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound("News ID not found");
            }
            var newsDTO = news.ToNewsDTO();
            return Ok(newsDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddNews([FromBody] CreateNewsResponseDTO createNewsResponseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newNews = createNewsResponseDTO.ToCreateNewsResponseDTO();
            var createdNews = await _newsRepository.CreateNewsAsync(newNews);
            var newsDTO = createdNews.ToNewsDTO();
            return CreatedAtAction(nameof(GetNewsById), new { id = newsDTO.Id }, newsDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateNews([FromBody] UpdateNewsResponseDTO updateNewsResponseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsToUpdate = updateNewsResponseDTO.ToUpdateNewsResponseDTO();
            var updatedNews = await _newsRepository.UpdateNewsAsync(newsToUpdate, id);
            if (updatedNews == null)
            {
                return NotFound("News not found");
            }
            var newsDTO = updatedNews.ToNewsDTO();
            return Ok(newsDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var deletedNews = await _newsRepository.DeleteNewsAsync(id);
            if (deletedNews == null)
            {
                return NotFound("News ID not found");
            }
            return Ok("News deleted successfully!");
        }
    }
}
