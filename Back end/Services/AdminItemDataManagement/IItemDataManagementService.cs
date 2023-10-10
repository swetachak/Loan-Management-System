using LMS.Models;

namespace LMS.Services
{
    public interface IItemDataManagementService
    {
        Task<IEnumerable<ItemMaster>> GetItemMastersAsync();
        Task<ItemMaster> GetItemMasterByIdAsync(string itemId);
        Task<Guid> CreateItemMasterAsync(ItemMaster item);
        Task UpdateItemMasterAsync(string id ,ItemMaster item);
        Task DeleteItemMasterAsync(string itemId);
    }
}
