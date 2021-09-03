using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;
using System.Linq;

namespace Avaliacao.Aplicacao.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ISession session) : base(session) { }

        public Cliente BuscarPorEmail(string email)
        {
            return this.Session.Query<Cliente>().SingleOrDefault(s => s.Email == email);
        }
    }
}