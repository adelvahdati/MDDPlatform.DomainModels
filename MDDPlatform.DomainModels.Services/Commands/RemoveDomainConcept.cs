using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class RemoveDomainConcept : ICommand
{
    public Guid ModelId {get;set;}    
    public string Name { get;set;}
    public string Type {get;set;}

    public RemoveDomainConcept(Guid modelId, string name, string type)
    {
        ModelId = modelId;
        Name = name;
        Type = type;
    }
}