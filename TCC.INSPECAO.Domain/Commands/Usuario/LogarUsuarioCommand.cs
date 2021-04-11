using Flunt.Notifications;
using Flunt.Validations;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Commands.Usuario
{
    public class LogarUsuarioCommand : Notifiable, ICommand
    {
        public LogarUsuarioCommand()
        {
        }

        public LogarUsuarioCommand(string idFirebase, string email)
        {
            IdFirebase = idFirebase;
            Email = email;
        }

        public string IdFirebase { get; set; }
        public string Email { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(IdFirebase, 20, "IdFirebase", "O campo deve conter no mínimo 20 caracteres")
                .HasMinLen(Email, 3, "Email", "O campo deve conter no mínimo 3 caracteres")
            );
        }
    }
}