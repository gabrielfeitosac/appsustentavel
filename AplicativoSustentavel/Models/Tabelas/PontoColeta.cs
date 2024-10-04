using AplicativoSustentavel.Models.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AplicativoSustentavel.Models.Tabelas
{
    [Table("PONTOCOLETA")]
    public class PontoColeta : BaseModel
    {
        public string Titulo { get; set; } 

        public string Endereco { get; set; } 

        public string PathFoto { get; set; }       

        [Ignore]
        public ImageSource Foto
        {
            get
            {
                try
                {
                    if (File.Exists(PathFoto))
                        return ImageSource.FromFile(PathFoto);
                    else
                        return null;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [Ignore]
        public bool IsVisibleFotoFromFile
        {
            get
            {
                try
                {
                    if (File.Exists(PathFoto))
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [Ignore]
        public bool IsVisibleFotoFromResource
        {
            get
            {
                try
                {
                    return !IsVisibleFotoFromFile;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [Ignore]
        public Thickness PaddingFrameLista
        {
            get
            {
                try
                {
                    if (IsVisibleFotoFromFile)
                        return new Thickness(0,2);
                    else
                        return new Thickness(10,2);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [Ignore]
        public Thickness MarginIconeMaps
        {
            get
            {
                try
                {
                    if (IsVisibleFotoFromFile)
                        return new Thickness(0,5,10,0);
                    else
                        return new Thickness(0);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
