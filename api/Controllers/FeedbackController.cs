using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Feedback;
using api.Mappers;
using api.Repositories.Feedbacks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();
            var feedbackDTOs = feedbacks.Select(f => f.ToFeedbackDTO()).ToList();
            return Ok(feedbackDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return BadRequest("Feedback ID not found");
            }
            var feedbackDTO = feedback.ToFeedbackDTO();
            return Ok(feedbackDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddFeedback([FromBody] CreateFeedbackResponseDTO createFeedbackResponseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newFeedback = createFeedbackResponseDTO.ToCreateFeedbackResponseDTO();
            var createdFeedback = await _feedbackRepository.CreateFeedbackAsync(newFeedback);
            var feedbackDTO = createdFeedback.ToFeedbackDTO();
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedbackDTO.Id }, feedbackDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackResponseDTO updateFeedbackResponseDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var feedbackToUpdate = updateFeedbackResponseDTO.ToUpdateFeedbackResponseDTO();
            var updatedFeedback = await _feedbackRepository.UpdateFeedbackAsync(feedbackToUpdate, id);
            if (updatedFeedback == null)
            {
                return NotFound("Feedback not found");
            }
            var feedbackDTO = updatedFeedback.ToFeedbackDTO();
            return Ok(feedbackDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var deletedFeedback = await _feedbackRepository.DeleteFeedbackAsync(id);
            if (deletedFeedback == null)
            {
                return NotFound("Feedback ID not found");
            }
            return Ok("Feedback deleted successfully!");
        }
    }
}
