using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IInspecaoRepository
    {
        void Criar(Inspecao inspecao);
        void Atualizar(Inspecao inspecao);
        Inspecao ObterPorId(Guid id);
        Inspecao ObterInspecaoEmAndamentoPorUsuario(Guid idUsuario);
        IEnumerable<Inspecao> ObterTodos();
        IEnumerable<Inspecao> ObterRelatorioPeriodo(DateTime inicio, DateTime fim);
        IEnumerable<Inspecao> ObterRelatorioPorUsuario(Guid idUsuario);
        Inspecao ObterRelatorioInspecaoDetalhes(Guid idInspecao);
        List<int> ObterAnosDisponiveisInspecao();
        List<TResult> ObterUsuariosDisponiveisInspecao<TResult>(Expression<Func<Inspecao, TResult>> expression);
    }
}