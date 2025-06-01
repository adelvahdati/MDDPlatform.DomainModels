using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Application.Commands;

public class RunScript : ModelOperationPattern
{
    public Guid DomainModelId {get;set;}
    public List<Instruction> Instructions {get;set;}

    public RunScript(Guid domainModelId, List<Instruction> instructions)
    {
        DomainModelId = domainModelId;
        Instructions = instructions;
    }
}
