using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppSustentavelMobile.ViewModels.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Alerta(string mensagem)
        {
            App.Current.MainPage.DisplayAlert("Atenção!", mensagem, "Ok");
        }

        public async Task AlertaAsync(string mensagem)
        {
            await App.Current.MainPage.DisplayAlert("Atenção!", mensagem, "Ok");
        }
    }
}
