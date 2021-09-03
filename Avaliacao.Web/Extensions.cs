using Avaliacao.Aplicacao.Repositorio;
using Avaliacao.Aplicacao.Servico;
using Avaliacao.Dados;
using Avaliacao.Dominio.Repositorio;
using Avaliacao.Dominio.Servico;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Avaliacao.Web
{
    public static class Extensions
    {
        public static void AddNHibernate(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ISessionFactory>(DadosConfigurador.CriarSessionFactory(connectionString));
            services.AddScoped<ISession>(provider => provider.GetRequiredService<ISessionFactory>().OpenSession());
        }

        public static void AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteTelefoneRepositorio, ClienteTelefoneRepositorio>();
        }

        public static void AddServicos(this IServiceCollection services)
        {
            services.AddScoped<IClienteServico, ClienteServico>();
            services.AddScoped<IClienteTelefoneServico, ClienteTelefoneServico>();
        }

        public static string DefinirTitulo(this IRazorPage page, string titulo)
        {
            page.ViewContext.ViewData["layout:titulo"] = titulo?.Trim();
            return string.Empty;
        }

        public static string BuscarTitulo(this IRazorPage page)
        {
            if (page.ViewContext.ViewData.TryGetValue("layout:titulo", out var valor) && valor is string titulo && !string.IsNullOrWhiteSpace(titulo))
                return titulo;

            return "Avaliação Rodrigo";
        }

        public static string BuscarDescricao<T>(this T valor) where T : struct, Enum
        {
            if (!Enum.IsDefined(valor))
                throw new ArgumentOutOfRangeException(nameof(valor), $"O valor '{valor}' não é valido para o enum '{typeof(T)}'.");

            var descricao = typeof(T).GetField(valor.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return descricao?.Description ?? valor.ToString();
        }

        public static List<SelectListItem> ToSelectListItem<T>(this T[] source) where T: struct, Enum
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source.Select(s => new SelectListItem
            {
                Text = s.BuscarDescricao(),
                Value = s.ToString()
            }).ToList();
        }
    }
}