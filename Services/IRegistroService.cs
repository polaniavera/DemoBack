using Entities;
using System.Collections.Generic;

namespace Services
{
    public interface IRegistroService
    {
        RegistroEntity GetRegistroById(int registroId);
        IEnumerable<RegistroEntity> GetAllRegistros();
        int CreateRegistro(RegistroEntity registroEntity);
        bool UpdateRegistro(int registroId, RegistroEntity registroEntity);
        bool DeleteRegistro(int registroId);
    }
}
