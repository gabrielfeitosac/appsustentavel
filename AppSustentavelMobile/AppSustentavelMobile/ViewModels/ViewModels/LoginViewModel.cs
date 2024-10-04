using Acr.UserDialogs;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Data.Repository;
using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.Common;
using AppSustentavelMobile.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region -> Propriedades
        private Usuario _usuario;
        private Usuario _usuarioSalvo;
        private string _login;
        private string _senha;
        private bool _isPasswordSenha = true;
        private bool _salvarUsuario = true;

        //Commands
        private Command _cadastraUsuario;
        private Command _executaLogin;
        private Command _trocaIsPasswordSenha;

        //Propriedades de Navegação
        private INavigation _navigation;
        private IUsuarioRepository _usuarioRepository;
        #endregion


        #region -> Construtor
        public LoginViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public Usuario Usuario { get { return _usuario; } set { _usuario = value; OnPropertyChanged("Usuario"); } }
        public Usuario UsuarioSalvo { get { return _usuarioSalvo; } set { _usuarioSalvo = value; OnPropertyChanged("UsuarioSalvo"); } }
        public string Login { get { return _login; } set { _login = value; OnPropertyChanged("Login"); } }
        public string Senha { get { return _senha; } set { _senha = value; OnPropertyChanged("Senha"); } }
        public string LabelMostraSenha { get { return IsPasswordSenha ? "\uf070" : "\uf06e"; } }
        public bool IsPasswordSenha { get { return _isPasswordSenha; } set { _isPasswordSenha = value; OnPropertyChanged("IsPasswordSenha"); } }
        public bool SalvarUsuario { get { return _salvarUsuario; } set { _salvarUsuario = value; OnPropertyChanged("SalvarUsuario"); } }
        #endregion

        #region -> Commands
        public Command CadastraUsuario => _cadastraUsuario ?? (_cadastraUsuario = new Command(() => Navigation.PushAsync(new CadastraUsuarioPage(), false)));
        public Command ExecutaLoginCommand => _executaLogin ?? (_executaLogin = new Command(async () =>
        {
            using (var loading = UserDialogs.Instance.Loading("Entrando..."))
            {
                await ExecutaLogin();
            };
        }));
        public Command TrocaIsPasswordSenha => _trocaIsPasswordSenha ?? (_trocaIsPasswordSenha = new Command(() =>
        {
            IsPasswordSenha = !IsPasswordSenha;
            OnPropertyChanged("IsPasswordSenha");
            OnPropertyChanged("LabelMostraSenha");
        }));

        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                _usuarioRepository = new UsuarioRepository();
                UsuarioSalvo = _usuarioRepository.GetAllAtivos().FirstOrDefault(u => u.UsuarioSalvoLogado);
                if (UsuarioSalvo != null)
                {
                    Login = UsuarioSalvo.Login;
                    Senha = UsuarioSalvo.SenhaSalvaLogin;
                    OnPropertyChanged("Login");
                    OnPropertyChanged("Senha");
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        private async Task ExecutaLogin()
        {
            try
            {
                await Task.Delay(2000);

                if (string.IsNullOrEmpty(Login))
                    throw new Exception("Preencha o campo de Usuário");
                else if (string.IsNullOrEmpty(Senha))
                    throw new Exception("Preencha o campo de Senha");
                else
                {
                    Usuario = _usuarioRepository.GetLoginApp(Login, SenhaCriptografada(Senha));

                    if (Usuario != null)
                    {
                        if (SalvarUsuario)
                        {
                            Usuario.UsuarioSalvoLogado = SalvarUsuario;
                            Usuario.SenhaSalvaLogin = Senha;
                            _usuarioRepository.Update(Usuario);

                            if (UsuarioSalvo != null && Usuario.Id != UsuarioSalvo.Id)
                            {
                                UsuarioSalvo.UsuarioSalvoLogado = false;
                                _usuarioRepository.Update(UsuarioSalvo);
                            }
                        }
                        else if(UsuarioSalvo != null)
                        {
                            UsuarioSalvo.UsuarioSalvoLogado = false;
                            _usuarioRepository.Update(UsuarioSalvo);
                        }

                        App._usuarioLogado = Usuario;
                        await Navigation.PushAsync(new MenuInicialPage(), false);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    }
                    else
                    {
                        throw new Exception("Usuário ou senha incorretos!");
                    }
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        public static string SenhaCriptografada(string value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }
        #endregion
    }
}
