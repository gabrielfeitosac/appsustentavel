using AplicativoSustentavel.Data.Interface;
using AplicativoSustentavel.Models.Tabelas;
using SQLite;
using Xamarin.Forms;

namespace AplicativoSustentavel.Context
{
    public class AppSustentavelContext
    {
        private static SQLiteConnection _sqliteConnection;

        public static AppSustentavelContext _lazy;

        public static AppSustentavelContext Current
        {
            get
            {
                if (_lazy == null)
                    _lazy = new AppSustentavelContext();

                return _lazy;
            }
        }

        private AppSustentavelContext()
        {
            _sqliteConnection = new SQLiteConnection(DependencyService.Get<IDBPath>().GetDbPath());
            _sqliteConnection.CreateTable<Video>();
            _sqliteConnection.CreateTable<PontoColeta>();
            _sqliteConnection.CreateTable<Usuario>();
            _sqliteConnection.CreateTable<PerguntaQuiz>();
        }

        public SQLiteConnection Conexao
        {
            get { return _sqliteConnection; }
            set { _sqliteConnection = value; }
        }
    }
}

