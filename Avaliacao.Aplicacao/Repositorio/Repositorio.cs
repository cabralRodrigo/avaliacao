using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Repositorio
{
    public class Repositorio<TModel> : IRepositorio<TModel> where TModel : class, IModel, new()
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

            await this.Session.SaveAsync(model);
        }

        public async Task Atualizar(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Session.MergeAsync(model);
            await this.Session.FlushAsync();
        }

        public async Task<IList<TModel>> Listar()
        {
            return await this.Session.CreateCriteria<TModel>().ListAsync<TModel>();
        }

        public async Task<TModel> Buscar(int id)
        {
            return await this.Session.GetAsync<TModel>(id);
        }

        public async Task Remover(int id)
        {
            var model = await this.Buscar(id);

            if (model is not null)
                await this.Remover(model);
        }

        public async Task Remover(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await this.Session.DeleteAsync(model);
            await this.Session.FlushAsync();
        }
    }
}