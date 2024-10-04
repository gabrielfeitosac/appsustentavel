using AplicativoSustentavel.Context;
using AplicativoSustentavel.Data.Common;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Data.Repository
{
    public class PerguntaQuizRepository : CoreRepository<PerguntaQuiz>, IPerguntaQuizRepository
    {
        public PerguntaQuizRepository()
        {
            DbContext = AppSustentavelContext.Current;
        }
    }
}
