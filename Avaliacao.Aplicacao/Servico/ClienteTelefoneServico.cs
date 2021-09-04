using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Servico
{
    public class ClienteTelefoneServico : Servico<ClienteTelefone, IClienteTelefoneRepositorio>, IClienteTelefoneServico
    {
        public ClienteTelefoneServico(IClienteTelefoneRepositorio repositorio) : base(repositorio) { }

        public IList<ClienteTelefone> ListarPorCliente(int clienteId)
        {
            return this.Repositorio.ListarPorCliente(clienteId);
        }

        public async Task Sincronizar(Cliente cliente)
        {
            if (cliente is null)
                throw new ArgumentNullException(nameof(cliente));

            var telefonesBanco = this.Repositorio.ListarPorCliente(cliente.Id);
            var query = (cliente.Telefones ?? Array.Empty<ClienteTelefone>()).FullJoin(telefonesBanco, s => s.Id, s => s.Id, (parametro, banco) => (parametro, banco));

            foreach (var (telefoneParametro, telefoneBanco) in query)
            {
                if (telefoneParametro is null)
                    await this.Remover(telefoneBanco);
                else
                {
                    telefoneParametro.Cliente = cliente;

                    if (telefoneBanco is null)
                        await this.Adicionar(telefoneParametro);
                    else
                        await this.Atualizar(telefoneParametro);
                }
            }
        }
    }
}