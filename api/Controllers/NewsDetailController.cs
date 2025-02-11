using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.NewsDetail;
using api.Mappers;
using api.Repositories.NewsDetails;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsDetailController : ControllerBase
    {
        private readonly INewsDetailRepository _newsDetailRepository;

        public NewsDetailController(INewsDetailRepository newsDetailRepository)
        {
            _newsDetailRepository = newsDetailRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllNewsDetails()
        {
            var newsDetails = await _newsDetailRepository.GetAllNewsDetailsAsync();
            var newsDetailDTOs = newsDetails.Select(nd => nd.ToNewsDetailDTO()).ToList();
            return Ok(newsDetailDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetNewsDetailById(int id)
        {
            var newsDetail = await _newsDetailRepository.GetNewsDetailByIdAsync(id);
            if (newsDetail == null)
            {
                return NotFound("NewsDetail ID not found");
            }
            var newsDetailDTO = newsDetail.ToNewsDetailDTO();
            return Ok(newsDetailDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddNewsDetail([FromBody] CreateNewsDetailResponseDTO createNewsDetailResponseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newNewsDetail = createNewsDetailResponseDTO.ToCreateNewsDetailResponseDTO();
            var createdNewsDetail = await _newsDetailRepository.CreateNewsDetailAsync(newNewsDetail);
            var newsDetailDTO = createdNewsDetail.ToNewsDetailDTO();
            return CreatedAtAction(nameof(GetNewsDetailById), new { id = newsDetailDTO.Id }, newsDetailDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateNewsDetail([FromBody] UpdateNewsDetailResponseDTO updateNewsDetailResponseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsDetailToUpdate = updateNewsDetailResponseDTO.ToUpdateNewsDetailResponseDTO();
            var updatedNewsDetail = await _newsDetailRepository.UpdateNewsDetailAsync(newsDetailToUpdate, id);
            if (updatedNewsDetail == null)
            {
                return NotFound("NewsDetail not found");
            }
            var newsDetailDTO = updatedNewsDetail.ToNewsDetailDTO();
            return Ok(newsDetailDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteNewsDetail(int id)
        {
            var deletedNewsDetail = await _newsDetailRepository.DeleteNewsDetailAsync(id);
            if (deletedNewsDetail == null)
            {
                return NotFound("NewsDetail ID not found");
            }
            return Ok("NewsDetail deleted successfully!");
        }
    }
}
