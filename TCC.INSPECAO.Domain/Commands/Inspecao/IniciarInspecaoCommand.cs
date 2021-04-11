using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Inspecao
{
    public class IniciarInspecaoCommand : Notifiable, ICommand
    {


        public IniciarInspecaoCommand()
        {
        }

        public IniciarInspecaoCommand(string idFirebase)
        {

            IdFirebase = idFirebase;
        }

        [JsonIgnore]
        public string IdFirebase { get; set; }

        public void Validate()
        {

            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(IdFirebase, 20, "IdFirebase", "O campo IdFirebase deve ter no mínimo 20 caracteres")
            );

            /*
            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuiTurnoCadastrado(), SiglaTurno, "O campo siglaTurno deve corresponder a um turno cadastrado")
            );*/
        }

        /*
        private bool PossuiTurnoCadastrado()
        {
            return _turno_repository.ObterPorSigla(SiglaTurno) != null;
        }*/
    }
}