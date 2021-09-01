namespace Avaliacao.Dominio.Model
{
    public class ClienteTelefone : IModel
    {
        public virtual int Id { get; set; }
        public virtual TelefoneTipo TelefoneTipoId { get; set; }
        public virtual string Telefone { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}