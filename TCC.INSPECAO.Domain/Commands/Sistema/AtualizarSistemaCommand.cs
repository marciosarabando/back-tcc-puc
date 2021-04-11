using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Repositories;

namespace TCC.INSPECAO.Domain.Commands.Sistema
{
    public class AtualizarSistemaCommand : Notifiable, ICommand
    {
        public AtualizarSistemaCommand()
        {
        }

        public AtualizarSistemaCommand(string idFirebase,
                                    string id,
                                    string nome,
                                    string descricao,
                                    bool ativo,
                                    List<ItensSistemaUpdate> itensSistema)
        {
            IdFirebase = idFirebase;
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            ItensSistema = itensSistema;
        }

        [JsonIgnore]
        public string IdFirebase { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<ItensSistemaUpdate> ItensSistema { get; set; }

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

    public class ItensSistemaUpdate
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int NumeroOrdem { get; set; }
        public string IdUnidadeMedida { get; set; }
        public bool Ativo { get; set; }
    }
}