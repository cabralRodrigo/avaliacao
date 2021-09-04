using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Aplicacao.Repositorio
{
    public class ClienteTelefoneRepositorio : Repositorio<ClienteTelefone>, IClienteTelefoneRepositorio
    {
        public ClienteTelefoneRepositorio(ISession session) : base(session) { }

        public IList<ClienteTelefone> ListarPorCliente(int clienteId)
        {
            return this.Session.Query<ClienteTelefone>().Where(s => s.Cliente.Id == clienteId).ToList();
        }
    }
}