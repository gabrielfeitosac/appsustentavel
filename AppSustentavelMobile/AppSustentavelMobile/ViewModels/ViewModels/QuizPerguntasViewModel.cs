using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Data.Repository;
using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.Common;
using AppSustentavelMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class QuizPerguntasViewModel : BaseViewModel
    {
        #region -> Propriedades
        private List<PerguntaQuiz> _listaPerguntas;
        private PerguntaQuiz _perguntaAtual;
        private int _indexPerguntaAtual;

        private string _corAlternativa1 = "#FFFFFF";
        private string _corAlternativa2 = "#FFFFFF";
        private string _corAlternativa3 = "#FFFFFF";
        private string _corAlternativa4 = "#FFFFFF";
        private bool _isVisibleProxPergunta = false;

        //Commands
        private Command _proximaPergunta;
        private Command _selecionaAlternativa;

        //Propriedades de Navegação
        private INavigation _navigation;
        private IPerguntaQuizRepository _perguntaQuizRepository;
        #endregion


        #region -> Construtor
        public QuizPerguntasViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public List<PerguntaQuiz> ListaPerguntas { get { return _listaPerguntas; } set { _listaPerguntas = value; OnPropertyChanged("ListaPerguntas"); } }
        public PerguntaQuiz PerguntaAtual { get { return _perguntaAtual; } set { _perguntaAtual = value; OnPropertyChanged("PerguntaAtual"); } }
        public string CorAlternativa1 { get { return _corAlternativa1; } set { _corAlternativa1 = value; OnPropertyChanged("CorAlternativa1"); } }
        public string CorAlternativa2 { get { return _corAlternativa2; } set { _corAlternativa2 = value; OnPropertyChanged("CorAlternativa2"); } }
        public string CorAlternativa3 { get { return _corAlternativa3; } set { _corAlternativa3 = value; OnPropertyChanged("CorAlternativa3"); } }
        public string CorAlternativa4 { get { return _corAlternativa4; } set { _corAlternativa4 = value; OnPropertyChanged("CorAlternativa4"); } }
        public bool IsVisibleProxPergunta { get { return _isVisibleProxPergunta; } set { _isVisibleProxPergunta = value; OnPropertyChanged("IsVisibleProxPergunta"); } }
        #endregion

        #region -> Commands
        public Command ProximaPergunta => _proximaPergunta ?? (_proximaPergunta = new Command(() => 
        {
            _indexPerguntaAtual++;
            if (ListaPerguntas.Count() > _indexPerguntaAtual)
            {
                CorAlternativa1 = "#FFFFFF";
                CorAlternativa2 = "#FFFFFF";
                CorAlternativa3 = "#FFFFFF";
                CorAlternativa4 = "#FFFFFF";
                IsVisibleProxPergunta = false;

                PerguntaAtual = ListaPerguntas[_indexPerguntaAtual];
                OnPropertyChanged("PerguntaAtual");
                OnPropertyChanged("IsVisibleProxPergunta");
                OnPropertyChanged("CorAlternativa1");
                OnPropertyChanged("CorAlternativa2");
                OnPropertyChanged("CorAlternativa3");
                OnPropertyChanged("CorAlternativa4");
            }
            else
            {
                var acertos = ListaPerguntas.Where(p => p.RespostaCorreta).Count();
                Navigation.PushAsync(new ResultadoQuizPage(acertos),false);
            }
        }));

        public Command SelecionaAlternativa => _selecionaAlternativa ?? (_selecionaAlternativa = new Command<string>((alternativaEscolhidaString) =>
        {
            if (!IsVisibleProxPergunta || PerguntaAtual.AlternativaCorreta == 0)
            {
                if (int.TryParse(alternativaEscolhidaString, out int alternativaEscolhida))
                {
                    if (alternativaEscolhida == PerguntaAtual.AlternativaCorreta || PerguntaAtual.AlternativaCorreta == 0)
                    {
                        PerguntaAtual.RespostaCorreta = true;
                        ListaPerguntas[_indexPerguntaAtual] = PerguntaAtual;

                        switch (alternativaEscolhida)
                        {
                            case 1:
                                CorAlternativa1 = "#008000";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 2:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#008000";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 3:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#008000";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 4:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#008000";
                                break;
                        }
                    }
                    else
                    {
                        PerguntaAtual.RespostaCorreta = false;
                        ListaPerguntas[_indexPerguntaAtual] = PerguntaAtual;

                        switch (alternativaEscolhida)
                        {
                            case 1:
                                CorAlternativa1 = "#C42B14";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 2:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#C42B14";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 3:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#C42B14";
                                CorAlternativa4 = "#FFFFFF";
                                break;
                            case 4:
                                CorAlternativa1 = "#FFFFFF";
                                CorAlternativa2 = "#FFFFFF";
                                CorAlternativa3 = "#FFFFFF";
                                CorAlternativa4 = "#C42B14";
                                break;
                        }

                        switch (PerguntaAtual.AlternativaCorreta)
                        {
                            case 1:
                                CorAlternativa1 = "#008000";
                                break;
                            case 2:
                                CorAlternativa2 = "#008000";
                                break;
                            case 3:
                                CorAlternativa3 = "#008000";
                                break;
                            case 4:
                                CorAlternativa4 = "#008000";
                                break;
                        }
                    }
                    IsVisibleProxPergunta = true;
                    OnPropertyChanged("IsVisibleProxPergunta");
                    OnPropertyChanged("CorAlternativa1");
                    OnPropertyChanged("CorAlternativa2");
                    OnPropertyChanged("CorAlternativa3");
                    OnPropertyChanged("CorAlternativa4");
                }
            }
        }));

        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                _perguntaQuizRepository = new PerguntaQuizRepository();
                ListaPerguntas = _perguntaQuizRepository.GetAllAtivos();
                PerguntaAtual = ListaPerguntas.FirstOrDefault();
                _indexPerguntaAtual = ListaPerguntas.IndexOf(PerguntaAtual);
                OnPropertyChanged("ListaPerguntas");
                OnPropertyChanged("PerguntaAtual");
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }
        #endregion
    }
}
