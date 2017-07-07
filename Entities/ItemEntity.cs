using System;

namespace Entities
{
    public class ItemEntity
    {
        public ItemEntity()
        {
        }

        public int IdItem { get; set; }
        public string TipoItem { get; set; }
        public string NombreUsuario { get; set; }
        public Nullable<decimal> Latitud { get; set; }
        public Nullable<decimal> Longitud { get; set; }
        public string Informacion { get; set; }
    }
}
