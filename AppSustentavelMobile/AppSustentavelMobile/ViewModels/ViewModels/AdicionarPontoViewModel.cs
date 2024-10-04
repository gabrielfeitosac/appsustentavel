using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Data.Repository;
using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.Common;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class AdicionarPontoViewModel : BaseViewModel
    {
        #region -> Propriedades
        private PontoColeta _pontoColeta;
        private bool _isVisibleFoto;

        //Commands
        private Command _cadastraPonto;
        private Command _tirarFotoCommand;

        //Propriedades de Navegação
        private INavigation _navigation;
        private IPontoColetaRepository _pontoColetaRepository;
        #endregion


        #region -> Construtor
        public AdicionarPontoViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public PontoColeta PontoColeta { get { return _pontoColeta; } set { _pontoColeta = value; OnPropertyChanged("PontoColeta"); } }
        public bool IsVisibleFoto { get { return _isVisibleFoto; } set { _isVisibleFoto = value; OnPropertyChanged("IsVisibleFoto"); } }
        public bool IsVisibleCardPadraoFoto { get { return !IsVisibleFoto; } } 
        #endregion

        #region -> Commands
        public Command CadastraPonto => _cadastraPonto ?? (_cadastraPonto = new Command(async() =>
        {
            try
            {
                ValidaCampos();
                PontoColeta.Ativo = true;
                _pontoColetaRepository.InsertOrReplace(PontoColeta);
                await AlertaAsync("Ponto de Coleta Cadastrado com Sucesso!");
                await Navigation.PopAsync(false);
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }));

        public Command TirarFotoCommand => _tirarFotoCommand ?? (_tirarFotoCommand = new Command(async() => 
        {
            if (File.Exists(PontoColeta.PathFoto))
            {
                if(await App.Current.MainPage.DisplayAlert("Atenção!", "Deseja substituir a foto do Ponto de Coleta?", "Sim", "Não"))
                {
                    await CapturaFoto();
                }
            }
            else
            {
                await CapturaFoto();
            }
        }));
        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                _pontoColetaRepository = new PontoColetaRepository();
                PontoColeta = new PontoColeta();
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        private void ValidaCampos()
        {
            if (string.IsNullOrEmpty(PontoColeta.Titulo))
                throw new Exception("Preencha o Título do Ponto de Coleta!");
            if (string.IsNullOrEmpty(PontoColeta.Endereco))
                throw new Exception("Preencha o Endereço do Ponto de Coleta!");
            //if (string.IsNullOrEmpty(PontoColeta.Latitude))
            //    throw new Exception("Preencha a Latitude do Ponto de Coleta!");
            //if (string.IsNullOrEmpty(PontoColeta.Longitude))
            //    throw new Exception("Preencha a Longitude do Ponto de Coleta!");
            if (!File.Exists(PontoColeta.PathFoto))
                throw new Exception("Insira uma foto do Ponto de Coleta!");
        }

        private async Task<bool> PermissaoCamera()
        {
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();                    
                }

                if(storageStatus != PermissionStatus.Granted)
                {
                    storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)                
                    return true;                
                else                
                    return false;                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task CapturaFoto()
        {
            try
            {
                if(await PermissaoCamera())
                {                    
                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = false,
                        SaveMetaData = true,
                        DefaultCamera = CameraDevice.Rear,
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 40,
                    });

                    if (file != null)
                    {
                        PontoColeta.PathFoto = file.Path;
                        IsVisibleFoto = true;
                        OnPropertyChanged("PontoColeta");
                        OnPropertyChanged("IsVisibleFoto");
                        OnPropertyChanged("IsVisibleCardPadraoFoto");
                    }
                }
                else
                {
                    Alerta("Permissão de Câmera necessária para tirar foto!");
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }
        #endregion
    }
}

