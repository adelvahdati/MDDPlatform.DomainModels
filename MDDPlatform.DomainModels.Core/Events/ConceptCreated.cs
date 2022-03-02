using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.Enums;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Events
{
    internal class ConceptCreated : IDomainEvent
    {
        public Guid DomainId {get;}
        public Guid ModelId {get;}
        public Guid ConceptId {get;}
        public string ConceptName {get;}
        public string ConceptType {get;}
        public ConceptCreated(DomainModel domainModel, Concept concept)
        {
            DomainId = domainModel.Domain.Id;
            ModelId = domainModel.Id;
            ConceptId = concept.TraceId.Value;
            ConceptName = concept.Name;
            ConceptType = concept.Type;
        }
    }
}