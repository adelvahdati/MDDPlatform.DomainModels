using System.Runtime.Serialization;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    internal class CoreModelCreationException : Exception
    {
        public CoreModelCreationException(string message="") : base(message)
        {
        }
    }
}