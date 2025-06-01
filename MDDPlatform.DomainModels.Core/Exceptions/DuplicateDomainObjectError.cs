using System.Runtime.Serialization;
using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects
{
    [Serializable]
    internal class DuplicateDomainObjectError : Exception
    {
        public string InstanceName { get; }
        public string InstanceType { get; }

        public DuplicateDomainObjectError(string message, string instanceName, string instanceType) : base(message)
        {
            InstanceName = instanceName;
            InstanceType = instanceName;
        }
    }
}