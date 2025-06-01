using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class DomainObjectTupleDto
{
    public DomainObjectDto Source {get;}
    public string RalationName {get;}
    public DomainObjectDto Target {get;}

    public DomainObjectTupleDto(DomainObjectDto source, string ralationName, DomainObjectDto target)
    {
        Source = source;
        RalationName = ralationName;
        Target = target;
    }

    internal static DomainObjectTupleDto Create(Tuple<DomainObject, string, DomainObject> tuple)
    {
        return new DomainObjectTupleDto(DomainObjectDto.CreateFrom(tuple.Item1),tuple.Item2,DomainObjectDto.CreateFrom(tuple.Item3));
    }
}