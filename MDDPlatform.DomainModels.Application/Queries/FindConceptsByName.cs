using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.DomainModels.Application.Queries
{
    public class FindConceptsByName : IQuery<IList<DomainConceptDto>>
    {
        public Guid DomainModelId {get;}
        public string Name { get; set;}

        public FindConceptsByName(Guid domainModelId, string name)
        {
            DomainModelId = domainModelId;
            Name = name;
        }
    }
}