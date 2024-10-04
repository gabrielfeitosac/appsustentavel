using AplicativoSustentavel.Data.Interface;
using AppSustentavelMobile.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class ResultadoQuizViewModel : BaseViewModel
    {
        #region -> Propriedades
        private Color _corEstrela1;
        private Color _corEstrela2;
        private Color _corEstrela3;
        private Color _corRespostasCertas;

        private string _palavraResultado;
        private string _fraseResultado;
        private int _quantidadeAcertos;

        //Commands
        private Command _finalizarQuiz;

        //Propriedades de Navegação
        private INavigation _navigation;
        //private IVideoRepository _videoRepository;
        #endregion


        #region -> Construtor
        public ResultadoQuizViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public Color CorEstrela1 { get { return _corEstrela1; } set { _corEstrela1 = value; OnPropertyChanged("CorEstrela1"); } }
        public Color CorEstrela2 { get { return _corEstrela2; } set { _corEstrela2 = value; OnPropertyChanged("CorEstrela2"); } }
        public Color CorEstrela3 { get { return _corEstrela3; } set { _corEstrela3 = value; OnPropertyChanged("CorEstrela3"); } }
        public Color CorRespostasCertas { get { return _corRespostasCertas; } set { _corRespostasCertas = value; OnPropertyChanged("CorRespostasCertas"); } }
        public string PalavraResultado { get { return _palavraResultado; } set { _palavraResultado = value; OnPropertyChanged("PalavraResultado"); } }
        public string FraseResultado { get { return _fraseResultado; } set { _fraseResultado = value; OnPropertyChanged("FraseResultado"); } }
        public int QuantidadeAcertos { get { return _quantidadeAcertos; } set { _quantidadeAcertos = value; OnPropertyChanged("QuantidadeAcertos"); } }
        #endregion

        #region -> Commands
        public Command FinalizarQuiz => _finalizarQuiz ?? (_finalizarQuiz = new Command(() =>
        {
            try
            {
                Navigation.PopToRootAsync(false);
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }));

        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                if(QuantidadeAcertos <= 3)
                {
                    CorEstrela1 = Color.FromHex("#e6f23f");
                    CorEstrela2 = Color.FromHex("#4F4F4F");
                    CorEstrela3 = Color.FromHex("#4F4F4F");

                    CorRespostasCertas = Color.FromHex("#C42B14");
                    PalavraResultado = "QUE PENA!";
                    FraseResultado = "Você precisa se inteirar sobre a reciclagem.";
                }
                else if(QuantidadeAcertos > 3 &&  QuantidadeAcertos <= 6)
                {
                    CorEstrela1 = Color.FromHex("#e6f23f");
                    CorEstrela2 = Color.FromHex("#e6f23f");
                    CorEstrela3 = Color.FromHex("#4F4F4F");

                    CorRespostasCertas = Color.FromHex("#e6f23f");
                    PalavraResultado = "MUITO BEM!";
                    FraseResultado = "Você está no caminho certo para reciclagem.";
                }
                else
                {
                    CorEstrela1 = Color.FromHex("#e6f23f");
                    CorEstrela2 = Color.FromHex("#e6f23f");
                    CorEstrela3 = Color.FromHex("#e6f23f");

                    CorRespostasCertas = Color.FromHex("#008000");
                    PalavraResultado = "PARABÉNS!!!";
                    FraseResultado = "Você já está pronto para reciclar.";
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }
        #endregion
    }
}
