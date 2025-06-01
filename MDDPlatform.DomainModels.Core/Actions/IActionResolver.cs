using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Core.Actions;

internal interface IActionResolver
{
    void Handle(DomainModel domainModel, Instruction instruction);
}