using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Admin;
using api.Mappers;
using api.Repositories.Admins;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(){
            var admins = await _adminRepository.GetAllAdminsAsync();
            var adminDTO = admins.Select(a => a.ToAdminDTO()).ToList();
            return Ok(adminDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAdminById(int id){
            var admin = await _adminRepository.GetAdminByIdAsync(id);
            if(admin == null) {
                return BadRequest("ID not found");
            }
            var adminDTO = admin.ToAdminDTO();
            return Ok(adminDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Add([FromBody] CreateAdminREsponseDTO dTO){
            var newAdmin = dTO.ToCreateAdminResponseDTO();
            var createdAdmin = await _adminRepository.CreateAdminAsync(newAdmin);
            var adminDTO = createdAdmin.ToAdminDTO();
            return CreatedAtAction(nameof(GetAdminById), new {id = adminDTO.Id}, adminDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateAdminResponseDTO dTO, int id){
            var adminToUpdate = dTO.ToUpdateAdminResponseDTO();
            var updatedAdmin = await _adminRepository.UpdateAdminAsync(adminToUpdate, id);
            if(updatedAdmin == null){
                return NotFound("Id not found!");
            }
            var adminDTO = updatedAdmin.ToAdminDTO();
            return Ok(adminDTO);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id){
            var deletedAdmin = await _adminRepository.DeleteAdminAsync(id);
            if(deletedAdmin == null){
                return NotFound("Id not found!");
            }
            return Ok("Delete admin successfully!");

        }
    }
}