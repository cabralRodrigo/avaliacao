using System;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Aplicacao
{
    public static class Extensions
    {
        public static List<TResultado> FullJoin<TPrimeiro, TSegundo, TKey, TResultado>(this IEnumerable<TPrimeiro> primeiraLista, IEnumerable<TSegundo> segundaLista, Func<TPrimeiro, TKey> primeiroKey, Func<TSegundo, TKey> segundoKey, Func<TPrimeiro, TSegundo, TResultado> resultado)
        {
            var leftJoin = primeiraLista
                .GroupJoin(segundaLista, primeiroKey, segundoKey, (primeiro, segundo) => (primeiro, segundo))
                .SelectMany(s => s.segundo.DefaultIfEmpty(), (s, segundo) => (s.primeiro, segundo));

            var rightJoin = segundaLista
                .GroupJoin(primeiraLista, segundoKey, primeiroKey, (segundo, primeiro) => (primeiro, segundo))
                .SelectMany(s => s.primeiro.DefaultIfEmpty(), (s, primeiro) => (primeiro, s.segundo));

            return leftJoin.Union(rightJoin).Select(s => resultado(s.primeiro, s.segundo)).ToList();
        }
    }
}