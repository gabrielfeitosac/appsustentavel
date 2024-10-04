using AplicativoSustentavel.Context;
using AplicativoSustentavel.Models.Common;
using System;
using System.Collections.Generic;

namespace AplicativoSustentavel.Data.Common
{
    public class CoreRepository<T> : ICoreRepository<T> where T : BaseModel, new()
    {
        protected AppSustentavelContext _dbContext;

        public AppSustentavelContext DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public void Delete(T model)
        {
            try
            {
                var rs = _dbContext.Conexao.Delete(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll(List<T> model)
        {
            try
            {
                _dbContext.Conexao.BeginTransaction();

                foreach (var _model in model)
                {
                    var rs = _dbContext.Conexao.Delete(_model);

                    if (rs == 0)
                        throw new Exception("Erro ao excluir registro");
                }

                _dbContext.Conexao.Commit();
            }
            catch (Exception ex)
            {
                _dbContext.Conexao.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll()
        {
            _dbContext.Conexao.DeleteAll<T>();
        }

        public void DeleteById(Guid? Id)
        {
            try
            {
                var rs = _dbContext.Conexao.Delete<T>(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<T> GetUsers()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<T> GetAllAtivos()
        {
            try
            {
                return _dbContext.Conexao.Table<T>().Where(t => t.Ativo == true).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public T GetById(Guid Id)
        {
            try
            {
                return _dbContext.Conexao.Get<T>(Id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void InsertOrReplace(T Model)
        {
            try
            {
                var rs = _dbContext.Conexao.InsertOrReplace(Model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertOrReplace(List<T> Model)
        {
            try
            {
                _dbContext.Conexao.BeginTransaction();

                foreach (var model in Model)
                {
                    var rs = _dbContext.Conexao.InsertOrReplace(model);

                    if (rs == 0)
                        throw new Exception("Erro ao inserir registro");
                }

                _dbContext.Conexao.Commit();
            }
            catch (Exception ex)
            {
                _dbContext.Conexao.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void Update(T model)
        {
            try
            {
                var rs = _dbContext.Conexao.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
