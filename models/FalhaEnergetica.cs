using System;

namespace Dod2233.Models
{
    public class FalhaEnergetica
    {
        public Guid Id { get; set; }
        public DateTime DataHoraFalha { get; set; }
        public string TipoFalha { get; set; }
        public string Descricao { get; set; }
    }
}