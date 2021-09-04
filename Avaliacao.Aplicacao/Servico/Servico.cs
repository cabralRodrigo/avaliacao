using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Servico
{
    public abstract class Servico<TModel, TRepositorio> : IServico<TModel>
        where TModel : class, IModel
        where TRepositorio : IRepositorio<TModel>
    {
        protected TRepositorio Repositorio { get; }

        public Servico(TRepositorio repositorio)
        {
            this.Repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        public virtual async Task Adicionar(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Repositorio.Adicionar(model);
        }

        public virtual async Task Atualizar(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Repositorio.Atualizar(model);
        }

        public async Task<IList<TModel>> Listar()
        {
            return await this.Repositorio.Listar();
        }

        public async Task<TModel> Buscar(int id)
        {
            return await this.Repositorio.Buscar(id);
        }

        public async virtual Task Remover(int id)
        {
            await this.Repositorio.Remover(id);
        }

        public async virtual Task Remover(TModel model)
        {
            await this.Repositorio.Remover(model);
        }
    }
}