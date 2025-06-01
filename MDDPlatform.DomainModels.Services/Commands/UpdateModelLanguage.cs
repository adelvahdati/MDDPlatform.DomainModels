using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands;
public class UpdateModelLanguage : ICommand 
{
    public Guid ModelId {get;set;}
    public Guid LanguageId {get;set;}

    public UpdateModelLanguage(Guid modelId, Guid languageId)
    {
        ModelId = modelId;
        LanguageId = languageId;
    }
}