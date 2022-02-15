using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Seguranca
{
    public class RegisterModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel ToLoginModel()
        {
            return new LoginModel()
            {
                UserName = UserName,
                Password = Password
            };
        }
    }
}
