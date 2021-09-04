using Avaliacao.Dominio.Model;
using System.Collections.Generic;

namespace Avaliacao.Dominio.Repositorio
{
    public interface IClienteTelefoneRepositorio : IRepositorio<ClienteTelefone>
    {
        IList<ClienteTelefone> ListarPorCliente(int clienteId);
    }
}