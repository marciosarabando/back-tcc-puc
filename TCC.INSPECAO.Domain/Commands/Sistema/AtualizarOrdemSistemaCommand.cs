using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AtualizarOrdemSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaRepository _sistema_repository;
        public AtualizarOrdemSistemaCommand(List<SistemasOrdem> sistemasOrdem,
                                            ISistemaRepository sistema_repository)
        {
            SistemasOrdem = sistemasOrdem;
            _sistema_repository = sistema_repository;
        }
        public List<SistemasOrdem> SistemasOrdem { get; set; }

        public void Validate()
        {
            foreach (var item in SistemasOrdem)
            {
                AddNotifications(
                    new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(item.IdSistema.ToString(), "IdSistema", "O IdSistema deve ser informado")
                    .IsNotNullOrEmpty(item.NumeroOrdem.ToString(), "NumeroOrdem", "O Numero de Ordem deve ser informado")
                );

                AddNotifications(
                    new Contract()
                    .Requires().IsTrue(PossuirSistemaCadastrado(item.IdSistema), item.IdSistema.ToString(), "O IdSistema informado não foi localizado!")
                );
            }
        }
        private bool PossuirSistemaCadastrado(Guid idSistema)
        {
            return _sistema_repository.ObterPorId(idSistema) != null;
        }
    }

    public class SistemasOrdem
    {
        public Guid IdSistema { get; set; }
        public int NumeroOrdem { get; set; }
    }
}