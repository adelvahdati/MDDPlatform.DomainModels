using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
public class InstanceDocument {
        public Guid Id {get;set;}
        public string Name { get; set;}
        public string Type { get; set;}

        public InstanceDocument(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        internal static InstanceDocument CreateFrom(Instance instance)
        {
            return new InstanceDocument(instance.Id,instance.Name.Value,instance.Type.Value);
        }
        public Instance ToInstance(){
            var instance  = Instance.Create(Id,Name,Type);
            return instance;
        }

}