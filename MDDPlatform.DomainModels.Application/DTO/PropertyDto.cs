using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;
public class PropertyDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsCollection { get; set; } = false;
    public string? Value { get; set; }
    public List<AttributeDto> Attributes { get; set; }

    public PropertyDto(string name, string type, string? value, bool isCollection = false, List<AttributeDto>? attributes = null)
    {
        Name = name;
        Type = type;
        IsCollection = isCollection;
        Value = value;
        Attributes = attributes == null ? new() : attributes;
    }

    public static PropertyDto CreateFrom(Property property)
    {
        var name = property.Name;
        var type = property.Type.Value;
        var isCollection = property.Type.IsCollection;
        PropertyValue propValue = property.Value;
        string? value = propValue.Value;
        var attributes = property.Attributes.Select(attr=>AttributeDto.CreateFrom(attr)).ToList();
        return new PropertyDto(name, type, value, isCollection, attributes);
    }
    internal Property ToProperty()
    {
        var property = Property.Create(Name, Type, IsCollection);
        if (Value != null)
            property.SetValue(Value);

        foreach (var attribute in Attributes)
        {
            property.SetAttribute(attribute.Name, attribute.Value);
        }
        return property;
    }
}