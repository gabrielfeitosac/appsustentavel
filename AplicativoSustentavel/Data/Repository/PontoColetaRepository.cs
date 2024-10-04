using AplicativoSustentavel.Context;
using AplicativoSustentavel.Data.Common;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Data.Repository
{
    public class PontoColetaRepository : CoreRepository<PontoColeta>, IPontoColetaRepository
    {
        public PontoColetaRepository()
        {
            DbContext = AppSustentavelContext.Current;
        }
    }
}
