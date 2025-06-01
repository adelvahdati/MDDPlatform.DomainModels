using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application.DTO;

public class DomainModelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public Guid DomainId { get; set; }
    public List<ConceptDto> Concepts { get; set; }

    public DomainModelDto(Guid id, string name, string tag, string type, int level, List<ConceptDto> concepts, Guid domainId)
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Level = level;
        DomainId = domainId;
        Concepts = concepts;
    }

    internal static DomainModelDto CreateFrom(DomainModel domainModel)
    {
        Guid id = domainModel.Id;
        string name = domainModel.Name;
        string tag = domainModel.Tag;
        string type = domainModel.Type;
        int level = domainModel.Level;
        Guid domainId = domainModel.Domain.Id;
        var concepts = domainModel.Concepts;
        if (concepts.Count == 0)
            return new DomainModelDto(id, name, tag, type, level, new List<ConceptDto>(), domainId);

        var conceptDtos = concepts.Select(c => ConceptDto.CreateFrom(c)).ToList();
        return new DomainModelDto(id, name, tag, type, level, conceptDtos, domainId);
    }
}