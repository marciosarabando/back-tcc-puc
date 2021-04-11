using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;

namespace TCC.INSPECAO.Infra.Repositories
{
    public class UsuarioClaimsRepository : IUsuarioClaimsRepository
    {
        private readonly DataContext _context;
        public UsuarioClaimsRepository(DataContext context)
        {
            _context = context;
        }

        public void Criar(UsuarioClaims usuarioClaims)
        {
            _context.UsuarioClaims.Add(usuarioClaims);
            _context.SaveChanges();
        }

        public void Atualizar(UsuarioClaims usuarioClaims)
        {
            _context.UsuarioClaims.Update(usuarioClaims);
            _context.SaveChanges();
        }

        public void Remover(UsuarioClaims usuarioClaims)
        {
            _context.UsuarioClaims.Remove(usuarioClaims);
            _context.SaveChanges();
        }


        public List<UsuarioClaims> ObterClaimsUsuario(string idFirebase)
        {
            return _context.UsuarioClaims
                .Include(x => x.Usuario).ThenInclude(x => x.Estabelecimento)
                .Include(x => x.Claim)
                .Where(x => x.Usuario.IdFirebase == idFirebase)
                .ToList();
        }


    }
}