using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Seguranca
{
    public class UsersContexto : IdentityDbContext<Usuario>
    {
        public UsersContexto(DbContextOptions<UsersContexto> options) : base(options)
        {

        }
    }
}
