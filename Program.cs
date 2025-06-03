using System;
using Dod2233.Models;
using Dod2233.Services;

namespace Dod2233
{
    class Program
    {
        static void Main(string[] args)
        {
            var loginService = new LoginService();
            bool autenticado = false;

            while (!autenticado)
            {
                Console.Clear();
                Console.Write("Usuário: ");
                string user = Console.ReadLine();

                Console.Write("Senha: ");
                string pass = Console.ReadLine();

                if (loginService.Autenticar(user, pass))
                {
                    autenticado = true;
                    Console.WriteLine("Login efetuado com sucesso!\n-------------");
                }
                else
                {
                    Console.WriteLine("Usuário ou senha incorretos. Tente novamente.\n-------------");
                    Console.ReadKey();
                }
            }

            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            var falhaService = new FalhaService();

            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("===== Sistema de Monitoramento de Falhas =====");
                Console.WriteLine("1 - Registrar nova falha");
                Console.WriteLine("2 - Listar falhas registradas");
                Console.WriteLine("3 - Listar todos os alertas");
                Console.WriteLine("4 - Listar alertas abertos");
                Console.WriteLine("5 - Listar alertas resolvidos");
                Console.WriteLine("6 - Marcar alerta como resolvido");
                Console.WriteLine("7 - Mostrar tipos de falha e nível (gera alerta?)");
                Console.WriteLine("8 - Limpar todas as falhas e alertas");
                Console.WriteLine("9 - Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        RegistrarFalha(falhaService);
                        break;
                    case "2":
                        Console.Clear();
                        ListarFalhas(falhaService);
                        break;
                    case "3":
                        Console.Clear();
                        ListarAlertas(falhaService, false, false);
                        break;
                    case "4":
                        Console.Clear();
                        ListarAlertas(falhaService, true, false);
                        break;
                    case "5":
                        Console.Clear();
                        ListarAlertas(falhaService, false, true);
                        break;
                    case "6":
                        Console.Clear();
                        MarcarAlertaResolvido(falhaService);
                        break;
                    case "7":
                        Console.Clear();
                        MostrarTiposFalha(falhaService);
                        break;
                    case "8":
                        Console.Clear();
                        LimparDados(falhaService);
                        break;
                    case "9":
                        sair = true;
                        Console.Clear();
                        Console.WriteLine("Encerrando o programa...\n-------------");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida! Tente novamente.\n-------------");
                        Console.ReadKey();
                        break;
                }
                if (!sair)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void RegistrarFalha(FalhaService falhaService)
        {
            Console.Clear();
            try
            {
                Console.Write("Digite o tipo da falha: ");
                string tipo = Console.ReadLine();

                Console.Write("Digite a descrição da falha: ");
                string descricao = Console.ReadLine();

                Console.Write("Digite a data/hora da falha (dd/MM/yyyy HH:mm): ");
                string dataHoraStr = Console.ReadLine();

                if (!DateTime.TryParseExact(dataHoraStr, "dd/MM/yyyy HH:mm", null,
                    System.Globalization.DateTimeStyles.None, out DateTime dataHora))
                {
                    Console.WriteLine("Data/hora inválida. Use o formato dd/MM/yyyy HH:mm\n-------------");
                    return;
                }

                var falha = new FalhaEnergetica
                {
                    Id = Guid.NewGuid(),
                    TipoFalha = tipo,
                    Descricao = descricao,
                    DataHoraFalha = dataHora
                };

                falhaService.RegistrarFalha(falha);
                Console.WriteLine("Falha registrada com sucesso!\n-------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no registro da falha: " + ex.Message + "\n-------------");
            }
        }

        static void ListarFalhas(FalhaService falhaService)
        {
            Console.Clear();
            var falhas = falhaService.ObterFalhas();
            if (falhas.Count == 0)
            {
                Console.WriteLine("Nenhuma falha registrada.\n-------------");
                return;
            }

            Console.WriteLine("\nFalhas registradas:");
            foreach (var f in falhas)
            {
                Console.WriteLine($"ID: {f.Id} | Tipo: {f.TipoFalha} | Data: {f.DataHoraFalha} | Descrição: {f.Descricao}");
            }
            Console.WriteLine("-------------");
        }

        static void ListarAlertas(FalhaService falhaService, bool somenteAbertos, bool resolvidos)
        {
            Console.Clear();
            var alertas = resolvidos
                ? falhaService.ObterAlertas().FindAll(a => a.Resolvido)
                : falhaService.ObterAlertas(somenteAbertos);

            if (alertas.Count == 0)
            {
                if (resolvidos) Console.WriteLine("Nenhum alerta resolvido.\n-------------");
                else Console.WriteLine(somenteAbertos ? "Nenhum alerta aberto.\n-------------" : "Nenhum alerta gerado.\n-------------");
                return;
            }

            Console.WriteLine(resolvidos ? "\nAlertas resolvidos:" : (somenteAbertos ? "\nAlertas abertos:" : "\nTodos os alertas:"));
            foreach (var a in alertas)
            {
                Console.WriteLine($"ID: {a.Id} | Tipo: {a.Tipo} | Data: {a.DataHora} | Mensagem: {a.Mensagem} | Nível: {a.Nivel} | Resolvido: {a.Resolvido}");
            }
            Console.WriteLine("-------------");
        }

        static void MarcarAlertaResolvido(FalhaService falhaService)
        {
            Console.Clear();
            Console.Write("Digite o ID do alerta para marcar como resolvido: ");
            string idStr = Console.ReadLine();

            if (Guid.TryParse(idStr, out Guid id))
            {
                falhaService.MarcarAlertaComoResolvido(id);
            }
            else
            {
                Console.WriteLine("ID inválido.\n-------------");
            }
        }

        static void MostrarTiposFalha(FalhaService falhaService)
        {
            Console.Clear();
            Console.WriteLine("\nTipos de falha e nível que geram alerta:\n");
            var exemplos = new[]
            {
                "Queda de energia",
                "Curto-circuito",
                "Oscilação",
                "Sobretensão",
                "Subtensão",
                "Intermitente",
                "Falha mecânica",
                "Manutenção",
                "Falha desconhecida"
            };

            foreach (var tipo in exemplos)
            {
                var (nivel, geraAlerta) = falhaService.AvaliarTipoFalha(tipo);
                string alertaStr = geraAlerta ? "Sim" : "Não";
                string nivelStr = nivel.HasValue ? nivel.ToString()! : "-";
                Console.WriteLine($"- {tipo}: Nível {nivelStr}, Gera alerta? {alertaStr}");
            }
            Console.WriteLine("-------------");
        }

        static void LimparDados(FalhaService falhaService)
        {
            Console.Clear();
            Console.Write("Tem certeza que deseja apagar todas as falhas e alertas? (S/N): ");
            var resposta = Console.ReadLine();
            if (resposta?.Trim().ToUpper() == "S")
            {
                falhaService.LimparFalhas();
                falhaService.LimparAlertas();
                Console.WriteLine("Dados apagados.\n-------------");
            }
            else
            {
                Console.WriteLine("Operação cancelada.\n-------------");
            }
        }
    }
}
