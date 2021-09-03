using Avaliacao.Dominio.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Avaliacao.Web.Models
{
    public class ClienteEditarModel
    {
        public ModoOperacao ModoOperacao { get; set; }
        public Cliente Cliente { get; set; }
        public List<SelectListItem> TelefoneTipos { get; set; }
    }
}