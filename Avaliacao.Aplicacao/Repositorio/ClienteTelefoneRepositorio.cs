using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;

namespace Avaliacao.Aplicacao.Repositorio
{
    public class ClienteTelefoneRepositorio : Repositorio<ClienteTelefone>, IClienteTelefoneRepositorio
    {
        public ClienteTelefoneRepositorio(ISession session) : base(session) { }
    }
}