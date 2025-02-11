using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.NewsDetail;
using api.Models;

namespace api.Mappers
{
    public static class NewsDetailMappers
    {
        public static NewsDetailDTO ToNewsDetailDTO(this NewsDetail newsDetail){
            return new NewsDetailDTO{
                Id = newsDetail.Id,
                ProductId = newsDetail.ProductId,
                NewsId = newsDetail.NewsId,
                CreatedAt = newsDetail.CreatedAt,
                UpdatedAt = newsDetail.UpdatedAt
            };
        }

        public static NewsDetail ToCreateNewsDetailResponseDTO(this CreateNewsDetailResponseDTO dTO){
            return new NewsDetail {
                ProductId = dTO.ProductId,
                NewsId = dTO.NewsId,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static NewsDetail ToUpdateNewsDetailResponseDTO(this UpdateNewsDetailResponseDTO dTO){
            return new NewsDetail {
                ProductId = dTO.ProductId,
                NewsId = dTO.NewsId,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}