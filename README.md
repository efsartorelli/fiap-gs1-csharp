===============================
🚨 SISTEMA DE MONITORAMENTO DE FALHAS ENERGÉTICAS
===============================

📌 Descrição
Aplicação console desenvolvida em C# (.NET 9.0) para registrar e monitorar falhas energéticas que podem impactar a cibersegurança em infraestruturas críticas. O sistema gera alertas automáticos classificados por níveis de severidade, permite gerenciamento dos incidentes, controle de acesso via login e persistência local via arquivos JSON. Projeto acadêmico focado em automação, segurança e análise de incidentes.

---

🛠️ Tecnologias Utilizadas

- C# (.NET 9.0)
- Console Application
- JSON (serialização para persistência de dados)
- Programação orientada a objetos (encapsulamento, herança, coesão)

---

⚙️ Funcionalidades do Sistema

📋 Registro de Falhas
- Registro de falhas com tipo, descrição e data/hora
- Validação rigorosa de dados e tratamento de exceções

🚨 Geração Automática de Alertas
- Alertas criados automaticamente com base no tipo da falha
- Classificação em níveis: Baixo, Médio, Alto
- Registro e histórico completo de alertas

👤 Controle de Acesso
- Login simples para garantir acesso autorizado

📊 Gerenciamento de Alertas
- Listagem de alertas: todos, abertos, resolvidos
- Marcar alertas como resolvidos
- Limpeza completa dos dados de falhas e alertas

---

📁 Estrutura do Projeto

Dod2233/
├── Models/                → Classes de domínio (FalhaEnergetica, Alerta, Usuario)
├── Repositories/          → Repositório JSON para persistência
├── Services/              → Lógica de negócios (FalhaService, LoginService)
├── Program.cs             → Interface de interação via console
├── Dod2233.csproj         → Projeto .NET
└── README.md              → Documentação do projeto

---

▶️ Como Executar o Projeto

1. Clone o repositório:
   git clone [URL-DO-REPOSITORIO]

2. Entre na pasta do projeto:
   cd Dod2233

3. Compile e rode o projeto:
   dotnet build
   dotnet run

4. Faça login com usuário e senha padrão (ex: admin / 1234)
5. Use o menu para registrar falhas, listar alertas, gerenciar o sistema

---

👥 Login Padrão

Usuário | Senha
--------|-------
admin   | 1234
user    | abcd

---

👨‍💻 Autor

- Nome: EDEZKANIRO
- GitHub: https://github.com/efsartorelli
- Link do vídeo no youtube: https://youtu.be/hgBNQZM_Bhw
- Curso: Engenharia de Software - FIAP 2025

---

📚 Observações Finais

- Projeto acadêmico com foco em automação e segurança de TI
- Dados armazenados localmente em JSON, podendo evoluir para banco de dados
- Código estruturado para fácil manutenção e expansão

Eduardo de Oliveira Nistal - RM94524 Enzo Vazquez Sartorelli - RM94618 

🗓️ Última atualização: 02/06/2025
