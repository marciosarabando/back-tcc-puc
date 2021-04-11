using System;
using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class ListarItensSistemaCommand : Notifiable, ICommand
    {
        public Guid IdSistema { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdSistema.ToString(), "IdSistema", "O IdSistema deve ser informado")
            );
        }
    }
}