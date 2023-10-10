using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class ItemMasterProvider : IItemMasterProvider
    {
        private readonly Lms3Context _context;
        
        public ItemMasterProvider(Lms3Context context) 
        {
            _context = context;
        }
        public async Task<List<ItemMaster>> GetItemMastersAsync()
        {
            return await _context.ItemMasters.ToListAsync();
        }

        public async Task<ItemMaster> GetItemMasterByIdAsync(string itemId)
        {
            Guid ConvItemId;
            Guid.TryParse(itemId, out ConvItemId);
            return await _context.ItemMasters.FirstOrDefaultAsync(i => i.ItemId == ConvItemId);
        }

        public async Task<Guid> CreateItemMasterAsync(ItemMaster item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.ItemMasters.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.ItemId;
        }

        public async Task UpdateItemMasterAsync(ItemMaster item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemMasterAsync(string itemId)
        {
            Guid ConvItemId;
            Guid.TryParse(itemId, out ConvItemId);
            var itemToDelete = await _context.ItemMasters.FirstOrDefaultAsync(i => i.ItemId == ConvItemId);
            if (itemToDelete != null)
            {
                _context.ItemMasters.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }

}
