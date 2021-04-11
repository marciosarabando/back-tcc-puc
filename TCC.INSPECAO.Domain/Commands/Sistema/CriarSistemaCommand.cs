using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class CriarSistemaCommand : Notifiable, ICommand
    {
        private readonly IEstabelecimentoRepository _estabelecimento_repository;

        public CriarSistemaCommand()
        {
        }

        public CriarSistemaCommand(string idFirebase,
                                    string nome,
                                    string descricao,
                                    List<ItensSistema> itensSistema,
                                    IEstabelecimentoRepository estabelecimento_repository)
        {
            IdFirebase = idFirebase;
            Nome = nome;
            Descricao = descricao;
            ItensSistema = itensSistema;
            _estabelecimento_repository = estabelecimento_repository;
        }

        [JsonIgnore]
        public string IdFirebase { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ItensSistema> ItensSistema { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(IdFirebase, 20, "IdFirebase", "O IdFirebase deve conter no mínimo 20 caracteres")
                .HasMinLen(Nome, 5, "Nome", "O campo descrição deve ter no mínimo 5 caracteres")
                .HasMinLen(Descricao, 5, "Descricao", "O campo descrição deve ter no mínimo 5 caracteres")
            );
        }

    }

    public class ItensSistema
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int NumeroOrdem { get; set; }
        public string IdUnidadeMedida { get; set; }
    }
}