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
    public partial class AdicionaPontoPage : ContentPage
    {
        private AdicionarPontoViewModel _adicionarPontoViewModel;
        public AdicionaPontoPage()
        {
            InitializeComponent();

            _adicionarPontoViewModel = BindingContext as AdicionarPontoViewModel;
            _adicionarPontoViewModel.Navigation = Navigation;
            _adicionarPontoViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            return true;
        }
    }
}