using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Feedback;
using api.Models;

namespace api.Mappers
{
    public static class FeedbackMappers
    {
        public static FeedbackDTO ToFeedbackDTO(this Feedback feedback){
            return new FeedbackDTO {
                Id = feedback.Id,
                ProductId = feedback.ProductId,
                UserId = feedback.UserId,
                Star = feedback.Star,
                Content = feedback.Content,
                CreatedAt = feedback.CreatedAt,
                UpdatedAt = feedback.UpdatedAt
            };
        }

        public static Feedback ToCreateFeedbackResponseDTO(this CreateFeedbackResponseDTO dTO){
            return new Feedback {
                ProductId = dTO.ProductId,
                UserId = dTO.UserId,
                Star = dTO.Star,
                Content = dTO.Content,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Feedback ToUpdateFeedbackResponseDTO(this UpdateFeedbackResponseDTO dTO){
            return new Feedback {
                Star = dTO.Star,
                Content = dTO.Content,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}