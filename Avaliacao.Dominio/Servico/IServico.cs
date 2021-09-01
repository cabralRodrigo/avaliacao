using Avaliacao.Dominio.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Dominio.Servico
{
    public interface IServico<TModel> where TModel : IModel
    {
        Task Adicionar(TModel model);
        Task Atualizar(TModel model);
        Task<IList<TModel>> Listar();
        Task Remover(int id);
        Task Remover(TModel model);
    }
}