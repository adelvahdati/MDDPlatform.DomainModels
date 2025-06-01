using Attribute = MDDPlatform.BaseConcepts.ValueObjects.Attribute;
namespace MDDPlatform.DomainModels.Application.DTO;
public class AttributeDto
{
    public string Name { get; set;}
    public string Value { get; set;}

    public AttributeDto(string name, string value)
    {
        this.Name = name;
        this.Value = value;
    }
    public static AttributeDto CreateFrom(Attribute attribute)
    {
        return new AttributeDto(attribute.Name,attribute.Value);
    }

    internal Attribute ToAttribute(){
        return new Attribute(Name,Value);
    }
}