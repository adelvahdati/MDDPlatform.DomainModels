using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;
public class InstanceDto {
    public Guid Id {get;set;}
    public string Name { get; set;}
    public string Type { get; set;}

    public InstanceDto(Guid id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    internal static InstanceDto CreateFrom(Instance instance)
    {
        return new InstanceDto(instance.Id,instance.Name,instance.Type);
    }
    internal static InstanceDto CreateFrom(DomainObject domainObject)
    {
        return new InstanceDto(domainObject.Id,domainObject.Name,domainObject.Type);
    }
    public Instance ToInstance(){
        return Instance.Create(Id,Name,Type);
    }
}