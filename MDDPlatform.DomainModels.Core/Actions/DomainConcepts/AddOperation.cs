using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Core.Actions;
public class AddOperation : ActionResolver
{
    public AddOperation() : base(nameof(AddOperation), 5)
    {

    }

    protected override void Resolve(DomainModel domainModel, params string[] args)
    {
        string conceptName = args[0];
        string conceptType = args[1];
        var output = OperationOutput.Create(args[2],false);
        OperationName opName = OperationName.Create(args[3]);
        var inputs = args[4].Split(",").Select(param=>OperationInput.Create(param)).ToList();
        domainModel.AddOperation(conceptName,conceptType,opName,inputs,output);
        
    }
}
