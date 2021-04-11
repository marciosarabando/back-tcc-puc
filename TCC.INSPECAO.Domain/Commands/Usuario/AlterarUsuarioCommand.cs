using System;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;

namespace TCC.INSPECAO.Domain.Commands.Usuario
{
    public class AlterarUsuarioCommand : Notifiable, ICommand
    {
        public AlterarUsuarioCommand() { }
        public AlterarUsuarioCommand(Guid idUsuario, string perfil, bool ativo)
        {
            IdUsuario = idUsuario;
            Perfil = perfil;
            Ativo = ativo;
        }

        public Guid IdUsuario { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Perfil, 3, "Perfil", "O campo Perfil deve ser informado")
            );
        }
    }
}