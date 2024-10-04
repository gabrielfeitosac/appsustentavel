using AplicativoSustentavel.Data.Common;
using AplicativoSustentavel.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Data.Interface
{
    public interface IUsuarioRepository : ICoreRepository<Usuario>
    {
        Usuario GetLoginApp(string login, string senha);
        bool GetUsuarioByLogin(string login);
    }
}
