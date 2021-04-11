using System;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        void Criar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario ObterPorIdFirebase(string idFirebase);
        Usuario ObterPorEmail(string email);
        Usuario ObterPorIdFirebaseEEmail(string idFirebase, string email);
        List<Usuario> ObterTodos();
        Usuario ObterPorId(Guid id);
    }
}