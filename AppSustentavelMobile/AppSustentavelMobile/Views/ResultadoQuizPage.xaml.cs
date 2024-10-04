using AppSustentavelMobile.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSustentavelMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultadoQuizPage : ContentPage
    {
        private ResultadoQuizViewModel resultadoQuizViewModel;
        public ResultadoQuizPage(int acertos)
        {
            InitializeComponent();

            resultadoQuizViewModel = BindingContext as ResultadoQuizViewModel;
            resultadoQuizViewModel.Navigation = Navigation;
            resultadoQuizViewModel.QuantidadeAcertos = acertos;
            resultadoQuizViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync(false);
            return true;
        }
    }
}