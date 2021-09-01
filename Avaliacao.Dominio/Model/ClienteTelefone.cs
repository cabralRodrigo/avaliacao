namespace Avaliacao.Dominio.Model
{
    public class ClienteTelefone
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public TelefoneTipo TelefoneTipoId { get; set; }

        public string Telefone { get; set; }
    }
}