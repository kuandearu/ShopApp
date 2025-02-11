using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.Repositories.Newss
{
    public interface INewsRepository
    {
        Task<IEnumerable<Models.News>> GetAllNewsAsync();
        Task<Models.News?> GetNewsByIdAsync(int id);
        Task<Models.News> CreateNewsAsync(Models.News news);
        Task<Models.News?> UpdateNewsAsync(Models.News news, int id);
        Task<Models.News?> DeleteNewsAsync(int id);
    }
}