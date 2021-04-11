using System;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AlterarItemSistemaCommand : Notifiable, ICommand
    {
        private readonly IUnidadeMedidaRepository _unidade_medida_repository;
        private readonly ISistemaItemRepository _sistema_item_repository;
        public AlterarItemSistemaCommand(Guid idItemSistema,
        string nome,
        string descricao,
        Guid idUnidadeMedida,
         IUnidadeMedidaRepository unidade_medida_repository,
         ISistemaItemRepository sistema_item_repository)
        {
            IdItemSistema = idItemSistema;
            Nome = nome.ToUpper();
            Descricao = descricao;
            IdUnidadeMedida = idUnidadeMedida;
            _unidade_medida_repository = unidade_medida_repository;
            _sistema_item_repository = sistema_item_repository;
        }

        public Guid IdItemSistema { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid IdUnidadeMedida { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Nome, 5, "Nome", "O campo descrição deve ter no mínimo 5 caracteres")
                .HasMinLen(Descricao, 5, "Descricao", "O campo descrição deve ter no mínimo 5 caracteres")
            );

            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuirItemSistemaCadastrado(), IdItemSistema.ToString(), "O IdItemSistema informado não foi localizado!")
                .Requires().IsTrue(PossuirUnidadeMedidaCadastrado(), IdUnidadeMedida.ToString(), "O IdUnidadeMedida informado não foi localizado!")
            );
        }

        private bool PossuirItemSistemaCadastrado()
        {
            return _sistema_item_repository.ObterPorId(IdItemSistema) != null;
        }

        private bool PossuirUnidadeMedidaCadastrado()
        {
            return _unidade_medida_repository.ObterPorId(IdUnidadeMedida) != null;
        }
    }
}