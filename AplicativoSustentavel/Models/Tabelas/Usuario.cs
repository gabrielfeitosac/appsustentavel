using AplicativoSustentavel.Models.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Models.Tabelas
{
    [Table("USUARIO")]
    public  class Usuario : BaseModel
    {
        public string Login { get; set; }

        public string Senha { get; set; }

        public string NomeCompleto { get; set; }

        public bool UsuarioSalvoLogado { get; set; }

        public string SenhaSalvaLogin{ get; set; }
    }
}
