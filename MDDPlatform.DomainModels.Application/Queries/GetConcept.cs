using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries{
    public class GetConcept : IQuery<DomainConceptDto>
    {
        public Guid DomainModelId {get;}

        public string Name { get; set;}
        public string Type { get; set;}

        public GetConcept(string name, string type, Guid domainModelId)
        {
            Name = name;
            Type = type;
            DomainModelId = domainModelId;
        }
    }
}