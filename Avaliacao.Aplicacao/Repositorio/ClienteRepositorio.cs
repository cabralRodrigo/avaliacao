using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;

namespace Avaliacao.Aplicacao.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ISession session) : base(session) { }
    }
}