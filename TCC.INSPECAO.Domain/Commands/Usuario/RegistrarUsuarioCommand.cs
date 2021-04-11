using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using TCC.INSPECAO.Domain.Commands.Contracts;
using TCC.INSPECAO.Domain.Entity;

namespace TCC.INSPECAO.Domain.Commands.Usuario
{
    public class RegistrarUsuarioCommand : Notifiable, ICommand
    {
        public RegistrarUsuarioCommand()
        {
        }

        public RegistrarUsuarioCommand(string nome, string cnpjEstabelecimento)
        {
            Nome = nome;
            CnpjEstabelecimento = cnpjEstabelecimento;
        }

        [JsonIgnore]
        public string IdFirebase { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        public string Nome { get; set; }

        public string CnpjEstabelecimento { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(IdFirebase, 20, "IdFirebase", "O IdFirebase deve conter no mínimo 20 caracteres")
                .HasMinLen(Email, 3, "Email", "O campo deve conter um e-mail válido")
                .HasMinLen(Nome, 6, "Nome", "O nome do usuário deve ter no mínimo 6 caracteres")
            );
        }
    }
}