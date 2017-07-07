using Entities;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoBack.Controllers
{
    public class ItemController : ApiController
    {
        private readonly IItemService _itemServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize item service instance
        /// </summary>
        public ItemController()
        {
            _itemServices = new ItemService();
        }

        #endregion

        // GET api/item
        public HttpResponseMessage Get()
        {
            var items = _itemServices.GetAllItems();
            if (items != null)
            {
                var itemEntities = items as List<ItemEntity> ?? items.ToList();
                if (itemEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, itemEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Items not found");
        }

        // GET api/item/5
        public HttpResponseMessage Get(int id)
        {
            var item = _itemServices.GetItemById(id);
            if (item != null)
                return Request.CreateResponse(HttpStatusCode.OK, item);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found for this id");
        }

        // POST api/item
        public int Post([FromBody] ItemEntity itemEntity)
        {
            return _itemServices.CreateItem(itemEntity);
        }

        // PUT api/item/5
        public bool Put(int id, [FromBody]ItemEntity itemEntity)
        {
            if (id > 0)
            {
                return _itemServices.UpdateItem(id, itemEntity);
            }
            return false;
        }

        // DELETE api/item/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _itemServices.DeleteItem(id);
            return false;
        }
    }
}
