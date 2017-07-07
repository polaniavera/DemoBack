using Entities;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoBack.Controllers
{
    public class RegistroController : ApiController
    {
        private readonly IRegistroService _registroServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize item service instance
        /// </summary>
        public RegistroController()
        {
            _registroServices = new RegistroService();
        }

        #endregion

        // GET api/registro
        public HttpResponseMessage Get()
        {
            var registros = _registroServices.GetAllRegistros();
            if (registros != null)
            {
                var registroEntities = registros as List<RegistroEntity> ?? registros.ToList();
                if (registroEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, registroEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registros not found");
        }

        // GET api/registro/5
        public HttpResponseMessage Get(int id)
        {
            var registro = _registroServices.GetRegistroById(id);
            if (registro != null)
                return Request.CreateResponse(HttpStatusCode.OK, registro);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No registro found for this id");
        }

        // POST api/registro
        public int Post([FromBody] RegistroEntity registroEntity)
        {
            return _registroServices.CreateRegistro(registroEntity);
        }

        // PUT api/registro/5
        public bool Put(int id, [FromBody]RegistroEntity registroEntity)
        {
            if (id > 0)
            {
                return _registroServices.UpdateRegistro(id, registroEntity);
            }
            return false;
        }

        // DELETE api/registro/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _registroServices.DeleteRegistro(id);
            return false;
        }
    }
}
