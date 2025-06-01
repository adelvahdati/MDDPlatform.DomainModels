using MDDPlatform.DomainModels.Services.Commands.Common;

namespace MDDPlatform.DomainModels.Application.Services;
public interface IModelOperationService
{
    Task HandleAsync(BaseRequest request);
}