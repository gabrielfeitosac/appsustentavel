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
    public partial class CadastraUsuarioPage : ContentPage
    {
        private CadastraUsuárioViewModel _cadastraUsuárioViewModel;
        public CadastraUsuarioPage()
        {
            InitializeComponent();

            _cadastraUsuárioViewModel = BindingContext as CadastraUsuárioViewModel;
            _cadastraUsuárioViewModel.Navigation = Navigation;
            _cadastraUsuárioViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            return base.OnBackButtonPressed();
        }

    }
}