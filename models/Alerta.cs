using System;

namespace Dod2233.Models
{
    public enum NivelAlerta
    {
        Baixo,
        Medio,
        Alto
    }

    public class Alerta
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Mensagem { get; set; }
        public NivelAlerta Nivel { get; set; }
        public bool Resolvido { get; set; }

        public Alerta(string tipo, string mensagem, NivelAlerta nivel)
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
            Tipo = tipo;
            Mensagem = mensagem;
            Nivel = nivel;
            Resolvido = false;
        }
    }
}
