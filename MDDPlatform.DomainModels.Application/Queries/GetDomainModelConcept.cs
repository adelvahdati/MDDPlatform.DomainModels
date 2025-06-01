using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries;
public class GetDomainModelConcept : IQuery<ConceptDto>
{
    public Guid DomainModelId { get; }
    public string FullName { get; }

    public GetDomainModelConcept(Guid domainModelId, string name, string type)
    {
        DomainModelId = domainModelId;
        var conceptFullName = ConceptFullName.Create(name, type);
        FullName = conceptFullName.Value;
    }
}