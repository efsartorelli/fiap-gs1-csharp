using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Dod2233.Models;
using Dod2233.Repositories;

namespace Dod2233.Services
{
    public class FalhaService
    {
        private RepositorioJson<FalhaEnergetica> repoFalhas;
        private RepositorioJson<Alerta> repoAlertas;

        private List<FalhaEnergetica> falhas;
        private List<Alerta> alertas;

        public FalhaService()
        {
            repoFalhas = new RepositorioJson<FalhaEnergetica>("falhas.json");
            repoAlertas = new RepositorioJson<Alerta>("alertas.json");

            falhas = repoFalhas.ObterTodos();
            alertas = repoAlertas.ObterTodos();
        }

        public void RegistrarFalha(FalhaEnergetica falha)
        {
            if (falha == null) throw new ArgumentNullException(nameof(falha));
            if (string.IsNullOrWhiteSpace(falha.TipoFalha)) throw new ArgumentException("Tipo da falha obrigatório");
            if (falha.DataHoraFalha > DateTime.Now) throw new ArgumentException("Data não pode ser futura");

            falhas.Add(falha);
            repoFalhas.Adicionar(falha);

            var (nivel, geraAlerta) = AvaliarTipoFalha(falha.TipoFalha);

            if (geraAlerta && nivel.HasValue)
            {
                var alerta = new Alerta(falha.TipoFalha, $"Falha do tipo '{falha.TipoFalha}' registrada: {falha.Descricao}", nivel.Value);
                alertas.Add(alerta);
                repoAlertas.Adicionar(alerta);
                Console.WriteLine($"Alerta gerado: {alerta.Mensagem} (Nível: {alerta.Nivel})");
            }
            else
            {
                Console.WriteLine("Falha registrada sem geração de alerta.");
            }
        }

        public (NivelAlerta? nivel, bool geraAlerta) AvaliarTipoFalha(string tipoFalha)
        {
            if (string.IsNullOrWhiteSpace(tipoFalha))
                return (null, false);

            var tipoLower = tipoFalha.ToLower();

            if (tipoLower.Contains("queda"))
                return (NivelAlerta.Alto, true);
            if (tipoLower.Contains("curto-circuito") || tipoLower.Contains("curto circuito"))
                return (NivelAlerta.Alto, true);
            if (tipoLower.Contains("oscilação") || tipoLower.Contains("oscilacao"))
                return (NivelAlerta.Medio, true);
            if (tipoLower.Contains("sobretensão") || tipoLower.Contains("sobretensao"))
                return (NivelAlerta.Medio, true);
            if (tipoLower.Contains("subtensão") || tipoLower.Contains("subtensao"))
                return (NivelAlerta.Medio, true);
            if (tipoLower.Contains("intermitente"))
                return (NivelAlerta.Baixo, true);
            if (tipoLower.Contains("falha mecânica") || tipoLower.Contains("falha mecanica"))
                return (NivelAlerta.Baixo, true);
            if (tipoLower.Contains("manutenção") || tipoLower.Contains("manutencao"))
                return (null, false);  // Não gera alerta
            if (tipoLower.Contains("desconhecida"))
                return (NivelAlerta.Baixo, true);

            return (null, false);  // Caso padrão
        }

        public List<FalhaEnergetica> ObterFalhas()
        {
            return new List<FalhaEnergetica>(falhas);
        }

        public List<Alerta> ObterAlertas(bool somenteAbertos = false)
        {
            if (somenteAbertos)
                return alertas.FindAll(a => !a.Resolvido);
            return new List<Alerta>(alertas);
        }

        public void MarcarAlertaComoResolvido(Guid id)
        {
            var alerta = alertas.Find(a => a.Id == id);
            if (alerta != null)
            {
                alerta.Resolvido = true;
                repoAlertas.Adicionar(alerta);  // Atualiza o JSON
                Console.WriteLine($"Alerta {id} marcado como resolvido.");
            }
            else
            {
                Console.WriteLine("Alerta não encontrado.");
            }
        }

        // NOVOS MÉTODOS PARA LIMPAR DADOS

        public void LimparFalhas()
        {
            falhas.Clear();
            SalvarListaFalhas();
            Console.WriteLine("Todas as falhas foram apagadas.");
        }

        public void LimparAlertas()
        {
            alertas.Clear();
            SalvarListaAlertas();
            Console.WriteLine("Todos os alertas foram apagados.");
        }

        private void SalvarListaFalhas()
        {
            var json = JsonSerializer.Serialize(falhas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("falhas.json", json);
        }

        private void SalvarListaAlertas()
        {
            var json = JsonSerializer.Serialize(alertas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("alertas.json", json);
        }
    }
}
