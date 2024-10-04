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
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _loginViewModel = BindingContext as LoginViewModel;
            _loginViewModel.Navigation = Navigation;
            _loginViewModel.CarregaDados();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}