using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dod2233.Repositories
{
    public class RepositorioJson<T> where T : class
    {
        private readonly string caminhoArquivo;
        private List<T> dadosCache;

        public RepositorioJson(string caminhoArquivo)
        {
            this.caminhoArquivo = caminhoArquivo;
            dadosCache = CarregarArquivo();
        }

        private List<T> CarregarArquivo()
        {
            if (!File.Exists(caminhoArquivo))
                return new List<T>();

            var json = File.ReadAllText(caminhoArquivo);
            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        private void SalvarArquivo()
        {
            var json = JsonSerializer.Serialize(dadosCache, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, json);
        }

        public void Adicionar(T item)
        {
            dadosCache.Add(item);
            SalvarArquivo();
        }

        public List<T> ObterTodos()
        {
            return new List<T>(dadosCache);
        }
    }
}