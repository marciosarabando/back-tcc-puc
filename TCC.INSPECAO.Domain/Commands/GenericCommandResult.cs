using TCC.INSPECAO.Domain.Commands.Contracts;

namespace TCC.INSPECAO.Domain.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult()
        {

        }

        public GenericCommandResult(bool success, string menssage, object data)
        {
            Success = success;
            Menssage = menssage;
            Data = data;
        }

        public bool Success { get; set; }
        public string Menssage { get; set; }
        public object Data { get; set; }
    }
}