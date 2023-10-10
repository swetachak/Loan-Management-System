using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class CategoryProvider : ICategoryProvider
    {
        private readonly Lms3Context _context; // Replace YourDbContext with your actual DbContext class

        public CategoryProvider(Lms3Context context)
        {       
            _context = context;
        }

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                       .Select(cat => cat.Category1)
                       .ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Category1 == categoryName);
        }
        public async Task<List<string>> GetAllMaterialsByCategoryAsync(string categoryName)
        {
            return await _context.Categories
                    .Include(cat => cat.Materials)
                    .Where(cat => cat.Category1 == categoryName)
                    .SelectMany(cat => cat.Materials.Select(m => m.Material1))
                    .ToListAsync();
        }

        public async Task<List<int?>> GetAvailableTenuresForCategoryAsync(string categoryName)
        { 
            var tenures = await _context.Categories
                    .Include(cat => cat.LoanCardMasters)
                    .Where(cat => cat.Category1 == categoryName)
                    .SelectMany(cat => cat.LoanCardMasters.Select(lcm => lcm.DurationInYears))
                    .ToListAsync();
            return tenures;
        }
    }
}
