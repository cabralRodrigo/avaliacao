using Avaliacao.Dominio.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Dominio.Repositorio
{
    public interface IRepositorio<TModel> where TModel : IModel
    {
        Task Adicionar(TModel model);
        Task Atualizar(TModel model);
        Task<IList<TModel>> Listar();
        Task<TModel> Buscar(int id);
        Task Remover(int id);
        Task Remover(TModel model);
    }
}