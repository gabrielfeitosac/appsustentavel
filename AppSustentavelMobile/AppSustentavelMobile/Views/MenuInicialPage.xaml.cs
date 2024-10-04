using AplicativoSustentavel.Models.Tabelas;
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
    public partial class MenuInicialPage : ContentPage
    {
        public MenuInicialViewModel menuInicialViewModel;
        public MenuInicialPage()
        {
            InitializeComponent();
            menuInicialViewModel = BindingContext as MenuInicialViewModel;
            menuInicialViewModel.Navigation = Navigation;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void MenuAppearing(object sender, EventArgs e)
        {
            menuInicialViewModel.CarregaDados();
        }

        private void AbrirVideo(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var video = e.ItemData as Video;

            if (video != null)
                Device.OpenUri(new Uri(video.Link));
        }
    }
}