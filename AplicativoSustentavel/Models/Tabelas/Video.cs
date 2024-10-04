using AplicativoSustentavel.Models.Common;
using AplicativoSustentavel.Util;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AplicativoSustentavel.Models.Tabelas
{
    [Table("VIDEO")]
    public class Video : BaseModel
    {
        public string Titulo { get; set; }

        public string Link { get; set; }

        public DateTime Data { get; set; }

        public NivelImportancia Importancia { get; set; }

        public string AutorVideo { get; set; }

        public enum NivelImportancia
        {
            [Description("Pouco Importante")]
            PoucoImportante = 0,

            [Description("Moderado")]
            Moderado = 1,

            [Description("Importante")]
            Importante = 2,

            [Description("Muito Importante")]
            MuitoImportante = 3,
        }

        [Ignore]
        public string GetImportanciaDescricao
        {
            get
            {
                try
                {
                    switch (Importancia)
                    {
                        case NivelImportancia.PoucoImportante: return "Pouco Importante";
                        case NivelImportancia.Moderado: return "Moderado";
                        case NivelImportancia.Importante: return "Importante";
                        case NivelImportancia.MuitoImportante: return "Muito Importante";
                        default: return "";
                    }
                }
                catch (Exception e)
                {
                    throw e;
                };
            }
        }

        [Ignore]
        public string GetImportanciaColor
        {
            get
            {
                try
                {
                    switch (Importancia)
                    {
                        case NivelImportancia.PoucoImportante: return "#59db0d";
                        case NivelImportancia.Moderado: return "#cfc813";
                        case NivelImportancia.Importante: return "#f27e18";
                        case NivelImportancia.MuitoImportante: return "#ed1d15";
                        default: return "#FFFFFF";
                    }
                }
                catch (Exception e)
                {
                    throw e;
                };
            }
        }

        [Ignore]
        public string GetDataFormatada
        {
            get 
            {
                try
                {
                    if (Data != null)
                        return Data.ToString("dd/MM/yyyy");
                    else
                        return "";
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [Ignore]
        public string ThumbImage
        {
            get
            {
                try
                {
                    if (!String.IsNullOrEmpty(Link?.Trim()))
                    {
                        var separacao = Link.Split('/');
                        var separacaoFinal = separacao.Length > 0 ? separacao[separacao.Length - 1].Trim() : string.Empty;
                        separacao = separacaoFinal.Split('=');
                        var id = separacao.Length > 0 ? separacao[separacao.Length - 1].Trim() : string.Empty;
                        return !String.IsNullOrEmpty(id?.Trim()) ? $"https://img.youtube.com/vi/{id}/mqdefault.jpg" : string.Empty;
                    }
                    return string.Empty;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<NivelImportanciaViewModel> RetornaNivelImportanciaList()
        {
            try
            {
                List<NivelImportanciaViewModel> enumList = ((NivelImportancia[])Enum.GetValues(typeof(NivelImportancia)))
                    .Select(c => new NivelImportanciaViewModel() { Valor = (int)c, Descrição = DescriptionEnums.GetEnumDescription(c) })
                    .ToList();

                return enumList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    public class NivelImportanciaViewModel
    {
        public string Descrição { get; set; }
        public int Valor { get; set; }
    }
}
