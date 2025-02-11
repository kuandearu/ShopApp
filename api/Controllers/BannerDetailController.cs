using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.BannerDetail;
using api.Mappers;
using api.Repositories.BannerDetails;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannerDetailController : ControllerBase
    {
        private readonly IBannerDetailRepository _bannerDetailRepository;

        public BannerDetailController(IBannerDetailRepository bannerDetailRepository)
        {
            _bannerDetailRepository = bannerDetailRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBannerDetails()
        {
            var bannerDetails = await _bannerDetailRepository.GetAllBannerDetailsAsync();
            var bannerDetailDTOs = bannerDetails.Select(bd => bd.ToBannerDetailDTO()).ToList();
            return Ok(bannerDetailDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBannerDetailById(int id)
        {
            var bannerDetail = await _bannerDetailRepository.GetBannerDetailByIdAsync(id);
            if (bannerDetail == null)
            {
                return NotFound("Banner Detail ID not found");
            }
            var bannerDetailDTO = bannerDetail.ToBannerDetailDTO();
            return Ok(bannerDetailDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBannerDetail([FromBody] CreateBannerDetailResponseDTO createBannerDetailResponseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newBannerDetail = createBannerDetailResponseDTO.ToCreateBannerDetailResponseDTO();
            var createdBannerDetail = await _bannerDetailRepository.CreateBannerDetailAsync(newBannerDetail);
            var bannerDetailDTO = createdBannerDetail.ToBannerDetailDTO();
            return CreatedAtAction(nameof(GetBannerDetailById), new { id = bannerDetailDTO.Id }, bannerDetailDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBannerDetail([FromBody] UpdateBannerDetailResponseDTO updateBannerDetailResponseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bannerDetailToUpdate = updateBannerDetailResponseDTO.ToUpdateBannerDetailResponseDTO();
            var updatedBannerDetail = await _bannerDetailRepository.UpdateBannerDetailAsync(bannerDetailToUpdate, id);
            if (updatedBannerDetail == null)
            {
                return NotFound("Banner Detail not found");
            }
            var bannerDetailDTO = updatedBannerDetail.ToBannerDetailDTO();
            return Ok(bannerDetailDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBannerDetail(int id)
        {
            var deletedBannerDetail = await _bannerDetailRepository.DeleteBannerDetailAsync(id);
            if (deletedBannerDetail == null)
            {
                return NotFound("Banner Detail ID not found");
            }
            return Ok("Banner Detail deleted successfully!");
        }
    }
}
