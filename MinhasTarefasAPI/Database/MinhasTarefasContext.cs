using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhasTarefasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Database
{
    //Rodamos no Console do Gerenciador de Pacotes (está no menu ferramentas) : 
    //Add-Migration BancoInicial, assim criamos o banco
    //Após a criação rodar o comando Update-Database
    public class MinhasTarefasContext : IdentityDbContext<ApplicationUser> //mudamos para IdentityDbContext, pois vamos usar usuário para login
    {
        public MinhasTarefasContext(DbContextOptions<MinhasTarefasContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; } //tabela tarefas
        public DbSet<Token> Token { get; set; }
    }
}
