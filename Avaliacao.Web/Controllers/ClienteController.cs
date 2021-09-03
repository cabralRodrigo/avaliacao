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
            var model = this.CriarModel(new(), ModoOperacao.Criacao);
            if (model is null)
                return this.NotFound(model);

            return this.View("Detalhes", model);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var model = this.CriarModel(await this.clienteServico.Buscar(id), ModoOperacao.Edicao);
            if (model is null)
                return this.NotFound();

            return this.View("Detalhes", model);
        }

        [HttpGet]
        public async Task<IActionResult> Remover(int id)
        {
            var cliente = await this.clienteServico.Buscar(id);
            if (cliente is null)
                return this.NotFound();

            return this.View(cliente);
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
        public async Task<IActionResult> Salvar(ClienteEditarModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model = this.CriarModel(model?.Cliente, model.ModoOperacao);
                if (model is null)
                    return this.NotFound();

                return this.View("Editar", model);
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

        private ClienteEditarModel CriarModel(Cliente cliente, ModoOperacao modoOperacao)
        {
            if (!Enum.IsDefined(modoOperacao))
                return null;

            if (cliente is null)
                return null;

            return new ClienteEditarModel
            {
                ModoOperacao = modoOperacao,
                Cliente = cliente,
                TelefoneTipos = Enum.GetValues<TelefoneTipo>().ToSelectListItem()
            };
        }
    }
}