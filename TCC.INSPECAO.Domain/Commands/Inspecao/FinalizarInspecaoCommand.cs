using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Inspecao
{
    public class FinalizarInspecaoCommand : Notifiable, ICommand
    {


        public FinalizarInspecaoCommand()
        {
        }

        public FinalizarInspecaoCommand(string idFirebase, Guid idInspecao, string observacao)
        {
            IdFirebase = idFirebase;
            IdInspecao = idInspecao;
            Observacao = observacao;
        }

        [JsonIgnore]
        public string IdFirebase { get; set; }
        [JsonIgnore]
        public Guid IdInspecao { get; set; }
        public string Observacao { get; set; }

        public void Validate()
        {

            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(IdFirebase, 20, "IdFirebase", "O campo IdFirebase deve ter no mínimo 20 caracteres")
            );
        }

    }
}