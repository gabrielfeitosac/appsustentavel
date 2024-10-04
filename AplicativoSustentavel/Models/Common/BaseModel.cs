using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Models.Common
{
    public class BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();

        //Flag de exclusão lógica
        public bool Ativo { get; set; }
    }
}
