using System;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class ListarDetalhesSistemaCommand : Notifiable, ICommand
    {
        [JsonIgnore]
        public string IdFirebase { get; set; }
        public Guid IdSistema { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdFirebase.ToString(), "IdFirebase", "O IdFirebase deve ser informado")
            );
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdSistema.ToString(), "IdSistema", "O IdSistema deve ser informado")
            );
        }
    }
}