using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;
using System;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Servico
{
    public class ClienteServico : Servico<Cliente, IClienteRepositorio>, IClienteServico
    {
        private readonly IClienteTelefoneServico clienteTelefoneServico;

        public ClienteServico(IClienteRepositorio repositorio, IClienteTelefoneServico clienteTelefoneServico) : base(repositorio)
        {
            this.clienteTelefoneServico = clienteTelefoneServico ?? throw new ArgumentNullException(nameof(clienteTelefoneServico));
        }

        public override async Task Adicionar(Cliente model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            await base.Adicionar(model);

            if (model.Telefones is not null)
                foreach (var telefone in model.Telefones)
                {
                    telefone.Cliente = model;
                    await this.clienteTelefoneServico.Adicionar(telefone);
                }
        }

        public override async Task Atualizar(Cliente model)
        {
            await this.clienteTelefoneServico.Sincronizar(model);
            await base.Atualizar(model);
        }

        public override async Task Remover(Cliente model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            foreach (var telefone in this.clienteTelefoneServico.ListarPorCliente(model.Id))
                await this.clienteTelefoneServico.Remover(telefone);

            await base.Remover(model);
        }

        public override async Task Remover(int id)
        {
            foreach (var telefone in this.clienteTelefoneServico.ListarPorCliente(id))
                await this.clienteTelefoneServico.Remover(telefone);

            await base.Remover(id);
        }

        public Cliente BuscarPorEmail(string email)
        {
            return this.Repositorio.BuscarPorEmail(email);
        }
    }
}