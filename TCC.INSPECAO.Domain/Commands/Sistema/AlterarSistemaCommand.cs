using System;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AlterarSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaRepository _sistema_repository;
        public AlterarSistemaCommand(Guid idSistema, string nome, string descricao, ISistemaRepository sistema_repository)
        {
            IdSistema = idSistema;
            Nome = nome.ToUpper();
            Descricao = descricao;
            _sistema_repository = sistema_repository;
        }
        public Guid IdSistema { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdSistema.ToString(), "IdSistema", "O IdSistema deve ser informado")
                .HasMinLen(Nome, 5, "Nome", "O campo descrição deve ter no mínimo 5 caracteres")
                .HasMinLen(Descricao, 5, "Descricao", "O campo descrição deve ter no mínimo 5 caracteres")
            );

            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuirSistemaCadastrado(), IdSistema.ToString(), "O IdSistema informado não foi localizado!")
            );
        }

        private bool PossuirSistemaCadastrado()
        {
            return _sistema_repository.ObterPorId(IdSistema) != null;
        }
    }
}