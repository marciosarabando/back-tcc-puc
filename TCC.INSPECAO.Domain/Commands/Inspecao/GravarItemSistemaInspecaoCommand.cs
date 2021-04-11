using System;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Inspecao
{
    public class GravarItemSistemaInspecaoCommand : Notifiable, ICommand
    {
        private readonly IInspecaoRepository _inspecao_repository;

        public GravarItemSistemaInspecaoCommand()
        {
        }

        public GravarItemSistemaInspecaoCommand(Guid idInspecao, Guid idItemSistema, string valor, string idFirebase, IInspecaoRepository inspecao_repository)
        {
            IdInspecao = idInspecao;
            IdItemSistema = idItemSistema;
            Valor = valor;
            IdFirebase = idFirebase;
            _inspecao_repository = inspecao_repository;
        }

        public Guid IdItemSistema { get; set; }
        public string Valor { get; set; }

        [JsonIgnore]
        public Guid IdInspecao { get; set; }

        [JsonIgnore]
        public string IdFirebase { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(IdFirebase.ToString(), "IdFirebase", "O campo IdFirebase deve ter no mínimo 20 caracteres")
            );

            AddNotifications(
                new Contract()
                .Requires().IsTrue(PossuirInspecaoCadastrada(), IdInspecao.ToString(), "Inspeção inválida!")
            );
        }

        private bool PossuirInspecaoCadastrada()
        {
            return _inspecao_repository.ObterPorId(IdInspecao) != null;
        }
    }
}