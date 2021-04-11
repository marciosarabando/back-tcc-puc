using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class CriarItemSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaRepository _sistema_repository;
        private readonly IUnidadeMedidaRepository _unidade_medida_repository;

        public CriarItemSistemaCommand(string nome,
        string descricao,
        Guid idUnidadeMedida,
        Guid idSistema,
        ISistemaRepository sistema_repository,
        IUnidadeMedidaRepository unidade_medida_repository)
        {

            Nome = nome.ToUpper();
            Descricao = descricao;
            IdUnidadeMedida = idUnidadeMedida;
            IdSistema = idSistema;
            _sistema_repository = sistema_repository;
            _unidade_medida_repository = unidade_medida_repository;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid IdUnidadeMedida { get; set; }
        public Guid IdSistema { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Nome, 5, "Nome", "O Nome deve conter no mínimo 5 caracteres")
                .HasMinLen(Descricao, 5, "Descricao", "O campo descrição deve ter no mínimo 5 caracteres")
            );

            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuirSistemaCadastrado(), IdSistema.ToString(), "O IdSistema informado não foi localizado!")
                .Requires().IsTrue(PossuirUnidadeMedidaCadastrado(), IdUnidadeMedida.ToString(), "O IdUnidadeMedida informado não foi localizado!")
            );
        }

        private bool PossuirSistemaCadastrado()
        {
            return _sistema_repository.ObterPorId(IdSistema) != null;
        }

        private bool PossuirUnidadeMedidaCadastrado()
        {
            return _unidade_medida_repository.ObterPorId(IdUnidadeMedida) != null;
        }
    }
}