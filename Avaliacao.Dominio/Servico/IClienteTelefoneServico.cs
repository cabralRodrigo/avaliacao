using Avaliacao.Dominio.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Dominio.Servico
{
    public interface IClienteTelefoneServico : IServico<ClienteTelefone>
    {
        Task Sincronizar(Cliente model);
        IList<ClienteTelefone> ListarPorCliente(int clienteId);
    }
}