using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;

namespace Avaliacao.Aplicacao.Servico
{
    public class ClienteTelefoneServico : Servico<ClienteTelefone, IClienteTelefoneRepositorio>, IClienteTelefoneServico
    {
        public ClienteTelefoneServico(IClienteTelefoneRepositorio repositorio) : base(repositorio) { }
    }
}