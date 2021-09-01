using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Repositorio
{
    public abstract class Repositorio<TModel> : IRepositorio<TModel> where TModel : class, IModel, new()
    {
        protected ISession Session { get; }

        public Repositorio(ISession session)
        {
            this.Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task Adicionar(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Session.SaveAsync(model).ConfigureAwait(false);
        }

        public async Task Atualizar(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Session.UpdateAsync(model).ConfigureAwait(false);
        }

        public async Task<IList<TModel>> Listar()
        {
            return await this.Session.CreateCriteria<TModel>().ListAsync<TModel>().ConfigureAwait(false);
        }

        public Task Remover(int id)
        {
            return this.Remover(new TModel { Id = id });
        }

        public async Task Remover(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Session.DeleteAsync(model).ConfigureAwait(false);
        }
    }
}