using System;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AtivarDesativarSistemaCommand : Notifiable, ICommand
    {
        private readonly ISistemaRepository _sistema_repository;
        public AtivarDesativarSistemaCommand(Guid idSistema, bool ativo, ISistemaRepository sistema_repository)
        {
            IdSistema = idSistema;
            Ativo = ativo;
            _sistema_repository = sistema_repository;
        }
        public Guid IdSistema { get; set; }
        public bool Ativo { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdSistema.ToString(), "IdSistema", "O IdSistema deve ser informado")
                .IsNotNullOrEmpty(Ativo.ToString(), "Ativo", "O campo Ativo deve ser informado")
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