using System.Collections.Generic;
using Dod2233.Models;

namespace Dod2233.Services
{
    public class LoginService
    {
        private List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario("admin", "1234"),
            new Usuario("user", "abcd")
        };

        public bool Autenticar(string username, string senha)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.Username == username && usuario.Senha == senha)
                    return true;
            }
            return false;
        }
    }
}