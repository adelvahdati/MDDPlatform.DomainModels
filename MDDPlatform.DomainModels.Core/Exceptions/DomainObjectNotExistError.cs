namespace MDDPlatform.DomainModels.Core.ValueObjects
{
    [Serializable]
    internal class DomainObjectNotExistError : Exception
    {
        public string InstanceName { get; }
        public string InstanceType { get; }

        public DomainObjectNotExistError(string message, string instanceName, string instanceType) : base(message)
        {
            InstanceName = instanceName;
            InstanceType = instanceName;
        }
    }
}