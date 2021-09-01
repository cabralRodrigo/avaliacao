using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;

namespace Avaliacao.Aplicacao.Servico
{
    public class ClienteServico : Servico<Cliente, IClienteRepositorio>, IClienteServico
    {
        public ClienteServico(IClienteRepositorio repositorio) : base(repositorio) { }
    }
}