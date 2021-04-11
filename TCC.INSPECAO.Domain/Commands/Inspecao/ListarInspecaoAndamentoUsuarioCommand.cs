using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;

namespace TCC.INSPECAO.Domain.Commands.Inspecao
{
    public class ListarInspecaoAndamentoUsuarioCommand : Notifiable, ICommand
    {
        public ListarInspecaoAndamentoUsuarioCommand()
        {
        }
        public ListarInspecaoAndamentoUsuarioCommand(string idFirebase)
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
                 .IsNotNullOrEmpty(IdFirebase.ToString(), "IdFirebase", "O campo IdFirebase deve ter no mínimo 20 caracteres")
             );

        }
    }
}