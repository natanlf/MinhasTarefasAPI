using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models
{
    public class Tarefa
    {
        [Key]
        public int IdTarefaApi { get; set; }
        public int IdTarefaApp { get; set; }
        public string Titulo { get; set; }
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public bool Concluido { get; set; }
        public bool Excluido { get; set; }
        public DateTime Criado { get; set; }
        public DateTime Atualizado { get; set; }

        //Informo que isso está relacionado com o objeto Usuario que está logo abaixo
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; } //declaro um nome de fk

        //Permite ter um objeto Usuário, acima é feito o relacionamento
        public virtual ApplicationUser Usuario { get; set; }
    }
}
