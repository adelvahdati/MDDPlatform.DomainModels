using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Core.Entities
{
    internal class DomainConceptRemoved : IDomainEvent
    {
        public Guid DomainId {get;}
        public Guid ModelId {get;}
        public Guid DomainConceptId {get;}
        public string ConceptName {get;}
        public string ConceptType {get;}

        public DomainConceptRemoved(Guid domainId, Guid modelId, Guid domainConceptId, string conceptName, string conceptType)
        {
            DomainId = domainId;
            ModelId = modelId;
            DomainConceptId = domainConceptId;
            ConceptName = conceptName;
            ConceptType = conceptType;
        }
    }
}