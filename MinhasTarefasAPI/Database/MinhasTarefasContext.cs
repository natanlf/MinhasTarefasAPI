using Microsoft.EntityFrameworkCore;
using MinhasTarefasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Database
{
    public class MinhasTarefasContext : DbContext //DbContext para usar o banco de dados
    {
        public MinhasTarefasContext(DbContextOptions<MinhasTarefasContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; } //tabela tarefas
    }
}
