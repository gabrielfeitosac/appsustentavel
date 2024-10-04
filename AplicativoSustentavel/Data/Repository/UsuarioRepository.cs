using AplicativoSustentavel.Context;
using AplicativoSustentavel.Data.Common;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Data.Repository
{
    public class UsuarioRepository : CoreRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository()
        {
            DbContext = AppSustentavelContext.Current;
        }

        public Usuario GetLoginApp(string login, string senha)
        {
            try
            {
                var usuario = _dbContext.Conexao.FindWithQuery<Usuario>("SELECT * FROM Usuario WHERE Ativo = 1 AND Login = ? AND Senha = ?", login, senha);

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool GetUsuarioByLogin(string login)
        {
            try
            {
                var usuarioExiste = _dbContext.Conexao.FindWithQuery<Usuario>("SELECT * FROM Usuario WHERE Ativo = 1 AND Login = ?", login) != null;

                return usuarioExiste;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
