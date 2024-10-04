using Acr.UserDialogs;
using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Data.Repository;
using AplicativoSustentavel.Models.Tabelas;
using AppSustentavelMobile.ViewModels.Common;
using AppSustentavelMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static AplicativoSustentavel.Models.Tabelas.Video;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
   public class AdicionarVideoViewModel : BaseViewModel
    {
        #region -> Propriedades
        private List<NivelImportanciaViewModel> _listaNivelImportancia;
        private Video _video;

        //Commands
        private Command _cadastraVideo;

        //Propriedades de Navegação
        private INavigation _navigation;
        private IVideoRepository _videoRepository;
        #endregion


        #region -> Construtor
        public AdicionarVideoViewModel()
        {
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public List<NivelImportanciaViewModel> ListaNivelImportancia { get { return _listaNivelImportancia; } set { _listaNivelImportancia = value; OnPropertyChanged("ListaNivelImportancia"); } }
        public Video Video { get { return _video; } set { _video = value; OnPropertyChanged("Video"); } }
        #endregion

        #region -> Commands
        public Command CadastraVideo => _cadastraVideo ?? (_cadastraVideo = new Command(async() =>
        {
            try
            {
                ValidaCampos();
                ValidaLink(Video.Link);
                Video.Ativo = true;
                _videoRepository.InsertOrReplace(Video);
                await AlertaAsync("Vídeo Cadastrado com Sucesso!");
                await Navigation.PopAsync(false);
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }));

        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                _videoRepository = new VideoRepository();
                ListaNivelImportancia = Video.RetornaNivelImportanciaList();
                Video = new Video();
                Video.Data = DateTime.Now;
                OnPropertyChanged("Video");
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        private void ValidaLink(string link)
        {
            if (link != null && link.Contains("/"))
            {
                if(link.Contains("https://"))
                    link = link.Replace("https://", "");

                link = link.Split('/').FirstOrDefault();
                if (!link.Contains("youtube.com") && !link.Contains("youtu.be"))
                    throw new Exception("Insira um link do youtube válido!");
            }
            else
                throw new Exception("Insira um link do youtube válido!");
        }

        private void ValidaCampos()
        {
            if (string.IsNullOrEmpty(Video.Titulo))
                throw new Exception("Preencha o Título do Vídeo!");
            if (string.IsNullOrEmpty(Video.Link))
                throw new Exception("Preencha o Link do Vídeo!");
            if (Video.Data == null || Video.Data == DateTime.MinValue)
                throw new Exception("Preencha o título do Vídeo!");
            if (string.IsNullOrEmpty(Video.AutorVideo))
                throw new Exception("Preencha a Data do Vídeo!");
        }
        #endregion
    }
}
