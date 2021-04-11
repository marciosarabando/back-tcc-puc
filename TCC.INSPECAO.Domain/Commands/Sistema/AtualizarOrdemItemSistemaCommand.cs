using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AtualizarOrdemItemSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaItemRepository _sistema_item_repository;
        public AtualizarOrdemItemSistemaCommand(List<ItensSistemaOrdem> itensSistemasOrdem,
                                            ISistemaItemRepository sistema_item_repository)
        {
            ItensSistemasOrdem = itensSistemasOrdem;
            _sistema_item_repository = sistema_item_repository;
        }
        public List<ItensSistemaOrdem> ItensSistemasOrdem { get; set; }

        public void Validate()
        {
            foreach (var item in ItensSistemasOrdem)
            {
                AddNotifications(
                    new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(item.IdItemSistema.ToString(), "IdItemSistema", "O IdItemSistema deve ser informado")
                    .IsNotNullOrEmpty(item.NumeroOrdem.ToString(), "NumeroOrdem", "O Numero de Ordem deve ser informado")
                );

                AddNotifications(
                    new Contract()
                    .Requires().IsTrue(PossuirItemSistemaCadastrado(item.IdItemSistema), item.IdItemSistema.ToString(), "O IdItemSistema informado não foi localizado!")
                );
            }
        }
        private bool PossuirItemSistemaCadastrado(Guid idSistema)
        {
            return _sistema_item_repository.ObterPorId(idSistema) != null;
        }
    }

    public class ItensSistemaOrdem
    {
        public Guid IdItemSistema { get; set; }
        public int NumeroOrdem { get; set; }
    }
}