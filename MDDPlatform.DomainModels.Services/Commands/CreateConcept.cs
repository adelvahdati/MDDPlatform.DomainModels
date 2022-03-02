using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands
{
    public class CreateConcept : ICommand
    {
        public Guid ModelId {get;}
        public string Name { get;}
        public string Type {get;}

        public CreateConcept(string name, string type,Guid modelId)
        {
            Name = name;
            Type = type;
            ModelId = modelId;
        }
    }
}