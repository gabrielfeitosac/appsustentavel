using AplicativoSustentavel.Data.Interface;
using AppSustentavelMobile.Droid.Helpers;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabasePath))]
namespace AppSustentavelMobile.Droid.Helpers
{
    public class DatabasePath : IDBPath
    {
        public DatabasePath()
        {

        }

        public string GetDbPath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "AppSustentavel.db3");
        }
    }
}