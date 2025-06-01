using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;

public class OperationDto
{
    public string Name { get; set; }
    public List<OperationInputDto> Inputs { get; set; }
    public OperationOutputDto Output { get; set; }
    public List<AttributeDto> Attributes { get; set; }

    public OperationDto(string name, List<OperationInputDto> inputs, OperationOutputDto output, List<AttributeDto>? attributes = null)
    {
        Name = name;
        Inputs = inputs;
        Output = output;
        Attributes = attributes == null ? new() : attributes;
    }
    public static OperationDto CreateFrom(Operation operation)
    {
        var name = operation.Name.Value;
        var inputs = operation.Inputs.Select(input => OperationInputDto.CreateFrom(input)).ToList();
        var output = OperationOutputDto.CreateFrom(operation.Output);
        var attributes = operation.Attributes.Select(attr=>AttributeDto.CreateFrom(attr)).ToList();
        return new OperationDto(name, inputs, output,attributes);

    }
    public Operation ToOperation()
    {
        OperationName name = OperationName.Create(Name);
        var inputs = Inputs.Select(inputDoc => inputDoc.ToOperationInput()).ToList();
        var output = Output.ToOperationOutput();

        var operation =  Operation.Create(name, inputs, output);
        foreach(var attribute in Attributes){
            operation.SetAttribute(attribute.Name,attribute.Value);
        }
        return operation;
    }
}
public class OperationInputDto
{
    public string Name { get; }
    public string Type { get; }

    private OperationInputDto(string name, string type)
    {
        Name = name;
        Type = type;
    }
    public static OperationInputDto CreateFrom(OperationInput input)
    {
        return new OperationInputDto(input.Name, input.Type);
    }
    public OperationInput ToOperationInput()
    {
        return OperationInput.Create(Name, Type);
    }
}

public class OperationOutputDto
{
    public string Type { get; }
    public bool IsCollection { get; }

    private OperationOutputDto(string type, bool isCollection)
    {
        Type = type;
        IsCollection = isCollection;
    }
    public static OperationOutputDto CreateFrom(OperationOutput output)
    {
        return new OperationOutputDto(output.Type, output.IsCollection);
    }
    public OperationOutput ToOperationOutput()
    {
        return OperationOutput.Create(Type, IsCollection);
    }
}
