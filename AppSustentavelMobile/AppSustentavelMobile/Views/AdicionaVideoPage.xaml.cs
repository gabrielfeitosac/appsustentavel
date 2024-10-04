using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AplicativoSustentavel.Models.Tabelas.Video;

namespace AppSustentavelMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionaVideoPage : ContentPage
    {
        private AdicionarVideoViewModel _adicionarVideoViewModel;
        public AdicionaVideoPage()
        {
            InitializeComponent();

            _adicionarVideoViewModel =  BindingContext as AdicionarVideoViewModel;
            _adicionarVideoViewModel.Navigation = Navigation;
            _adicionarVideoViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            return true;
        }

        private void SelecionaImportancia(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selecionado = e.Value as NivelImportanciaViewModel;
            if(selecionado != null)
                _adicionarVideoViewModel.Video.Importancia = (NivelImportancia)selecionado.Valor;
        }
    }
}