using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } //adicionamos essa propriedade porque não existe no IdentityUser

        //Um usuário pode ter várias tarefas, essa ForeignKey foi declarada na entidade Tarefa
        //Pois uma tarefa pertence a um usuário
        [ForeignKey("UsuarioId")]
        public virtual ICollection<Tarefa> Tarefas { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
