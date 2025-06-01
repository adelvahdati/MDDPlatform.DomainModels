using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Services.Commands;
public class ReuseModel : ICommand{
    public Guid Source {get;set;}
    public Guid Destination {get;set;}

    public ReuseModel(Guid source, Guid destination)
    {
        this.Source = source;
        this.Destination = destination;
    }
}
public class ReuseModelHandler : ICommandHandler<ReuseModel>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ReuseModelHandler(IDomainModelRepository domainModelRepository, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        this._domainModelRepository = domainModelRepository;
        this._messageBroker = messageBroker;
        this._eventMapper = eventMapper;
    }

    public void Handle(ReuseModel command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ReuseModel command)
    {
        var sourceModel = await _domainModelRepository.GetDomainModelAsync(command.Source);
        var destinationModel = await _domainModelRepository.GetDomainModelAsync(command.Destination);
        var soucreDomainObjects = sourceModel.GetDomainObjects();
        foreach(var domainObject in soucreDomainObjects){            
            destinationModel.TryCloneObject(domainObject);            
        }
        await _domainModelRepository.UpdateDomainModelAsync(destinationModel);
    }
}
