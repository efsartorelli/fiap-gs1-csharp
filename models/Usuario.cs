namespace Dod2233.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Senha { get; set; }  // Em produção, use hash e não texto puro!

        public Usuario(string username, string senha)
        {
            Username = username;
            Senha = senha;
        }
    }
}