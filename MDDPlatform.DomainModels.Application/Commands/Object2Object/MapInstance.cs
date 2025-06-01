using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public class MapInstance : ModelOperationPattern
{
        public Guid InputModel { get; set;}
        public Guid OutputModel { get; set;}
}