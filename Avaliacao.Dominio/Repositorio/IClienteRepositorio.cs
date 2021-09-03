using Avaliacao.Dominio.Model;

namespace Avaliacao.Dominio.Repositorio
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        Cliente BuscarPorEmail(string email);
    }
}