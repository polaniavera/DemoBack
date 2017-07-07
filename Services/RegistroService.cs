using AutoMapper;
using DAL;
using DAL.UnitOfWork;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Services
{
    public class RegistroService : IRegistroService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public RegistroService()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches registro details by id
        /// </summary>
        /// <param name="registroId"></param>
        /// <returns></returns>
        public RegistroEntity GetRegistroById(int registroId)
        {
            var registro = _unitOfWork.RegistroRepository.GetByID(registroId);
            if (registro != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Registro, RegistroEntity>(); });
                IMapper mapper = config.CreateMapper();

                var registroModel = mapper.Map<Registro, RegistroEntity>(registro);
                return registroModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the registros.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegistroEntity> GetAllRegistros()
        {
            var registros = _unitOfWork.RegistroRepository.GetAll().ToList();
            if (registros.Any())
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Registro, RegistroEntity>(); });
                IMapper mapper = config.CreateMapper();

                var registrosModel = mapper.Map<List<Registro>, List<RegistroEntity>>(registros);
                return registrosModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a registro
        /// </summary>
        /// <param name="registroEntity"></param>
        /// <returns></returns>
        public int CreateRegistro(RegistroEntity registroEntity)
        {
            using (var scope = new TransactionScope())
            {
                var registro = new Registro
                {
                    BotonPanico = registroEntity.BotonPanico,
                    Fecha = registroEntity.Fecha,
                    Hora = registroEntity.Hora,
                    IdItem = registroEntity.IdItem,
                    Nivel = registroEntity.Nivel,
                    Odometro = registroEntity.Odometro,
                    Presion = registroEntity.Presion,
                    Temperatura = registroEntity.Temperatura
                };
                _unitOfWork.RegistroRepository.Insert(registro);
                _unitOfWork.Save();
                scope.Complete();
                return registro.IdRegistro;
            }
        }

        /// <summary>
        /// Updates a registro
        /// </summary>
        /// <param name="registroId"></param>
        /// <param name="registroEntity"></param>
        /// <returns></returns>
        public bool UpdateRegistro(int registroId, RegistroEntity registroEntity)
        {
            var success = false;
            if (registroEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var registro = _unitOfWork.RegistroRepository.GetByID(registroId);
                    if (registro != null)
                    {
                        registro.BotonPanico = registroEntity.BotonPanico;
                        registro.Fecha = registroEntity.Fecha;
                        registro.Hora = registroEntity.Hora;
                        registro.IdItem = registroEntity.IdItem;
                        registro.Nivel = registroEntity.Nivel;
                        registro.Odometro = registroEntity.Odometro;
                        registro.Presion = registroEntity.Presion;
                        registro.Temperatura = registroEntity.Temperatura;

                        _unitOfWork.RegistroRepository.Update(registro);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular registro
        /// </summary>
        /// <param name="registroId"></param>
        /// <returns></returns>
        public bool DeleteRegistro(int registroId)
        {
            var success = false;
            if (registroId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var registro = _unitOfWork.RegistroRepository.GetByID(registroId);
                    if (registro != null)
                    {
                        _unitOfWork.RegistroRepository.Delete(registro);
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
