using Microsoft.AspNetCore.Identity;
using System;

namespace ControleFinanceiro.Seguranca
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
