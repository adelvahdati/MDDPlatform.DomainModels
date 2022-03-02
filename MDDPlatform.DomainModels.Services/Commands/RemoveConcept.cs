using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands{
    public class RemoveConcept : ICommand
    {
        public Guid ModelId {get;}    
        public string Name { get;}
        public string Type {get;}

        public RemoveConcept(Guid modelId, string name, string type)
        {
            ModelId = modelId;
            Name = name;
            Type = type;
        }
    }
}