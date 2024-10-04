using AplicativoSustentavel.Models.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicativoSustentavel.Models.Tabelas
{
    [Table("PERGUNTAQUIZ")]
    public class PerguntaQuiz : BaseModel
    {
        public int Ordem { get; set; }

        public string Pergunta { get; set; }

        public string Alterantiva1 { get; set; }

        public string Alterantiva2 { get; set; }

        public string Alterantiva3 { get; set; }

        public string Alterantiva4 { get; set; }

        public int AlternativaCorreta { get; set; }

        public bool RespostaCorreta { get; set; }
    }
}
