using Entities;
using System.Collections.Generic;
namespace Services
{
    public interface IItemService
    {
        ItemEntity GetItemById(int itemId);
        IEnumerable<ItemEntity> GetAllItems();
        int CreateItem(ItemEntity itemEntity);
        bool UpdateItem(int itemId, ItemEntity itemEntity);
        bool DeleteItem(int itemId);
    }
}
