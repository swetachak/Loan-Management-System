using LMS.Models;

namespace LMS.Services
{
    public interface ICategoryService
    {
        Task<List<string>> GetAllCategoriesAsync();
        Task<List<string>> GetAllMaterialsByCategoryAsync(string categoryName);
        Task<List<int?>> GetAvailableTenuresForCategoryAsync(string categoryName);
    }
}
