using Acr.UserDialogs;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Data.Repository;
using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class CadastraUsuárioViewModel : BaseViewModel
    {
        #region -> Propriedades       
        private string _confirmacaoSenha;
        private Usuario _usuario;

        //Commands
        private Command _cadastrarUsuario;

        //Propriedades de Navegação
        private INavigation _navigation;
        private IUsuarioRepository _usuarioRepository;
        #endregion


        #region -> Construtor
        public CadastraUsuárioViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public Usuario Usuario { get { return _usuario; } set { _usuario = value; OnPropertyChanged("Usuario"); } }
        public string ConfirmacaoSenha { get { return _confirmacaoSenha; } set { _confirmacaoSenha = value; OnPropertyChanged("ConfirmacaoSenha"); } }
        #endregion

        #region -> Commands
        public Command CadastrarUsuario => _cadastrarUsuario ?? (_cadastrarUsuario = new Command(async() => await CadastraUsuario()));
        #endregion

        #region -> Metodos
        private async Task CadastraUsuario()
        {
            try
            {
                using (var loading = UserDialogs.Instance.Loading("Cadastrando..."))
                {
                    

                    if (string.IsNullOrEmpty(Usuario.Login))
                        throw new Exception("Preencha o campo de Nome de Usuário!");
                    else if (string.IsNullOrEmpty(Usuario.NomeCompleto))
                        throw new Exception("Preencha o campo de Nome Completo!");
                    else if (string.IsNullOrEmpty(Usuario.Senha))
                        throw new Exception("Preencha o campo de Senha!");
                    else if (string.IsNullOrEmpty(ConfirmacaoSenha))
                        throw new Exception("Preencha o campo de Confirmação da senha!");
                    else if (!Usuario.Senha.Equals(ConfirmacaoSenha))
                        throw new Exception("Os campos de Senha devem ser iguais!");
                    else if(_usuarioRepository.GetUsuarioByLogin(Usuario.Login))
                        throw new Exception("Nome de Usuário já cadastrado!");
                    else
                    {
                        Usuario.Ativo = true;
                        Usuario.Senha = SenhaCriptografada(Usuario.Senha);
                        _usuarioRepository.InsertOrReplace(Usuario);
                        await AlertaAsync("Usuário Cadastrado com Sucesso!");
                        await Navigation.PopAsync(false);
                    }
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        public void CarregaDados()
        {
            try
            {
                _usuarioRepository = new UsuarioRepository();
                Usuario = new Usuario();                
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
