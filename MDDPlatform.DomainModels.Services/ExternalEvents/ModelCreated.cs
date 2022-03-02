using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Services.ExternalEvents{
    public class ModelCreated : IDomainEvent
    {
        public Guid DomainId {get;set;}
        public Guid ModelId {get;set;}
        public string Name {get;set;}
        public string Tag {get;set;}
        public string Type {get;set;}
        public int Level {get;set;}

        public ModelCreated(Guid domainId, Guid modelId, string name, string tag, string type, int level)
        {
            DomainId = domainId;
            ModelId = modelId;
            Name = name;
            Tag = tag;
            Type = type;
            Level = level;
        }
    }

}