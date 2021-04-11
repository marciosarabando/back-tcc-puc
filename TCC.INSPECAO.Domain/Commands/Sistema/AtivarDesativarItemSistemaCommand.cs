using System;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AtivarDesativarItemSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaItemRepository _sistema_item_repository;
        public AtivarDesativarItemSistemaCommand(Guid idItemSistema, bool ativo, ISistemaItemRepository sistema_item_repository)
        {
            IdItemSistema = idItemSistema;
            Ativo = ativo;
            _sistema_item_repository = sistema_item_repository;
        }
        public Guid IdItemSistema { get; set; }
        public bool Ativo { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdItemSistema.ToString(), "IdItemSistema", "O IdItemSistema deve ser informado")
                .IsNotNullOrEmpty(Ativo.ToString(), "Ativo", "O campo Ativo deve ser informado")
            );

            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuirItemSistemaCadastrado(), IdItemSistema.ToString(), "O IdItemSistema informado não foi localizado!")
            );
        }

        private bool PossuirItemSistemaCadastrado()
        {
            return _sistema_item_repository.ObterPorId(IdItemSistema) != null;
        }
    }
}