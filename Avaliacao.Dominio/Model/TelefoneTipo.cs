using System.ComponentModel;

namespace Avaliacao.Dominio.Model
{
    public enum TelefoneTipo
    {
        Pessoal = 1,
        Comercial = 2,

        [Description("Residêncial")]
        Residencial = 3,

        Outros = 4
    }
}