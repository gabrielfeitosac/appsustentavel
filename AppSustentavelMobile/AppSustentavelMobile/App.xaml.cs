using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("fa-solid-900.ttf", Alias = "FontAwesome5FreeSolid")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "FontAwesome5FreeSolidRegular")]
[assembly: ExportFont("fa6-solid-900.otf", Alias = "FontAwesome6FreeSolid")]
[assembly: ExportFont("fa6-regular-400.otf", Alias = "FontAwesome6FreeSolidRegular")]
namespace AppSustentavelMobile
{
    public partial class App : Application
    {
        public static Usuario _usuarioLogado;
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjMyNTQ1QDMyMzAyZTMxMmUzME5Vc0tQZi93T25Mc1pVZ0E5SVZtYmZWd1NLVG9sVWRNU29zRnZmVXFhSU09");

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static bool VerificaConexao()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
