using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Avaliacao.Dominio.Model
{
    public class ClienteTelefone : IModel
    {
        public virtual int Id { get; set; }
        
        [DisplayName("Tipo")]
        public virtual TelefoneTipo TelefoneTipoId { get; set; }
        
        [Required(ErrorMessage = "O telefone do cliente deve ser informado")]
        public virtual string Telefone { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}