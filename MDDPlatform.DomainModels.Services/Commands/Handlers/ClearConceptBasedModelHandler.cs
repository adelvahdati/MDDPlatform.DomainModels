using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Intefaces;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands.Handlers;

public class ClearConceptBasedModelHandler : ICommandHandler<ClearConceptBasedModel>
{
    private IDomainModelRepository _domainModelRepository;
    private IDomainModelNotificationService _notificationService;

    public ClearConceptBasedModelHandler(IDomainModelRepository domainModelRepository, IDomainModelNotificationService notificationService)
    {
        _domainModelRepository = domainModelRepository;
        _notificationService = notificationService;
    }

    public void Handle(ClearConceptBasedModel command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ClearConceptBasedModel command)
    {
        DomainModel? domainModel = await _domainModelRepository.GetDomainModelAsync(command.DomainModelId);
        if(Equals(domainModel,null))
            throw new Exception("The model does not exist");
        
        domainModel.ClearConcepts(); 
        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
        await _notificationService.ConceptBasedModelClearedAsync(domainModel.Id,domainModel.Name,domainModel.Type,domainModel.Tag);
    }
}
