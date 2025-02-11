using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Feedback
{
    public class UpdateFeedbackResponseDTO
    {
        [Range(1, 5, ErrorMessage = "Star rating must be between 1 and 5.")]
        public int Star { get; set; }
        public string Content { get; set; }
    }
}