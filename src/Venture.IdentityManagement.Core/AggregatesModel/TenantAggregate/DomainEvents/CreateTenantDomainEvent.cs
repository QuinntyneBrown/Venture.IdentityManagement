using System;
using Venture.IdentityManagement.SharedKernal.Abstractions;

namespace Venture.IdentityManagement.Core
{
    public class CreateTenantDomainEvent: BaseDomainEvent
    {
        public Guid TenantId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        public CreateTenantDomainEvent(string name)
        {
            Name = name;
        }
    }
}
