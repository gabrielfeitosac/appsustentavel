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
    public partial class QuizPerguntasPage : ContentPage
    {
        private QuizPerguntasViewModel _quizPerguntasViewModel;
        public QuizPerguntasPage()
        {
            InitializeComponent();

            _quizPerguntasViewModel = BindingContext as QuizPerguntasViewModel;
            _quizPerguntasViewModel.Navigation = Navigation;
            _quizPerguntasViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            return true;
        }
    }
}