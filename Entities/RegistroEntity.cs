using System;

namespace Entities
{
    public class RegistroEntity
    {
        public int IdRegistro { get; set; }
        public Nullable<int> IdItem { get; set; }
        public Nullable<decimal> Nivel { get; set; }
        public Nullable<int> Odometro { get; set; }
        public Nullable<decimal> Temperatura { get; set; }
        public Nullable<decimal> Presion { get; set; }
        public Nullable<bool> BotonPanico { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.TimeSpan> Hora { get; set; }
    }
}
