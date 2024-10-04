using AplicativoSustentavel.Models.Common;
using System;
using System.Collections.Generic;

namespace AplicativoSustentavel.Data.Common
{
    public interface ICoreRepository<T> where T : BaseModel
    {        
        T GetById(Guid Id);

        void InsertOrReplace(T Model);

        void InsertOrReplace(List<T> Model);

        List<T> GetAll();

        void DeleteById(Guid? Id);

        void Delete(T model);

        void DeleteAll(List<T> model);

        void DeleteAll();

        void Update(T model);

        List<T> GetAllAtivos();
    }
}

