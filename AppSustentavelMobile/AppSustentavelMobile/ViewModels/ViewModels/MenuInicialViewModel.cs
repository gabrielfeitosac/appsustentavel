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
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppSustentavelMobile.ViewModels.ViewModels
{
    public class MenuInicialViewModel : BaseViewModel
    {
        #region -> Propriedades
        //Variáveis
        private List<Video> _listaVideos;
        private List<PontoColeta> _listaPontos;
        private Usuario _usuario;

        //Commands
        private Command _sairCommand;
        private Command _adicionarVideoCommand;
        private Command _adicionarPontoCommand;
        private Command _abrirMapsCommand;
        private Command _quizCommand;

        //Utils
        private VideoRepository _videoRepository = new VideoRepository();
        private PontoColetaRepository _pontoColetaRepository = new PontoColetaRepository();
        private IPerguntaQuizRepository _perguntaQuizRepository = new PerguntaQuizRepository();

        //Propriedade de Navegação
        private INavigation _navigation;
        #endregion


        #region -> Construtor
        public MenuInicialViewModel()
        {
            ListaVideos = new List<Video>();
            ListaPontos = new List<PontoColeta>();
            Usuario = App._usuarioLogado;
            if (_videoRepository.GetAll().Count() == 0)
                CriaDados();
        }
        #endregion

        #region -> Encapsulamentos
        public INavigation Navigation { get { return _navigation; } set { _navigation = value; OnPropertyChanged("Navigation"); } }
        public List<Video> ListaVideos { get { return _listaVideos; } set { _listaVideos = value; OnPropertyChanged("ListaVideos"); } }
        public List<PontoColeta> ListaPontos { get { return _listaPontos; } set { _listaPontos = value; OnPropertyChanged("ListaPontos"); } }
        public Usuario Usuario { get { return _usuario; } set { _usuario = value; OnPropertyChanged("Usuario"); } }
        #endregion

        #region -> Commands
        public Command SairCommand => _sairCommand ?? (_sairCommand = new Command(async () => await SairApp()));

        public Command AdicionarVideoCommand => _adicionarVideoCommand ?? (_adicionarVideoCommand = new Command(() => 
        {
            Navigation.PushAsync(new AdicionaVideoPage(),false);
        }));

        public Command AdicionarPontoCommand => _adicionarPontoCommand ?? (_adicionarPontoCommand = new Command(() =>
        {
            Navigation.PushAsync(new AdicionaPontoPage(), false);
        }));

        public Command QuizCommand => _quizCommand ?? (_quizCommand = new Command(() =>
        {
            Navigation.PushAsync(new QuizPerguntasPage(), false);
        }));

        public Command AbrirMapsCommand => _abrirMapsCommand ?? (_abrirMapsCommand = new Command<PontoColeta>((pontoColeta) =>
        {
            AbreMaps(pontoColeta);
        }));
        #endregion

        #region -> Metodos
        public void CarregaDados()
        {
            try
            {
                ListaVideos = _videoRepository.GetAllAtivos();
                ListaPontos = _pontoColetaRepository.GetAllAtivos();
                OnPropertyChanged("ListaVideos");
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        private void CriaDados()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("");
                var video = new Video() { Titulo = "O que é reciclagem?", Ativo = true, AutorVideo = "eCycle", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.PoucoImportante, Link = "https://www.youtube.com/watch?v=OQ5jpiKzNqg" };
                _videoRepository.InsertOrReplace(video);
                video = new Video() { Titulo = "Por Que a Reciclagem é Tão Importante?", Ativo = true, AutorVideo = "Aula365 - Brasil", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.Moderado, Link = "https://www.youtube.com/watch?v=ZcymnW5NRYQ" };
                _videoRepository.InsertOrReplace(video);
                video = new Video() { Titulo = "RECICLAR | CORES DAS LIXEIRAS | MEIO AMBIENTE | ENSINO FUNDAMENTAL | EDUCAÇÃO INFANTIL", Ativo = true, AutorVideo = "ALFABRINCA | VÍDEOS EDUCATIVOS", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.Importante, Link = "https://www.youtube.com/watch?v=6G6f2stDCN0" };
                _videoRepository.InsertOrReplace(video);
                video = new Video() { Titulo = "O SEGREDO DO LIXO - Nostalgia Animado", Ativo = true, AutorVideo = "Canal Nostalgia", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.MuitoImportante, Link = "https://www.youtube.com/watch?v=sfa-jnXtA84" };
                _videoRepository.InsertOrReplace(video);
                video = new Video() { Titulo = "As cores das lixeiras da coleta seletiva para reciclagem na educação ambiental", Ativo = true, AutorVideo = "Ensinando meu filho", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.PoucoImportante, Link = "https://www.youtube.com/watch?v=IfJ1z6ahgzk" };
                _videoRepository.InsertOrReplace(video);
                video = new Video() { Titulo = "A IMPORTÂNCIA DA RECICLAGEM PARA O MEIO AMBIENTE - V1", Ativo = true, AutorVideo = "LAR Plásticos", Data = new DateTime(2017, 10, 04), Importancia = Video.NivelImportancia.Moderado, Link = "https://www.youtube.com/watch?v=4OVW4SRYRp0" };
                _videoRepository.InsertOrReplace(video);                                

                var ponto = new PontoColeta() { Titulo = "Ecoponto Antonio Eufrasio de Toledo", Ativo = true, Endereco = "120, R. Sorocabana, 2 - Vila Souto, Bauru - SP, 17054-000" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Mary Dota", Ativo = true, Endereco = "R. Américo Finazzi, 4 - Nucleo Hab. Nobuji Nagasawa, Bauru - SP, 17025-821" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Jardim Redentor/Geisel", Ativo = true, Endereco = "213, R. Noé Onófre Teixeira, 109 - Jardim Dona Lili, Bauru - SP, 17032-500" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Pousada I", Ativo = true, Endereco = "Pousada da Esperança I, Bauru - SP, 17022-091" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Edson Francisco da Silva",  Ativo = true, Endereco = "R. Dúlce Duarte Carrijo, 4 - Jardim Nova Esperanca, Bauru - SP, 17065-360" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Parque Viaduto",  Ativo = true, Endereco = "R. Bernardino de Campos, 28 - Vila Jardim Celina, Bauru - SP, 17055-025" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Engenheiro Octávio Rasi", Ativo = true, Endereco = "R. Manoel Lopes Neves - Bauru, SP, 17039-040" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);
                ponto = new PontoColeta() { Titulo = "Ecoponto Santa Edwirges", Ativo = true, Endereco = "R. Francisco do Rêgo Carranca, 1 - Jardim Vania Maria, Bauru - SP, 17063-490" }; //Foto = Salva caminho da foto
                _pontoColetaRepository.InsertOrReplace(ponto);


                var pergunta = new PerguntaQuiz()
                {
                    Ordem = 1,
                    Pergunta = "Qual cor representa o descarte de metal dentro da reciclagem?",
                    Alterantiva1 = "Verde",
                    Alterantiva2 = "Amarelo",
                    Alterantiva3 = "Azul",
                    Alterantiva4 = "Vermelho",
                    AlternativaCorreta = 2,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 2,
                    Pergunta = "De acordo com informações do App, quantos ecopontos existem em Bauru?",
                    Alterantiva1 = "5",
                    Alterantiva2 = "3",
                    Alterantiva3 = "7",
                    Alterantiva4 = "8",
                    AlternativaCorreta = 4,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 3,
                    Pergunta = "Em que século o conceito de reciclagem começou a surgir?",
                    Alterantiva1 = "Século XX (20)",
                    Alterantiva2 = "Século XVIII (18)",
                    Alterantiva3 = "Século XIX (19)",
                    Alterantiva4 = "Século XXI (21)",
                    AlternativaCorreta = 1,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 4,
                    Pergunta = "Qual o horário de atendimento dos pontos de coleta de Bauru nos domingos e feriados?",
                    Alterantiva1 = "7 às 19h",
                    Alterantiva2 = "5 às 12h",
                    Alterantiva3 = "10 às 17h",
                    Alterantiva4 = "8 às 16h",
                    AlternativaCorreta = 4,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 5,
                    Pergunta = "Qual dos itens abaixo NÃO se pode levar em um ponto de coleta?",
                    Alterantiva1 = "Pequenas quantidade de entulho",
                    Alterantiva2 = "Madeira",
                    Alterantiva3 = "Lixo hospitalar",
                    Alterantiva4 = "Plástico",
                    AlternativaCorreta = 3,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 6,
                    Pergunta = "Qual tipo de descarte a cor marrom representa na reciclagem?",
                    Alterantiva1 = "Plástico",
                    Alterantiva2 = "Vidro",
                    Alterantiva3 = "Papel",
                    Alterantiva4 = "Resíduos Orgânicos",
                    AlternativaCorreta = 4,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);
                pergunta = new PerguntaQuiz()
                {
                    Ordem = 7,
                    Pergunta = "O que a reciclagem é para o mundo?",
                    Alterantiva1 = "Importante",
                    Alterantiva2 = "Muito Importante",
                    Alterantiva3 = "Necessária",
                    Alterantiva4 = "Essencial",
                    AlternativaCorreta = 0,
                    Ativo = true
                };
                _perguntaQuizRepository.InsertOrReplace(pergunta);

            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task SairApp()
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Atenção!", "Deseja sair do aplicativo?", "Sim", "Não"))
                {
                    App._usuarioLogado = null;
                    await Navigation.PushAsync(new LoginPage(), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
        }

        public async void AbreMaps(PontoColeta pontoColeta)
        {
            try
            {
                if (App.VerificaConexao())
                {
                    using (var loading = UserDialogs.Instance.Loading("Abrindo Maps..."))
                    {
                        var locations = await Geocoding.GetLocationsAsync(pontoColeta.Endereco);

                        Location location = locations?.FirstOrDefault();

                        if (location != null)
                        {
                            var lat = location.Latitude.ToString().Replace(",", ".");
                            var longi = location.Longitude.ToString().Replace(",", ".");

                            string mapsURL = ("https://www.google.com/maps/dir/?api=1&destination=" + lat + longi + "&travelmode = driving");
                            await Launcher.OpenAsync(mapsURL);
                        }
                    }                       
                }
                else
                    throw new Exception("É necessário internet para abrir o google maps!");
            }
            catch (Exception e)
            {
                Alerta(e.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion
    }
}
