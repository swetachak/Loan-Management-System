using LMS.Data;
using LMS.Models;

namespace LMS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryProvider _provider;

        public CategoryService(CategoryProvider provider)
        {
            _provider = provider;
        }
        public async Task<List<string>> GetAllCategoriesAsync()
        { 
            return await _provider.GetAllCategoriesAsync();
        }
        public async Task<List<string>> GetAllMaterialsByCategoryAsync(string categoryName)
        {
            return await _provider.GetAllMaterialsByCategoryAsync(categoryName);
        }
        public async Task<List<int?>> GetAvailableTenuresForCategoryAsync(string categoryName)
        {
            return await _provider.GetAvailableTenuresForCategoryAsync(categoryName);
        }
    }
}
