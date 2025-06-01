using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;
public class DomainModelElementsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public Guid DomainId { get; set; }
    public List<ElementDto> Elements {get;set;}

    public DomainModelElementsDto(Guid id, string name, string tag, string type, int level, List<ElementDto> elements,Guid domainId )
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Level = level;
        DomainId = domainId;
        Elements = elements;
    }

    internal static DomainModelElementsDto CreateFrom(DomainModel domainModel)
    {
        Guid id = domainModel.Id;
        string name = domainModel.Name;
        string tag = domainModel.Tag;
        string type = domainModel.Type;
        int level = domainModel.Level;
        Guid domainId = domainModel.Domain.Id;
        var domainConcepts = domainModel.DomainConcepts;
        if (domainConcepts.Count == 0)
            return new DomainModelElementsDto(id, name, tag, type, level, new List<ElementDto>(), domainId);

        var domainModelElements = domainConcepts.Select(c => ElementDto.CreateFrom(c)).ToList();
        return new DomainModelElementsDto(id, name, tag, type, level, domainModelElements,domainId);
    }

}