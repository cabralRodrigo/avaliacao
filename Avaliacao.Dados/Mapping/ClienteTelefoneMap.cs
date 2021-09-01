using Avaliacao.Dominio.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Avaliacao.Dados.Mapping
{
    public class ClienteTelefoneMap : ClassMapping<ClienteTelefone>
    {
        public ClienteTelefoneMap()
        {
            this.Table("ClienteTelefone");

            this.Id(s => s.Id, id =>
            {
                id.Column("Id");
                id.Generator(Generators.Identity);
            });

            this.ManyToOne(s => s.Cliente, map =>
            {
                map.Column(clienteId =>
                {
                    clienteId.Name("ClienteId");
                    clienteId.NotNullable(true);
                });
            });

            this.Property(s => s.TelefoneTipoId, telefoneTipoId =>
            {
                telefoneTipoId.Column("TelefoneTipoId");
            });

            this.Property(s => s.Telefone, telefone =>
            {
                telefone.Column("Telefone");
                telefone.NotNullable(true);
                telefone.Length(13);
            });
        }
    }
}