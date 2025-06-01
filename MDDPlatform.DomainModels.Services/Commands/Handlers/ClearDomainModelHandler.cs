using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Intefaces;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers;
public class ClearDomainModelHandler : ICommandHandler<ClearDomainModel>
{
    private IDomainModelRepository _domainModelRepository;
    private IDomainModelNotificationService _notificationService;

    public ClearDomainModelHandler(IDomainModelRepository domainModelRepository, IDomainModelNotificationService notificationService)
    {
        _domainModelRepository = domainModelRepository;
        _notificationService = notificationService;
    }

    public void Handle(ClearDomainModel command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ClearDomainModel command)
    {
        DomainModel? domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        if(Equals(domainModel,null))
            throw new Exception("The model does not exist");
        
        domainModel.ClearInstances(); 
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        await _notificationService.DomainModelClearedAsync(domainModel.Id,domainModel.Name,domainModel.Type,domainModel.Tag);
    }
}
