namespace Dod2233.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Senha { get; set; }  // Em produ��o, use hash e n�o texto puro!

        public Usuario(string username, string senha)
        {
            Username = username;
            Senha = senha;
        }
    }
}