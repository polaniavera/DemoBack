using AutoMapper;
using DAL;
using DAL.UnitOfWork;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ItemService()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches item details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ItemEntity GetItemById(int itemId)
        {
            var item = _unitOfWork.ItemRepository.GetByID(itemId);
            if (item != null)
            {
                var config = new MapperConfiguration(cfg => {cfg.CreateMap<Item, ItemEntity>();});
                IMapper mapper = config.CreateMapper();
                
                var productModel = mapper.Map<Item, ItemEntity>(item);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemEntity> GetAllItems()
        {
            var items = _unitOfWork.ItemRepository.GetAll().ToList();
            if (items.Any())
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Item, ItemEntity>(); });
                IMapper mapper = config.CreateMapper();
                
                var itemsModel = mapper.Map<List<Item>, List<ItemEntity>>(items);
                return itemsModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a item
        /// </summary>
        /// <param name="itemEntity"></param>
        /// <returns></returns>
        public int CreateItem(ItemEntity itemEntity)
        {
            using (var scope = new TransactionScope())
            {
                var item = new Item
                {
                    Informacion = itemEntity.Informacion,
                    Latitud = itemEntity.Latitud,
                    Longitud = itemEntity.Longitud,
                    NombreUsuario = itemEntity.NombreUsuario,
                    TipoItem = itemEntity.TipoItem
                };
                _unitOfWork.ItemRepository.Insert(item);
                _unitOfWork.Save();
                scope.Complete();
                return item.IdItem;
            }
        }

        /// <summary>
        /// Updates a item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemEntity"></param>
        /// <returns></returns>
        public bool UpdateItem(int itemId, ItemEntity itemEntity)
        {
            var success = false;
            if (itemEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var item = _unitOfWork.ItemRepository.GetByID(itemId);
                    if (item != null)
                    {
                        item.Informacion = itemEntity.Informacion;
                        item.Latitud = itemEntity.Latitud;
                        item.Longitud = itemEntity.Longitud;
                        item.NombreUsuario = itemEntity.NombreUsuario;
                        item.TipoItem = itemEntity.TipoItem;

                        _unitOfWork.ItemRepository.Update(item);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular item
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteItem(int itemId)
        {
            var success = false;
            if (itemId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var item = _unitOfWork.ItemRepository.GetByID(itemId);
                    if (item != null)
                    {
                        _unitOfWork.ItemRepository.Delete(item);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
