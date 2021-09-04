using Avaliacao.Dominio.Model;
using Avaliacao.Dominio.Servico;
using Avaliacao.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Avaliacao.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServico clienteServico;

        public ClienteController(IClienteServico clienteServico)
        {
            this.clienteServico = clienteServico ?? throw new ArgumentNullException(nameof(clienteServico));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return this.View(await this.clienteServico.Listar());
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return this.View("Detalhes", new ClienteDetalhesModel
            {
                ModoOperacao = ModoOperacao.Criacao,
                Cliente = new()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await this.clienteServico.Buscar(id);
            if (cliente is null)
                return this.NotFound();

            return this.View("Detalhes", new ClienteDetalhesModel
            {
                ModoOperacao = ModoOperacao.Edicao,
                Cliente = cliente
            });
        }

        [HttpGet]
        public async Task<IActionResult> Remover(int id)
        {
            var cliente = await this.clienteServico.Buscar(id);
            if (cliente is null)
                return this.NotFound();

            return this.View(cliente);
        }

        [HttpGet]
        public IActionResult Telefone()
        {
            return this.PartialView("Telefone", new ClienteTelefone());
        }

        [HttpPost]
        [ActionName("Remover")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverPost(int id)
        {
            await this.clienteServico.Remover(id);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(ClienteDetalhesModel model)
        {
            var outroCliente = this.clienteServico.BuscarPorEmail(model?.Cliente?.Email);
            if (outroCliente is not null && outroCliente.Id != model?.Cliente?.Id)
                this.ModelState.AddModelError(nameof(ClienteDetalhesModel.Cliente) + "." + nameof(Cliente.Email), $"Esse email já está sendo utilizado pelo cliente '{outroCliente.Nome}'.");

            if (!this.ModelState.IsValid)
            {
                if (model is null)
                    return this.NotFound();

                return this.View("Detalhes", model);
            }

            switch (model.ModoOperacao)
            {
                case ModoOperacao.Criacao:
                    await this.clienteServico.Adicionar(model.Cliente);
                    break;

                case ModoOperacao.Edicao:
                    await this.clienteServico.Atualizar(model.Cliente);
                    break;
            }

            return this.RedirectToAction("Index");
        }
    }
}