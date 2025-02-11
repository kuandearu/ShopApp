using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Banner;
using api.Mappers;
using api.Repositories.Banners;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerController(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBanners()
        {
            var banners = await _bannerRepository.GetAllBannersAsync();
            var bannerDTOs = banners.Select(b => b.ToBannerDTO()).ToList();
            return Ok(bannerDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBannerById(int id)
        {
            var banner = await _bannerRepository.GetBannerByIdAsync(id);
            if (banner == null)
            {
                return NotFound("Banner ID not found");
            }
            var bannerDTO = banner.ToBannerDTO();
            return Ok(bannerDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddBanner([FromBody] CreateBannerResponseDTO createBannerResponseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newBanner = createBannerResponseDTO.ToCreateBannerResponseDTO();
            var createdBanner = await _bannerRepository.CreateBannerAsync(newBanner);
            var bannerDTO = createdBanner.ToBannerDTO();
            return CreatedAtAction(nameof(GetBannerById), new { id = bannerDTO.Id }, bannerDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBanner([FromBody] UpdateBannerResponseDTO updateBannerResponseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bannerToUpdate = updateBannerResponseDTO.ToUpdateBannerResponseDTO();
            var updatedBanner = await _bannerRepository.UpdateBannerAsync(bannerToUpdate, id);
            if (updatedBanner == null)
            {
                return NotFound("Banner not found");
            }
            var bannerDTO = updatedBanner.ToBannerDTO();
            return Ok(bannerDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var deletedBanner = await _bannerRepository.DeleteBannerAsync(id);
            if (deletedBanner == null)
            {
                return NotFound("Banner ID not found");
            }
            return Ok("Banner deleted successfully!");
        }
    }
}
