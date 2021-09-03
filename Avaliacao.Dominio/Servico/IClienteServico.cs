using Avaliacao.Dominio.Model;

namespace Avaliacao.Dominio.Servico
{
    public interface IClienteServico : IServico<Cliente>
    {
        Cliente BuscarPorEmail(string email);
    }
}