using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Queries;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public void Criar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> ObterTodos()
        {
            return _context.Usuarios
                                .AsNoTracking()
                                .Include(x => x.UsuarioClaims)
                                .ThenInclude(x => x.Claim)
                                .ToList();
        }

        public Usuario ObterPorId(Guid id)
        {
            return _context.Usuarios
                                .AsNoTracking()
                                .Include(x => x.UsuarioClaims)
                                .ThenInclude(x => x.Claim)
                                .FirstOrDefault(x => x.Id == id);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _context.Usuarios.AsNoTracking()
                                    .Include(x => x.UsuarioClaims)
                                    .FirstOrDefault(UsuarioQueries.ObterPorEmail(email));
        }

        public Usuario ObterPorIdFirebase(string idFirebase)
        {
            return _context.Usuarios
                                .Include(x => x.UsuarioClaims).ThenInclude(x => x.Claim)
                                .Include(x => x.Estabelecimento)
                                .FirstOrDefault(x => x.IdFirebase == idFirebase);
        }

        public Usuario ObterPorIdFirebaseEEmail(string idFirebase, string email)
        {
            return _context.Usuarios.AsNoTracking()
                                .Include(x => x.UsuarioClaims)
                                .Where(x => x.IdFirebase == idFirebase && x.Email == email)
                                //.Include(x => x.UsuarioClaims.Select(c => c.Claim))

                                .FirstOrDefault();
        }
    }
}