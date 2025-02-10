using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.News;
using api.Models;

namespace api.Mappers
{
    public static class NewsMappers
    {
        public static NewsDTO ToNewsDTO(this News news){
            return new NewsDTO{
                Id = news.Id,
                Title = news.Title,
                Image = news.Image,
                Content = news.Content,
                CreatedAt = news.CreatedAt,
                UpdatedAt = news.UpdatedAt
            };
        }
        public static News ToCreateNewsResponseDTO(this News news){
            return new News{
                Title = news.Title,
                Image = news.Image,
                Content = news.Content,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static News ToUpdateNewsResponseDTO(this News news){
            return new News{
                Title = news.Title,
                Image = news.Image,
                Content = news.Content,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}