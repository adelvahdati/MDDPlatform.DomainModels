using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;

public class CreateDomainConcept : ICommand
{
    public Guid ModelId {get; set;}
    public string Name {get; set;}
    public string Type {get; set;}
    public List<Property>? Properties {get;set;}
    public List<Relation>? Relations {get;set;}
    public List<Operation>? Operations {get;set;}

    public CreateDomainConcept(Guid modelId, string name, string type, List<Property>? properties, List<Relation>? relations, List<Operation>? operations)
    {
        ModelId = modelId;
        Name = name;
        Type = type;
        Properties = properties;
        Relations = relations;
        Operations = operations;
    }
}