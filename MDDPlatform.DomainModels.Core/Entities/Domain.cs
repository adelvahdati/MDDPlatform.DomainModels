using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Core.Entities
{
    public class Domain : BaseEntity<Guid>
    {
        public Domain(Guid domainId)
        {
            Id = domainId;            
        }        
    }
}