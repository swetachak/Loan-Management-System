using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Http.Features;

namespace LMS.Services
{
    public class ItemDataManagementService: IItemDataManagementService
    {
        private readonly ItemMasterProvider _provider;

        public ItemDataManagementService(ItemMasterProvider provider)
        {
            _provider= provider;
        }

        public async Task<IEnumerable<ItemMaster>> GetItemMastersAsync()
        {
            return await _provider.GetItemMastersAsync();
        }

        public async Task<ItemMaster> GetItemMasterByIdAsync(string itemId)
        {
            return await _provider.GetItemMasterByIdAsync(itemId);
        }

        public async Task<Guid> CreateItemMasterAsync(ItemMaster item)
        {
            return await _provider.CreateItemMasterAsync(item);
        }

        public async Task UpdateItemMasterAsync(string id ,ItemMaster item)
        {
            var _item = await _provider.GetItemMasterByIdAsync(id);
            if(_item != null)
            {
                _item.IssueStatus = item.IssueStatus;
                _item.ItemDescription = item.ItemDescription;
                _item.ItemValuation = item.ItemValuation;
                _item.ItemMake = item.ItemMake;
                _item.ItemCategory = item.ItemCategory;
                await _provider.UpdateItemMasterAsync(_item);
            }
        }

        public async Task DeleteItemMasterAsync(string itemId)
        {
            await _provider.DeleteItemMasterAsync(itemId);
        }
    }
}
