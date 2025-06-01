using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Core.Actions;
public abstract class ActionResolver : IActionResolver
{
    protected string ActionCode {get;}
    protected int NumberOfArguments {get;}
    public Signature ActionSignature => new Signature(ActionCode,NumberOfArguments);

    protected ActionResolver(string actionCode , int numberOfArguments)
    {
        ActionCode = actionCode;
        NumberOfArguments = numberOfArguments;
    }

    public void Handle(DomainModel domainModel, Instruction instruction)
    {
        if(!IsValidResolver(instruction))
            throw new Exception("Invalid Action Resolver");

        Resolve(domainModel,instruction.Arguments.ToArray());
    }

    protected abstract void Resolve(DomainModel domainModel, params string[] args);

    protected bool IsValidResolver(Instruction instruction)
    {
        if(Equals(instruction,null))
            return false;
        if(instruction.Code.ToLower()!= ActionCode.ToLower())
            return false;
        
        if(instruction.Arguments == null && NumberOfArguments>0)
            return false;

        if(instruction.Arguments != null && instruction.Arguments.Count!=NumberOfArguments)
            return false;
        
        return true;
    }

}