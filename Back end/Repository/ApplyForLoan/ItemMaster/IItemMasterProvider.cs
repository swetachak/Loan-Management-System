using LMS.Models;

namespace LMS.Data
{
    public interface IItemMasterProvider
    {
        Task<List<ItemMaster>> GetItemMastersAsync();
        Task<ItemMaster> GetItemMasterByIdAsync(string itemId);
        Task<Guid> CreateItemMasterAsync(ItemMaster item);
        Task UpdateItemMasterAsync(ItemMaster item);
        Task DeleteItemMasterAsync(string itemId);
    }
}
