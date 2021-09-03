using Avaliacao.Dominio.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Avaliacao.Dados.Mapping
{
    public class ClienteMap : ClassMapping<Cliente>
    {
        public ClienteMap()
        {
            this.Table("Cliente");

            this.Id(s => s.Id, id =>
            {
                id.Column("Id");
                id.Generator(Generators.Identity);
            });

            this.Property(s => s.Nome, nome =>
            {
                nome.Column("Nome");
                nome.NotNullable(true);
                nome.Length(100);
            });

            this.Property(s => s.Email, email =>
            {
                email.Column("Email");
                email.NotNullable(true);
                email.Unique(true);
                email.Length(255);
            });

            this.Property(s => s.Nascimento, nascimento =>
            {
                nascimento.Column("Nascimento");
                nascimento.NotNullable(false);
            });

            this.Bag(s => s.Telefones, set =>
            {
                set.Inverse(true);
                set.Key(key => key.Column(column => column.Name("ClienteId")));
            }, relation => relation.OneToMany());
        }
    }
}