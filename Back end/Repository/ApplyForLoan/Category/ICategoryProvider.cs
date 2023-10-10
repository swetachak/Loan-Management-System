using LMS.Models;

namespace LMS.Data
{
    public interface ICategoryProvider
    {
        Task<List<string>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByNameAsync (string categoryName);
        Task<List<string>> GetAllMaterialsByCategoryAsync(string categoryName);
        Task<List<int?>> GetAvailableTenuresForCategoryAsync(string categoryName);
    }
}
